using NotEnoughFlareGuns.Projectiles.TheFactoryOnslaught;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.NPCs.TheFactoryOnslaught
{
    public class PlasmaTurret : FactoryTurret
    {
        public override void SetDefaults()
        {
            NPC.lifeMax = 700;
            NPC.defense = 16;
            laserType = ModContent.ProjectileType<PlasmaLaser>();
            laserDamage = 14;
            laserSpeed = 32f;
            fireRate = 3;
            NPC.ElementMultipliers(new[] { 0.8f, 2.5f, 0.8f, 0.5f });
            base.SetDefaults();
        }
    }
}
