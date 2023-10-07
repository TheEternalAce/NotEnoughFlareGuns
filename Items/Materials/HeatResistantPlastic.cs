using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Materials
{
    public class HeatResistantPlastic : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.White;

            Item.maxStack = 9999;
            Item.value = Item.buyPrice(copper: 50);
        }
    }
}
