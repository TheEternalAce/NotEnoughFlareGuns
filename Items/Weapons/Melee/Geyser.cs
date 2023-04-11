using Microsoft.Xna.Framework;
using MMZeroElements.Utilities;
using NotEnoughFlareGuns.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Melee
{
    public class Geyser : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            Item.AddFire();
            Item.AddIce();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 48; // Hitbox width of the item.
            Item.height = 48; // Hitbox height of the item.
            Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.
            Item.value = 150000;

            Item.damage = 16;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4;

            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 17)
                .AddIngredient(ItemID.GeyserTrap, 2)
                .AddIngredient(ItemID.Seashell, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(Item.GetSource_OnHit(target), target.Center, Vector2.Zero,
                ModContent.ProjectileType<GeyserExplosion>(), player.GetWeaponDamage(Item), player.GetWeaponKnockback(Item),
                player.whoAmI);
        }
    }
}
