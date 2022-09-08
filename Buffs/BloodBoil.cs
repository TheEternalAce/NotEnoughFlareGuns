using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Buffs
{
    public class BloodBoil : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Boil"); // Buff display name
            Description.SetDefault("Reduced defense"); // Buff description
            Main.debuff[Type] = true;  // Is it a debuff?
            Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
            Main.buffNoSave[Type] = true; // It means the buff won't save when you exit the world
            BuffID.Sets.LongerExpertDebuff[Type] = true; // If this buff is a debuff, setting this to true will make this buff last twice as long on players in expert mode
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 4;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense -= 4;
        }
    }
}
