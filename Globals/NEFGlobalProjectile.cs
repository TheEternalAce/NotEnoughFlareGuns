using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Globals
{
    public class NEFGlobalProjectile : GlobalProjectile
    {
        public static List<int> Flare = new();
        public bool blacklightDeath = false;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.Flare || projectile.type == ProjectileID.BlueFlare /*||
                projectile.type = ProjectileID.SpelunkerFlare || projectile.type = ProjectileID.CursedFlare ||
                projectile.type = ProjectileID.RainbowFlare || projectile.type = ProjectileID.ShimmerFlare*/)
            {
                Flare.Add(projectile.type);
            }
        }

        public override void AI(Projectile projectile)
        {
            base.AI(projectile);
            Player player = Main.player[projectile.owner];
            if (player.whoAmI == projectile.owner)
            {
                if (Flare.Contains(projectile.type))
                {
                    if (player.InfernalPlayer().cokeStarlight && player.InfernalPlayer().cokeStarlightActive)
                    {
                        Vector3 color = new(238, 128, 214);
                        Lighting.AddLight(projectile.Center, color * 0.01f);
                    }
                }
            }
        }

        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if (Main.player[projectile.owner].InfernalPlayer().blacklight)
            {
                if (Flare.Contains(projectile.type) && !blacklightDeath)
                {
                    projectile.timeLeft = 60 * (1 + projectile.extraUpdates);
                    blacklightDeath = true;
                }
            }
            return base.OnTileCollide(projectile, oldVelocity);
        }

        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if (blacklightDeath)
            {
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.GolfPaticle, newColor: Color.Black);
                }
            }
        }
    }
}
