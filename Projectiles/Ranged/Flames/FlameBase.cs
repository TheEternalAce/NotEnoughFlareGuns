using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Ranged.Flames
{
    public class FlameBase : ModProjectile
    {
        public int dustType = DustID.Torch;
        public Color flameColor = Color.White;

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Flames;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 7;
            Projectile.AddElementFire();
            Projectile.AddElementWood();
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Flames);
            Projectile.aiStyle = 0;
            Projectile.scale = 0.1f;
        }

        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 50;
            }
            Projectile.SetVisualOffsets(98, true);

            if (Projectile.scale < 1)
            {
                Projectile.scale += 0.04f;
            }

            if (++Projectile.ai[0] >= 10)
            {
                if (++Projectile.ai[1] == 10)
                {
                    Projectile.Kill();
                }
                Projectile.ai[0] = 0;
            }
            var color = Color.White;
            float lightMultiplier = 1 - (0.1f * Projectile.ai[1]);
            color.R = (byte)(color.R * lightMultiplier);
            color.G = (byte)(color.G * lightMultiplier);
            color.B = (byte)(color.B * lightMultiplier);
            var position = Projectile.position;
            position.X -= 22;
            position.Y -= 22;
            var dust = Dust.NewDustDirect(position, 50, 50, dustType, 0, 0, 0, color);
            dust.noGravity = true;
            dust.fadeIn = 1;
            dust.velocity = Projectile.velocity
                .RotatedByRandom(MathHelper.ToRadians(15));
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.85f);
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X -= 22;
            hitbox.Y -= 22;
            hitbox.Width = 50;
            hitbox.Height = 50;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = flameColor;
            float lightMultiplier = 1 - (0.1f * Projectile.ai[1]);
            lightColor.R = (byte)(lightColor.R * lightMultiplier);
            lightColor.G = (byte)(lightColor.G * lightMultiplier);
            lightColor.B = (byte)(lightColor.B * lightMultiplier);

            var texture = ModContent.Request<Texture2D>(Texture).Value;
            for (int i = 0; i < 7; i++)
            {
                var drawPosition = Projectile.position - Main.screenPosition;
                drawPosition.X += DrawOffsetX * Projectile.scale;
                drawPosition.Y += DrawOriginOffsetY * Projectile.scale;
                Rectangle sourceRect = new(0, 98 * i, 98, 98);
                Main.spriteBatch.Draw(texture,
                    drawPosition,
                    sourceRect,
                    lightColor,
                    0,
                    Vector2.Zero,
                    Projectile.scale,
                    SpriteEffects.None,
                    0);
            }
            return false;
        }
    }
}
