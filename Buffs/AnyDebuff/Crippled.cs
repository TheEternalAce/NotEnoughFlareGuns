using Terraria;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Buffs.AnyDebuff
{
    public class Crippled : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;  // Is it a debuff?
            Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense *= 0;
            npc.velocity.X *= 0.4f;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense = 0;
            player.moveSpeed -= 0.8f;
        }
    }
}
