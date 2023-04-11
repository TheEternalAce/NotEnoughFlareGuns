using Microsoft.Xna.Framework;
using MMZeroElements.Utilities;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Projectiles.Ranged.Flares;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns
{
    public class DarkSignal : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            NEFGlobalItem.FlareGuns.Add(Type);
            Item.AddFire();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 60; // Hitbox width of the item.
            Item.height = 28; // Hitbox height of the item.
            Item.scale = 0.8f;
            Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.

            Item.DefaultToFlareGun(25, 20);
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AzulFlame>())
                .AddIngredient(ModContent.ItemType<BloodFlareGun>())
                .AddIngredient(ModContent.ItemType<ScorchShot>())
                .AddIngredient(ModContent.ItemType<SporeFlare>())
                .AddTile(TileID.DemonAltar)
                .Register();

            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AzulFlame>())
                .AddIngredient(ModContent.ItemType<ScorchShot>())
                .AddIngredient(ModContent.ItemType<SporeFlare>())
                .AddIngredient(ModContent.ItemType<VileflareGun>())
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.BlueFlare || type == NotEnoughFlareGuns.ConvertibleFlare)
            {
                type = ModContent.ProjectileType<ShadowFlare>();
            }
        }
    }
}
