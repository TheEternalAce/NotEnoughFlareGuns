using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Magic
{
    public class WelderLaser : ModProjectile
    {
        public override string Texture => NEFG.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElementFire();
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 15;
            Projectile.penetrate = 2;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (++Projectile.ai[0] >= 5)
            {
                Projectile.damage--;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color color = new(227, 90, 0);
            Projectile.DrawProjectilePrims(color, FactoryHelper.LineX1);
            return false;
        }
    }
}
