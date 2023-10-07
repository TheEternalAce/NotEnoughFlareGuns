using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Materials
{
    public class RustedCaller : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 30;
            Item.rare = ItemRarityID.Yellow;

            Item.maxStack = 9999;
            Item.value = Item.buyPrice(gold: 1);
        }
    }
}
