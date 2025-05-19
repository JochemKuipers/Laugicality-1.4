using System;
using System.IO;
using System.Collections.Generic;
using Laugicality.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace Laugicality.Utilities
{
    public partial class LaugicalityWorld : ModSystem
    {
        public static bool downedAnnihilator = false;
        public static bool downedSlybertron = false;
        public static bool downedSteamTrain = false;
        public static bool downedDuneSharkron = false;
        public static bool downedHypothema = false;
        public static bool downedRagnar = false;
        public static bool obsidiumHeart = false;
        public static bool downedRocks = false;
        public static bool downedEtheria = false; 
        public static bool downedTrueEtheria = false;
        public static bool downedDioritus = false;
        public static bool downedAndesia = false;
        public static bool downedAnDio = false;
        public static int obsidiumTiles = 0;
        public static int power = 0;
        public static bool obEnf = false; //obsidiumEnfused
        public static bool bysmal = false;
        public static int obsidiumPosition = 0;

        public static int sizeMult = (int)(Math.Round(Main.maxTilesX / 4200f)); //Small = 2; Medium = ~3; Large = 4;

        public static int zawarudo = 0;
        public static int dungeonSide = 1;

        private void ResetData()
        {
            sizeMult = (int)(Math.Floor(Main.maxTilesX / 4200f));
            power = 0;
            downedAnnihilator = false;
            downedSlybertron = false;
            downedSteamTrain = false;
            downedDuneSharkron = false;
            downedHypothema = false;
            downedRagnar = false;
            downedRocks = false;
            downedEtheria = false;
            downedTrueEtheria = false;
            downedDioritus = false;
            downedAndesia = false;
            downedAnDio = false;
            zawarudo = 0;
            obEnf = false;
            obsidiumHeart = false;
            bysmal = false;
            obsidiumPosition = 0;
        }
        public override void PreWorldGen()
        {
            ResetData();
        }

        public override void OnWorldLoad()
        {
            ResetData();
        }

        public override void OnWorldUnload()
        {
            ResetData();
        }

        public override void PostUpdateWorld()
        {
            zawarudo = Laugicality.zaWarudo;

            if (downedEtheria)
            {
                Main.dayTime = false;
                Main.time = 16200.0;
            }

            Ameldera = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            List<string> downed = new List<string>();
            if (downedAnnihilator) downed.Add("annihilator");
            if (downedSlybertron) downed.Add("slybertron");
            if (downedSteamTrain) downed.Add("steamtrain");
            if (downedDuneSharkron) downed.Add("dunesharkron");
            if (downedHypothema) downed.Add("hypothema");
            if (downedRagnar) downed.Add("ragnar");
            if (downedRocks) downed.Add("rocks");
            if (downedTrueEtheria) downed.Add("trueetheria");
            if (downedDioritus) downed.Add("dioritus");
            if (downedAndesia) downed.Add("andesia");
            if (downedAnDio) downed.Add("andio");

            tag["downed"] = downed;
            tag["etherial"] = downedEtheria;
            tag["obsidium"] = obEnf;
            tag["obsidiumHeart"] = obsidiumHeart;
            tag["bysmal"] = bysmal;
            tag["power"] = power;
        }


        public override void LoadWorldData(TagCompound tag)
        {
            IList<string> downed = tag.GetList<string>("downed");
            downedAnnihilator = downed.Contains("annihilator");
            downedSlybertron = downed.Contains("slybertron");
            downedSteamTrain = downed.Contains("steamtrain");
            downedDuneSharkron = downed.Contains("dunesharkron");
            downedHypothema = downed.Contains("hypothema");
            downedRagnar = downed.Contains("ragnar");
            downedRocks = downed.Contains("rocks");
            downedTrueEtheria = downed.Contains("trueetheria");
            downedDioritus = downed.Contains("dioritus");
            downedAndesia = downed.Contains("andesia");
            downedAnDio = downed.Contains("andio");
            obEnf = tag.GetBool("obsidium");
            downedEtheria = tag.GetBool("etherial");
            obsidiumHeart = tag.GetBool("obsidiumHeart");
            bysmal = tag.GetBool("bysmal");
            power = tag.GetInt("power");
            DryTheObsidium();
        }

        /*public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedAnnihilator = flags[0];
                downedSlybertron = flags[1];
                downedSteamTrain = flags[2];
                downedDuneSharkron = flags[3];
                downedHypothema = flags[4];
                downedRagnar = flags[5];
                downedEtheria = flags[6];
                downedTrueEtheria = flags[7];

                BitsByte flags2 = reader.ReadByte();
                downedRocks = flags2[0];
                downedDioritus = flags2[1];
                downedAndesia = flags2[2];
                downedAnDio = flags2[3];
                obEnf = flags2[4];
                obsidiumHeart = flags2[5];
                bysmal = flags2[6];
            }
            else
            {
                ErrorLogger.Log("Enigma: Unknown loadVersion: " + loadVersion);
            }
        }*/

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedAnnihilator;
            flags[1] = downedSlybertron;
            flags[2] = downedSteamTrain;
            flags[3] = downedDuneSharkron;
            flags[4] = downedHypothema;
            flags[5] = downedRagnar;
            flags[6] = downedEtheria;
            flags[7] = downedTrueEtheria;

            BitsByte flags2 = new BitsByte();
            flags2[0] = downedRocks;
            flags2[1] = downedDioritus;
            flags2[2] = downedAndesia;
            flags2[3] = downedAnDio;
            flags2[4] = obEnf;
            flags2[5] = obsidiumHeart;
            flags2[6] = bysmal;
            writer.Write(flags);
            writer.Write(flags2);

            //int zTime = Laugicality.zaWarudo;
            //writer.Write(zTime);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedAnnihilator = flags[0];
            downedSlybertron = flags[1];
            downedSteamTrain = flags[2];
            downedDuneSharkron = flags[3];
            downedHypothema = flags[4];
            downedRagnar = flags[5];
            downedEtheria = flags[6];
            downedTrueEtheria = flags[7];
            
            BitsByte flags2 = reader.ReadByte();
            downedRocks = flags2[0];
            downedDioritus = flags2[1];
            downedAndesia = flags2[2];
            downedAnDio = flags2[3];
            obEnf = flags2[4];
            obsidiumHeart = flags2[5];
            bysmal = flags2[6];
            //int zTime = reader.ReadInt32();
            //zaWarudo = (int)zTime;
        }

        public override void ResetNearbyTileEffects()
        {
            obsidiumTiles = 0;
        }

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            obsidiumTiles = tileCounts[56] + tileCounts[ModContent.TileType<ObsidiumCore>()] + tileCounts[ModContent.TileType<ObsidiumRock>()] +  tileCounts[ModContent.TileType<Lycoris>()] + tileCounts[ModContent.TileType<Radiata>()];
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            int xO = Main.maxTilesX / 2;
            int yO = (int)(Main.maxTilesY * .7f);
            tasks.Insert(genIndex + 1, new PassLegacy("Generating Obsidian Cavern", (progress, _) =>
            {
                progress.Message = "Obsidification";
                GenerateObsidium(xO, yO);
            }));

            int genIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            tasks.Insert(genIndex2 + 2, new PassLegacy("Obsidium Features", (progress, _) =>
            {
                progress.Message = "Obsidium Core";
                GenerateObsidiumStructures(xO, yO);
            }));
        }
        
        private void PlaceTile(int x, int y, int tileType)
        {
            WorldGen.KillTile(x, y);
            WorldGen.KillWall(x, y);
            Main.tile[x, y].LiquidAmount = 0;
            WorldGen.PlaceTile(x, y, tileType, true, true);
        }

        private void PlaceTile(int x, int y, int tileType, int wallType)
        {
            WorldGen.KillTile(x, y);
            WorldGen.KillWall(x, y);
            Main.tile[x, y].LiquidAmount = 0;
            WorldGen.PlaceTile(x, y, tileType, true, true);
            WorldGen.PlaceWall(x, y, wallType, true);
        }

        private static bool TileCheckSafe(int i, int j)
        {
            if (i > 0 && i < Main.maxTilesX - 1 && j > 0 && j < Main.maxTilesY - 1)
            {
                if (TileID.Sets.BasicChest[Main.tile[i, j].TileType])
                    return false;
                return true;
            }
            return false;
        }

        private float Distance(int x1, int y1, int x2, int y2)
        {
            return (float)(Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
        }
        
        private static void ClearSpaceForChest(int x, int y, ushort floorType)
        {
            WorldGen.KillTile(x, y);
            WorldGen.KillTile(x, y - 1);
            WorldGen.KillTile(x + 1, y - 1);
            WorldGen.KillTile(x + 1, y);
            WorldGen.PlaceTile(x + 1, y + 1, floorType, true, true);
            WorldGen.PlaceTile(x, y + 1, floorType, true, true);
            Main.tile[x, y].LiquidAmount = 0;
            Main.tile[x + 1, y].LiquidAmount = 0;
            Main.tile[x, y + 1].LiquidAmount = 0;
            Main.tile[x + 1, y + 1].LiquidAmount = 0;
        }

        private static void FillChest(int chestIndex, int[] itemsToPlaceInChests, int[] itemCounts)
        {
            if (chestIndex < Main.chest.GetLength(0) && chestIndex >= 0)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == 0 && inventoryIndex < itemsToPlaceInChests.GetLength(0))
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChests[inventoryIndex]);
                            chest.item[inventoryIndex].stack = itemCounts[inventoryIndex];
                        }
                    }
                }
            }
        }

        public static int GetCurseCount()
        {
            int count = 0;
            int numBosses = CountDownedBosses();
            if (numBosses >= 5)
                count++;
            if (numBosses >= 10)
                count++;
            if (numBosses >= 15)
                count++;
            if (numBosses >= 20)
                count++;
            return count;
        }

        public static int CountDownedBosses()
        {
            int count = 0;
            if (NPC.downedSlimeKing) count++;
            if (NPC.downedBoss1) count++;
            if (downedDuneSharkron) count++;
            if (NPC.downedBoss2) count++;
            if (downedHypothema) count++;
            if (NPC.downedQueenBee) count++;
            if (downedRagnar) count++;
            if (NPC.downedBoss3) count++;
            if (downedAnDio) count++;
            if (Main.hardMode) count++;
            if (NPC.downedMechBoss2) count++;
            if (NPC.downedMechBoss1) count++;
            if (NPC.downedMechBoss3) count++;
            if (downedAnnihilator) count++;
            if (downedSlybertron) count++;
            if (downedSteamTrain) count++;
            if (NPC.downedPlantBoss) count++;
            if (NPC.downedGolemBoss) count++;
            if (NPC.downedFishron) count++;
            if (downedEtheria || downedTrueEtheria) count++;
            if (NPC.downedMoonlord) count++;
            return count;
        }

        public static bool Ameldera { get; private set; }
    }
}