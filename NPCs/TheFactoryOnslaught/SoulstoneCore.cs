using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
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

namespace NotEnoughFlareGuns.NPCs.TheFactoryOnslaught
{
    public class SoulstoneCore : ModNPC
    {
        int closedCoreTimer;
        int closedCoreTimerMax = 100;
        bool coreClosed = true;
        int openCoreTimer;
        int openCoreTimerMax = 1000;
        int attackTimer;
        int attackCooldown;
        int attackType;

        int turretType;
        int laserType;

        const int SpawnTurrets = 0;
        const int SpawnDrones = 1;
        const int SpawnExplosiveDrones = 2;

        const int SpawnWorkers = 0;
        const int LaserBarrage = 1;
        const int SpreadFire = 2;

        NPC[] turrets = new NPC[2];

        public override void SetStaticDefaults()
        {
            // Add this in for bosses that have a summon item, requires corresponding code in the item (See MinionBossSummonItem.cs)
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            Main.npcFrameCount[Type] = 2;

            // Specify the debuffs it is immune to
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
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
            NPC.defense = 34;
            NPC.boss = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            Music = MusicID.Monsoon;
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
                return;
            }

            if (NPC.ai[0] == 0)
            {
                Reset();
                SummonTurrets();
                NPC.ai[0] = 1;
                SoundEngine.PlaySound(NotEnoughFlareGuns.IntruderAlert);
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

        public int ChooseClosedAttack()
        {
            int randomAttack = Main.rand.Next(3);
            randomAttack = 0;
            switch (randomAttack)
            {
                case SpawnTurrets:
                    attackTimer = 11;
                    attackCooldown = 5;
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
            int randomAttack = Main.rand.Next(2);
            switch (randomAttack)
            {
                case SpawnWorkers:
                    attackTimer = 10;
                    attackCooldown = 5;
                    break;
                case LaserBarrage:
                    attackTimer = 60;
                    attackCooldown = 15;
                    break;
                case SpreadFire:
                    attackTimer = 60;
                    attackCooldown = 15;
                    break;
            }
            return randomAttack;
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
                switch (attackType)
                {
                    case SpawnTurrets:
                        if (attackTimer == 10)
                        {
                            SummonTurrets();
                        }
                        break;
                    case SpawnDrones:
                        break;
                    case SpawnExplosiveDrones:
                        break;
                }
            }
            DecrimentAttackTimer();
        }

        public void OpenCorePhase(Player player)
        {
            if (NPC.ai[1] == 0)
            {
                NPC.defense = 34;
                NPC.ai[1] = 1;
            }
            Vector2 vector = Vector2.One.RotatedByRandom(MathHelper.ToRadians(360)) * 8 * Main.rand.NextFloat();
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke1);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke2);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke3);

            if (++openCoreTimer >= openCoreTimerMax)
            {
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
                switch (attackType)
                {
                    case SpawnWorkers:
                        break;
                    case LaserBarrage:
                        if (attackTimer % 15 == 0)
                        {
                            ShootLasers();
                        }
                        break;
                }
            }
            DecrimentAttackTimer();
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
                NPC turrret = NPC.NewNPCDirect(NPC.GetSource_FromThis(), x, y, type);
                turrets[0] = turrret;
            }
            else if (!turrets[0].active)
            {
                NPC turrret = NPC.NewNPCDirect(NPC.GetSource_FromThis(), x, y, type);
                turrets[0] = turrret;
            }
            if (turrets[1] == null)
            {
                NPC turrret = NPC.NewNPCDirect(NPC.GetSource_FromThis(), x2, y, type);
                turrets[1] = turrret;
            }
            else if (!turrets[1].active)
            {
                NPC turret = NPC.NewNPCDirect(NPC.GetSource_FromThis(), x2, y, type);
                turrets[1] = turret;
            }
        }

        public void ShootLasers(int projectileAmount = 6, Vector2 velocityOverride = new())
        {
            for (int i = 0; i < projectileAmount; i++)
            {
                Vector2 vel = new Vector2(0, -1).RotatedBy(MathHelper.ToRadians(360 / projectileAmount * i));
                SoundEngine.PlaySound(SoundID.Item33);
                Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center, vel * 32, laserType,
                    30, 4, Main.myPlayer);
                if (velocityOverride.LengthSquared() != 0)
                {
                    proj.velocity = velocityOverride;
                }
            }
        }

        public void ShootFire(int projectileAmount = 6)
        {
            for (int i = 0; i < projectileAmount; i++)
            {
                Vector2 vel = new Vector2(0, -1).RotatedBy(MathHelper.ToRadians(360 / projectileAmount * i));
                SoundEngine.PlaySound(SoundID.Item33);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel * 32, laserType, 30, 4, Main.myPlayer);
            }
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
            if (NPC.downedGolemBoss)
            {
                turretType = ModContent.NPCType<IonTurret>();
                laserType = ModContent.ProjectileType<IonLaser>();
            }
            else if (DownedSystem.factoryDefeatAmount >= 2)
            {
                turretType = ModContent.NPCType<PhotonTurret>();
                laserType = ModContent.ProjectileType<PhotonLaser>();
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
                if (++closedCoreTimer >= closedCoreTimerMax)
                {
                    closedCoreTimer = 0;
                    coreClosed = false;
                }
            }
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedSystem.downedFactoryEvent, -1);
            DownedSystem.factoryDefeatAmount++;
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"Soulstone core defeated {DownedSystem.factoryDefeatAmount} time(s)."), Color.White);
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("WARNING: SUBSTANTIAL DAMAGE TO THE CORE, EVACUATE IMMEDIATELY."), Color.Red);
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
