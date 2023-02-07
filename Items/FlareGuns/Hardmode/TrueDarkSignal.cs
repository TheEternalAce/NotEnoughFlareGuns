using Microsoft.Xna.Framework;
using MMZeroElements;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Projectiles.Flares;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.FlareGuns.Hardmode
{
	public class TrueDarkSignal : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires a random flare based on flare guns used to make this");
			SacrificeTotal = 1;
			NEFGlobalItem.FlareGuns.Add(Type);
			WeaponElements.Fire.Add(Type);
		}

		public override void SetDefaults()
		{
			// Common Properties
			Item.width = 60; // Hitbox width of the item.
			Item.height = 28; // Hitbox height of the item.
			Item.scale = 0.8f;
			Item.rare = ItemRarityID.Yellow; // The color that the item's name will be in-game.

			Item.DefaultToFlareGun(90, 18);
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<Items.FlareGuns.PreHardmode.DarkSignal>())
				.AddIngredient(ItemID.SoulofFright, 10)
				.AddIngredient(ItemID.SoulofMight, 10)
				.AddIngredient(ItemID.SoulofSight, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

		// This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(2f, -2f);
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
			type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<ShadowFlare>(), ModContent.ProjectileType<Vileflare>(),
				ModContent.ProjectileType<Bloodflare>(), ModContent.ProjectileType<PoisonFlare>(),
				ModContent.ProjectileType<AquaFlare>(), ModContent.ProjectileType<Blazer>() });
		}
	}
}
