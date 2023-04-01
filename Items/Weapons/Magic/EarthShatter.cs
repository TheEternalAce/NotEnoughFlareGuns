using Microsoft.Xna.Framework;
using MMZeroElements;
using NotEnoughFlareGuns.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Magic
{
	public class EarthShatter : ModItem
	{
		public override void SetStaticDefaults()
		{
			WeaponElements.Fire.Add(Type);
			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 48;
			Item.rare = ItemRarityID.Pink;
			Item.channel = true;
			Item.value = 360000;

			Item.damage = 50;
			Item.DamageType = DamageClass.Magic;
			Item.knockBack = 8;
			Item.crit = 22;
			Item.mana = 17;

			Item.useTime = 3;
			Item.useAnimation = 12;
			Item.reuseDelay = 18;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item8;
			Item.noMelee = true;
			Item.autoReuse = true;
			Item.staff[Type] = true;

			Item.shoot = ModContent.ProjectileType<FlameJavelin>();
			Item.shootSpeed = 14;
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			int outer = 50;
			if (Main.rand.NextBool())
			{
				outer = -50;
			}
			position += new Vector2(outer, Main.rand.Next(-50, 51));
			if (Main.rand.NextBool())
			{
				position += new Vector2(Main.rand.Next(-50, 51), outer);
			}
			base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.SkyFracture)
				.AddIngredient(ItemID.Flamelash)
				.AddIngredient(ItemID.SoulofFright, 14)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
