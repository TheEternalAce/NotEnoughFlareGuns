using MMZeroElements;
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
            SacrificeTotal = 99;
            WeaponElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 6;
            Item.height = 10;

            Item.DefaultToBullet(20, ModContent.ProjectileType<Projectiles.Ranged.Bullets.HighExplosiveBullet>());

            Item.value = Item.sellPrice(copper: 8);
            Item.rare = ItemRarityID.LightPurple;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddIngredient(ItemID.Flare, 50)
                .AddIngredient(ItemID.Cog)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
