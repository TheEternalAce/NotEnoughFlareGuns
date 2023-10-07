using NotEnoughFlareGuns.Projectiles.Magic;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Magic
{
    public class Welder : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElementFire();
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.rare = ItemDefaults.RarityPreBoss;
            Item.channel = true;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;

            Item.damage = 24;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3;
            Item.mana = 8;

            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item34;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.shoot = ModContent.ProjectileType<WelderLaser>();
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SilverBar, 14)
                .AddIngredient(ItemID.Ruby, 5)
                .AddIngredient(ItemID.Torch, 99)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TungstenBar, 14)
                .AddIngredient(ItemID.Ruby, 5)
                .AddIngredient(ItemID.Torch, 99)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
