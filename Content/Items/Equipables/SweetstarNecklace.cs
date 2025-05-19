using Laugicality.Utilities.Base;
using Terraria;
using Terraria.ID;

namespace Laugicality.Content.Items.Equipables
{
    public class SweetstarNecklace : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Increased invincibility time and movement speed after taking damage \nRelease stars and bees after taking damage");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 36;
            Item.value = 100;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.longInvince = true;
            player.panic = true;
            player.honeyCombItem = Item;
            player.starCloakItem = Item; 
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(862);
            recipe.AddIngredient(1578);
            recipe.AddTile(114);
            recipe.Register();
        }
    }
}