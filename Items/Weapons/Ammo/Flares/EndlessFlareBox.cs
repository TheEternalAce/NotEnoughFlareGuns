using MMZeroElements.Utilities;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Systems;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ammo.Flares
{
	public class EndlessFlareBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 99;
			NEFGlobalItem.Flares.Add(Type);
			Item.AddFire();
		}

		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 22;

			Item.DefaultToFlare(1, NotEnoughFlareGuns.ConvertibleFlare, false);

			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.White;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup(FactoryRecipes.Flares, 3996)
				.AddTile(TileID.CrystalBall)
				.Register();
		}
	}
}
