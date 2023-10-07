using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Magic.PyroGlove
{
    public class SmallSpark : ModProjectile
    {
        public override string Texture => NEFG.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElementFire();
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 8;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 255;
            Projectile.penetrate = 10;
        }

        Vector2 mousePos = Vector2.Zero;
        int initialDmg = 0;

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 2f;
            if (mousePos == Vector2.Zero)
            {
                var player = Main.player[Projectile.owner];
                if (player.whoAmI == Main.myPlayer)
                {
                    mousePos = Main.MouseWorld;
                }
                initialDmg = Projectile.damage;
                Projectile.damage = 0;
            }

            if (Projectile.ai[1] == 0)
            {
                if (++Projectile.ai[0] % 4 == 0)
                {
                    Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Torch);
                    d.velocity *= 0;
                    d.fadeIn = 1.3f;
                    d.noGravity = true;
                }

                if (Projectile.Hitbox.Contains(mousePos.ToPoint()))
                {
                    Projectile.Explode(60);
                    Projectile.damage = initialDmg;
                }
            }
        }
    }
}
