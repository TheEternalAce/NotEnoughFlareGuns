using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using NotEnoughFlareGuns.Projectiles.TheFactoryOnslaught;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.NPCs.TheFactoryOnslaught
{
    public class SoulstoneCore : ModNPC
    {
        int closedCoreTimer;
        int closedCoreTimerMax = 120;
        bool coreClosed = true;
        int openCoreTimer;
        int openCoreTimerMax = 120;
        int attackTimer;
        int attackCooldown;
        int attackType;

        int turretType;
        int laserType;

        const int SpawnTurrets = 0;
        const int SpawnWorkers = 1;
        const int SpawnDrones = 2;
        const int SpawnExplosiveDrones = 3;
        const int LaserBarrage = 4;

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
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.04f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }

            if (NPC.ai[0] == 0)
            {
                NPC.position = new Vector2(7864, 3369);
                turretType = ModContent.NPCType<PlasmaTurret>();
                laserType = ModContent.ProjectileType<PlasmaLaser>();
                SummonTurrets();
                NPC.ai[0] = 1;
            }

            RestrictPlayerMovement(player);

            UpdateVisuals();

            if (attackCooldown <= 0)
            {
                attackType = LaserBarrage;
                if (NPC.downedGolemBoss)
                {
                    //turretType = ModContent.NPCType<IonTurret>()
                }
                switch (attackType)
                {
                    case SpawnTurrets:
                        attackTimer = 10;
                        attackCooldown = 5;
                        break;
                    case SpawnWorkers:
                        attackTimer = 10;
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
                    case LaserBarrage:
                        attackTimer = 60;
                        attackCooldown = 15;
                        break;
                }
            }
            else if (attackTimer > 0)
            {
                Vector2 toPlayer = Vector2.Normalize(player.Center - NPC.Center);
                switch (attackType)
                {
                    case SpawnTurrets:
                        if (attackTimer == 10)
                        {
                            SummonTurrets();
                        }
                        break;
                    case SpawnWorkers:
                        break;
                    case SpawnDrones:
                        break;
                    case SpawnExplosiveDrones:
                        break;
                    case LaserBarrage:
                        if (attackTimer % 6 == 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item33);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, toPlayer * 32, laserType, 30, 4, Main.myPlayer);
                        }
                        break;
                }

                if (attackTimer > 0)
                {
                    attackTimer--;
                }
                else
                {
                    attackCooldown--;
                }
            }

            if (coreClosed)
            {
                ClosedCorePhase(player);
            }
            else
            {
                OpenCorePhase(player);
            }
        }

        public int ChoseAttack()
        {
            return Main.rand.Next();
        }

        public void ClosedCorePhase(Player player)
        {
            NPC.dontTakeDamage = true;
            NPC.damage = 0;
            if (++closedCoreTimer >= closedCoreTimerMax)
            {
                closedCoreTimer = 0;
                coreClosed = false;
            }
        }

        public void OpenCorePhase(Player player)
        {
            NPC.dontTakeDamage = false;
            NPC.damage = 67;
            Vector2 vector = Vector2.One.RotatedByRandom(MathHelper.ToRadians(360)) * 8 * Main.rand.NextFloat();
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke1);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke2);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, vector, GoreID.Smoke3);
            if (++openCoreTimer >= openCoreTimerMax)
            {
                openCoreTimer = 0;
                coreClosed = true;
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
                ModContent.GetInstance<Utilities.CameraFocus>().SetTarget("Soulstone Core", NPC.Center, CameraPriority.Weak, 12f, 60);
            }
        }

        public void SummonTurrets()
        {
            int type = turretType;
            if (!NPC.AnyNPCs(type))
            {
                int y = (int)NPC.Center.Y - 144;
                int x = (int)NPC.Center.X + 888;
                int x2 = (int)NPC.Center.X - 888;
                NPC.NewNPC(NPC.GetSource_FromThis(), x, y, type);
                NPC.NewNPC(NPC.GetSource_FromThis(), x2, y, type);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 offset = new(48, 48);
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
