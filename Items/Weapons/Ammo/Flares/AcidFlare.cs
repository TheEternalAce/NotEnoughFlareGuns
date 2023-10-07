using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ammo.Flares
{
    public class AcidFlare : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
            NEFGlobalItem.Flares.Add(Type);
            Item.AddElementFire();
            Item.AddElementWood();
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 22;

            Item.DefaultToFlare(18, ModContent.ProjectileType<Projectiles.Ranged.Flares.AcidFlare>());

            Item.value = Item.sellPrice(copper: 8);
            Item.rare = ItemRarityID.Green;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddIngredient(ItemID.Flare, 50)
                .AddIngredient(ItemID.VialofVenom)
                .AddTile(TileID.ImbuingStation)
                .Register();
        }
    }
}
