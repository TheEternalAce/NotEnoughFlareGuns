using Microsoft.Xna.Framework;
using MMZeroElements;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns
{
    public class TrueHallowedFlareGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            NEFGlobalItem.FlareGuns.Add(Type);
            WeaponElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 74; // Hitbox width of the item.
            Item.height = 42; // Hitbox height of the item.
            Item.rare = ItemRarityID.Yellow; // The color that the item's name will be in-game.

            Item.DefaultToFlareGun(66, 10);
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HallowedFlareGun>())
                .AddIngredient(ItemID.ChlorophyteBar, 12)
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
