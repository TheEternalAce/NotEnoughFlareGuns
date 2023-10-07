using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Utilities
{
    public partial class FactoryHelper
    {
        public static Player GetPlayerOwner(this Projectile projectile)
        {
            return Main.player[projectile.owner];
        }

        public static NPC GetNPCOwner(this Projectile projectile)
        {
            return Main.npc[projectile.owner];
        }
        public static NPC GetNPCOwner(this Projectile projectile, int i)
        {
            if (i > 2 || i < 0)
            {
                return null;
            }
            return Main.npc[(int)projectile.ai[i]];
        }

        public static void SetVisualOffsets(this Projectile projectile, int spriteSize, bool center = false)
        {
            projectile.SetVisualOffsets(new Vector2(spriteSize), center);
        }
        public static void SetVisualOffsets(this Projectile projectile, Vector2 spriteSize, bool center = false)
        {
            // 32 is the sprite size (here both width and height equal)
            int HalfSpriteWidth = (int)spriteSize.X / 2;
            int HalfSpriteHeight = (int)spriteSize.Y / 2;

            int HalfProjWidth = projectile.width / 2;
            int HalfProjHeight = projectile.height / 2;

            ModProjectile Projectile = projectile.ModProjectile;

            if (center)
            {
                // Vanilla configuration for "hitbox in middle of sprite"
                Projectile.DrawOriginOffsetX = 0;
                Projectile.DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
                Projectile.DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
            }
            // Vanilla configuration for "hitbox towards the end"
            else if (projectile.spriteDirection == 1)
            {
                Projectile.DrawOriginOffsetX = -(HalfProjWidth - HalfSpriteWidth);
                Projectile.DrawOffsetX = (int)-Projectile.DrawOriginOffsetX * 2;
                Projectile.DrawOriginOffsetY = 0;
            }
            else
            {
                Projectile.DrawOriginOffsetX = (HalfProjWidth - HalfSpriteWidth);
                Projectile.DrawOffsetX = 0;
                Projectile.DrawOriginOffsetY = 0;
            }
        }

        public static SpriteEffects GetSpriteEffect(this Projectile projectile)
        {
            return (-projectile.spriteDirection).ToSpriteEffect();
        }

        public static void Explode(this Projectile projectile, int explosionSize)
        {
            if (projectile.ai[1] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item14);
                projectile.ai[1] = 1;
                Vector2 oldSize = projectile.Size;
                projectile.velocity = Vector2.Zero;
                projectile.alpha = 255;
                projectile.hide = true;
                projectile.hostile = true;
                projectile.Size = new Vector2(explosionSize);
                projectile.timeLeft = 30;
                projectile.position += (oldSize - projectile.Size) / 2;

                for (int i = 0; i < 15; i++)
                {
                    Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, DustID.Torch, Scale: 1.3f);
                    dust.velocity *= 4f;
                }
                for (int i = 0; i < 5; i++)
                {
                    Vector2 vector = Vector2.One.RotatedByRandom(MathHelper.ToRadians(360)) * 4 * Main.rand.NextFloat();
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            Gore.NewGore(projectile.GetSource_FromThis(), projectile.Center, vector, GoreID.Smoke1);
                            break;
                        case 1:
                            Gore.NewGore(projectile.GetSource_FromThis(), projectile.Center, vector, GoreID.Smoke2);
                            break;
                        case 2:
                            Gore.NewGore(projectile.GetSource_FromThis(), projectile.Center, vector, GoreID.Smoke3);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Draws a basic single-frame glowmask for an item dropped in the world. Use in <see cref="Terraria.ModLoader.ModProjectile.PostDraw"/>
        /// </summary>
        public static void BasicInWorldGlowmask(this Projectile projectile, Texture2D glowTexture, Color color, float rotation, float scale)
        {
            Main.EntitySpriteDraw(
                glowTexture,
                new Vector2(
                    projectile.position.X - Main.screenPosition.X + projectile.width * 0.5f,
                    projectile.position.Y - Main.screenPosition.Y + projectile.height - glowTexture.Height * 0.5f
                ),
                new Rectangle(0, 0, glowTexture.Width, glowTexture.Height),
                color,
                rotation,
                glowTexture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0);
        }

        public const string DiamondX1 = "DiamondBlur1";
        public const string DiamondX2 = "DiamondBlur2";
        public const string OrbX1 = "OrbBlur1";
        public const string OrbX2 = "OrbBlur2";
        public const string LineX1 = "LineTrail1";
        public const string LineX2 = "LineTrail2";
        public static void DrawProjectilePrims(this Projectile projectile, Color color, string style, float angleAdd = 0f, float scale = 1f)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("NotEnoughFlareGuns/Assets/BlurTrails/" + style).Value;
            float plusRot = 0;
            if (style == DiamondX1 || style == DiamondX2 || style == LineX1 || style == LineX2)
            {
                plusRot = MathHelper.ToRadians(90);
            }

            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(projectile.width / 2f, projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
                Vector2 drawPos = (projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = scale * (projectile.oldPos.Length - k) / (projectile.oldPos.Length * 0.8f);
                Color drawColor = color * (1f - projectile.alpha) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, drawColor, projectile.oldRot[k] + plusRot + angleAdd, frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
        }
        //Credits to Aslysmic/Tewst Mod (so cool)

        public static void DrawPrimsAfterImage(this Projectile projectile, Color color, Texture2D texture)
        {
            Rectangle frame = projectile.Frame();
            Vector2 offset = new Vector2(projectile.width / 2, projectile.height / 2);
            var effects = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = frame.Size() / 2f;
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                float progress = 1f / ProjectileID.Sets.TrailCacheLength[projectile.type] * i;
                Main.spriteBatch.Draw(texture, projectile.oldPos[i] + offset - Main.screenPosition, frame, color * (1f - progress), projectile.rotation, origin, Math.Max(projectile.scale * (1f - progress), 0.1f), effects, 0f);
            }

            Main.spriteBatch.Draw(texture, projectile.position + offset - Main.screenPosition, frame, Color.White, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
        }
        public static void DrawPrimsAfterImage(this Projectile projectile, Color color)
        {
            var texture = TextureAssets.Projectile[projectile.type].Value;
            projectile.DrawPrimsAfterImage(color, texture);
        }
        //Credits to Aequus Mod (Omega Starite my beloved)

        public static Player FindClosestPlayer(this Projectile projectile, float maxDetectDistance, params int[] blaclkistedWhoAmI)
        {
            return FindClosestPlayer(projectile.position, maxDetectDistance, blaclkistedWhoAmI);
        }

        public static void Track(this Projectile projectile, Vector2 position, float maxDist, float speed = 16f, float inertia = 16f)
        {
            bool shouldTrack = true;
            if (maxDist > 0)
            {
                if (Vector2.Distance(projectile.Center, position) > maxDist)
                {
                    shouldTrack = false;
                }
            }
            if (shouldTrack)
            {
                // The immediate range around the target (so it doesn't latch onto it when close)
                Vector2 direction = position - projectile.Center;
                direction.Normalize();
                direction *= speed;

                projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
            }
        }

        public static void AddElementFire(this Projectile projectile)
        {
            NEFG.TryElementCall("assignElement", projectile, 0);
        }
        public static void AddElementAqua(this Projectile projectile)
        {
            NEFG.TryElementCall("assignElement", projectile, 1);
        }
        public static void AddElementElec(this Projectile projectile)
        {
            NEFG.TryElementCall("assignElement", projectile, 2);
        }
        public static void AddElementWood(this Projectile projectile)
        {
            NEFG.TryElementCall("assignElement", projectile, 3);
        }
    }
}
