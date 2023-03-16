using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Items.Placeable;
using NotEnoughFlareGuns.NPCs.TheFactoryOnslaught;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace NotEnoughFlareGuns.Tiles
{
    public class OnslaughtTerminal : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName("OnslaughtTerminal");
            AddMapEntry(new Color(200, 200, 200), name);
            DustType = DustID.Iron;
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<OnslaughtTerminalItem>());
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            int type = ModContent.NPCType<SoulstoneCore>();
            Tile terminal = Main.tile[i, j];
            int frameX = terminal.TileFrameX;
            int frameY = terminal.TileFrameY;
            if (frameX >= 72)
            {
                frameX -= 72;
            }

            Vector2 tilePos = new(i * 16 - frameX, j * 16 - frameY);
            Vector2 offset = new(40, 42);
            Vector2 spawnPos = tilePos + new Vector2(-10 * 16, -41 * 16) + offset;
            //for (int d = 0; d < 20; d++)
            //{
            //    Dust dust = Dust.NewDustPerfect(spawnPos, DustID.Electric);
            //    dust.noGravity = true;
            //    dust.velocity *= 0f;
            //}
            //for (int d = 0; d < 20; d++)
            //{
            //    Dust dust = Dust.NewDustPerfect(tilePos, DustID.Electric);
            //    dust.noGravity = true;
            //    dust.velocity *= 0f;
            //}
            //return true;

            if (!NPC.AnyNPCs(type))
            {
                NPC core = NPC.NewNPCDirect(NPC.GetBossSpawnSource(player.whoAmI), (int)spawnPos.X, (int)spawnPos.Y, type);
                core.position = spawnPos;
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", Main.npc[core.whoAmI].GetTypeNetName()), new Color(175, 75, 255));
            }

            return true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;

            player.cursorItemIconID = ModContent.ItemType<OnslaughtTerminalItem>();
            player.cursorItemIconText = "";
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
        }
    }
}
