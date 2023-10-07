using Terraria;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Buffs.PlayerBuff
{
    public class Wyvern : ModBuff
    {
        public override string Texture => NEFG.Buff;

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Ranged) += 0.15f;
            player.GetAttackSpeed(DamageClass.Ranged) += 0.15f;
        }
    }
}
