using Microsoft.Xna.Framework;
using MMZeroElements.Utilities;
using NotEnoughFlareGuns.Globals;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns
{
    public class Sparkler : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            NEFGlobalItem.FlareGuns.Add(Type);
            Item.AddFire();
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 44; // Hitbox width of the item.
            Item.height = 26; // Hitbox height of the item.
            Item.scale = 0.75f;
            Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 50; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 50; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = false; // Whether or not you can hold click to automatically use it again.
            Item.UseSound = SoundID.Unlock;

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 1; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 0f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shoot = ProjectileID.Spark; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 8f; // The speed of the projectile (measured in pixels per frame.)
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                .AddIngredient(ItemID.StoneBlock, 2)
                .AddIngredient(ItemID.Cobweb, 3)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextFloat() > .33)
                return true;
            else return false;
        }
    }
}