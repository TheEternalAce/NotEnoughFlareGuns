using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Projectiles.Melee;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace NotEnoughFlareGuns.Items.Weapons.Melee
{
    public class Geyser : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElementFire();
            Item.AddElementAqua();
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
                .AddIngredient(ItemID.GeyserTrap)
                .AddIngredient(ItemID.Seashell, 5)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 17)
                .AddIngredient(ItemID.GeyserTrap)
                .AddIngredient(ItemID.Seashell, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.CanBeChasedBy())
            {
                for (int i = 0; i < 10; i++)
                {
                    var dust = Dust.NewDustPerfect(target.Center, DustID.Smoke);
                    dust.fadeIn = 2;
                    dust.velocity = new Vector2(0, -1).RotatedByRandom(MathHelper.PiOver4);
                    dust.velocity *= 8f * Main.rand.NextFloat(0.33f, 1f);
                }
                for (int i = 0; i < 4; i++)
                {
                    WeightedRandom<int> randSmoke = new();
                    randSmoke.Add(GoreID.Smoke1);
                    randSmoke.Add(GoreID.Smoke2);
                    randSmoke.Add(GoreID.Smoke3);

                    var gore = Gore.NewGoreDirect(target.GetSource_FromThis(),
                        target.Center, new Vector2(0, -1), randSmoke);
                    gore.velocity.X = MathHelper.Clamp(gore.velocity.X, 0, 1);
                }
                Projectile.NewProjectile(Item.GetSource_OnHit(target), target.Center,
                    Vector2.Zero, ModContent.ProjectileType<GeyserExplosion>(),
                    damageDone / 2, 0f, player.whoAmI);
            }
        }
    }
}
