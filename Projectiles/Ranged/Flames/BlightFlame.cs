using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Buffs.AnyDebuff;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Ranged.Flames
{
    public class BlightFlame : FlameBase
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            dustType = DustID.YellowTorch;
            flameColor = Color.Yellow;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.AddBuff(ModContent.BuffType<BlightFire>(), FactoryHelper.Seconds(10));
        }
    }
}
