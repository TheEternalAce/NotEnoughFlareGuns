using Microsoft.Xna.Framework;
using MMZeroElements;
using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Projectiles.Magic;
using NotEnoughFlareGuns.Tiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace NotEnoughFlareGuns.Items.Weapons.Magic
{
    public class TheFireLord : ModItem
    {
        int altAttCounter = 0;

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Fire.Add(Type);
            WeaponElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 20;
            Item.rare = ItemRarityID.LightPurple;
            Item.channel = true;
            Item.value = 200000;

            Item.damage = 136;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3;
            Item.crit = 8;

            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.UseSound = SoundID.Item20;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.shoot = ProjectileID.Flames;
            Item.shootSpeed = 16;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SoulstonePlating>(), 18)
                .AddTile(ModContent.TileType<FactoryWorkstation>())
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (++altAttCounter > 5)
            {
                altAttCounter = 0;
                if (Main.rand.NextBool())
                {
                    Lightning(player, source, position, velocity, damage, knockback);
                    SoundEngine.PlaySound(SoundID.Item43);
                }
                else
                {
                    Meteor(player, source, damage, knockback);
                    SoundEngine.PlaySound(SoundID.Item88);
                }
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        void Lightning(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int damage, float knockback)
        {
            int type = ModContent.ProjectileType<LightningBolt>();
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(5);
            position += Vector2.Normalize(velocity) * 10f;
            damage *= 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
        }

        void Meteor(Player player, EntitySource_ItemUse_WithAmmo source, int damage, float knockback)
        {
            WeightedRandom<int> randomMetor = new();
            randomMetor.Add(ProjectileID.Meteor1);
            randomMetor.Add(ProjectileID.Meteor2);
            randomMetor.Add(ProjectileID.Meteor3);
            int type = randomMetor.Get();
            damage *= 3;
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            Vector2 position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
            position.Y -= 100;
            Vector2 heading = target - position;

            if (heading.Y < 0f)
            {
                heading.Y *= -1f;
            }

            if (heading.Y < 20f)
            {
                heading.Y = 20f;
            }

            heading.Normalize();
            heading *= 35;
            Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI);
        }
    }
}
