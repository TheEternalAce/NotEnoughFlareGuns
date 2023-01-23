using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Flares
{
	public class HighVelocityFlare : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 99;
		}

		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 22;

			Item.DefaultToFlare(12, 0, ModContent.ProjectileType<Projectiles.Flares.HighVelocityFlare>());

			Item.value = Item.sellPrice(copper: 8);
			Item.rare = ItemRarityID.Green;
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
