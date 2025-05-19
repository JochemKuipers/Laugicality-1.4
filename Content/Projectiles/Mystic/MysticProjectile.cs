using Laugicality.Content.Buffs;
using Terraria;
using Terraria.ModLoader;
using Laugicality.Utilities.Players;

namespace Laugicality.Content.Projectiles.Mystic
{
    public class MysticProjectile : ModProjectile
    {
        bool durationed = false;
        public float duration = 1;
        public bool overflowed = false;
        public int buffID = 0;
        public int baseDuration = 4 * 60;


        public override void SetDefaults()
        {
            overflowed = false;
            durationed = false;
            duration = 1;
            //LaugicalityVars.eProjectiles.Add(Projectile.type);
        }

        public override bool PreAI()
        {
            if (!durationed)
            {
                duration = Main.player[Projectile.owner].GetModPlayer<LaugicalityPlayer>().MysticDuration;
                durationed = true;
                Projectile.timeLeft = (int)(Projectile.timeLeft * duration);
                overflowed = CheckOverflow();
            }
            if (overflowed)
            {
                OverflowEffects();
            }
            return true;
        }

        private bool CheckOverflow()
        {
            LaugicalityPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<LaugicalityPlayer>();
            if (modPlayer.MysticMode == 1 && modPlayer.Lux + modPlayer.CurrentLuxCost > modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost)
                return true;
            if (modPlayer.MysticMode == 2 && modPlayer.Vis + modPlayer.CurrentVisCost > modPlayer.VisMax + modPlayer.VisMaxPermaBoost)
                return true;
            if (modPlayer.MysticMode == 3 && modPlayer.Mundus + modPlayer.CurrentMundusCost > modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost)
                return true;
            return false;
        }
        
        private void OverflowEffects()
        {
            LaugicalityPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<LaugicalityPlayer>();
            if (modPlayer.ShroomOverflow > 0)
                Projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(buffID, (int)(baseDuration * duration) + Main.rand.Next(1 * 60));

            LaugicalityPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<LaugicalityPlayer>();

            if (modPlayer.Incineration > 0)
                target.AddBuff(ModContent.BuffType<Incineration>(), (int)(4 * 60 * duration) + Main.rand.Next(1 * 60));
            if (modPlayer.SporeShard > 0)
                target.AddBuff(ModContent.BuffType<Spored>(), (int)(4 * 60 * duration) + Main.rand.Next(1 * 60));
        }
    }
}
