using System;
using Terraria;
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

        public static void DefaultToFlamethrower(this Item thrower, int damage, int useTime, int crit = 0, float knockback = 4f, bool autoReuse = false, float velocity = 8f)
        {
            // Use Properties
            thrower.useTime = useTime / 2; // The item's use time in ticks (60 ticks == 1 second.)
            thrower.useAnimation = useTime; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            thrower.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            thrower.autoReuse = autoReuse; // Whether or not you can hold click to automatically use it again.
            thrower.UseSound = SoundID.Item34;

            // Weapon Properties
            thrower.damage = damage; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            thrower.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            thrower.knockBack = knockback;  // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            thrower.crit = crit;
            thrower.noMelee = true; // So the item's animation doesn't do damage.

            // Flare Gun Properties
            thrower.shoot = ProjectileID.Flames; // For some reason, all the guns in the vanilla source have this.
            thrower.shootSpeed = velocity; // The speed of the projectile (measured in pixels per frame.)
            thrower.useAmmo = AmmoID.Gel; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
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

        public static void DefaultToFlare(this Item flare, int damage, int crit, int projectile, bool consumable = true, float velocity = 6)
        {
            flare.maxStack = consumable ? 9999 : 1;

            flare.damage = damage;
            flare.DamageType = DamageClass.Ranged;
            flare.knockBack = 1.5f;
            flare.crit = crit;

            flare.consumable = consumable;
            flare.shoot = projectile;
            flare.shootSpeed = velocity;
            flare.ammo = AmmoID.Flare;
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
