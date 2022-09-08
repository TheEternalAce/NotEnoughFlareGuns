using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using NotEnoughFlareGuns.Tiles;
using NotEnoughFlareGuns.Items.FlareGuns.PreHardmode;
using NotEnoughFlareGuns.Utilities;

namespace NotEnoughFlareGuns
{
    public class TheFactory : ModSystem
    {
        public override void PostWorldGen()
        {
            for (int k = 0; k < Main.maxChests; k++)
            {
                Chest c = Main.chest[k];
                if (c != null)
                {
                    if (Main.tile[c.x, c.y].TileType == TileID.Containers)
                    {
                        int style = ChestTypes.GetChestStyle(c);
                        if (style == ChestTypes.LockedGold)
                        {
                            if (Main.rand.NextBool(6))
                            {
                                c.Insert(ModContent.ItemType<AzulFlame>(), 1);
                            }
                        }
                    }
                }
            }
        }
    }
}
