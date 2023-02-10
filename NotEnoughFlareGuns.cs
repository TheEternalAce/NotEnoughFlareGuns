using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NotEnoughFlareGuns.Items.BossSummons;
using NotEnoughFlareGuns.Items.Materials;
using NotEnoughFlareGuns.Items.Misc;
using NotEnoughFlareGuns.Items.Placable;
using NotEnoughFlareGuns.NPCs.TheFactoryOnslaught;
using NotEnoughFlareGuns.Systems;
using StructureHelper;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns
{
    public class NotEnoughFlareGuns : Mod
    {
        public static int ConvertibleFlare = 0;

        public override void Load()
        {
            Point16 point = new Point16(0, 0);
            Generator.GetDimensions("Subworlds/TheFactoryCoreRoom", ModContent.GetInstance<NotEnoughFlareGuns>(), ref point);
            ModLoader.TryGetMod("Wikithis", out Mod wikithis);
            if (wikithis != null && !Main.dedServ)
            {
                wikithis.Call("AddModURL", this, "terrariamods.wiki.gg$Infernal_Arson-al");

                // You can also use call ID for some calls!
                //wikithis.Call(0, this, "https://terrariamods.wiki.gg/wiki/Infernal_Arson-al/");

                // Alternatively, you can use this instead, if your wiki is on terrariamods.fandom.com
                //wikithis.Call(0, this, "https://terrariamods.fandom.com/wiki/Example_Mod/{}");
                //wikithis.Call("AddModURL", this, "https://terrariamods.fandom.com/wiki/Example_Mod/{}");

                // If there wiki on other languages (such as russian, spanish, chinese, etch), then you can also call that:
                //wikithis.Call(0, this, "https://examplemod.wiki.gg/zh/wiki/{}", GameCulture.CultureName.Chinese)

                // If you want to replace default icon for your mod, then call this. Icon should be 30x30, either way it will be cut.
                wikithis.Call("AddWikiTexture", this, ModContent.Request<Texture2D>("NotEnoughFlareGuns/icon_small"));
                //wikithis.Call(3, this, ModContent.Request<Texture2D>(pathToIcon));

                // If you want to add wiki entries to your custom element (for example, mod enchantments, buffs, etch, literally anything).
                // Example of adding wiki pages for projectiles:
                //wikithis.Call("CustomWiki",
                //    this, // instance of your mod
                //    "ProjectileWiki", // name of wiki
                //    new Func<object, IConvertible>(x => (x as Projectile).type), // type of your entry (can be anything)
                //    new Action<Func<object, bool>, Action<object, IConvertible, string>, Func<string, Mod, string>>((hasEntryFunc, addEntryFunc, defaultSearchStr) =>
                //    {
                //        foreach (Projectile proj in ContentSamples.ProjectilesByType.Values) // iterate through each instance
                //        {
                //                if (hasEntryFunc(proj.type)) // check if entry exists, and if it does, then skip
                //                    continue;
                //                
                //                // get projectile name
                //                string name = proj.type < ProjectileID.Count
                //                    ? Language.GetTextValue($"ProjectileName.{ProjectileID.Search.GetName(proj.type)}")
                //                    : Language.GetTextValue($"Mods.{proj.ModProjectile.Mod.Name}.ProjectileName.{proj.ModProjectile.Name}");
                //                
                //                addEntryFunc(proj, proj.type, defaultSearchStr(name, proj.ModProjectile?.Mod)); // add entry
                //        }
                //    }
                //
                // Whenever you want to open your custom wiki page, then you should use this mod call:
                //wikithis.Call("OpenCustomWiki",
                //    this, // instance of your mod
                //    "ProjectileWiki",
                //    (int)ProjectileID.AdamantiteChainsaw, // id of projectile. should match type of your entry (Projectile.type is int)
                //    true // forces check for keybind, you would most likely want to keep this as it is
                //    );
            }
        }

        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("ShardsOfAtheria", out Mod shards))
            {
                shards.Call("addColoredNecronomiconEntry", "Not Enough Flare Guns", "The Factory Onslaught",
                    Language.GetTextValue("Mods.NotEnoughFlareGuns.ItemTooltip.FactorySoulCrystal"), Color.Red, ModContent.ItemType<FactorySoulCrystal>());
            }
            if (ModLoader.TryGetMod("BossChecklist", out Mod checklist))
            {
                List<int> enemyList = new List<int>()
                {
                    ModContent.NPCType<SoulstoneCore>(),
                    ModContent.NPCType<PlasmaTurret>(),
                    ModContent.NPCType<PhotonTurret>(),
                    ModContent.NPCType<IonTurret>(),
                };

                List<int> collection = new List<int>()
                {
                    ModContent.ItemType<SoulstonePlating>()
                };

                checklist.Call("AddEvent", this, "Factory Onslaught", enemyList, 11.5f,
                    () => DownedSystem.downedFactoryEvent, () => true, collection, ModContent.ItemType<SoulstoneKeys>(),
                    $"Create and use [i:{ModContent.ItemType<SoulstoneKeys>()}] and interact with the " +
                    $"[i:{ModContent.ItemType<OnslaughtTerminalItem>()}] once you enter.");
            }
        }
    }

    public class LocalizationPath
    {
        public static readonly string ItemName = "Mods.NotEnoughFlareGuns.ItemName.";
        public static readonly string ItemDesc = "Mods.NotEnoughFlareGuns.ItemTooltip.";
        public static readonly string ProjectileName = "Mods.NotEnoughFlareGuns.ItemTooltip.";
    }
}
