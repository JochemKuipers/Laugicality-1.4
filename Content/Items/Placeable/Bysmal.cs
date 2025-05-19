using Laugicality.Content.Tiles;
using Terraria.ModLoader;
using Laugicality.Utilities.Base;

namespace Laugicality.Content.Items.Placeable
{
    public class Bysmal : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Painful to even touch'");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = ModContent.TileType<BysmalOre>();
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AntitherialBlock");
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe Erecipe = new ModRecipe(mod);
            Erecipe.AddIngredient(mod, nameof(EtherialEssence));
            Erecipe.SetResult(this, 20);
            Erecipe.AddRecipe();
        }*/
    }
}