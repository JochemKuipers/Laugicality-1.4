﻿using System;
using Laugicality.Content.Tiles;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;

namespace Laugicality.Utilities.Globals
{
    public class FanGlobalProjectile : GlobalProjectile
    {
        public const int MIN_SPEED = 10, MAX_SPEED = 32, BOOST_COOL = 5;
        private int boosted = 0;

        public override void SetDefaults(Projectile projectile)
        {
            boosted = 0;
            base.SetDefaults(projectile);
        }

        public override bool PreAI(Projectile projectile)
        {
            if(boosted > 0)
            {
                boosted--;
                return true;
            }

            if (!projectile.active)
                return true;

            Tile tile = projectile.GetTileOnCenter();

            if (tile.TileType == ModContent.TileType<BrassFAN>())
            {
                BoostLeft(projectile);
                boosted = BOOST_COOL;
            }

            if (tile.TileType == ModContent.TileType<BrassFANRight>())
            {
                BoostRight(projectile);
                boosted = BOOST_COOL;
            }

            return true;
        }

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        private void BoostLeft(Projectile projectile)
        {
            projectile.velocity.X = -MAX_SPEED;
            SoundEngine.PlaySound(new SoundStyle("Laugicality/Sounds/BrassFAN"), projectile.position);
        }

        private void BoostRight(Projectile projectile)
        {
            projectile.velocity.X = MAX_SPEED;
            SoundEngine.PlaySound(new SoundStyle("Laugicality/Sounds/BrassFAN"), projectile.position);
        }

        private void HandleDirectionalFans(Projectile projectile, bool left)
        {
            int directionMultiplier = left ? -1 : 1;

            if (left && projectile.velocity.X < MAX_SPEED * directionMultiplier || !left && projectile.velocity.X > MAX_SPEED * directionMultiplier)
                return;

            if (Math.Abs(projectile.velocity.X) < MIN_SPEED * directionMultiplier)
                projectile.velocity.X = MIN_SPEED * directionMultiplier;

            SoundEngine.PlaySound(new SoundStyle("Laugicality/Sounds/BrassFAN"), projectile.position);

            projectile.velocity.X *= 2;

            if (Math.Abs(projectile.velocity.X) > MAX_SPEED)
                projectile.velocity.X = MAX_SPEED * directionMultiplier;
        }
    }
}