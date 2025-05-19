using Terraria;
using Terraria.ModLoader;

namespace Laugicality.Content.Projectiles.Tools
{
	public class DrillinatorProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 34;
			Projectile.height = 82;
			Projectile.scale = 1.25f;
			Projectile.aiStyle = 20;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            float mag = 38f;
            Player player = Main.player[Projectile.owner];
            player.velocity = mag * player.DirectionTo(Main.MouseWorld);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            float mag = 38f;
            Player player = Main.player[Projectile.owner];
            player.velocity = mag * player.DirectionTo(Main.MouseWorld);
        }
    }
}