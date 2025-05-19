﻿using Laugicality.Content.NPCs.Slybertron;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Laugicality.Utilities;

namespace Laugicality.Content.NPCs.Etherial.BossFights
{
    public class SparkSlime : ModNPC
    {
        int counter = 0;
        public override void SetDefaults()
        {
            LaugicalityVars.etherial.Add(NPC.type);
            NPC.width = 18;
            NPC.height = 18;
            NPC.damage = 80;
            NPC.defense = 80;
            NPC.lifeMax = 4000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 1;
            NPC.lavaImmune = true;
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void AI()
        {
            counter++;
            if (counter > 2 * 60)
            {
                counter = 0;
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, -4, 0, ModContent.ProjectileType<Electroshock>(), (int)(NPC.damage / 2), 3, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 4, 0, ModContent.ProjectileType<Electroshock>(), (int)(NPC.damage / 2), 3, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0, -4, ModContent.ProjectileType<Electroshock>(), (int)(NPC.damage / 2), 3, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0, 4, ModContent.ProjectileType<Electroshock>(), (int)(NPC.damage / 2), 3, Main.myPlayer);
                }
            }
            MovementCheck();
        }

        private void MovementCheck()
        {
            if (Main.player[NPC.target].Center.Y < NPC.Center.Y - 200)
            {
                NPC.velocity.Y -= .4f;
            }
            if (Main.player[NPC.target].Center.Y > NPC.Center.Y + 20)
            {
                NPC.noTileCollide = true;
            }
            else
                NPC.noTileCollide = false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }
}
