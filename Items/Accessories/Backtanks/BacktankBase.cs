using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items.Accessories.Backtanks
{
    public abstract class BacktankBase : ModItem
    {
        protected int backtankTier = BacktankTiers.None;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.InfernalPlayer().metalBacktankEquipped = backtankTier;
        }
    }

    public class BacktankTiers
    {
        public const int None = 0;
        public const int Copper = 1;
        public const int Cobalt = 2;
        public const int Soulstone = 3;
        public const int Luminite = 4;
    }
}
