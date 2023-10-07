using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using NotEnoughFlareGuns.Projectiles.Ranged;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.Flamethrowers
{
    public class HeatWave : ModItem
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
            Item.width = 30; // Hitbox width of the item.
            Item.height = 16; // Hitbox height of the item.
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.

            Item.DefaultToFlamethrower(10, 30, autoReuse: true);
            Item.value = 130000;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(5))
            {
                Projectile.NewProjectile(source, position, velocity * 2,
                    ModContent.ProjectileType<FlameFist>(), damage * 2, knockback,
                    player.whoAmI);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(5, -4);
        }
    }
}
