using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Projectiles.Ranged.Flames;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.Flamethrowers
{
    public class WyvernsWrath : ModItem
    {
        public int stacks;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            NEFGlobalItem.Flamethrowers.Add(Type);
            Item.AddElementFire();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 30; // Hitbox width of the item.
            Item.height = 16; // Hitbox height of the item.
            Item.rare = ItemDefaults.RarityWallofFlesh; // The color that the item's name will be in-game.

            Item.DefaultToFlamethrower(46, 26, 8, autoReuse: true);

            Item.useTime = 26;
            Item.UseSound = SoundID.Item20;

            Item.shoot = ModContent.ProjectileType<ShadowFlame>();
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, -2);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Veinsinge>()
                .AddIngredient<PoisonFury>()
                .AddIngredient<HeatWave>()
                .AddIngredient<Exterminator>()
                .AddTile(TileID.DemonAltar)
                .Register();

            CreateRecipe()
                .AddIngredient<Blightsear>()
                .AddIngredient<PoisonFury>()
                .AddIngredient<HeatWave>()
                .AddIngredient<Exterminator>()
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
