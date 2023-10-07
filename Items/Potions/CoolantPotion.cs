using BattleNetworkElements.Utilities;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Potions
{
    [JITWhenModsEnabled(NEFG.ElementModName)]
    public class CoolantPotion : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return NEFG.ElementModEnabled;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPotion(ModContent.BuffType<Coolant>(), FactoryHelper.Minutes(4));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.Fireblossom)
                .AddIngredient(ItemID.FlarefinKoi)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }

    [JITWhenModsEnabled(NEFG.ElementModName)]
    public class Coolant : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            if (NEFG.ElementModEnabled)
                player.FireMultiplier() -= 0.2f;
        }
    }
}
