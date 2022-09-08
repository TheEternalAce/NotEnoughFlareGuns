using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Flares
{
    public class UnlimitedBlueFlare : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 1; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
			Item.DamageType = DamageClass.Ranged;
			Item.width = 10;
			Item.height = 22;
			Item.maxStack = 1;
			Item.consumable = false; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
			Item.knockBack = 1.5f;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.White;
			Item.shoot = ProjectileID.BlueFlare; // The projectile that weapons fire when using this item as ammunition.
			Item.shootSpeed = 6f; // The speed of the projectile.
			Item.ammo = AmmoID.Flare; // The ammo class this ammo belongs to.
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
