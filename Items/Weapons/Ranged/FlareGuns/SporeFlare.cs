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
    public class SporeFlare : ModItem
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
            Item.width = 52; // Hitbox width of the item.
            Item.height = 28; // Hitbox height of the item.
            Item.scale = 1f;
            Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.

            Item.DefaultToFlareGun(18, 18);
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.JungleSpores, 12)
                .AddIngredient(ItemID.Stinger, 6)
                .AddIngredient(ItemID.RichMahogany, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Flare || type == NotEnoughFlareGuns.ConvertibleFlare && Main.rand.NextFloat() > .33f)
            {
                type = ModContent.ProjectileType<PoisonFlare>();
            }
        }
    }
}
