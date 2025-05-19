using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Laugicality.Utilities.Base;

namespace Laugicality.Content.Items.Materials
{
    public class BottledGel : LaugicalityItem
    {
        int _time = 0;
        int _sec = 15;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("The gels will mesh together over time");
        }
        public override void SetDefaults()
        {
            _time = 0;
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            //item.UseSound = SoundID.Item9;
            Item.consumable = true;
        }

        public override void UpdateInventory(Player player)
        {
            if (_time < 60 * _sec)
                _time++;
            else
            {
                player.QuickSpawnItem(player.GetSource_FromThis(), ModContent.ItemType<BottledPinkGel>(), 1);
                Item.stack -= 1;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(126); //Water Bottle
            recipe.AddIngredient(ItemID.PinkGel);
            recipe.AddIngredient(ItemID.Gel, 5);
            //recipe.AddTile(null, "AlchemicalInfuser");
            recipe.Register();
        }
    }
}