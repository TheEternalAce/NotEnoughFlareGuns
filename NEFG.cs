using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using NotEnoughFlareGuns.Projectiles.Ranged.Flares;
using StructureHelper;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NotEnoughFlareGuns
{
    public partial class NEFG : Mod
    {
        public static int ConvertibleFlare { get; private set; }

        public static NEFG Instance { get; private set; }

        public const string ElementModName = "BattleNetworkElements";
        public static bool ElementModEnabled => ModLoader.TryGetMod(ElementModName, out Mod _);

        public static readonly SoundStyle IntruderAlert = new("NotEnoughFlareGuns/Sounds/TheFactoryOnslaught/IntruderAlert", SoundType.Ambient);
        public static readonly SoundStyle AnomalyFound = new("NotEnoughFlareGuns/Sounds/TheFactoryOnslaught/AnomalyFound", SoundType.Ambient);
        public static readonly SoundStyle PriorityUpdate = new("NotEnoughFlareGuns/Sounds/TheFactoryOnslaught/PriorityUpdate", SoundType.Ambient);
        public static readonly SoundStyle GloveSnap = new("NotEnoughFlareGuns/Sounds/ItemSFX/GloveSnap", SoundType.Sound);
        public static readonly SoundStyle Lacerate = new("NotEnoughFlareGuns/Sounds/ItemSFX/Lacerate", SoundType.Sound);

        public static readonly string BlankTexture = "NotEnoughFlareGuns/Assets/Blank";
        public static readonly string Placeholder = "NotEnoughFlareGuns/Assets/Placeholder/PlaceholderSprite";
        public static readonly string Debuff = "NotEnoughFlareGuns/Assets/DebuffTemplate";
        public static readonly string Buff = "NotEnoughFlareGuns/Assets/BuffTemplate";

        public override void Load()
        {
            Instance = this;
            Point16 point = new(0, 0);
            Generator.GetDimensions("Subworlds/TheFactoryCoreRoom", ModContent.GetInstance<NEFG>(), ref point);
            ModLoader.TryGetMod("Wikithis", out Mod wikithis);
            if (wikithis != null && !Main.dedServ)
            {
                wikithis.Call("AddModURL", this, "terrariamods.wiki.gg$Infernal_Arson-al");

                // If you want to replace default icon for your mod, then call this. Icon should be 30x30, either way it will be cut.
                wikithis.Call("AddWikiTexture", this, ModContent.Request<Texture2D>("NotEnoughFlareGuns/icon_small"));
            }
        }

        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("ShardsOfAtheria", out Mod shards))
            {
                shards.Call("addColoredNecronomiconEntry", "Not Enough Flare Guns", "The Factory Onslaught",
                    Language.GetTextValue("Mods.NotEnoughFlareGuns.ItemTooltip.FactorySoulCrystal"), Color.Red, "FactorySoulCrystal");
            }
            ConvertibleFlare = ModContent.ProjectileType<ConvertibleFlare>();
            /*
            //if (ModLoader.TryGetMod("BossChecklist", out Mod checklist))
            //{
            //    List<int> enemyList = new List<int>()
            //    {
            //        ModContent.NPCType<SoulstoneCore>(),
            //        ModContent.NPCType<PlasmaTurret>(),
            //        ModContent.NPCType<IonTurret>(),
            //    };

            //    List<int> collection = new List<int>()
            //    {
            //        ModContent.ItemType<SoulstonePlating>()
            //    };

            //    checklist.Call("AddEvent", this, "Factory Onslaught", enemyList, 11.5f,
            //        () => DownedSystem.downedFactoryEvent, () => true, collection, ModContent.ItemType<SoulstoneKeys>(),
            //        $"Create and use [i:{ModContent.ItemType<SoulstoneKeys>()}] and interact with the " +
            //        $"[i:{ModContent.ItemType<OnslaughtTerminalItem>()}] once you enter.");
            //}
            */

            if (ModLoader.TryGetMod("TerraTyping", out Mod terratyping))
            {
                string basePath = "CrossMod/TerraTypes/{0}.csv";
                Dictionary<string, object> addWeapon = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "weapon" },
                    { "callingmod", Instance },
                    { "filename", basePath.FormatWith("Weapons") }
                };
                terratyping.Call(addWeapon);

                Dictionary<string, object> addProjectile = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "projectile" },
                    { "callingmod", Instance },
                    { "filename", basePath.FormatWith("Projectiles") }
                };
                terratyping.Call(addProjectile);

                Dictionary<string, object> addAmmo = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "ammo" },
                    { "callingmod", Instance },
                    { "filename", basePath.FormatWith("Ammo") }
                };
                terratyping.Call(addAmmo);

                Dictionary<string, object> addNPC = new()
                {
                    { "call", "AddTypes" },
                    { "typestoadd", "npc" },
                    { "callingmod", Instance },
                    { "filename", basePath.FormatWith("NPCs") }
                };
                terratyping.Call(addNPC);
            }
        }

        public static void TryElementCall(params object[] args)
        {
            if (ElementModEnabled)
            {
                var em = ModLoader.GetMod(ElementModName);
                em.Call(args);
            }
        }

        public static Dictionary<string, List<string>> GetContentArrayFile(string name)
        {
            using (var stream = Instance.GetFileStream($"Content/{name}.json", newFileStream: true))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(streamReader.ReadToEnd());
                }
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
