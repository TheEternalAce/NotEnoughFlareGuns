using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Projectiles.Ranged.SFG;
using NotEnoughFlareGuns.Tiles;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns
{
    public class SFG : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            NEFGlobalItem.FlareGuns.Add(Type);
            Item.AddElementFire();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 36; // Hitbox width of the item.
            Item.height = 20; // Hitbox height of the item.
            Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.

            Item.DefaultToFlareGun(200, 56, 8, 4, false, 14);
            Item.UseSound = SoundID.Item38;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<SFGHeld>();
            Item.channel = true;
            Item.value = 200000;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<SFGHeld>();
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 corkVelocity = -velocity;
            corkVelocity.Normalize();
            corkVelocity *= 4f;
            Projectile.NewProjectile(source, player.Center, corkVelocity, ModContent.ProjectileType<SFGCork>(), 0, 0, player.whoAmI);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SoulstonePlating>(), 18)
                .AddTile(ModContent.TileType<FactoryWorkstation>())
                .Register();
        }
    }
}
