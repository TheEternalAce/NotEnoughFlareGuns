using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Melee
{
    public class BloodSpray : ModProjectile
    {
        public override string Texture => NEFG.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElementAqua();
            Projectile.AddElementWood();
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 360;
        }

        public override void AI()
        {
            if (++Projectile.ai[1] >= 16)
            {
                if (++Projectile.velocity.Y >= 16)
                {
                    Projectile.velocity.Y = 16;
                }
            }

            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Blood);
            d.velocity *= 0;
            d.fadeIn = 1.3f;
            d.noGravity = true;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return Projectile.ai[0] != target.whoAmI;
        }
    }
}
