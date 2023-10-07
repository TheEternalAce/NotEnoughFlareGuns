using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Projectiles.Magic.PyroGlove;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Magic
{
    public class PyrotexGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElementFire();
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 20;
            Item.rare = ItemRarityID.LightPurple;
            Item.channel = true;
            Item.value = 180000;

            Item.damage = 120;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 6;
            Item.crit = 22;

            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = NEFG.GloveSnap;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shoot = ModContent.ProjectileType<LargeSpark>();
            Item.shootSpeed = 16;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 14)
                .AddIngredient(ItemID.Ectoplasm, 16)
                .AddIngredient(ItemID.MagmaStone)
                .AddIngredient(ItemID.PhilosophersStone)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                damage *= 2;
                type = ModContent.ProjectileType<SmallSpark>();
            }
        }
    }
}
