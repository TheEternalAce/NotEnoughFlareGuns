using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Tools
{
    public class PaydayDrill : ModProjectile
    {
        int heatTimer;

        public override void SetStaticDefaults()
        {
            Projectile.AddElementFire();
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 48;
            Projectile.aiStyle = 20;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Melee;
            heatTimer = 8;
        }

        public override void AI()
        {
            if (++heatTimer >= 8)
            {
                heatTimer = 0;
                var player = Projectile.GetPlayerOwner();
                if (player.whoAmI == Main.myPlayer)
                {
                    float numberProjectiles = 4;
                    float rotation = MathHelper.ToRadians(15);
                    Vector2 posOffset = player.Center - Main.MouseWorld;
                    posOffset.Normalize();
                    posOffset *= 75f;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 position = player.Center + posOffset.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                        var toPos = Main.MouseWorld - player.Center;
                        toPos.Normalize();
                        toPos *= 400;
                        toPos += player.Center;
                        Vector2 velocity = toPos - position;
                        velocity.Normalize();
                        velocity *= 12f;
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                            position, velocity, ModContent.ProjectileType<PaydayDrillHeat>(),
                            Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                }
            }
        }
    }
}