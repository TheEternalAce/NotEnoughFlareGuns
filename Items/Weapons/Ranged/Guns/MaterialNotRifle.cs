using Microsoft.Xna.Framework;
using MMZeroElements.Utilities;
using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Projectiles.Ranged.Bullets;
using NotEnoughFlareGuns.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.Guns
{
    public class MaterialNotRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            Item.AddFire();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 100; // Hitbox width of the item.
            Item.height = 30; // Hitbox height of the item.
            Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.
            Item.value = 250000;

            Item.DefaultToRangedWeapon(ProjectileID.PurificationPowder, AmmoID.Bullet, 32, 16);
            Item.damage = 120;
            Item.knockBack = 7;
            Item.crit = 6;
            Item.ArmorPenetration = 15;

            Item.UseSound = SoundID.Item40;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
            {
                type = ModContent.ProjectileType<HighExplosiveBullet>();
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SoulstonePlating>(), 18)
                .AddTile(ModContent.TileType<FactoryWorkstation>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12f, -3f);
        }
    }
}
