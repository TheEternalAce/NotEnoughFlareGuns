using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Materials
{
    public class RustedCaller : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'It's so worn and rusted it's useless..?'");
            SacrificeTotal = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 30;
            Item.rare = ItemRarityID.Yellow;

            Item.maxStack = 1;
            Item.value = Item.buyPrice(gold: 1);
        }
    }
}
