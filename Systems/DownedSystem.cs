using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace NotEnoughFlareGuns.Systems
{
    public class DownedSystem : ModSystem
    {
        public static bool downedFactoryEvent = false;

        public override void OnWorldUnload()
        {
            downedFactoryEvent = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["downedFactoryEvent"] = downedFactoryEvent;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            if (tag.ContainsKey("downedFactoryEvent"))
            {
                downedFactoryEvent = tag.GetBool("downedFactoryEvent");
            }
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();

            downedFactoryEvent = flags[0];
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedFactoryEvent;
        }
    }
}
