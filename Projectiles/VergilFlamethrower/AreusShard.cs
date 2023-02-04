using Microsoft.Xna.Framework;
using MMZeroElements;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.VergilFlamethrower
{
    public class AreusShard : ModProjectile
    {
        int gravityTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Language.GetTextValue(LocalizationPath.ProjectileName + Name));
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10; // The width of projectile hitbox
            Projectile.height = 10; // The height of projectile hitbox
            Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.timeLeft = 180; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
        }

        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(32) * Projectile.direction;
            gravityTimer++;
            if (gravityTimer > 20)
            {
                Projectile.velocity.Y++;
                if (Projectile.velocity.Y > 16)
                {
                    Projectile.velocity.Y = 16;
                }
            }
            if (ModLoader.TryGetMod("ShardsOfAtheria", out Mod shards))
            {
                if (Main.rand.NextBool(4))
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, shards.Find<ModDust>("AreusDust_Standard").Type);
                }
            }
        }
    }
}
