using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Laugicality.Content.Dusts;
using Laugicality.Utilities;

namespace Laugicality.Content.NPCs.Etheria
{
	public class TrueEtherialYeet : ModProjectile
    {
        public bool bitherial = true;
        public bool stopped = false;
        public int power = 0;
        public int damage = 0;
        public int delay = 0;
        public bool spawned = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Etherial Pulse");
        }
        public override void SetDefaults()
        {
            LaugicalityVars.eProjectiles.Add(Projectile.type);
            power = 0;
            stopped = false;
            damage = 20;
            spawned = false;
            //mystDmg = (float)projectile.damage;
            //mystDur = 1f + projectile.knockBack;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.timeLeft = 180;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }


        public override void AI()
        {
            bitherial = true;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            if (Projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref Projectile.velocity);
                Projectile.localAI[0] = 1f;
            }
            Vector2 move = Vector2.Zero;
            float distance = 1400f;
            bool target = false;
            for (int k = 0; k < 8; k++)
            {
                if (Main.player[k].active)
                {
                    Vector2 newMove = Main.player[k].Center - Projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                AdjustMagnitude(ref move);
                Projectile.velocity = (10 * Projectile.velocity + move) / 11f;
                AdjustMagnitude(ref Projectile.velocity);
            }

            Projectile.velocity *= 4;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;
        }


        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 6f / magnitude;
            }
        }

        public override Color? GetAlpha(Color drawColor)
        {
            int b = 225;
            int b2 = 125;
            int b3 = 155;
            if (drawColor.R != (byte)b)
            {
                drawColor.R = (byte)b;
            }
            if (drawColor.G < (byte)b2)
            {
                drawColor.G = (byte)b2;
            }
            if (drawColor.B < (byte)b3)
            {
                drawColor.B = (byte)b3;
            }
            return drawColor;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(44, 300, true);//Frostburn
        }

    }
}