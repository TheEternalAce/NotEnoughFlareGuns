using Terraria;
using Terraria.ID;

namespace NotEnoughFlareGuns.Items.Accessories.Backtanks
{
    public class CopperBacktank : BacktankBase
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            backtankTier = BacktankTiers.Copper;

            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CopperBar, 26)
                .AddIngredient(ItemID.StoneBlock, 10)
                .AddIngredient(ItemID.Leather, 16)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }

    public class TinBacktank : BacktankBase
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            backtankTier = BacktankTiers.Copper;

            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TinBar, 26)
                .AddIngredient(ItemID.StoneBlock, 10)
                .AddIngredient(ItemID.Leather, 16)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
