using Terraria;

namespace NotEnoughFlareGuns.Utilities
{
    public partial class FactoryHelper
    {
        public static void AddElementFire(this NPC npc)
        {
            NEFG.TryElementCall("assignElement", npc, 0);
        }
        public static void AddElementAqua(this NPC npc)
        {
            NEFG.TryElementCall("assignElement", npc, 1);
        }
        public static void AddElementElec(this NPC npc)
        {
            NEFG.TryElementCall("assignElement", npc, 2);
        }
        public static void AddElementWood(this NPC npc)
        {
            NEFG.TryElementCall("assignElement", npc, 3);
        }
        public static void ElementMultipliers(this NPC npc, float[] multipliers)
        {
            NEFG.TryElementCall("assignElement", npc, multipliers);
        }
    }
}
