﻿using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns
{
    public class HallowedFlareGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            NEFGlobalItem.FlareGuns.Add(Type);
            Item.AddElementFire();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 70; // Hitbox width of the item.
            Item.height = 38; // Hitbox height of the item.
            Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.

            Item.DefaultToFlareGun(47, 12);
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Sparkler>())
                .AddIngredient(ModContent.ItemType<HeatResistantPlastic>(), 6)
                .AddIngredient(ItemID.HallowedBar, 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }
    }
}
