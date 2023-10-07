using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ammo.Bullets
{
    public class HighExplosiveBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
            Item.AddElementFire();
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 26;
            Item.value = 1800;
            Item.rare = ItemRarityID.LightPurple;

            Item.DefaultToBullet(20, ModContent.ProjectileType<Projectiles.Ranged.Bullets.HighExplosiveBullet>());
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddIngredient(ItemID.EmptyBullet, 50)
                .AddIngredient(ItemID.Cog)
                .AddIngredient(ItemID.ExplosivePowder)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
