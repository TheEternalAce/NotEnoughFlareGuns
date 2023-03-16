using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Accessories.Backtanks
{
    public class SoulstoneBacktank : BacktankBase
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

            backtankTier = BacktankTiers.Soulstone;

            Item.rare = ItemRarityID.Yellow;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SoulstonePlating>(), 26)
                .AddIngredient(ItemID.Cog, 5)
                .AddIngredient(ItemID.Leather, 16)
                .AddTile(ModContent.TileType<FactoryWorkstation>())
                .Register();
        }
    }
}
