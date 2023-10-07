using NotEnoughFlareGuns.Projectiles.Magic;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Magic
{
    public class PlasmaBeacon : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElementFire();
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 20;
            Item.rare = ItemRarityID.LightPurple;
            Item.channel = true;
            Item.value = 200000;

            Item.damage = 100;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 0;
            Item.crit = 14;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item11;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.shoot = ModContent.ProjectileType<PlasmaBeaconProj>();
            Item.shootSpeed = 35;
        }
    }
}
