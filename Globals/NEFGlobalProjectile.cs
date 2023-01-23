using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Globals
{
    public class NEFGlobalProjectile : GlobalProjectile
    {
        public static List<int> Flare = new();

        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.Flare || projectile.type == ProjectileID.BlueFlare /*||
                projectile.type = ProjectileID.SpelunkerFlare || projectile.type = ProjectileID.CursedFlare ||
                projectile.type = ProjectileID.RainbowFlare || projectile.type = ProjectileID.ShimmerFlare*/)
            {
                Flare.Add(projectile.type);
            }
        }
    }
}
