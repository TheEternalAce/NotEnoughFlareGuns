using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Projectiles.Tools;
using NotEnoughFlareGuns.Tiles;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Tools
{
    public class InfernalDrill : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsDrill[Type] = true;

            Item.ResearchUnlockCount = 1;
            Item.AddElementFire();
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 30;

            Item.pick = 200;

            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 0;
            Item.tileBoost += 3;

            Item.useTime = 2; //Actual Break 1 = FAST 50 = SUPER SLOW
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item23;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 32;
            Item.rare = ItemDefaults.RaritySoulstone;
            Item.value = ItemDefaults.ValueEarlyHardmode;
            Item.shoot = ModContent.ProjectileType<PaydayDrill>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TitaniumDrill, 6)
                .AddIngredient<SoulstonePlating>(10)
                .AddIngredient(ItemID.Wire, 10)
                .AddTile<FactoryWorkstation>()
                .Register();
        }
    }
}