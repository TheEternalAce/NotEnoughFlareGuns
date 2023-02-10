using NotEnoughFlareGuns.Items.Accessories;
using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns.Hardmode;
using NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns.PreHardmode;
using NotEnoughFlareGuns.Items.Weapons.Ranged.Launchers.PreHardmode;
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
            if (type == NPCID.Steampunker)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<PressurizedNozzle>());
                nextSlot++;
            }
            if (type == NPCID.TravellingMerchant && WorldGen.shadowOrbSmashed)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Revolnade>());
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
    }
}
