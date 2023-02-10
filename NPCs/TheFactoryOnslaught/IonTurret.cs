using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using NotEnoughFlareGuns.Projectiles.TheFactoryOnslaught;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.NPCs.TheFactoryOnslaught
{
    public class IonTurret : ModNPC
    {
        int fireTimer;
        int fireTimerMax = 120;
        int fireTimerCooldown = 240;

        public override void SetStaticDefaults()
        {
            // Add this in for bosses that have a summon item, requires corresponding code in the item (See MinionBossSummonItem.cs)
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            // Specify the debuffs it is immune to
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,

                    BuffID.Confused // Most NPCs have this
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            NPCElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 44;
            NPC.lifeMax = 700;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
        }

        public override bool CheckActive()
        {
            return !NPC.AnyNPCs(ModContent.NPCType<SoulstoneCore>());
        }

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            fireTimer++;
            if (fireTimer < fireTimerMax)
            {
                if (fireTimer % 10 == 0)
                {
                    Vector2 vector = player.Center - NPC.Center;
                    vector.Normalize();
                    Projectile.NewProjectileDirect(NPC.GetSource_FromThis(), NPC.Center, vector * 32f,
                        ModContent.ProjectileType<IonLaser>(), 42, 3, Main.myPlayer);
                    SoundEngine.PlaySound(SoundID.Item33);
                }
            }
            else if (fireTimer >= fireTimerMax + fireTimerCooldown)
            {
                fireTimer = 0;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Player player = Main.player[NPC.target];
            Asset<Texture2D> gun = ModContent.Request<Texture2D>(Texture + "_Gun");
            Rectangle rect = new(0, 0, 78, 38);
            SpriteEffects spriteEffects = SpriteEffects.None;
            Vector2 offset = new(-24, -12);
            Vector2 origin = new(39, 19);
            float rotation = (player.Center - NPC.Center).ToRotation();
            if (NPC.direction == -1)
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }
            Vector2 drawPos = NPC.position - offset - screenPos;

            spriteBatch.Draw(gun.Value, drawPos, rect, drawColor, rotation, origin, NPC.scale, spriteEffects, 0f);

            base.PostDraw(spriteBatch, screenPos, drawColor);
        }
    }
}
