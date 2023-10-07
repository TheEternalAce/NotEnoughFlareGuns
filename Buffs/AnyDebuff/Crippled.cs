using Terraria;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Buffs.AnyDebuff
{
    public class Crippled : ModBuff
    {
        public const float DefenseMultiplier = 0f;
        public const float SpeedMultiplier = 0f;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;  // Is it a debuff?
            Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense *= DefenseMultiplier;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<CrippledNPC>().crippled = true;
        }
    }

    class CrippledNPC : GlobalNPC
    {
        public bool crippled = false;

        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            crippled = false;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (crippled)
            {
                modifiers.Defense *= Crippled.DefenseMultiplier;
            }
        }
    }
}
