using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Globals
{
    public class NEFGlobalItem : GlobalItem
    {
        public static List<int> FlareGuns = new();

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.FlareGun)
            {
                item.DamageType = DamageClass.Ranged;
                FlareGuns.Add(item.type);
            }
        }
    }
}
