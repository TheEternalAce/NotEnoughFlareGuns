using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Buffs.AnyDebuff;
using NotEnoughFlareGuns.Items.FlareGuns.Hardmode;
using NotEnoughFlareGuns.Items.FlareGuns.PreHardmode;
using NotEnoughFlareGuns.Items.Materials;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Globals
{
    public class NEFGlobalNPC : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant)
            {
                for (int i = 0; i < ItemLoader.ItemCount; i++)
                {
                    if (NEFGlobalItem.FlareGuns.Contains(i))
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Flare);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.BlueFlare);
                        nextSlot++;
                    }
                }
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<HeatResistantPlastic>());
                nextSlot++;
            }
            if (type == NPCID.TravellingMerchant && WorldGen.shadowOrbSmashed)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Revoflare>());
                nextSlot++;
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.SwampThing)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RustedCaller>(), 20));
            }
            if (npc.type == NPCID.CreatureFromTheDeep)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RustedCaller>(), 20));
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClockworkFlareGun>(), 7));
            }
            if (npc.type == NPCID.GolemHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SolarStorm>(), 7));
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SkillShot>(), 6));
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<Burning>()) && Main.rand.NextBool(3))
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Torch, 1, 1, 0, default, 2.5f);
        }
    }
}
