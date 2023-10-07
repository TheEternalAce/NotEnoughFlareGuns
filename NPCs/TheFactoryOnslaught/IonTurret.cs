using BattleNetworkElements.Utilities;
using NotEnoughFlareGuns.Projectiles.TheFactoryOnslaught;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.NPCs.TheFactoryOnslaught
{
    public class IonTurret : FactoryTurret
    {
        public override void SetDefaults()
        {
            NPC.lifeMax = 1200;
            NPC.defense = 32;
            laserType = ModContent.ProjectileType<IonLaser>();
            laserDamage = 24;
            laserSpeed = 32f;
            fireRate = 3;
            fireTimerCooldown = 60;
            NPC.ElementMultipliers(new[] { 0.8f, 2.5f, 0.8f, 0.5f });
            base.SetDefaults();
        }
    }
}
