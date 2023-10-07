//using NotEnoughFlareGuns.Systems;
//using NotEnoughFlareGuns.Utilities;
//using StructureHelper;
//using SubworldLibrary;
//using System.Collections.Generic;
//using Terraria;
//using Terraria.DataStructures;
//using Terraria.IO;
//using Terraria.ModLoader;
//using Terraria.WorldBuilding;

//namespace NotEnoughFlareGuns.Subworlds
//{
//    public class TheFactory : Subworld
//    {
//        public override int Width => 1000;

//        public override int Height => 500;

//        public override List<GenPass> Tasks => new List<GenPass>()
//        {
//            new FactoryPass("Factory", 1)
//        };

//        // Sets the time to the middle of the day whenever the subworld loads
//        public override void OnLoad()
//        {
//            Main.dayTime = true;
//            Main.time = 27000;
//        }

//        public override void OnEnter()
//        {
//            SubworldSystem.hideUnderworld = true;
//            SubworldSystem.noReturn = true;
//        }

//        public override void CopyMainWorldData()
//        {
//            CopyData();
//        }

//        public override void OnExit()
//        {
//            Main.LocalPlayer.InfernalPlayer().factoryTimer = 0;
//        }

//        public void CopyData()
//        {
//            SubworldSystem.CopyWorldData(nameof(DownedSystem.downedFactoryEvent), DownedSystem.downedFactoryEvent);
//            SubworldSystem.CopyWorldData(nameof(NPC.downedGolemBoss), NPC.downedGolemBoss);
//        }
//    }

//    public class FactoryPass : GenPass
//    {
//        public FactoryPass(string name, float loadWeight) : base(name, loadWeight)
//        {

//        }

//        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
//        {
//            progress.Message = "Building The Factory";
//            int x = (1000 - 120) / 2;
//            int y = (500 - 64) / 2;
//            y -= 31;
//            Point16 point = new Point16(x, y);
//            Generator.GenerateStructure("Subworlds/TheFactoryCoreRoom", point, ModContent.GetInstance<NEFG>());
//        }
//    }
//}
