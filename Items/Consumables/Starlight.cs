using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Consumables
{
    public class Starlight : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.consumable = true;

            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.UseSound = SoundID.Item4;
            Item.useTurn = true;

            Item.rare = ItemRarityID.Yellow;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.InfernalPlayer().cokeStarlight;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                player.InfernalPlayer().cokeStarlight = true;
            }
            return true;
        }
    }
}
