using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns
{
    public class Recipes : ModSystem
    {
		public override void AddRecipes()
		{
			Recipe.Create(ItemID.FlareGun)
				.AddIngredient(ModContent.ItemType<Items.FlareGuns.PreHardmode.Sparkler>())
				.AddIngredient(ModContent.ItemType<Items.HeatResistantPlastic>(), 12)
				.AddRecipeGroup(RecipeGroupID.IronBar, 5)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
