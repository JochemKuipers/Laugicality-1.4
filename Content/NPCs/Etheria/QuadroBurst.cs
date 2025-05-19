using Laugicality.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Laugicality.Utilities;

namespace Laugicality.Content.NPCs.Etheria
{
	public class QuadroBurst : ModProjectile
    {
        public bool bitherial = true;
        int _delay = 0;
        int _spawned = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Etherial Pulse");
            //ProjectileID.Sets.Homing[projectile.type] = true;
			//ProjectileID.Sets.MinionShot[projectile.type] = true;
		}

		public override void SetDefaults()
        {
            _spawned = 0;
            _delay = 0;
            LaugicalityVars.eProjectiles.Add(Projectile.type);
            bitherial = true;
            Projectile.width = 44;
			Projectile.height = 44;
			//projectile.alpha = 255;
            Projectile.timeLeft = 160;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.rand.Next(0, 14) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), 0f, 0f);

            bitherial = true;
            _delay++;
            if(_delay > 20)
            {
                _spawned++;
                _delay = 0;
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, -Projectile.velocity.X / 4, -Projectile.velocity.Y / 4, ModContent.ProjectileType<EtherialPulsar>(), (int)(Projectile.damage), 3, Main.myPlayer);
            }
            if(_spawned >=4)
                Projectile.Kill();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(44, 300, true);//Frostburn
        }

        public override Color? GetAlpha(Color drawColor)
        {
            int b = 125;
            int b2 = 225;
            int b3 = 255;
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
    }
}