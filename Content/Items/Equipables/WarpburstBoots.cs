﻿using Laugicality.Content.Dusts;
using Laugicality.Content.Items.Loot;
using Laugicality.Content.Items.Materials;
using Laugicality.Content.Projectiles.Accessory;
using Laugicality.Utilities;
using Laugicality.Utilities.Base;
using Laugicality.Utilities.Players;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.Content.Items.Equipables
{
    public class WarpburstBoots : LaugicalityItem
    {
        int dashDelay = 0;
        int dashCooldown = 0;
        int jumpDashes = 0;
        int trail = 0;
        int dashDir = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Warpburst Boots");
            // Tooltip.SetDefault("Allows the wearer to dash\nIncreased movement speed\nCan dash multiple times in the air\nBecome immune for a time while dashing");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = 100;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.defense = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += .25f;
            Dashes(player);
            Delays(player);            
        }

        private void Dashes(Player player)
        {
            float dashSpeed = 15;
            int dashCooldownMax = 60;
            int trailLength = 45;
            int verticalCooldownMax = 45;
            int maxJumps = 3;
            int immuneTime = 15;

            if (!player.mount.Active && player.grappling[0] == -1 && dashCooldown <= 0)
            {
                if (player.controlRight && player.releaseRight)
                {
                    if (dashDelay > 0 && dashDir == 1)
                    {
                        dashCooldown = dashCooldownMax;
                        trail = trailLength;
                        player.velocity.X = dashSpeed;
                        player.GetModPlayer<LaugicalityPlayer>().DustBurst(ModContent.DustType<White>(), 20);
                        dashDir = 0;
                        player.immune = true;
                        player.immuneTime = immuneTime;
                    }
                    else
                    {
                        dashDelay = 15;
                        dashDir = 1;
                    }
                }
                if (player.controlLeft && player.releaseLeft)
                {
                    if (dashDelay > 0 && dashDir == 2)
                    {
                        dashCooldown = dashCooldownMax;
                        trail = trailLength;
                        player.velocity.X = -dashSpeed;
                        player.GetModPlayer<LaugicalityPlayer>().DustBurst(ModContent.DustType<White>(), 20);
                        dashDir = 0;
                        player.immune = true;
                        player.immuneTime = immuneTime;
                    }
                    else
                    {
                        dashDelay = 15;
                        dashDir = 2;
                    }
                }
                if (player.controlDown && player.releaseDown)
                {
                    if (dashDelay > 0 && dashDir == 3)
                    {
                        dashCooldown = verticalCooldownMax;
                        trail = trailLength;
                        player.velocity.Y = 2 * dashSpeed;
                        player.GetModPlayer<LaugicalityPlayer>().DustBurst(ModContent.DustType<White>(), 40);
                        dashDir = 0;
                        player.fallStart = (int)player.position.Y / 16;
                        player.immune = true;
                        player.immuneTime = immuneTime;
                    }
                    else
                    {
                        dashDelay = 15;
                        dashDir = 3;
                    }
                }
                if (player.controlUp && player.releaseUp)
                {
                    if (dashDelay > 0 && jumpDashes < maxJumps && dashDir == 4)
                    {
                        dashCooldown = verticalCooldownMax;
                        trail = trailLength;
                        player.velocity.Y = -dashSpeed;
                        player.GetModPlayer<LaugicalityPlayer>().DustBurst(ModContent.DustType<White>(), 40);
                        dashDir = 0;
                        player.fallStart = (int)player.position.Y / 16;
                        jumpDashes++;
                        player.immune = true;
                        player.immuneTime = immuneTime;
                    }
                    else
                    {
                        dashDelay = 15;
                        dashDir = 4;
                    }
                }
            }
        }

        private void Delays(Player player)
        {
            if (dashDelay > 0)
                dashDelay--;
            if (dashCooldown > 0)
                dashCooldown--;
            if (trail > 0)
            {
                trail--;
                player.GetModPlayer<LaugicalityPlayer>().DustTrail(ModContent.DustType<White>(), 1);
            }
            if (Main.tileSolid[Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].TileType] && Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].TileType != 0)
                jumpDashes = 0;
            if (player.grappling[0] != -1)
                jumpDashes = 0;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DemonsparkBoots>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DioritusCore>());
            recipe.AddIngredient(ModContent.ItemType<AndesiaCore>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}