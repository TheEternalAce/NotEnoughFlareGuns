﻿using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Systems
{
    public class FactoryRecipes : ModSystem
    {
        // A place to store the recipe group so we can easily use it later
        public static RecipeGroup EvilMaterial;
        public static RecipeGroup EvilGun;
        public static RecipeGroup Copper;
        public static RecipeGroup Silver;
        public static RecipeGroup Gold;
        public static RecipeGroup EvilBar;
        public static RecipeGroup Cobalt;
        public static RecipeGroup Mythril;
        public static RecipeGroup Adamantite;

        public override void Unload()
        {
            EvilMaterial = null;
            EvilGun = null;
            Copper = null;
            Silver = null;
            Gold = null;
            EvilBar = null;
            Cobalt = null;
            Mythril = null;
            Adamantite = null;
        }

        public override void AddRecipeGroups()
        {
            EvilMaterial = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Evil Material",
                   ItemID.ShadowScale, ItemID.TissueSample);
            RecipeGroup.RegisterGroup("Shards:EvilMaterials", EvilMaterial);

            Copper = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CopperBar)}",
                   ItemID.CopperBar, ItemID.TinBar);
            RecipeGroup.RegisterGroup("Shards:CopperBars", Copper);

            Silver = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.SilverBar)}",
                   ItemID.SilverBar, ItemID.TungstenBar);
            RecipeGroup.RegisterGroup("Shards:SilverBars", Silver);

            Gold = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.GoldBar)}",
                   ItemID.GoldBar, ItemID.PlatinumBar);
            RecipeGroup.RegisterGroup("Shards:GoldBars", Gold);

            EvilBar = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Evil Bar",
                   ItemID.DemoniteBar, ItemID.CrimtaneBar);
            RecipeGroup.RegisterGroup("Shards:EvilBars", EvilBar);

            Cobalt = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 1 Bar",
                   ItemID.CobaltBar, ItemID.PalladiumBar);
            RecipeGroup.RegisterGroup("Shards:Tier1Bars", Cobalt);

            Mythril = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 2 Bar",
                   ItemID.MythrilBar, ItemID.OrichalcumBar);
            RecipeGroup.RegisterGroup("Shards:Tier2Bars", Mythril);

            Adamantite = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Tier 3 Bar",
                   ItemID.AdamantiteBar, ItemID.TitaniumBar);
            RecipeGroup.RegisterGroup("Shards:Tier3Bars", Adamantite);
        }
    }
}