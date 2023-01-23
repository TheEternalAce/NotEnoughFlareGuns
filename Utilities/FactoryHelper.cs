using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Utilities
{
    public static class FactoryHelper
    {
        public static void DefaultToFlareGun(this Item flareGun, int damage, int useTime, bool autoReuse = false, int crit = 0, float knockback = 0f, float velocity = 6f)
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
