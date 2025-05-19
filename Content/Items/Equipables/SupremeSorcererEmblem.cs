using Laugicality.Content.Items.Loot;
using Laugicality.Content.Tiles;
using Laugicality.Utilities.Base;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.Content.Items.Equipables
{
    public class SupremeSorcererEmblem : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+20% magic damage");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 100;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) += .2f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(489, 1);
            recipe.AddIngredient(ModContent.ItemType<EtherialEssence>(), 5);
            recipe.AddTile(ModContent.TileType<AncientEnchanter>());
            recipe.Register();
        }
    }
}