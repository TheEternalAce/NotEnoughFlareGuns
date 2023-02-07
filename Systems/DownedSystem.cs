using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace NotEnoughFlareGuns.Systems
{
    public class DownedSystem : ModSystem
    {
        public static bool downedFactoryEvent = false;
        public static int factoryDefeatAmount = 0;

        public override void OnWorldUnload()
        {
            downedFactoryEvent = false;
            factoryDefeatAmount = 0;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["downedFactoryEvent"] = downedFactoryEvent;
            tag["factoryDefeatAmount"] = factoryDefeatAmount;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            if (tag.ContainsKey("downedFactoryEvent"))
            {
                downedFactoryEvent = tag.GetBool("downedFactoryEvent");
            }
            if (tag.ContainsKey("factoryDefeatAmount"))
            {
                factoryDefeatAmount = tag.GetInt("factoryDefeatAmount");
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
