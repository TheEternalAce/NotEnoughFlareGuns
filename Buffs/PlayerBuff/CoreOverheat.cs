using Terraria;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Buffs.PlayerBuff
{
    public class CoreOverheat : ModBuff
    {
        public override string Texture => "NotEnoughFlareGuns/Buffs/DebuffTemplate";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;  // Is it a debuff?
            Main.buffNoSave[Type] = true; // It means the buff won't save when you exit the world
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance -= 0.5f;
            player.GetDamage(DamageClass.Generic) += 0.5f;
            player.GetModPlayer<FactoryPlayer>().coreOverheat = true;
        }
    }
}
