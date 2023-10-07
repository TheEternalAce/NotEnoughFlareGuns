using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Buffs.AnyDebuff
{
    public class BlightFire : ModBuff
    {
        public override string Texture => NEFG.Debuff;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<BlightedNPC>().blightFire = true;
        }
    }

    public class BlightedNPC : GlobalNPC
    {
        public bool blightFire = false;

        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            blightFire = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (blightFire)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 12;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (blightFire)
            {
                var dust = Dust.NewDustDirect(npc.position, npc.width, npc.height,
                    DustID.YellowTorch);
                dust.velocity.X = 0;
                dust.velocity.Y = -1 * Main.rand.NextFloat(0.33f, 3.4f);
                dust.noGravity = true;
                dust.fadeIn = 1;
            }
        }
    }
}
