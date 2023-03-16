using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Tools
{
    public class Blacklight : ModItem
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
            Item.UseSound = SoundID.Item4;
            Item.useTurn = true;

            Item.rare = ItemRarityID.Yellow;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                player.InfernalPlayer().blacklight = !player.InfernalPlayer().blacklight;
                if (player.InfernalPlayer().blacklight)
                {
                    ChatHelper.SendChatMessageToClient(NetworkText.FromKey("Mods.NotEnoughFlareGuns.BlacklightActivate"), Color.White, player.whoAmI);
                }
                else
                {
                    ChatHelper.SendChatMessageToClient(NetworkText.FromKey("Mods.NotEnoughFlareGuns.BlacklightDeactivate"), Color.White, player.whoAmI);
                }
            }
            return true;
        }
    }
}
