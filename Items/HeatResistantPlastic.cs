using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items
{
    public class HeatResistantPlastic : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Plastic that is less likely to melt at moderately high temperatures'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.White;

            Item.maxStack = 999;
            Item.value = Item.buyPrice(copper: 50);
        }
    }
}
