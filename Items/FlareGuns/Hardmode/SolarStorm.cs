using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.FlareGuns.Hardmode
{
    public class SolarStorm : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
			ModLoader.TryGetMod("ShardsOfAtheria", out Mod sharded);
			return sharded == null;
		}

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Converts flares into Solar Flares\n" +
				"'Harness energy from the sun to vaporize your foes!'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults()
		{
			// Common Properties
			Item.width = 40; // Hitbox width of the item.
			Item.height = 22; // Hitbox height of the item.
			Item.scale = 0.75f;
			Item.rare = ItemRarityID.Expert; // The color that the item's name will be in-game.

			// Use Properties
			Item.useTime = 10; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 10; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
			Item.UseSound = SoundID.Item11;

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			Item.damage = 150; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.knockBack = 0f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.noMelee = true; // So the item's animation doesn't do damage.

			// Gun Properties
			Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
			Item.shootSpeed = 6f; // The speed of the projectile (measured in pixels per frame.)
			Item.useAmmo = AmmoID.Flare; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
			if (type == ProjectileID.Flare)
				type = ModContent.ProjectileType<SolarFlare>();
		}

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(2f, -2f);
		}
	}
}
