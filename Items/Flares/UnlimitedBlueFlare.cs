using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Flares
{
	public class UnlimitedBlueFlare : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 99;
		}

		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 22;

			Item.DefaultToFlare(1, 0, ProjectileID.BlueFlare, false);

			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.White;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.BlueFlare, 3996)
				.AddTile(TileID.CrystalBall)
				.Register();
		}
	}
}
