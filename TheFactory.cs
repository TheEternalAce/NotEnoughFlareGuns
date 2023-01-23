﻿using NotEnoughFlareGuns.Items.FlareGuns.PreHardmode;
using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Tiles;
using NotEnoughFlareGuns.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns
{
    public class TheFactory : ModSystem
    {
        public override void PostWorldGen()
        {
            for (int k = 0; k < Main.maxChests; k++)
            {
                Chest c = Main.chest[k];
                if (c != null)
                {
                    if (Main.tile[c.x, c.y].TileType == TileID.Containers)
                    {
                        int style = ChestTypes.GetChestStyle(c);
                        if (style == ChestTypes.LockedGold)
                        {
                            if (Main.rand.NextBool(6))
                            {
                                c.Insert(ModContent.ItemType<AzulFlame>(), 1);
                            }
                        }
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe.Create(ItemID.FlareGun)
                .AddIngredient(ModContent.ItemType<Sparkler>())
                .AddIngredient(ModContent.ItemType<HeatResistantPlastic>(), 12)
                .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
