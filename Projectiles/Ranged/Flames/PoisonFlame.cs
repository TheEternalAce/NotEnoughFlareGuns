using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;

namespace NotEnoughFlareGuns.Projectiles.Ranged.Flames
{
    public class PoisonFlame : FlameBase
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            dustType = DustID.GreenTorch;
            flameColor = Color.GreenYellow;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.AddBuff(BuffID.Poisoned, FactoryHelper.Seconds(10));
        }
    }
}
