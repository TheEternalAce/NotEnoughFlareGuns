using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;

namespace NotEnoughFlareGuns.Utilities
{
    public static partial class FactoryHelper
    {
        public static int Hours(int hours)
        {
            int result = (int)(hours * Math.Pow(60, 3));
            return result;
        }

        public static int Minutes(int minutes)
        {
            int result = (int)(minutes * Math.Pow(60, 2));
            return result;
        }

        public static int Seconds(int seconds)
        {
            int result = seconds * 60;
            return result;
        }

        public static Projectile[] ProjectileRing(IEntitySource source, Vector2 center,
            int amount, float radius, float speed, int type, int damage, float knockback,
            int owner, float rotationAdd = 0f, float ai0 = 0f, float ai1 = 0f,
            float ai2 = 0f)
        {
            Projectile[] projectiles = new Projectile[amount];
            float rotation = MathHelper.ToRadians(360 / amount);
            for (int i = 0; i < amount; i++)
            {
                Vector2 position = center + Vector2.One.RotatedBy(rotation * i + rotationAdd) * radius;
                Vector2 velocity = Vector2.Normalize(center - position) * speed;
                projectiles[i] = Projectile.NewProjectileDirect(source, position, velocity,
                    type, damage, knockback, owner, ai0, ai1, ai2);
            }
            return projectiles;
        }

        public static int ConvertFlareTo(this int type, int flareOutput, int flareTypeOverride = ProjectileID.Flare, bool convertAllFlares = false)
        {
            bool convert = type == flareTypeOverride || type == NEFG.ConvertibleFlare;
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

        public static Vector2 GetPointInRegion(this Rectangle region)
        {
            Vector2 result = new()
            {
                X = region.X + Main.rand.Next(region.Width + 1),
                Y = region.Y + Main.rand.Next(region.Height + 1)
            };
            return result;
        }

        public static Player FindClosestPlayer(this Vector2 position, float maxDetectDistance, params int[] blaclkistedWhoAmI)
        {
            Player closestPlayer = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
            if (maxDetectDistance < 0)
            {
                sqrMaxDetectDistance = float.PositiveInfinity;
            }

            // Loop through all Players(max always 255)
            for (int k = 0; k < Main.maxPlayers; k++)
            {
                Player target = Main.player[k];
                // Check if Player able to be targeted. It means that Player is
                // 1. active and alive
                if (target.active && !target.dead)
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, position);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        // Check if NPC.whoAmI is not blacklisted
                        if (!blaclkistedWhoAmI.Contains(target.whoAmI))
                        {
                            sqrMaxDetectDistance = sqrDistanceToTarget;
                            closestPlayer = target;
                        }
                    }
                }
            }

            return closestPlayer;
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
