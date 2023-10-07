using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Projectiles.Ranged.Flames;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.Flamethrowers
{
    public class PoisonFury : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            NEFGlobalItem.Flamethrowers.Add(Type);
            Item.AddElementFire();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 48; // Hitbox width of the item.
            Item.height = 30; // Hitbox height of the item.
            Item.rare = ItemDefaults.RarityJungle; // The color that the item's name will be in-game.

            Item.DefaultToFlamethrower(8, 22, autoReuse: true);
            Item.shoot = ModContent.ProjectileType<PoisonFlame>();
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(3))
            {
                Projectile.NewProjectile(source, position, velocity
                    .RotatedByRandom(MathHelper.ToRadians(15)) * 2, ProjectileID.Leaf,
                    damage, knockback, player.whoAmI);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Stinger, 12)
                .AddIngredient(ItemID.JungleSpores, 12)
                .AddIngredient(ItemID.RichMahogany, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -2);
        }
    }
}
