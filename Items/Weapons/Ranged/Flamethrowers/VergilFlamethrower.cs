using MMZeroElements.Utilities;
using MMZeroElements.Utilities;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Projectiles.Ranged.VergilFlamethrower;
using NotEnoughFlareGuns.Systems;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.Flamethrowers
{
    /// <summary>
    /// Vergil Flamethrower
    /// </summary>
    public class VergilFlamethrower : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            NEFGlobalItem.Flamethrowers.Add(Type);
            Item.AddFire();
            Item.AddElectric();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 48; // Hitbox width of the item.
            Item.height = 28; // Hitbox height of the item.
            Item.rare = ItemRarityID.Cyan; // The color that the item's name will be in-game.

            Item.DefaultToFlamethrower(90, 20, 12, autoReuse: true, velocity: 16);
            Item.shoot = ModContent.ProjectileType<ApproachingStorm>();
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.useTime = 20;
            Item.value = 10000;
        }

        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("ShardsOfAtheria", out Mod shards))
            {
                CreateRecipe()
                    .AddIngredient(shards.Find<ModItem>("AreusShard"), 14)
                    .AddRecipeGroup(FactoryRecipes.Gold, 4)
                    .AddIngredient(ItemID.LunarBar, 15)
                    .AddTile(TileID.LunarCraftingStation)
                    .Register();
            }
        }
    }
}
