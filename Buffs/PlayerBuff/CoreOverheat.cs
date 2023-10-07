using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Buffs.PlayerBuff
{
    public class CoreOverheat : ModBuff
    {
        public override string Texture => NEFG.Buff;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance -= 0.5f;
            player.GetDamage(DamageClass.Generic) += 0.5f;
            player.GetModPlayer<FactoryPlayer>().coreOverheat = true;
        }
    }
}
