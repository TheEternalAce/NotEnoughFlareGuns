using NotEnoughFlareGuns.Items.Accessories;
using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Items.Placeable;
using NotEnoughFlareGuns.Items.Weapons.Ranged.FlareGuns;
using NotEnoughFlareGuns.Items.Weapons.Ranged.Launchers;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Globals
{
    public class NEFGlobalNPC : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add<HeatResistantPlastic>();
            }
            if (shop.NpcType == NPCID.Steampunker)
            {
                shop.Add<PressurizedNozzle>();
                shop.Add<FactoryWorkstationItem>();
                shop.Add<SoulstonePlating>();
            }
            if (shop.NpcType == NPCID.TravellingMerchant)
            {
                shop.Add<Revolnade>(Condition.SmashedShadowOrb);
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
        }
    }
}
