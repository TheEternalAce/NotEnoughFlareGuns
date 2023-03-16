using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using NotEnoughFlareGuns.Config;
using NotEnoughFlareGuns.Projectiles.TheFactoryOnslaught;
using NotEnoughFlareGuns.Systems;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace NotEnoughFlareGuns.NPCs.TheFactoryOnslaught
{
    public class SoulstoneCore : ModNPC
    {
        int coreDamage;
        bool coreClosed = true;
        int openCoreTimer;
        int openCoreTimerMax = 1000;
        int attackTimer;
        int attackCooldown;
        int attackType;

        int turretType;
        int laserType;

        const int NoAttack = -1;

        const int SpawnDrones = 0;
        const int SpawnExplosiveDrones = 1;

        const int SpawnWorkers = 0;
        const int LaserBarrage = 1;
        const int SpreadFire = 2;

        NPC[] turrets = new NPC[2];
        FactoryTurret turret1;
        FactoryTurret turret2;

        bool intercomAudio = ModContent.GetInstance<NEFGServerConfig>().IntercomAudio;

        public override void SetStaticDefaults()
        {
            // Add this in for bosses that have a summon item, requires corresponding code in the item (See MinionBossSummonItem.cs)
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            Main.npcFrameCount[Type] = 2;

            // Specify the debuffs it is immune to
            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    BuffID.OnFire,
                    BuffID.OnFire3,
                    BuffID.Frostburn,
                    BuffID.Frostburn2,
                    BuffID.Ichor,
                    BuffID.CursedInferno,
                    BuffID.Confused // Most NPCs have this
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            NPCElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.width = NPC.height = 300;
            NPC.lifeMax = 47000;
            NPC.defense = 150;
            NPC.boss = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = 140000;
            NPC.SpawnWithHigherTime(30);
            NPC.npcSlots = 1f; // Take up open spawn slots, preventing random NPCs from spawning during the fight

            // Don't set immunities like this as of 1.4:
            // NPC.buffImmune[BuffID.Confused] = true;
            // immunities are handled via dictionaries through NPCID.Sets.DebuffImmunitySets

            // Custom AI, 0 is "bound town NPC" AI which slows the NPC down and changes sprite orientation towards the target
            NPC.aiStyle = -1;

            // The following code assigns a music track to the boss in a simple way.
            if (!Main.dedServ)
            {
                Music = MusicID.Monsoon;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void AI()
        {
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];
            Lighting.AddLight(NPC.Center, TorchID.Orange);

            if (player.dead)
            {
                NPC.despawnEncouraged = true;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                if (NPC.ai[0] == 1)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.NotEnoughFlareGuns.FactoryIntercom.NeutralizedAnomaly"), Color.Red);
                    NPC.ai[0] = 2;
                }
                return;
            }

            if (NPC.ai[0] == 0)
            {
                Reset();
                SummonTurrets();
                if (intercomAudio)
                {
                    SoundEngine.PlaySound(NotEnoughFlareGuns.IntruderAlert);
                }
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.NotEnoughFlareGuns.FactoryIntercom.IntruderAlert"), Color.Red);
                NPC.ai[0] = 1;
            }

            RestrictPlayerMovement(player);

            UpdateVisuals();

            if (coreClosed)
            {
                ClosedCorePhase(player);
            }
            else
            {
                OpenCorePhase(player);
            }
        }

        public void ClosedCorePhase(Player player)
        {
            if (attackCooldown <= 0)
            {
                Reset();
                attackType = ChooseClosedAttack();
            }
            else if (attackTimer > 0)
            {
                CycleClosedAttack();
            }
            DecrimentAttackTimer();
        }

        public void OpenCorePhase(Player player)
        {
            if (NPC.ai[1] == 0)
            {
                NPC.defense = 34;
                turret1.ToggleActive();
                turret2.ToggleActive();
                NPC.ai[1] = 1;
            }
            Vector2 vector = Vector2.One.RotatedByRandom(MathHelper.ToRadians(360)) * 8 * Main.rand.NextFloat();
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke1);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke2);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke3);

            if (++openCoreTimer >= openCoreTimerMax && attackCooldown <= 0)
            {
                turret1.ToggleActive();
                turret2.ToggleActive();
                openCoreTimer = 0;
                coreClosed = true;
                NPC.defense = 150;
                NPC.ai[1] = 0;
            }

            if (attackCooldown <= 0)
            {
                Reset();
                attackType = ChooseOpenAttack();
            }
            else if (attackTimer > 0)
            {
                Vector2 toPlayer = Vector2.Normalize(player.Center - NPC.Center);
                CycleOpenAttack();
            }
            DecrimentAttackTimer();
        }

        public int ChooseClosedAttack()
        {
            WeightedRandom<int> closedAttacks = new();
            closedAttacks.Add(NoAttack, 0.8);
            closedAttacks.Add(SpawnDrones, 0.1);
            closedAttacks.Add(SpawnExplosiveDrones, 0.1);

            int randomAttack = closedAttacks.Get();
            switch (randomAttack)
            {
                case NoAttack:
                    attackTimer = 0;
                    attackCooldown = 180;
                    break;
                case SpawnDrones:
                    attackTimer = 10;
                    attackCooldown = 5;
                    break;
                case SpawnExplosiveDrones:
                    attackTimer = 20;
                    attackCooldown = 5;
                    break;
            }
            return randomAttack;
        }

        public int ChooseOpenAttack()
        {
            WeightedRandom<int> openAttacks = new();
            openAttacks.Add(SpawnWorkers, 0.8);
            openAttacks.Add(LaserBarrage, 0.1);
            openAttacks.Add(SpreadFire, 0.1);

            int randomAttack = openAttacks.Get();
            switch (randomAttack)
            {
                case SpawnWorkers:
                    attackTimer = 60;
                    attackCooldown = 20;
                    break;
                case LaserBarrage:
                    attackTimer = 172;
                    attackCooldown = 30;
                    damage = 16;
                    break;
                case SpreadFire:
                    attackTimer = 180;
                    attackCooldown = 600;
                    damage = 16;
                    break;
            }
            return randomAttack;
        }

        int damage = 0;
        public void CycleClosedAttack()
        {
            switch (attackType)
            {
                case SpawnDrones:
                    break;
                case SpawnExplosiveDrones:
                    break;
            }
        }

        public void CycleOpenAttack()
        {
            switch (attackType)
            {
                case SpawnWorkers:
                    SummonWorkers();
                    break;
                case LaserBarrage:
                    if (attackTimer % 8 == 0)
                    {
                        ShootLasers();
                    }
                    break;
                case SpreadFire:
                    if (attackTimer % 8 == 0)
                    {
                        ShootFire(2);
                    }
                    break;
            }
        }

        public void RestrictPlayerMovement(Player player)
        {
            Vector2 newCenter = player.Center;
            int dirX = NPC.Center.X < player.Center.X ? 1 : -1;
            float distX = MathHelper.Distance(player.Center.X, NPC.Center.X);
            float distXMax = 950;
            if (distX > distXMax)
            {
                newCenter.X -= (distX - distXMax) * dirX;
            }
            int dirY = NPC.Center.Y < player.Center.Y ? 1 : -1;
            float distY = MathHelper.Distance(player.Center.Y, NPC.Center.Y);
            float distYMax = 485;
            if (distY > distYMax)
            {
                newCenter.Y -= (distY - distYMax) * dirY;
            }
            player.Center = newCenter;
        }

        public void UpdateVisuals()
        {
            if (NPC.CountNPCS(Type) == 1)
            {
                ModContent.GetInstance<CameraFocus>().SetTarget("Soulstone Core", NPC.Center, CameraPriority.Weak, 12f, 60);
            }
        }

        public void SummonTurrets()
        {
            int type = turretType;
            int y = (int)NPC.Center.Y - 160;
            int x = (int)NPC.Center.X + 888;
            int x2 = (int)NPC.Center.X - 888;

            if (turrets[0] == null)
            {
                NPC turret = NPC.NewNPCDirect(NPC.GetSource_FromThis(), x, y, type);
                turrets[0] = turret;
                turret1 = turret.ModNPC as FactoryTurret;
            }
            else if (!turrets[0].active)
            {
                NPC turret = NPC.NewNPCDirect(NPC.GetSource_FromThis(), x, y, type);
                turrets[0] = turret;
                turret1 = turret.ModNPC as FactoryTurret;
            }
            if (turrets[1] == null)
            {
                NPC turret = NPC.NewNPCDirect(NPC.GetSource_FromThis(), x2, y, type);
                turrets[1] = turret;
                turret2 = turret.ModNPC as FactoryTurret;
            }
            else if (!turrets[1].active)
            {
                NPC turret = NPC.NewNPCDirect(NPC.GetSource_FromThis(), x2, y, type);
                turrets[1] = turret;
                turret2 = turret.ModNPC as FactoryTurret;
            }
        }

        void SummonWorkers()
        {
            if (NPC.CountNPCS(ModContent.NPCType<FactoryWorker>()) > 6)
            {
                attackTimer = 0;
                attackType = 0;
                return;
            }
            Vector2 spawnadjust = new(950, 480);
            if (Main.rand.NextBool())
            {
                spawnadjust.X *= -1;
            }
            Vector2 spawnPos = NPC.Center + spawnadjust;
            if (Main.rand.NextBool(20))
            {
                int index = NPC.NewNPC(NPC.GetSource_FromAI(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<FactoryWorker>());
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

        float rotation;
        public void ShootLasers()
        {
            int projectileAmount = 3;
            for (int i = 0; i < projectileAmount; i++)
            {
                Vector2 vel = new Vector2(0, -1).RotatedBy(MathHelper.ToRadians(360 / projectileAmount * i) + rotation);
                SoundEngine.PlaySound(SoundID.Item33);
                Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center, vel * 32, laserType, damage, 4, Main.myPlayer);
            }
            rotation += MathHelper.ToRadians(15);
        }

        public void ShootFire(int projectileAmount = 6)
        {
            for (int i = 0; i < projectileAmount; i++)
            {
                Vector2 vel = new Vector2(0, -1).RotatedBy(MathHelper.ToRadians(360 / projectileAmount * i) + rotation);
                SoundEngine.PlaySound(SoundID.Item20);
                Projectile fire = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center + vel * 200, vel * 8,
                    ProjectileID.Fireball, damage, 4, Main.myPlayer);
                fire.friendly = true;
            }
            rotation += MathHelper.ToRadians(10);
        }

        public void DecrimentAttackTimer()
        {
            if (attackTimer > 0)
            {
                attackTimer--;
            }
            else
            {
                attackCooldown--;
            }
        }

        public void Reset()
        {
            rotation = 0;
            if (NPC.downedGolemBoss)
            {
                turretType = ModContent.NPCType<IonTurret>();
                laserType = ModContent.ProjectileType<IonLaser>();
            }
            else
            {
                turretType = ModContent.NPCType<PlasmaTurret>();
                laserType = ModContent.ProjectileType<PlasmaLaser>();
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (coreClosed)
            {
                int maxCoreDamage = 400;
                coreDamage += (int)damage;
                if (coreDamage >= maxCoreDamage)
                {
                    coreDamage = 0;
                    coreClosed = false;
                }
            }
        }

        public override void OnKill()
        {
            turret1.SetActive(true);
            turret2.SetActive(true);
            NPC.SetEventFlagCleared(ref DownedSystem.downedFactoryEvent, -1);
            ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.NotEnoughFlareGuns.FactoryIntercom.EvacuateFactory"), Color.Red);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 offset = new(42, 44);
            Vector2 drawPos = NPC.position - offset - screenPos;
            Rectangle rect = new(0, 0, 388, 388);
            if (coreClosed)
            {
                rect.Y = 0;
            }
            else
            {
                rect.Y = 388;
            }
            spriteBatch.Draw(texture, drawPos, rect, Color.White);
            return false;
        }
    }
}
