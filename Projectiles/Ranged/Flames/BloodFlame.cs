using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Buffs.AnyDebuff;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Ranged.Flames
{
    public class BloodFlame : FlameBase
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            dustType = DustID.RedTorch;
            flameColor = Color.Red;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.AddBuff(ModContent.BuffType<BloodBoil>(), FactoryHelper.Seconds(10));
        }
    }
}
