using MMZeroElements;
using NotEnoughFlareGuns.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Flares
{
    // This Example show how to implement simple homing projectile
    // Can be tested with ExampleCustomAmmoGun
    public class ScorchShotExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scorch Shot Explosion"); // Name of the projectile. It can be appear in chat
            NEFGlobalProjectile.Flare.Add(Type);
            ProjectileElements.Fire.Add(Type);
        }

        // Setting the default parameters of the projectile
        // You can check most of Fields and Properties here https://github.com/tModLoader/tModLoader/wiki/Projectile-Class-Documentation
        public override void SetDefaults()
        {
            Projectile.width = 20; // The width of projectile hitbox
            Projectile.height = 20; // The height of projectile hitbox

            Projectile.aiStyle = 0; // The ai style of the projectile (0 means custom AI). For more please reference the source code of Terraria
            Projectile.DamageType = DamageClass.Ranged; // What type of damage does this projectile affect?
            Projectile.light = 1f; // How much light emit around the projectile
            Projectile.friendly = true;
            Projectile.hostile = true;
            Projectile.timeLeft = 2;
        }

        // Custom AI
        public override void AI()
        {
            if (Main.rand.NextBool(3))
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 0, default, 2.5f);
        }
    }
}