using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.Flamethrowers
{
    public class Exterminator : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            NEFGlobalItem.Flamethrowers.Add(Type);
            Item.AddElementFire();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 48; // Hitbox width of the item.
            Item.height = 30; // Hitbox height of the item.
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.

            Item.DefaultToFlamethrower(15, 26, 4, autoReuse: true, velocity: 6);
            Item.value = 70000;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 14)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, -2);
        }
    }
}
