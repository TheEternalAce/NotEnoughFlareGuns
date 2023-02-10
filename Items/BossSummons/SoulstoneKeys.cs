using NotEnoughFlareGuns.Subworlds;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.BossSummons
{
    public class SoulstoneKeys : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.UseSound = SoundID.Roar;
            Item.useTurn = true;

            Item.rare = ItemRarityID.Orange;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                SubworldSystem.Enter<TheFactory>();
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddIngredient(ItemID.SoulofFlight, 5)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
