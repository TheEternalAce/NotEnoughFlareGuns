using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Projectiles.Ranged.Flares;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns
{
    public class SolarStorm : ModItem
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
            Item.width = 40; // Hitbox width of the item.
            Item.height = 22; // Hitbox height of the item.
            Item.rare = ItemRarityID.Expert; // The color that the item's name will be in-game.

            Item.DefaultToFlareGun(150, 10, autoReuse: true);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            if (type == ProjectileID.Flare || type == NEFG.ConvertibleFlare)
                type = ModContent.ProjectileType<SolarFlare>();
        }

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }
    }
}
