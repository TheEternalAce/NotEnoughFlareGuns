﻿using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Projectiles.Ranged.Flares;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns
{
    public class AzulFlame : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            NEFGlobalItem.FlareGuns.Add(Type);
            Item.AddElementFire();
            Item.AddElementAqua();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 48; // Hitbox width of the item.
            Item.height = 28; // Hitbox height of the item.
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.

            Item.DefaultToFlareGun(19, 18, autoReuse: true);
        }

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.BlueFlare || type == NEFG.ConvertibleFlare)
            {
                type = ModContent.ProjectileType<AquaFlare>();
            }
        }
    }
}
