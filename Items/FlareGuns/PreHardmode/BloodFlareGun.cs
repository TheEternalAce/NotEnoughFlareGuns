using Microsoft.Xna.Framework;
using MMZeroElements;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.FlareGuns.PreHardmode
{
	public class BloodFlareGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
			NEFGlobalItem.FlareGuns.Add(Type);
			WeaponElements.Fire.Add(Type);
			WeaponElements.Ice.Add(Type);
		}

		public override void SetDefaults()
		{
			// Common Properties
			Item.width = 44; // Hitbox width of the item.
			Item.height = 22; // Hitbox height of the item.
			Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.

			Item.DefaultToFlareGun(18, 24);
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<Sparkler>())
				.AddIngredient(ModContent.ItemType<HeatResistantPlastic>(), 6)
				.AddIngredient(ItemID.CrimtaneBar, 6)
				.AddTile(TileID.Anvils)
				.Register();
		}

		// This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(2f, -2f);
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Flare)
			{
				type = ModContent.ProjectileType<Projectiles.Flares.Bloodflare>();
			}
		}
	}
}
