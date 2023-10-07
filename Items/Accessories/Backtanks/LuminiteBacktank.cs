using Terraria;
using Terraria.ID;

namespace NotEnoughFlareGuns.Items.Accessories.Backtanks
{
    public class LuminiteBacktank : BacktankBase
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

            backtankTier = BacktankTiers.Luminite;

            Item.rare = ItemRarityID.Red;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LunarBar, 26)
                .AddIngredient(ItemID.LihzahrdBrick, 10)
                .AddIngredient(ItemID.Leather, 16)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
