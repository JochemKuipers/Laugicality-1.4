using Laugicality.Content.Dusts;
using Laugicality.Content.Items.Placeable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using WebmilioCommonsAddon.Extensions;
using Laugicality.Utilities;
using System.Collections.Generic;

namespace Laugicality.Content.Tiles
{
    public class Lycoris : AmelderaTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileMerge[ModContent.TileType<Lycoris>()][ModContent.TileType<Tiles.Radiata>()] = true;
            Main.tileMerge[ModContent.TileType<Tiles.Radiata>()][ModContent.TileType<Lycoris>()] = true;
            AddMapEntry(new Color(225, 50, 0));
            MineResist = .5f;
            MinPick = 10;
            DustType = ModContent.DustType<Magma>();

            if (!Main.dedServ)
            {
                //obsidiumTexture = this.GetType().GetTexture().Value;
                //amelderaTexture = ModContent.Request<Texture2D>(this.GetType().GetRootPath() + "/ElderlilyTile").Value;
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.6f;
            if (LaugicalityWorld.Ameldera)
                r = 0.1f;
            g = 0.5f;
            b = 0f;
            if (LaugicalityWorld.Ameldera)
                b = 0.6f;
        }

        public override void RandomUpdate(int i, int j)
        {
            int randm = Main.rand.Next(30);
            if(randm < 8)
            {
                if(CheckTile(i, j - 1))
                {
                    Terraria.WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumGrass>(), true, 0, -1, -1);
                }
            }
            else if(randm < 12)
            {
                if(Check2Tiles(i, j-1) && Main.tile[i,j + 1].TileType == ModContent.TileType<Lycoris>())
                {
                    int chance = Main.rand.Next(2);
                    if(chance == 0)
                        Terraria.WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumPlantGrass>(), true, 0, -1, -1);
                    else
                        Terraria.WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumPlantGrass2>(), true, 0, -1, -1);
                }
                else if(CheckTile(i, j - 1))
                    Terraria.WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumGrass>(), true, 0, -1, -1);
            }
            else if(randm < 14)
            {
                if (Check4Tiles(i, j - 1) && Main.tile[i, j].TileType == ModContent.TileType<Lycoris>() && Main.tile[i + 1, j].TileType == ModContent.TileType<Lycoris>() && Main.tile[i + 2, j].TileType == ModContent.TileType<Lycoris>() && Main.tile[i + 3, j].TileType == ModContent.TileType<Lycoris>())
                {
                    int chance = Main.rand.Next(4);
                    if (chance == 0)
                        Terraria.WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumPlantBulbs>(), true, 0, -1, -1);
                    if (chance == 1)
                        Terraria.WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumPlantHeart>(), true, 0, -1, -1);
                    if (chance == 2)
                        Terraria.WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumPlantLeaves>(), true, 0, -1, -1);
                    if (chance == 3)
                        Terraria.WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumPlantMine>(), true, 0, -1, -1);
                }
                else if (Check2Tiles(i, j - 1) && Main.tile[i, j + 1].TileType == ModContent.TileType<Lycoris>())
                {
                    int chance = Main.rand.Next(2);
                    if (chance == 0)
                        Terraria.WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumPlantGrass>(), true, 0, -1, -1);
                    else
                        Terraria.WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumPlantGrass2>(), true, 0, -1, -1);
                }
                else if (CheckTile(i, j - 1))
                    Terraria.WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumGrass>(), true, 0, -1, -1);
            }
        }

        private bool Check4Tiles(int i, int j)
        {
            for (int k = i; k < i + 4; k++)
            {
                for (int l = j; l > j - 4; l--)
                {
                    if (Main.tile[k, l].TileType != 0)
                        return false;
                }
            }
            return true;
        }

        private bool Check2Tiles(int i, int j)
        {
            for (int k = i; k < i + 2; k++)
            {
                for (int l = j; l > j - 2; l--)
                {
                    if (Main.tile[k, l].TileType != 0)
                        return false;
                }
            }
            return true;
        }

        private bool CheckTile(int i, int j)
        {
            if (Main.tile[i, j].TileType != 0)
                return false;
            return true;
        }


        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (j < Main.maxTilesY - 4)
            {
                if (Main.tile[i, j + 1].TileType == ModContent.TileType<ObsidiumVine>())
                    Terraria.WorldGen.KillTile(i, j + 1);
            }
        }

        public override bool CanDrop(int i, int j)
        {
            return true;
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ModContent.ItemType<ElderlilyItem>());
        }
    }
}