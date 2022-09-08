using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.FlareGuns.Hardmode
{
    public class TrueDarkSignal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a random flare based on flare guns used to make this");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults()
		{
			// Common Properties
			Item.width = 60; // Hitbox width of the item.
			Item.height = 28; // Hitbox height of the item.
			Item.scale = 0.75f;
			Item.rare = ItemRarityID.Yellow; // The color that the item's name will be in-game.

			// Use Properties
			Item.useTime = 18; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 18; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = false; // Whether or not you can hold click to automatically use it again.
			Item.UseSound = SoundID.Item11;

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			Item.damage = 90; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.knockBack = 0f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.noMelee = true; // So the item's animation doesn't do damage.

			// Gun Properties
			Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
			Item.shootSpeed = 6f; // The speed of the projectile (measured in pixels per frame.)
			Item.useAmmo = AmmoID.Flare; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
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
			type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<Projectiles.ShadowFlare>(), ModContent.ProjectileType<Projectiles.Vileflare>(),
				ModContent.ProjectileType<Projectiles.Bloodflare>(), ModContent.ProjectileType<Projectiles.PoisonFlare>(),
				ModContent.ProjectileType<Projectiles.AquaFlare>(), ModContent.ProjectileType<Projectiles.Blazer>() });
		}
	}
}
