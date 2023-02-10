using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.SFG
{
    public class SFGCork : ModProjectile
    {
        int gravityTimer;
        const int gravityTimerMax = 16;

        public override void SetDefaults()
        {
            Projectile.width = 6; // The width of projectile hitbox
            Projectile.height = 6; // The height of projectile hitbox
            Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.timeLeft = FactoryHelper.SecondsToTick(40); // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
        }

        public override void AI()
        {
            if (Projectile.velocity != Vector2.Zero)
            {
                if (++gravityTimer >= gravityTimerMax)
                {
                    if (++Projectile.velocity.Y >= 16)
                    {
                        Projectile.velocity.Y = 16;
                    }
                }
                Projectile.rotation += MathHelper.ToRadians(4) * Projectile.direction;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity.Y *= -0.6f;
            Projectile.velocity.X *= 0.8f;
            if (Projectile.velocity.LengthSquared() < 0.05f)
            {
                Projectile.velocity = Vector2.Zero;
            }
            return false;
        }
    }
}
