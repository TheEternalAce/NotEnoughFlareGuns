using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Globals;
using Terraria;
using Terraria.Audio;
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
                int killedFlares = 0;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile flare = Main.projectile[i];
                    if (flare.active && flare.owner == player.whoAmI && NEFGlobalProjectile.Flare.Contains(flare.type))
                    {
                        flare.Kill();
                        for (int d = 0; d < 10; d++)
                        {
                            Dust.NewDust(flare.position, flare.width, flare.height, DustID.GolfPaticle, newColor: Color.Black);
                        }
                        killedFlares++;
                        //Main.projectile[i] = null;
                    }
                }
                if (killedFlares > 0)
                {
                    ChatHelper.SendChatMessageToClient(NetworkText.FromKey("Mods.NotEnoughFlareGuns.BlacklightDestroy", killedFlares),
                        Color.White, player.whoAmI);
                    SoundEngine.PlaySound(SoundID.NPCDeath37);
                }
            }
            return true;
        }
    }
}
