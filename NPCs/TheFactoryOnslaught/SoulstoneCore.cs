using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.NPCs.TheFactoryOnslaught
{
    public class SoulstoneCore : ModNPC
    {
        int closedCoreTimer;
        int closedCoreTimerMax = 120;
        int openCoreTimer;
        int openCoreTimerMax = 120;
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
        }

        public override void SetDefaults()
        {
            NPC.width = NPC.height = 300;
            NPC.lifeMax = 47000;
            NPC.boss = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            Music = MusicID.Monsoon;

            DrawOffsetY = 42;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void AI()
        {
            if (++closedCoreTimer >= closedCoreTimerMax)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(NPC.position.ToString()), Color.White);
                closedCoreTimer = 0;
            }
        }
    }
}
