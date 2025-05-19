using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.Content.Walls
{
	public class ObsidiumRockWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = 1;
			AddMapEntry(new Color(20, 20, 32));
            //drop = ModContent.ItemType<Items.Placeable.ObsidiumRockWall>();
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

        public override void RandomUpdate(int i, int j)
        {
            if (Main.tile[i, j].LiquidAmount > 1 && !(Main.tile[i, j].LiquidType == LiquidID.Lava) && Main.tile[i, j].TileType < 1)
            {
                Main.tile[i, j].LiquidAmount = 0;
                Main.tile[i, j].ClearTile();
            }
            base.RandomUpdate(i, j);
        }
    }
}