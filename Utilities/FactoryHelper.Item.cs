using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Utilities
{
    public partial class FactoryHelper
    {
        public static void DefaultToPotion(this Item potion, int buff, int buffTime)
        {
            potion.useTime = 17;
            potion.useAnimation = 17;
            potion.useStyle = ItemUseStyleID.DrinkLiquid;
            potion.UseSound = SoundID.Item3;
            potion.consumable = true;
            potion.useTurn = true;
            potion.maxStack = 9999;

            potion.buffType = buff;
            potion.buffTime = buffTime;
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

        public static void DefaultToFlamethrower(this Item flamethrower, int damage, int useTime, int crit = 0, float knockback = 0.33f, bool autoReuse = false, float velocity = 8f)
        {
            // Use Properties
            flamethrower.useTime = useTime / 5;
            flamethrower.useAnimation = useTime;
            flamethrower.useStyle = ItemUseStyleID.Shoot;
            flamethrower.autoReuse = autoReuse;
            flamethrower.UseSound = SoundID.Item34;

            // Weapon Properties
            flamethrower.damage = damage;
            flamethrower.DamageType = DamageClass.Ranged;
            flamethrower.knockBack = knockback;
            flamethrower.crit = crit;
            flamethrower.noMelee = true;

            // Flare Gun Properties
            flamethrower.shoot = ProjectileID.Flames;
            flamethrower.shootSpeed = velocity;
            flamethrower.useAmmo = AmmoID.Gel;
        }

        public static void DefaultToLauncher(this Item launcher, int damage, int useTime, int crit = 0, float knockback = 4f, bool autoReuse = false, float velocity = 8f)
        {
            // Use Properties
            launcher.useTime = useTime;
            launcher.useAnimation = useTime;
            launcher.useStyle = ItemUseStyleID.Shoot;
            launcher.autoReuse = autoReuse;
            launcher.UseSound = SoundID.Item11;

            // Weapon Properties
            launcher.damage = damage;
            launcher.DamageType = DamageClass.Ranged;
            launcher.knockBack = knockback;
            launcher.crit = crit;
            launcher.noMelee = true; // So the item's animation doesn't do damage.

            // Flare Gun Properties
            launcher.shoot = ProjectileID.PurificationPowder;
            launcher.shootSpeed = velocity;
            launcher.useAmmo = AmmoID.Rocket;
        }

        public static void DefaultToPreHMLauncher(this Item preHMLauncher, int damage, int useTime, int crit = 0, float knockback = 4f, bool autoReuse = false, float velocity = 6f)
        {
            // Use Properties
            preHMLauncher.useTime = useTime;
            preHMLauncher.useAnimation = useTime;
            preHMLauncher.useStyle = ItemUseStyleID.Shoot;
            preHMLauncher.autoReuse = autoReuse;
            preHMLauncher.UseSound = SoundID.Item11;

            // Weapon Properties
            preHMLauncher.damage = damage;
            preHMLauncher.DamageType = DamageClass.Ranged;
            preHMLauncher.knockBack = knockback;
            preHMLauncher.crit = crit;
            preHMLauncher.noMelee = true;

            // Flare Gun Properties
            preHMLauncher.shoot = ProjectileID.PurificationPowder;
            preHMLauncher.shootSpeed = velocity;
            preHMLauncher.useAmmo = ItemID.Grenade;
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

        public static void AddElementFire(this Item item)
        {
            NEFG.TryElementCall("assignElement", item, 0);
        }
        public static void AddElementAqua(this Item item)
        {
            NEFG.TryElementCall("assignElement", item, 1);
        }
        public static void AddElementElec(this Item item)
        {
            NEFG.TryElementCall("assignElement", item, 2);
        }
        public static void AddElementWood(this Item item)
        {
            NEFG.TryElementCall("assignElement", item, 3);
        }
    }
}
