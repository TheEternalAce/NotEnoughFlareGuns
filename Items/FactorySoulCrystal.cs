using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns.Items
{
    public class FactorySoulCrystal : ModItem
    {
        public int absorbSoulTimer = 300;

        public override bool IsLoadingEnabled(Mod mod)
        {
            ModLoader.TryGetMod("ShardsOfAtheria", out Mod shards);
            return shards != null;
        }

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 24;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line;
            ModLoader.TryGetMod("ShardsOfAtheria", out Mod shards);
            if (!(bool)shards.Call("checkSoulConfig"))
                line = new TooltipLine(Mod, "SoulCrystal", "Hold left click for 5 seconds to absorb the soul inside, this grants you this boss's powers")
                {
                    OverrideColor = Color.Purple
                };
            else line = new TooltipLine(Mod, "SoulCrystal", "Use to absorb the soul inside, this grants you this boss's powers")
            {
                OverrideColor = Color.Purple
            };
            tooltips.Add(line);
        }

        public override void UpdateInventory(Player player)
        {
            if (!Main.mouseLeft)
                absorbSoulTimer = 300;
        }

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            Mod shards = ModLoader.GetMod("ShardsOfAtheria");
            absorbSoulTimer--;
            if (Main.rand.NextBool(3))
                Dust.NewDustDirect(player.Center, 4, 4, DustID.SandstormInABottle, .2f, .2f, 0, Scale: 1.2f);
            Lighting.AddLight(player.Center, TorchID.Yellow);
            if (absorbSoulTimer == 299 && !(bool)shards.Call("checkSoulConfig"))
                SoundEngine.PlaySound(SoundID.Item46);
            if (absorbSoulTimer == 240)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 180)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 120)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 60)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 0 || (bool)shards.Call("checkSoulConfig"))
            {
                for (int i = 0; i < 20; i++)
                {
                    if (Main.rand.NextBool(3))
                        Dust.NewDustDirect(player.Center, 4, 4, DustID.SandstormInABottle, .2f, .2f, 0, default, 1.2f);
                    Lighting.AddLight(player.Center, TorchID.Yellow);
                }
                if (!(bool)shards.Call("checkHasSoulCrystal", player, Type))
                {
                    shards.Call("addSoulCrystal", player, Type);
                    Item.TurnToAir();
                    SoundEngine.PlaySound(SoundID.Item4);
                }
            }
            return true;
        }
    }
}
