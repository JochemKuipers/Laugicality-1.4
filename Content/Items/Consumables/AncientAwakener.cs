﻿using Laugicality.Content.NPCs.RockTwins;
using Laugicality.Content.Projectiles.BossSummons;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Laugicality.Content.Projectiles.Special;
using Laugicality.Utilities.Base;

namespace Laugicality.Content.Items.Consumables
{
	public class AncientAwakener : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Summons Dioritus\n\'The rulers of the caverns are fearsome foes\'");
        }

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = ModContent.ProjectileType<Nothing>();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<GeneralBossSpawn>(), ModContent.NPCType<Dioritus>(), knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            return (player.ZoneRockLayerHeight && NPC.CountNPCS(ModContent.NPCType<Andesia>()) < 1 && NPC.CountNPCS(ModContent.NPCType<Dioritus>()) < 1 && NPC.CountNPCS(ModContent.NPCType<AnDio3>()) < 1);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MarbleBlock, 40);
            recipe.AddIngredient(ItemID.GoldenKey);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
        }
	}
}