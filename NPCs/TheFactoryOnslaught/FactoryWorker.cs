using Microsoft.Xna.Framework;
using MMZeroElements;
using NotEnoughFlareGuns.Projectiles.TheFactoryOnslaught;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.NPCs.TheFactoryOnslaught
{
    public class FactoryWorker : ModNPC
    {
        public override void SetStaticDefaults()
        {
            //Main.npcFrameCount[Type] = 2;

            // Specify the debuffs it is immune to
            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.OnFire,
                    BuffID.OnFire3,
                    BuffID.Frostburn,
                    BuffID.Frostburn2,
                    BuffID.CursedInferno
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            NPCElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.lifeMax = 250;
            NPC.defense = 20;
            NPC.knockBackResist = 0.8f;
            NPC.aiStyle = NPCAIStyleID.Fighter;
            NPC.HitSound = SoundID.PlayerHit;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.SetElementMultipliersByElement(Element.Fire);
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            if (++NPC.ai[0] == 90f && !player.dead)
            {
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
                {
                    int type = ModContent.ProjectileType<PlasmaLaser>();
                    if (NPC.downedGolemBoss)
                    {
                        type = ModContent.ProjectileType<IonLaser>();
                    }
                    Vector2 vectorToPlayer = Vector2.Normalize(player.Center - NPC.Center);
                    Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center, vectorToPlayer * 16,
                        type, 16, 0, Main.myPlayer);
                }
            }
            else if (NPC.ai[0] >= 200 + Main.rand.Next(200))
            {
                NPC.ai[0] = 0f;
            }
            base.AI();
        }
    }
}
