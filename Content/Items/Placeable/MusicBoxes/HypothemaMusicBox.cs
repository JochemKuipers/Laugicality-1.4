using Laugicality.Content.Items.Materials;
using Laugicality.Utilities.Base;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.Content.Items.Placeable.MusicBoxes
{
    public class HypothemaMusicBox : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Hypothema)");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = ModContent.TileType<Tiles.MusicBoxes.HypothemaMusicBox>();
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(ModContent.TileType<Tiles.LaugicalWorkbench>());
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ModContent.ItemType<ChilledBar>(), 5);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.Register();
        }
    }
}