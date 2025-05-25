using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Laugicality.Utilities.Base;

namespace Laugicality.Content.Items.Placeable.MusicBoxes
{
    public class DioritusMusicBox : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Dioritus)");
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
            Item.createTile = ModContent.TileType<Tiles.MusicBoxes.DioritusMusicBox>();
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(ModContent.TileType<Tiles.LaugicalWorkbench>());
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Marble, 40);
            recipe.Register();
        }
    }
}