using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Projectiles.Ranged.Flames;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.Flamethrowers
{
    public class Veinsinge : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            NEFGlobalItem.Flamethrowers.Add(Type);
            Item.AddElementFire();
            Item.AddElementAqua();
            Item.AddElementWood();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 48; // Hitbox width of the item.
            Item.height = 30; // Hitbox height of the item.
            Item.rare = ItemDefaults.RarityBossMasks; // The color that the item's name will be in-game.

            Item.DefaultToFlamethrower(7, 26, 2, autoReuse: true, velocity: 6);
            Item.shoot = ModContent.ProjectileType<BloodFlame>();
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CrimtaneBar, 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
