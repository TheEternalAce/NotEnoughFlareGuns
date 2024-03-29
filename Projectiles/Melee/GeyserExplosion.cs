﻿using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Projectiles.Melee
{
    // This Example show how to implement simple homing projectile
    // Can be tested with ExampleCustomAmmoGun
    public class GeyserExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
            Projectile.AddElementFire();
            Projectile.AddElementAqua();
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 54;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
        }

        // Custom AI
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item14);
                Projectile.ai[0] = 1;
            }
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.SteampunkSteam, Scale: 1.0f);
            dust.velocity *= 4f;

            if (++Projectile.frameCounter > 1)
            {
                if (++Projectile.frame > Main.projFrames[Type])
                {
                    Projectile.Kill();
                }
                Projectile.frameCounter = 0;
            }
        }
    }
}