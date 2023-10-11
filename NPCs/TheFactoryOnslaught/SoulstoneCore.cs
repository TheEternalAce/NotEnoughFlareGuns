using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NotEnoughFlareGuns.Config;
using NotEnoughFlareGuns.Projectiles.TheFactoryOnslaught;
using NotEnoughFlareGuns.Systems;
using NotEnoughFlareGuns.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.Bestiary;
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

        const int NoAttack = -1;

        const int SpawnDrones = 0;
        const int SpawnExplosiveDrones = 1;

        const int LaserBarrage = 1;
        const int SpreadFire = 2;

        NPC[] turrets = new NPC[2];
        FactoryTurret turret1;
        FactoryTurret turret2;

        bool intercomAudio = ModContent.GetInstance<NEFGServerConfig>().IntercomAudio;

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Sets the description of this NPC that is listed in the bestiary
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                //BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.NotEnoughFlareGuns.NPCBestiary.SoulstoneCore"))
            });
        }

        public override void SetStaticDefaults()
        {
            // Add this in for bosses that have a summon item, requires corresponding code in the item (See MinionBossSummonItem.cs)
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            Main.npcFrameCount[Type] = 2;

            // Specify the debuffs it is immune to
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Venom] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire3] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn2] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Ichor] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.CursedInferno] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;

            NPC.AddElementFire();
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
            NPC.npcSlots = 1f;
            NPC.aiStyle = -1;
            if (!Main.dedServ)
            {
                Music = MusicID.Monsoon;
            }
            NPC.ElementMultipliers(new[] { 0.8f, 2.5f, 0.8f, 0.5f });
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
                    SoundEngine.PlaySound(NEFG.IntruderAlert);
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
                if (!Main.expertMode)
                {
                    turret1.SetActive(false);
                    turret2.SetActive(false);
                }
                NPC.ai[1] = 1;
            }
            Vector2 vector = Vector2.One.RotatedByRandom(MathHelper.ToRadians(360)) * 8 * Main.rand.NextFloat();
            switch (Main.rand.Next(3))
            {
                case 0:
                    Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke1);
                    break;
                case 1:
                    Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke2);
                    break;
                case 2:
                    Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke3);
                    break;
            }

            if (++openCoreTimer >= openCoreTimerMax && attackCooldown <= 0)
            {
                turret1.SetActive(true);
                turret2.SetActive(true);
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
                //Vector2 toPlayer = Vector2.Normalize(player.Center - NPC.Center);
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
            openAttacks.Add(LaserBarrage, 0.1);
            openAttacks.Add(SpreadFire, 0.1);

            int randomAttack = openAttacks.Get();
            switch (randomAttack)
            {
                case LaserBarrage:
                    attackTimer = 172;
                    attackCooldown = 30;
                    damage = 16;
                    break;
                case SpreadFire:
                    attackTimer = 61;
                    attackCooldown = 60;
                    damage = 16;
                    break;
            }
            return randomAttack;
        }

        int damage = 0;
        public void CycleClosedAttack()
        {
            SummonTurrets();
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
                case LaserBarrage:
                    ShootLasers();
                    break;
                case SpreadFire:
                    ShootFire(8);
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
            int type = ModContent.NPCType<PlasmaTurret>();
            int y = (int)NPC.Center.Y - 160;
            int x = (int)NPC.Center.X + 888;
            int x2 = (int)NPC.Center.X - 888;

            if (turrets[0] == null || !turrets[0].active || turrets[0].life <= 0)
            {
                NPC turret = NPC.NewNPCDirect(NPC.GetSource_FromThis(), x, y, type);
                turrets[0] = turret;
                turret1 = turret.ModNPC as FactoryTurret;
            }
            if (turrets[1] == null || !turrets[1].active || turrets[1].life <= 0)
            {
                NPC turret = NPC.NewNPCDirect(NPC.GetSource_FromThis(), x2, y, type);
                turrets[1] = turret;
                turret2 = turret.ModNPC as FactoryTurret;
            }
        }

        float rotation;
        public void ShootLasers()
        {
            if (attackTimer % 8 == 0)
            {
                FactoryHelper.ProjectileRing(NPC.GetSource_FromThis(), NPC.Center, 4, 1f,
                    32f, ModContent.ProjectileType<PlasmaLaser>(), damage, 0,
                    Main.myPlayer, rotation);
                rotation += MathHelper.ToRadians(15);
            }
        }

        public void ShootFire(int projectileAmount = 6)
        {
            if (attackTimer % 20 == 0)
            {
                var flames = FactoryHelper.ProjectileRing(NPC.GetSource_FromThis(), NPC.Center,
                    8, 1f, 12f, ProjectileID.Flames, damage, 0, Main.myPlayer, rotation);
                foreach (var flame in flames)
                {
                    flame.hostile = true;
                    flame.friendly = false;
                }
                rotation += MathHelper.ToRadians(22.5f);
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
            rotation = 0;
        }

        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            HitEffect(damageDone);
        }

        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            HitEffect(damageDone);
        }

        void HitEffect(int damageTaken)
        {
            if (coreClosed)
            {
                int maxCoreDamage = 400;
                coreDamage += damageTaken;
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
