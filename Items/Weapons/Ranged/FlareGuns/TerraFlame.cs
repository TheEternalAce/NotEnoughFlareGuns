﻿using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Projectiles.Ranged.Flares;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns
{
    public class TerraFlame : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            NEFGlobalItem.FlareGuns.Add(Type);
            Item.AddElementFire();
            Item.AddElementAqua();
            Item.AddElementElec();
            Item.AddElementWood();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 54; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.rare = ItemRarityID.Yellow; // The color that the item's name will be in-game.

            Item.DefaultToFlareGun(140, 10, autoReuse: true);
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TrueDarkSignal>())
                .AddIngredient(ModContent.ItemType<TrueHallowedFlareGun>())
                .AddIngredient(ModContent.ItemType<RustedCaller>())
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
            type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<ShadowFlare>(), ModContent.ProjectileType<Vileflare>(),
                ModContent.ProjectileType<Bloodflare>(), ModContent.ProjectileType<PoisonFlare>(),
                ModContent.ProjectileType<AquaFlare>(), ModContent.ProjectileType<Blazer>() });
        }

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }
    }
}
