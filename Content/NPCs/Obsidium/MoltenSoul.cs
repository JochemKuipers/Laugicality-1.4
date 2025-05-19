using Laugicality.Content.Dusts;
using Laugicality.Content.Items.Loot;
using Laugicality.Content.Items.Placeable;
using Laugicality.Content.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Laugicality.Utilities;
using Terraria.GameContent.ItemDropRules;

namespace Laugicality.Content.NPCs.Obsidium
{
    public class MoltenSoul : ModNPC
    {
        int shootDel = 0;
        public override void SetDefaults()
        {
            shootDel = 300;
            NPC.width = 56;
            NPC.height = 56;
            NPC.damage = 60;
            NPC.defense = 22;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.value = 6000f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 10;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[24] = true;
        }

        public override void AI()
        {
            NPC.rotation = 0;
            if (Main.rand.Next(3) == 0)
                Dust.NewDust(NPC.Center, NPC.width / 2, 4, ModContent.DustType<Magma>(), 0f, 0f);
            shootDel--;
            if (shootDel <= 0)
            {
                if (Main.netMode != 1)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<MagmaBallLaunched>(), (int)(NPC.damage / 4f), 3, Main.myPlayer);
                shootDel = 60 * 5;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 300, true);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MagmaticCrystal>(), 1));
        }
    }
}
