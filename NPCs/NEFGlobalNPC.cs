using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Buffs;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.NPCs
{
    public class NEFGlobalNPC : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.HeatResistantPlastic>());
                nextSlot++;
            }
            if (type == NPCID.TravellingMerchant && WorldGen.shadowOrbSmashed)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.FlareGuns.PreHardmode.Revoflare>());
                nextSlot++;
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.SwampThing)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.RustedCaller>(), 80));
            if (npc.type == NPCID.WallofFlesh)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.FlareGuns.Hardmode.ClockworkFlareGun>(), 7));
            if (npc.type == NPCID.GolemHead)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.FlareGuns.Hardmode.SolarStorm>(), 7));
            if (npc.type == NPCID.SkeletronHead)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.FlareGuns.PreHardmode.SkillShot>(), 6));
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<Burning>()) && Main.rand.NextBool(3))
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Torch, 1, 1, 0, default, 2.5f);
        }
    }
}
