using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.NPCs.TheFactoryOnslaught
{
    public abstract class FactoryTurret : ModNPC
    {
        internal bool active = true;
        internal int fireTimer;
        internal const int fireTimerMax = 60;
        internal int fireTimerCooldown = 60;
        internal Vector2 gunOffset = new();
        internal int laserType;
        internal float laserSpeed = 0f;
        internal int laserAmount = 1;
        internal float laserSpreadAngle = 0f;
        internal int laserDamage;
        /// <summary>
        /// Shots per second
        /// </summary>
        internal int fireRate = 0;
        private int FireRate;

        public override void SetStaticDefaults()
        {
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
            NPC.width = 48;
            NPC.height = 44;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            FireRate = fireTimerMax / fireRate;
            NPC.SetElementMultipliersByElement(Element.Fire);
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            NPC.dontTakeDamage = NPC.AnyNPCs(ModContent.NPCType<SoulstoneCore>());
            if (!active && fireTimer == 0)
            {
                return;
            }
            fireTimer++;
            if (fireTimer <= fireTimerMax)
            {
                if (fireTimer % FireRate == 0)
                {
                    Vector2 vector = player.Center - NPC.Center;
                    vector.Normalize();
                    if (laserAmount > 1)
                    {
                        SpreadLasers(vector);
                    }
                    else
                    {
                        ShootLasers(vector);
                    }
                }
            }
            else if (fireTimer >= fireTimerMax + fireTimerCooldown)
            {
                fireTimer = 0;
            }
        }

        void SpreadLasers(Vector2 velocity)
        {
            velocity *= Main.rand.NextFloat(0.5f, 0.8f);
            for (int i = 0; i < laserAmount; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                ShootLasers(newVelocity);
            }
        }

        void ShootLasers(Vector2 velocity)
        {
            Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center, velocity * laserSpeed,
                laserType, laserDamage, 3, Main.myPlayer);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Player player = Main.player[NPC.target];
            Asset<Texture2D> gun = ModContent.Request<Texture2D>(Texture + "_Gun");
            Rectangle rect = new(0, 0, 78, 38);
            SpriteEffects spriteEffects = SpriteEffects.None;
            Vector2 offset = new(-24, -12);
            Vector2 origin = new Vector2(39, 19) + gunOffset;
            float rotation = (player.Center - NPC.Center).ToRotation();
            if (NPC.direction == -1)
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }
            Vector2 drawPos = NPC.position - offset - screenPos;

            if (!active && fireTimer == 0)
            {
                rotation = MathHelper.ToRadians(45);
                if (NPC.direction == -1)
                {
                    spriteEffects = SpriteEffects.FlipVertically;
                    rotation = MathHelper.ToRadians(135);
                }
            }

            spriteBatch.Draw(gun.Value, drawPos, rect, drawColor, rotation, origin, NPC.scale, spriteEffects, 0f);

            base.PostDraw(spriteBatch, screenPos, drawColor);
        }

        public void ToggleActive()
        {
            active = !active;
        }

        public void SetActive(bool active)
        {
            this.active = active;
        }
    }
}
