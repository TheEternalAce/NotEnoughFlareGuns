using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Items.Placable;
using NotEnoughFlareGuns.NPCs.TheFactoryOnslaught;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
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
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Onslaught Terminal");
            AddMapEntry(new Color(200, 200, 200), name);
            DustType = DustID.Titanium;
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
            if (!NPC.AnyNPCs(type))
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // If the Player is not in multiPlayer, spawn directly
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {
                    // If the Player is in multiPlayer, request a spawn
                    // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type);
                }
            }

            return true;
        }
    }
}
