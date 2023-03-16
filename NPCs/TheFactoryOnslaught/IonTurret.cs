using NotEnoughFlareGuns.Projectiles.TheFactoryOnslaught;
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
            base.SetDefaults();
        }
    }
}
