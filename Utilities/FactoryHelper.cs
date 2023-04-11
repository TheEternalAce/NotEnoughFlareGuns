using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Utilities
{
    public static class FactoryHelper
    {
        public static int Hours(int hours)
        {
            int result = (int)(hours * Math.Pow(60, 3));
            return result;
        }

        public static int Minutues(int minutes)
        {
            int result = (int)(minutes * Math.Pow(60, 2));
            return result;
        }

        public static int Seconds(int seconds)
        {
            int result = seconds * 60;
            return result;
        }

        public static int ConvertFlareTo(this int type, int flareOutput, int flareTypeOverride = ProjectileID.Flare, bool convertAllFlares = false)
        {
            bool convert = type == flareTypeOverride || type == NotEnoughFlareGuns.ConvertibleFlare;
            if (convert || convertAllFlares)
            {
                return flareOutput;
            }
            return type;
        }

        public static SpriteEffects ToSpriteEffect(this int value)
        {
            return value == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public static Rectangle Frame(this Rectangle rectangle, int frameX, int frameY, int sizeOffsetX = 0, int sizeOffsetY = 0)
        {
            return new Rectangle(rectangle.X + (rectangle.Width - sizeOffsetX) * frameX, rectangle.Y + (rectangle.Width - sizeOffsetY) * frameY, rectangle.Width, rectangle.Height);
        }
        public static Rectangle Frame(this Projectile projectile)
        {
            return TextureAssets.Projectile[projectile.type].Value.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
        }

        public static SpriteEffects GetSpriteEffect(this Projectile projectile)
        {
            return (-projectile.spriteDirection).ToSpriteEffect();
        }

        public static Vector2 RotateTowards(Vector2 currentPosition, Vector2 currentVelocity, Vector2 targetPosition, float maxChange)
        {
            float scaleFactor = currentVelocity.Length();
            float targetAngle = currentPosition.AngleTo(targetPosition);
            return currentVelocity.ToRotation().AngleTowards(targetAngle, maxChange).ToRotationVector2() * scaleFactor;
        }

        public static Color MaxRGBA(this Color color, byte amt)
        {
            return color.MaxRGBA(amt, amt);
        }
        public static Color MaxRGBA(this Color color, byte amt, byte a)
        {
            return color.MaxRGBA(amt, amt, amt, a);
        }
        public static Color MaxRGBA(this Color color, byte r, byte g, byte b, byte a)
        {
            color.R = Math.Max(color.R, r);
            color.G = Math.Max(color.G, g);
            color.B = Math.Max(color.B, b);
            color.A = Math.Max(color.A, a);
            return color;
        }

        public static void Explode(this Projectile projectile, int explosionSize)
        {
            if (projectile.ai[1] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item14);
                projectile.ai[1] = 1;
                Vector2 oldSize = projectile.Size;
                projectile.velocity = Vector2.Zero;
                projectile.alpha = 255;
                projectile.hide = true;
                projectile.hostile = true;
                projectile.Size = new Vector2(explosionSize);
                projectile.timeLeft = 30;
                projectile.position += (oldSize - projectile.Size) / 2;
            }
        }

        /// <summary>
        /// Draws a basic single-frame glowmask for an item dropped in the world. Use in <see cref="Terraria.ModLoader.ModProjectile.PostDraw"/>
        /// </summary>
        public static void BasicInWorldGlowmask(this Projectile projectile, Texture2D glowTexture, Color color, float rotation, float scale)
        {
            Main.EntitySpriteDraw(
                glowTexture,
                new Vector2(
                    projectile.position.X - Main.screenPosition.X + projectile.width * 0.5f,
                    projectile.position.Y - Main.screenPosition.Y + projectile.height - glowTexture.Height * 0.5f
                ),
                new Rectangle(0, 0, glowTexture.Width, glowTexture.Height),
                color,
                rotation,
                glowTexture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0);
        }

        public const string DiamondX1 = "DiamondBlur1";
        public const string DiamondX2 = "DiamondBlur2";
        public const string OrbX1 = "OrbBlur1";
        public const string OrbX2 = "OrbBlur2";
        public const string LineX1 = "LineTrail1";
        public const string LineX2 = "LineTrail2";
        public static void DrawProjectilePrims(this Projectile projectile, Color color, string style, float angleAdd = 0f, float scale = 1f)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("NotEnoughFlareGuns/BlurTrails/" + style).Value;
            float plusRot = 0;
            if (style == DiamondX1 || style == DiamondX2 || style == LineX1 || style == LineX2)
            {
                plusRot = MathHelper.ToRadians(90);
            }

            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(projectile.width / 2f, projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
                Vector2 drawPos = (projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = scale * (projectile.oldPos.Length - k) / (projectile.oldPos.Length * 0.8f);
                Color drawColor = color * (1f - projectile.alpha) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, drawColor, projectile.oldRot[k] + plusRot + angleAdd, frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
        }
        //Credits to Aslysmic/Tewst Mod (so cool)

        public static void DrawPrimsAfterImage(this Projectile projectile, Color color, Texture2D texture)
        {
            Rectangle frame = projectile.Frame();
            Vector2 offset = new Vector2(projectile.width / 2, projectile.height / 2);
            var effects = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = frame.Size() / 2f;
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                float progress = 1f / ProjectileID.Sets.TrailCacheLength[projectile.type] * i;
                Main.spriteBatch.Draw(texture, projectile.oldPos[i] + offset - Main.screenPosition, frame, color * (1f - progress), projectile.rotation, origin, Math.Max(projectile.scale * (1f - progress), 0.1f), effects, 0f);
            }

            Main.spriteBatch.Draw(texture, projectile.position + offset - Main.screenPosition, frame, Color.White, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
        }
        public static void DrawPrimsAfterImage(this Projectile projectile, Color color)
        {
            var texture = TextureAssets.Projectile[projectile.type].Value;
            projectile.DrawPrimsAfterImage(color, texture);
        }
        //Credits to Aequus Mod (Omega Starite my beloved)

        public static void DefaultToFlareGun(this Item flareGun, int damage, int useTime, int crit = 0, float knockback = 0f, bool autoReuse = false, float velocity = 6f)
        {
            // Use Properties
            flareGun.useTime = useTime; // The item's use time in ticks (60 ticks == 1 second.)
            flareGun.useAnimation = useTime; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            flareGun.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            flareGun.autoReuse = autoReuse; // Whether or not you can hold click to automatically use it again.
            flareGun.UseSound = SoundID.Item11;

            // Weapon Properties
            flareGun.damage = damage; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            flareGun.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            flareGun.knockBack = knockback;  // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            flareGun.crit = crit;
            flareGun.noMelee = true; // So the item's animation doesn't do damage.

            // Flare Gun Properties
            flareGun.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            flareGun.shootSpeed = velocity; // The speed of the projectile (measured in pixels per frame.)
            flareGun.useAmmo = AmmoID.Flare; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
        }

        public static void DefaultToFlamethrower(this Item flamethrower, int damage, int useTime, int crit = 0, float knockback = 4f, bool autoReuse = false, float velocity = 8f)
        {
            // Use Properties
            flamethrower.useTime = useTime / 2; // The item's use time in ticks (60 ticks == 1 second.)
            flamethrower.useAnimation = useTime; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            flamethrower.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            flamethrower.autoReuse = autoReuse; // Whether or not you can hold click to automatically use it again.
            flamethrower.UseSound = SoundID.Item34;

            // Weapon Properties
            flamethrower.damage = damage; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            flamethrower.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            flamethrower.knockBack = knockback;  // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            flamethrower.crit = crit;
            flamethrower.noMelee = true; // So the item's animation doesn't do damage.

            // Flare Gun Properties
            flamethrower.shoot = ProjectileID.Flames; // For some reason, all the guns in the vanilla source have this.
            flamethrower.shootSpeed = velocity; // The speed of the projectile (measured in pixels per frame.)
            flamethrower.useAmmo = AmmoID.Gel; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
        }

        public static void DefaultToLauncher(this Item launcher, int damage, int useTime, int crit = 0, float knockback = 4f, bool autoReuse = false, float velocity = 8f)
        {
            // Use Properties
            launcher.useTime = useTime; // The item's use time in ticks (60 ticks == 1 second.)
            launcher.useAnimation = useTime; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            launcher.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            launcher.autoReuse = autoReuse; // Whether or not you can hold click to automatically use it again.
            launcher.UseSound = SoundID.Item11;

            // Weapon Properties
            launcher.damage = damage; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            launcher.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            launcher.knockBack = knockback;  // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            launcher.crit = crit;
            launcher.noMelee = true; // So the item's animation doesn't do damage.

            // Flare Gun Properties
            launcher.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            launcher.shootSpeed = velocity; // The speed of the projectile (measured in pixels per frame.)
            launcher.useAmmo = AmmoID.Rocket; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
        }

        public static void DefaultToPreHMLauncher(this Item preHMLauncher, int damage, int useTime, int crit = 0, float knockback = 4f, bool autoReuse = false, float velocity = 6f)
        {
            // Use Properties
            preHMLauncher.useTime = useTime; // The item's use time in ticks (60 ticks == 1 second.)
            preHMLauncher.useAnimation = useTime; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            preHMLauncher.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            preHMLauncher.autoReuse = autoReuse; // Whether or not you can hold click to automatically use it again.
            preHMLauncher.UseSound = SoundID.Item11;

            // Weapon Properties
            preHMLauncher.damage = damage; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            preHMLauncher.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            preHMLauncher.knockBack = knockback;  // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            preHMLauncher.crit = crit;
            preHMLauncher.noMelee = true; // So the item's animation doesn't do damage.

            // Flare Gun Properties
            preHMLauncher.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            preHMLauncher.shootSpeed = velocity; // The speed of the projectile (measured in pixels per frame.)
            preHMLauncher.useAmmo = ItemID.Grenade; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
        }

        public static void DefaultToFlare(this Item flare, int damage, int projectile, bool consumable = true)
        {
            flare.maxStack = consumable ? 9999 : 1;

            flare.damage = damage;
            flare.DamageType = DamageClass.Ranged;
            flare.knockBack = 1.5f;

            flare.consumable = consumable;
            flare.shoot = projectile;
            flare.shootSpeed = 6f;
            flare.ammo = AmmoID.Flare;
        }

        public static void DefaultToBullet(this Item bullet, int damage, int projectile)
        {
            bullet.maxStack = 9999;

            bullet.damage = damage;
            bullet.DamageType = DamageClass.Ranged;
            bullet.knockBack = 4f;

            bullet.consumable = true;
            bullet.shoot = projectile;
            bullet.shootSpeed = 4f;
            bullet.ammo = AmmoID.Bullet;
        }

        public static FactoryPlayer InfernalPlayer(this Player player)
        {
            return player.GetModPlayer<FactoryPlayer>();
        }

        public static Item DefaultItem(int type)
        {
            var item = new Item();
            item.SetDefaults(type);
            return item;
        }

        public static bool Insert(this Chest chest, int itemType, int itemStack, int index)
        {
            var item = DefaultItem(itemType);
            item.stack = itemStack;
            return InsertIntoUnresizableArray(chest.item, item, index);
        }

        public static bool Insert(this Chest chest, int itemType, int index)
        {
            return chest.Insert(itemType, 1, index);
        }

        public static bool InsertIntoUnresizableArray<T>(T[] arr, T value, int index)
        {
            if (index >= arr.Length)
            {
                return false;
            }
            for (int j = arr.Length - 1; j > index; j--)
            {
                arr[j] = arr[j - 1];
            }
            arr[index] = value;
            return true;
        }
    }
}
