using NotEnoughFlareGuns.Projectiles.Melee;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Melee
{
    public class BurningBaselard : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElementFire();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 40; // Hitbox width of the item.
            Item.height = 40; // Hitbox height of the item.
            Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.
            Item.value = 150000;

            Item.damage = 14;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.noMelee = true;

            Item.shootSpeed = 4f;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.UseSound = SoundID.Item20;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<FlameBaselard>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldShortsword)
                .AddIngredient(ItemID.Torch, 99)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumShortsword)
                .AddIngredient(ItemID.Torch, 99)
                .Register();
        }
    }
}
