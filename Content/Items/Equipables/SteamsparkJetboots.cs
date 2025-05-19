using Laugicality.Content.Dusts;
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
    public class SteamsparkJetboots : LaugicalityItem
    {
        int dashDelay = 0;
        int dashCooldown = 0;
        int jumpDashes = 0;
        int trail = 0;
        int rocketBootTime = 0;
        int rocketBootTimeMax = 4 * 60 + 30;
        float rocketAccel = .25f;
        int dashDir = 0;
        float maxVel = 12;

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Steam speed!\nDominion over liquids and the sky\nGrants the ability to dash\nBecome immune for a time while dashing\nLeave a trail of Steam while moving");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = 100;
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
            Item.defense = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 4;
            player.moveSpeed += .5f;
            player.maxRunSpeed += 5.5f;
            player.iceSkate = true;
            player.GetJumpState(ExtraJump.BlizzardInABottle).Enable();
            player.noFallDmg = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.buffImmune[24] = true;
            player.gills = true;
            player.ignoreWater = true;
            player.accFlipper = true;
            player.GetModPlayer<LaugicalityPlayer>().SteamTrail = true;
            Run(player);
            GetRocketBoots(player);
            Dashes(player);
            Delays(player);
        }

        private void Run(Player player)
        {
            if (Main.tileSolid[Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].TileType] && Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].TileType != 0 && Math.Abs(player.velocity.X) > 4)
            {
                int newDust = Dust.NewDust(new Vector2(player.Center.X + player.velocity.X, player.position.Y + 4 + (float)player.height - 10f + player.velocity.Y), 8, 8, ModContent.DustType<Steam>(), 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[newDust].scale = 4;
            }
        }

        private void GetRocketBoots(Player player)
        {
            float accelMax = .55f;

            if (player.controlJump && rocketBootTime < rocketBootTimeMax)
            {
                if (rocketAccel < accelMax)
                    rocketAccel += .075f;
                if (player.velocity.Y > -maxVel)
                    player.velocity.Y -= rocketAccel;
                if (player.velocity.Y > 0)
                    player.velocity.Y *= .8f;
                RocketDust(player);
                rocketBootTime++;
                player.fallStart = (int)player.position.Y / 16;
                if (player.rocketDelay2 <= 0)
                {
                    SoundEngine.PlaySound(SoundID.Item13, player.position);
                    player.rocketDelay2 = 30;
                }
            }
            else
                rocketAccel = .5f;
        }

        private void RocketDust(Player player)
        {
            int alpha = 0;
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    int newDust = Dust.NewDust(new Vector2(player.position.X - 4f + player.velocity.X, player.position.Y + (float)player.height - 10f + player.velocity.Y), 8, 8, ModContent.DustType<SteamTrailDust>(), 0f, 0f, alpha, default(Color), 1.5f);
                    Main.dust[newDust].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                    Main.dust[newDust].velocity.X = (Main.dust[newDust].velocity.X * 1f - 2f - player.velocity.X * 0.3f) / 2;
                    Main.dust[newDust].velocity.Y = (Main.dust[newDust].velocity.Y * 1f + 2f * player.gravDir - player.velocity.Y * 0.3f) / 2;
                    Main.dust[newDust].noGravity = true;
                }
                else
                {
                    int newDust = Dust.NewDust(new Vector2(player.position.X + (float)player.width - 4f + player.velocity.X, player.position.Y + (float)player.height - 10f + player.velocity.Y), 8, 8, ModContent.DustType<SteamTrailDust>(), 0f, 0f, alpha, default(Color), 1.5f);
                    Main.dust[newDust].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                    Main.dust[newDust].velocity.X = (Main.dust[newDust].velocity.X * 1f + 2f - player.velocity.X * 0.3f) / 2;
                    Main.dust[newDust].velocity.Y = (Main.dust[newDust].velocity.Y * 1f + 2f * player.gravDir - player.velocity.Y * 0.3f) / 2;
                    Main.dust[newDust].noGravity = true;
                }
            }
        }

        private void Dashes(Player player)
        {
            float dashSpeed = 22;
            int dashCooldownMax = 45;
            int trailLength = 45;
            int verticalCooldownMax = 45;
            int maxJumps = 6;
            int immuneTime = 18;

            if (!player.mount.Active && player.grappling[0] == -1 && dashCooldown <= 0)
            {
                if (player.controlRight && player.releaseRight)
                {
                    if (dashDelay > 0 && dashDir == 1)
                    {
                        dashCooldown = dashCooldownMax;
                        trail = trailLength;
                        player.velocity.X = dashSpeed;
                        player.GetModPlayer<LaugicalityPlayer>().DustBurst(ModContent.DustType<SteamTrailDust>(), 40);
                        dashDir = 0;
                        player.immune = true;
                        player.immuneTime = immuneTime;
                        DashBurst(player);
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
                        player.GetModPlayer<LaugicalityPlayer>().DustBurst(ModContent.DustType<SteamTrailDust>(), 40);
                        dashDir = 0;
                        player.immune = true;
                        player.immuneTime = immuneTime;
                        DashBurst(player);
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
                        player.GetModPlayer<LaugicalityPlayer>().DustBurst(ModContent.DustType<SteamTrailDust>(), 50);
                        dashDir = 0;
                        player.fallStart = (int)player.position.Y / 16;
                        player.immune = true;
                        player.immuneTime = immuneTime;
                        DashBurst(player);
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
                        player.GetModPlayer<LaugicalityPlayer>().DustBurst(ModContent.DustType<SteamTrailDust>(), 50);
                        dashDir = 0;
                        player.fallStart = (int)player.position.Y / 16;
                        jumpDashes++;
                        player.immune = true;
                        player.immuneTime = immuneTime;
                        DashBurst(player);
                    }
                    else
                    {
                        dashDelay = 15;
                        dashDir = 4;
                    }
                }
            }
        }

        private void DashBurst(Player player)
        {
            int numProj = Main.rand.Next(2) + 4;
            for (int i = 0; i < numProj; i++)
            {
                float mag = Main.rand.NextFloat() * 4 + 2;
                float theta = Main.rand.NextFloat() * 2 * (float)Math.PI;
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, mag * (float)Math.Cos(theta), mag * (float)Math.Sin(theta), ModContent.ProjectileType<SteamTrailProj>(), (int)(32 * player.GetModPlayer<LaugicalityPlayer>().GetGlobalDamage()), 0, player.whoAmI);
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
                player.GetModPlayer<LaugicalityPlayer>().DustTrail(ModContent.DustType<SteamTrailDust>(), 2);
            }
            if (Main.tileSolid[Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].TileType] && Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16) + 2].TileType != 0 && Math.Abs(player.velocity.Y) < .25f)
            {
                jumpDashes = 0;
                rocketBootTime = 0;
            }
            if (player.grappling[0] != -1)
            {
                jumpDashes = 0;
                rocketBootTime = 0;
            }
        }
    }
}