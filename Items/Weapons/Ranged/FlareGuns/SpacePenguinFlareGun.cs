﻿using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Projectiles.Ranged.Flares;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns
{
    public class SpacePenguinFlareGun : ModItem
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
            Item.rare = ItemRarityID.Red; // The color that the item's name will be in-game.

            Item.DefaultToFlareGun(120, 32, 7, autoReuse: true);
            Item.value = 300000;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            SoundEngine.PlaySound(Item.UseSound);
            if (type == NEFG.ConvertibleFlare)
            {
                type = ModContent.ProjectileType<FrostbiteFlare>();
            }
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}
