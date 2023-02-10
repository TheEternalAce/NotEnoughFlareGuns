using Microsoft.Xna.Framework;
using MMZeroElements;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.Launchers.PreHardmode
{
    public class Revolnade : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 60; // Hitbox width of the item.
            Item.height = 24; // Hitbox height of the item.
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
            Item.value = Item.buyPrice(gold: 10);

            Item.DefaultToPreHMLauncher(20, 16);
        }

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }
    }
}
