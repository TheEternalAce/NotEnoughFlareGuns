using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Buffs.PlayerBuff;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Ranged.Flames
{
    public class ShadowFlame : FlameBase
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            dustType = DustID.PurpleTorch;
            flameColor = Color.Purple;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.AddBuff(BuffID.ShadowFlame, FactoryHelper.Seconds(10));
            var player = Projectile.GetPlayerOwner();
            player.AddBuff(ModContent.BuffType<Wyvern>(), 60);
        }
    }
}
