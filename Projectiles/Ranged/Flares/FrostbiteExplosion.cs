using MMZeroElements;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Ranged.Flares
{
    // This Example show how to implement simple homing projectile
    // Can be tested with ExampleCustomAmmoGun
    public class FrostbiteExplosion : ModProjectile
    {
        public override string Texture => "NotEnoughFlareGuns/Blank";

        public override void SetStaticDefaults()
        {
            ProjectileElements.Ice.Add(Type);
        }

        // Setting the default parameters of the projectile
        // You can check most of Fields and Properties here https://github.com/tModLoader/tModLoader/wiki/Projectile-Class-Documentation
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 150;

            Projectile.aiStyle = 0; // The ai style of the projectile (0 means custom AI). For more please reference the source code of Terraria
            Projectile.DamageType = DamageClass.Ranged; // What type of damage does this projectile affect?
            Projectile.light = 1f; // How much light emit around the projectile
            Projectile.friendly = true;
            Projectile.hostile = true;
            Projectile.timeLeft = 10;
            Projectile.penetrate = 5;
        }

        // Custom AI
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item14);
                Projectile.ai[0] = 1;
            }
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Frost, Scale: 1.3f);
            dust.velocity *= 4f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn2, FactoryHelper.Seconds(10));
        }
    }
}