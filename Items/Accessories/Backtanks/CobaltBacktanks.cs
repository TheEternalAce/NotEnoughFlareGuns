using Terraria;
using Terraria.ID;

namespace NotEnoughFlareGuns.Items.Accessories.Backtanks
{
    public class CobaltBacktank : BacktankBase
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            backtankTier = BacktankTiers.Cobalt;

            Item.rare = ItemRarityID.Lime;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CobaltBar, 26)
                .AddIngredient(ItemID.StoneBlock, 10)
                .AddIngredient(ItemID.Leather, 16)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }

    public class PalladiumBacktank : BacktankBase
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            backtankTier = BacktankTiers.Cobalt;

            Item.rare = ItemRarityID.Lime;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PalladiumBar, 26)
                .AddIngredient(ItemID.StoneBlock, 10)
                .AddIngredient(ItemID.Leather, 16)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
