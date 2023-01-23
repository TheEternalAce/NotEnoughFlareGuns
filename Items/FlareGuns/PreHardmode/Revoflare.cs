﻿using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.FlareGuns.PreHardmode
{
	public class Revoflare : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
			NEFGlobalItem.FlareGuns.Add(Type);
		}

		public override void SetDefaults()
		{
			// Common Properties
			Item.width = 60; // Hitbox width of the item.
			Item.height = 24; // Hitbox height of the item.
			Item.scale = 0.75f;
			Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
			Item.value = Item.buyPrice(gold: 10);

			Item.DefaultToFlareGun(20, 16);
		}

		// This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(2f, -2f);
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.BlueFlare)
			{
				type = ModContent.ProjectileType<Projectiles.ShadowFlare>();
			}
		}
	}
}
