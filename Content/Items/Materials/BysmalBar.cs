﻿using Laugicality.Content.Items.Loot;
using Laugicality.Content.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Laugicality.Utilities.Base;

namespace Laugicality.Content.Items.Materials
{
	public class BysmalBar : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Tangibly intangible'");
        }
        public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 1;
			Item.useTime = 15;
			Item.useStyle = 1;
		}
        
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Bysmal>(), 3);
            recipe.AddIngredient(ModContent.ItemType<EtherialEssence>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
	}
}