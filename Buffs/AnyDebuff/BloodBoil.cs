using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Buffs.AnyDebuff
{
    public class BloodBoil : ModBuff
    {
        public override void SetStaticDefaults()
        {
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
            npc.GetGlobalNPC<BloodBoilNPC>().boil = true;
        }
    }

    public class BloodBoilNPC : GlobalNPC
    {
        public bool boil = false;

        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            boil = false;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (boil)
            {
                modifiers.Defense.Flat -= 4;
            }
        }
    }
}
