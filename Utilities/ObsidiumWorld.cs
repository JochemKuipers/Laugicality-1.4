using Laugicality.Content.Items.Consumables.Potions;
using Laugicality.Content.Items.Equipables;
using Laugicality.Utilities.Structures;
using Laugicality.Content.Tiles;
using System;
using Laugicality.Content.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.Utilities
{
    partial class LaugicalityWorld
    {
        private void GenerateObsidium(int xO, int yO)
        {
            CreateObsidiumRock(xO, yO);

            CreateObsidiumCaverns(xO, yO);

            GenerateObsidiumFeatures(xO, yO);
        }

        private void CreateObsidiumRock(int xO, int yO)
        {
            for (int i = (int)(-225 * sizeMult); i <= (int)(225 * sizeMult); i++)
            {
                for (int j = (int)(-275 * sizeMult); j <= (int)(275 * sizeMult); j++)
                {
                    CreateObsidiumTileCheck(xO, yO, i, j);
                }
            }
        }

        private void CreateObsidiumTileCheck(int xO, int yO, int i, int j)
        {
            if (TileCheckSafe(xO + i, yO + j))
            {
                if (Main.tile[xO + i, yO + j].WallType != WallID.LihzahrdBrick && Main.tile[xO + i, yO + j].TileType != TileID.LihzahrdBrick)
                {
                    if (j < -(int)(150 * sizeMult))
                    {
                        GenerateCavernTop(xO, yO, i, j);
                    }
                    else if (j < 0)
                    {
                        GenerateCavernTopMid(xO, yO, i, j);
                    }
                    else if (j < (int)(100 * sizeMult))
                    {
                        GenerateCavernMid(xO, yO, i, j);
                    }
                    else if (yO + j < Main.maxTilesY - 200)
                    {
                        GenerateCavernBottom(xO, yO, i, j);
                    }
                }
            }
        }

        private void GenerateCavernTop(int xO, int yO, int i, int j)
        {
            int sign = 1;
            if (i != 0)
                sign = (int)(Math.Abs(i) / i);
            if (Distance(xO + sign * 100 * sizeMult, yO - 150 * sizeMult, xO + i, yO + j) < 100 * sizeMult)
            {
                PlaceTile(xO + i, yO + j, ModContent.TileType<ObsidiumRock>(), Mod.Find<ModWall>("ObsidiumRockWall").Type);
            }
            else if (Distance(xO + sign * 100 * sizeMult, yO - (int)(150 * sizeMult), xO + i, yO + j) < 100 * sizeMult + 6)
            {
                if (Main.rand.Next(6) < 100 * sizeMult + 6 - Distance(xO + sign * 100 * sizeMult, yO - 150 * sizeMult, xO + i, yO + j))
                {
                    PlaceTile(xO + i, yO + j, ModContent.TileType<ObsidiumRock>());
                }
            }
        }

        private void GenerateCavernTopMid(int xO, int yO, int i, int j)
        {
            if (Distance(xO + i, yO + j, xO, yO) < (int)(150 * sizeMult - (int)(j * 2 / 3)))
            {
                PlaceTile(xO + i, yO + j, ModContent.TileType<ObsidiumRock>(), Mod.Find<ModWall>("ObsidiumRockWall").Type);
            }
            else if (Distance(xO + i, yO + j, xO, yO) < (int)(150 * sizeMult - (int)(j * 2 / 3)) + 6)
            {
                if (Main.rand.Next(6) < -Distance(xO + i, yO + j, xO, yO) + 6 + (int)(150 * sizeMult - (int)(j * 2 / 3)))
                {
                    PlaceTile(xO + i, yO + j, ModContent.TileType<ObsidiumRock>());
                }
            }
        }

        private void GenerateCavernMid(int xO, int yO, int i, int j)
        {
            if (Distance(xO + i, yO + j, xO, yO) < (int)(150 * sizeMult - .47 * j))
            {
                PlaceTile(xO + i, yO + j, ModContent.TileType<ObsidiumRock>(), Mod.Find<ModWall>("ObsidiumRockWall").Type);
            }
            else if (Distance(xO + i, yO + j, xO, yO) < (int)(150 * sizeMult - .47 * j) + 6)
            {
                if (Main.rand.Next(6) < -Distance(xO + i, yO + j, xO, yO) + 6 + (int)(150 * sizeMult - .47 * j))
                {
                    PlaceTile(xO + i, yO + j, ModContent.TileType<ObsidiumRock>());
                }
            }
        }

        private void GenerateCavernBottom(int xO, int yO, int i, int j)
        {
            int radius = (Main.maxTilesY - 200) - (yO + (int)(100 * sizeMult));
            if (i < 0 && i > -radius)
            {
                if (Distance(xO + i, yO + j, xO - (int)(25 * sizeMult) - radius, yO + (int)(100 * sizeMult)) > radius)
                {
                    PlaceTile(xO + i, yO + j, ModContent.TileType<ObsidiumRock>(), Mod.Find<ModWall>("ObsidiumRockWall").Type);
                }
                else if (Distance(xO + i, yO + j, xO - (int)(25 * sizeMult) - radius, yO + (int)(100 * sizeMult)) < radius + 6)
                {
                    if (Main.rand.Next(6) < Distance(xO + i, yO + j, xO - (int)(25 * sizeMult) - radius, yO + (int)(100 * sizeMult)) - 6 - radius)
                    {
                        PlaceTile(xO + i, yO + j, ModContent.TileType<ObsidiumRock>());
                    }
                }
            }
            else if (i >= 0 && i < radius + 1)
            {
                if (Distance(xO + i, yO + j, xO + (int)(25 * sizeMult) + radius, yO + (int)(100 * sizeMult)) > radius)
                {
                    PlaceTile(xO + i, yO + j, ModContent.TileType<ObsidiumRock>(), Mod.Find<ModWall>("ObsidiumRockWall").Type);
                }
                else if (Distance(xO + i, yO + j, xO + (int)(25 * sizeMult) + radius, yO + (int)(100 * sizeMult)) < radius + 6)
                {
                    if (Main.rand.Next(6) < Distance(xO + i, yO + j, xO + (int)(25 * sizeMult) + radius, yO + (int)(100 * sizeMult)) - 6 - radius)
                    {
                        PlaceTile(xO + i, yO + j, ModContent.TileType<ObsidiumRock>());
                    }
                }
            }
        }

        private void CreateObsidiumCaverns(int xO, int yO)
        {
            GenerateCave(xO, yO, 100 * sizeMult, (ushort)TileID.BubblegumBlock, 9 * sizeMult, 15 * sizeMult, 3 * sizeMult);
            GenerateCave(xO, yO, 50 * sizeMult, (ushort)TileID.BubblegumBlock, 16, 25, 140 * sizeMult);
            GenerateCave(xO, yO, 50 * sizeMult, (ushort)TileID.BubblegumBlock, 25, 30, 25 * sizeMult);

            for (int i = (int)(-250 * sizeMult); i <= (int)(250 * sizeMult); i++)
            {
                for (int j = (int)(-325 * sizeMult); j <= (int)(325 * sizeMult); j++)
                {
                    if (TileCheckSafe(xO + i, yO + j))
                    {
                        if (Main.tile[xO + i, yO + j].TileType == (ushort)249)
                        {
                            WorldGen.KillTile(xO + i, yO + j);
                        }
                    }
                }
            }
        }


        private void GenerateObsidiumFeatures(int xO, int yO)
        {
            GenerateFeature(xO, yO, 25, (ushort)ModContent.TileType<Radiata>(), 2, 6, 180 * sizeMult);
            GenerateFeature(xO, yO, 50, (ushort)ModContent.TileType<Lycoris>(), 3, 6, 140 * sizeMult);
            GenerateFeature(xO, yO, 75, (ushort)ModContent.TileType<Radiata>(), 3, 5, 180 * sizeMult);
            GenerateFeature(xO, yO, 75, (ushort)ModContent.TileType<Lycoris>(), 3, 5, 140 * sizeMult);
            GenerateFeature(xO, yO, 100, (ushort)ModContent.TileType<SootTile>(), 12, 18, 25 * sizeMult);
            GenerateFeature(xO, yO, 300, (ushort)ModContent.TileType<ObsidiumOreBlock>(), 6, 14, 8);
        }
        
        private void GenerateCave(int xO, int yO, int numSteps, ushort tileType, int minSize, int maxSize, int length)
        {
            for (int k = 0; k < numSteps * sizeMult; k++)
            {
                int x = xO + Main.rand.Next(-200 * sizeMult, 200 * sizeMult);
                int y = yO + Main.rand.Next(-250 * sizeMult, 250 * sizeMult);
                if (Main.tile[x, y].TileType == (ushort)ModContent.TileType<ObsidiumRock>() || (Main.tile[x, y].HasTile == false && Main.tile[x, y].WallType == Mod.Find<ModWall>("ObsidiumRockWall").Type) || Main.tile[x, y].TileType == (ushort)ModContent.TileType<Lycoris>() || Main.tile[x, y].TileType == (ushort)ModContent.TileType<Radiata>())
                    WorldGen.TileRunner(x, y, Main.rand.Next(minSize, maxSize), length, tileType, false, 0f, 0f, false, true);
            }
        }

        private void GenerateFeature(int xO, int yO, int numSteps, ushort tileType, int minSize, int maxSize, int length)
        {
            for (int k = 0; k < numSteps * sizeMult; k++)
            {
                int x = xO + Main.rand.Next(-225 * sizeMult, 225 * sizeMult);
                int y = yO + Main.rand.Next(-275 * sizeMult, 275 * sizeMult);
                if (Main.tile[x, y].TileType == (ushort)ModContent.TileType<ObsidiumRock>() || (Main.tile[x, y].HasTile == false && Main.tile[x, y].WallType == Mod.Find<ModWall>("ObsidiumRockWall").Type) || Main.tile[x, y].TileType == (ushort)ModContent.TileType<Lycoris>() || Main.tile[x, y].TileType == (ushort)ModContent.TileType<Radiata>())
                    WorldGen.TileRunner(x, y, Main.rand.Next(minSize, maxSize), length, tileType, false, 0f, 0f, false, true);
            }
        }

        private void GenerateObsidiumStructures(int xO, int yO)
        {
            DecorationStructures(xO, yO);

            LootStructures(xO, yO);

            HeartWorld.HeartGen(xO - 60, yO - 90);
        }

        private void DecorationStructures(int xO, int yO)
        {
            int s = Main.rand.Next(8);
            int structX = xO - 225 * sizeMult + Main.rand.Next(225 * sizeMult * 2);
            int structY = yO - 275 * sizeMult + Main.rand.Next(275 * sizeMult / 2);

            for (int q = 0; q < 12 * sizeMult; q++)
            {
                s = GenerateDecorationStructure(s, structX, structY);
                Point16 newStructurePosition = new Point16(structX, structY);
                newStructurePosition = AlterStructureLocation(xO, yO, structX, structY);
                structX = newStructurePosition.X;
                structY = newStructurePosition.Y;
            }
        }

        private int GenerateDecorationStructure(int s, int structX, int structY)
        {
            if (TileCheckSafe(structX, structY))
            {
                if (Main.tile[structX, structY].WallType == (ushort)Mod.Find<ModWall>("ObsidiumRockWall").Type)
                {
                    bool mirrored = false;
                    if (Main.rand.Next(2) == 0)
                        mirrored = true;

                    PickDecorationStructure(s, structX, structY, mirrored);

                    s++;
                    if (s >= 8)
                        s = 0;
                }
            }
            return s;
        }

        private Point16 AlterStructureLocation(int xO, int yO, int structX, int structY)
        {
            structX = Main.rand.Next(-250 * sizeMult, 250 * sizeMult) + xO;
            structY = Main.rand.Next(-275 * sizeMult, 275 * sizeMult) + yO;
            Point16 structurePosition = new Point16(structX, structY);
            return structurePosition;
        }

        private void PickDecorationStructure(int s, int structX, int structY, bool mirrored)
        {
            switch (s)
            {
                case 0:
                    TreeRuin.StructureGen(structX, structY, mirrored);
                    break;
                case 1:
                    PetrifiedTitans.StructureGen(structX, structY, mirrored);
                    break;
                case 2:
                    ObsidiumChalice.StructureGen(structX, structY, mirrored);
                    break;
                case 3:
                    LycorisCave.StructureGen(structX, structY, mirrored);
                    break;
                case 4:
                    LavaCave1.StructureGen(structX, structY, mirrored);
                    break;
                case 5:
                    LavaCave2.StructureGen(structX, structY, mirrored);
                    break;
                case 6:
                    LavaCave3.StructureGen(structX, structY, mirrored);
                    break;
                case 7:
                    LivingLycoris.StructureGen(structX, structY, mirrored);
                    break;
                default:
                    LavaCave1.StructureGen(structX, structY, mirrored);
                    break;
            }
        }

        private void LootStructures(int xO, int yO)
        {
            int structX = xO - 225 * sizeMult + Main.rand.Next(225 * sizeMult * 2);
            int structY = yO - 275 * sizeMult + Main.rand.Next(275 * sizeMult / 2);

            for (int q = 0; q < 10 * sizeMult; q++)
            {
                GenerateLootStructure(structX, structY);
                Point16 newStructurePosition = new Point16(structX, structY);
                newStructurePosition = AlterStructureLocation(xO, yO, structX, structY);
                structX = newStructurePosition.X;
                structY = newStructurePosition.Y;
            }
        }

        private void GenerateLootStructure(int structX, int structY)
        {
            if (TileCheckSafe(structX, structY))
            {
                if (Main.tile[structX, structY].WallType == (ushort)Mod.Find<ModWall>("ObsidiumRockWall").Type)
                {
                    ObsidiumHouses.GenerateHouse(structX, structY);
                }
            }
        }

        public static void PlaceObsidiumChest(int x, int y, ushort floorType)
        {
            ClearSpaceForChest(x, y, floorType);
            int chestIndex = WorldGen.PlaceChest(x, y, (ushort)ModContent.TileType<ObsidiumChest>(), false, 0);

            int specialItem = GetObsidiumLoot();
            obsidiumPosition++;
            int[] oreLoot = GetObsidiumOreLoot();
            int[] potionLoot = GetObsidiumPotionLoot();
            int[] money = GetObsidiumMoneyLoot();
            int[] miscLoot = GetObsidiumMiscLoot();

            int[] itemsToPlaceInChests = new int[] { specialItem, oreLoot[0], potionLoot[0], money[0], miscLoot[0] };
            int[] itemCounts = new int[] { 1, oreLoot[1], potionLoot[1], money[1], miscLoot[1] };

            FillChest(chestIndex, itemsToPlaceInChests, itemCounts);
        }

        private static int GetObsidiumLoot()
        {
            int[] obsidiumLoot = new int[] { ModContent.ItemType<Eruption>(), ModContent.ItemType<FireDust>(), ModContent.ItemType<CrystalizedMagma>(), ModContent.ItemType<ObsidiumLily>(), ModContent.ItemType<MagmaHeart>(), ModContent.ItemType<Ragnashia>(), };

            if (obsidiumPosition < obsidiumLoot.GetLength(0))
                return obsidiumLoot[obsidiumPosition];
            else
            {
                obsidiumPosition = 0;
                return obsidiumLoot[obsidiumPosition];
            }
        }

        private static int[] GetObsidiumOreLoot()
        {
            int[] oreLoot = new int[] { ItemID.GoldBar, ItemID.PlatinumBar, ItemID.TungstenBar, ItemID.SilverBar };
            int orePos = Main.rand.Next(oreLoot.GetLength(0));
            int oreCount = Main.rand.Next(6, 16);
            int[] ore = { 0, 0 };
            ore[0] = oreLoot[orePos];
            ore[1] = oreCount;
            return ore;
        }

        private static int[] GetObsidiumPotionLoot()
        {
            int[] potLoot = new int[] { ModContent.ItemType<DestructionPotion>(), ModContent.ItemType<IllusionPotion>(), ModContent.ItemType<ConjurationPotion>(), ModContent.ItemType<JumpBoostPotion>(), ItemID.InfernoPotion, ItemID.LifeforcePotion, ItemID.WrathPotion };
            int potPos = Main.rand.Next(potLoot.GetLength(0));
            int potCount = Main.rand.Next(2, 5);
            int[] pot = { 0, 0 };
            pot[0] = potLoot[potPos];
            pot[1] = potCount;
            return pot;
        }

        private static int[] GetObsidiumMoneyLoot()
        {
            int monType = 0;
            int monCount = 0;
            if (Main.rand.Next(2) == 0)
            {
                monType = ItemID.GoldCoin;
                monCount = Main.rand.Next(1, 4);
            }
            else
            {
                monType = ItemID.SilverCoin;
                monCount = Main.rand.Next(60, 99);
            }
            int[] mon = { 0, 0 };
            mon[0] = monType;
            mon[1] = monCount;
            return mon;
        }

        private static int[] GetObsidiumMiscLoot()
        {
            int[] mscLoot = new int[] {
                ModContent.ItemType<Content.Items.Placeable.LavaGem>(), ModContent.ItemType<ArcaneShard>(),
                ModContent.ItemType<Content.Items.Placeable.LavaGem>(), ModContent.ItemType<RubrumDust>(),
                ModContent.ItemType<AlbusDust>(), ModContent.ItemType<VerdiDust>() };

            int mscPos = Main.rand.Next(mscLoot.GetLength(0));
            int mscCount = Main.rand.Next(2, 6);
            int[] msc = { 0, 0 };
            msc[0] = mscLoot[mscPos];
            msc[1] = mscCount;
            return msc;
        }

        public override void PostWorldGen()
        {
            DryTheObsidium();
            GrowLavaGems();
        }
        
        private void DryTheObsidium()
        {
            for(int i = 0; i < Main.maxTilesX - 2; i++)
            {
                for (int j = 0; j < Main.maxTilesY - 2; j++)
                {
                    if(TileCheckSafe(i, j))
                    {
                        if(Main.tile[i,j].LiquidAmount > 0 && (Main.tile[i,j].LiquidType == LiquidID.Lava) == false && (Main.tile[i,j].WallType == (ushort)Mod.Find<ModWall>("ObsidiumRockWall").Type || Main.tile[i, j].WallType == WallID.Lavafall))
                        {
                            Main.tile[i, j].LiquidAmount = 0;
                        }
                    }
                }
            }
        }

        private void GrowLavaGems()
        {
            for (int i = 0; i < Main.maxTilesX - 2; i++)
            {
                for (int j = 0; j < Main.maxTilesY - 2; j++)
                {
                    if (TileCheckSafe(i, j) && TileCheckSafe(i, j + 1))
                    {
                        if (Main.tile[i, j].WallType == (ushort)Mod.Find<ModWall>("ObsidiumRockWall").Type && Main.tile[i, j + 1].TileType == (ushort)ModContent.TileType<ObsidiumRock>() && Main.tile[i, j].TileType == 0 && Main.tile[i, j].HasTile == false)
                        {
                            if(Main.rand.Next(8) == 0)
                            {
                                WorldGen.PlaceTile(i, j, ModContent.TileType<LavaGem>(), true);
                            }
                        }
                        else if(Main.tile[i, j].WallType == (ushort)Mod.Find<ModWall>("ObsidiumRockWall").Type && Main.tile[i, j].TileType == (ushort)ModContent.TileType<ObsidiumRock>())
                        {
                            if (Main.rand.Next(3) == 0)
                            {
                                if (TileCheckSafe(i, j - 1) && TileCheckSafe(i, j + 1) && TileCheckSafe(i, j - 2) && TileCheckSafe(i, j + 2))
                                    SpawnRocks(i, j);
                            }
                        }
                    }
                }
            }
        }

        private bool SpawnRocks(int i, int j)
        {
            if (Main.tile[i, j - 1].TileType == 0 && Main.rand.Next(4) == 0)
            {
                WorldGen.PlaceTile(i, j - 1, ModContent.TileType<ObsidiumRocks>(), true);
                return true;
            }
            else if (Main.tile[i, j - 1].TileType == 0 && Main.tile[i, j - 2].TileType == 0 && Main.rand.Next(3) == 0)
            {
                WorldGen.PlaceTile(i, j - 1, ModContent.TileType<ObsidiumStalagmites>(), true);
                return true;
            }
            else if (Main.tile[i, j + 1].TileType == 0 && Main.tile[i, j + 2].TileType == 0 && Main.rand.Next(2) == 0)
            {
                WorldGen.PlaceTile(i, j + 1, ModContent.TileType<ObsidiumStalactites>(), true);
                return true;
            }
            return false;
        }
    }
}
