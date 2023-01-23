using Microsoft.Xna.Framework;
using NotEnoughFlareGuns.Items;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns
{
    public class NotEnoughFlareGuns : Mod
    {
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("ShardsOfAtheria", out Mod shards))
            {
                shards.Call("addColoredNecronomiconEntry", "Not Enough Flare Guns", "The Factory",
                    Language.GetTextValue("Mods.NotEnoughFlareGuns.ItemTooltip.FactorySoulCrystal"), Color.Red, ModContent.ItemType<FactorySoulCrystal>());
            }
        }
    }
}
