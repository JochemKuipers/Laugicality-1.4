using Laugicality.Content.NPCs.Etheria;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Laugicality.Utilities;

namespace Laugicality.Content.NPCs.Etherial.BossFights
{
    public class SuperBee : ModNPC
    {
        public bool bitherial = false;
        public bool etherial = true;
        int delay = 0;
        int index = 0;
        Vector2 targetPos;
        public float tVel = 0f;
        public float vel = 0f;
        public float vMax = 12f;
        public float vAccel = .2f;
        public float vMag = 0f;
        float theta = 0;
        int targetType = 0;
        int counter = 0;

        public override void SetDefaults()
        {
            counter = 0;
            targetType = 0;
            vMag = 0f;
            vMax = 14f;
            tVel = 0f;
            index = 0;
            delay = 0;
            LaugicalityVars.etherial.Add(NPC.type);
            NPC.width = 18;
            NPC.height = 18;
            NPC.damage = 80;
            NPC.defense = 80;
            NPC.lifeMax = 40;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void AI()
        {
            Movement(NPC);
            NPC.lifeMax = 40;
            if (NPC.life > NPC.lifeMax)
                NPC.life = NPC.lifeMax;
        }

        private void Movement(NPC npc)
        {
            delay++;
            if (counter > 0)
                counter--;
            if ((delay > 8 * 60 || Main.rand.Next(8 * 60) == 0) && counter == 0)
            {
                delay = Main.rand.Next(0, 120);
                counter = 2 * 60;
                MirrorTeleport(npc, false);
            }
            npc.rotation = 0;
            if (counter > 1 * 60 + 30)
            {
                npc.velocity.X = 0;
                npc.velocity.Y = 0;
            }
            else
            {
                targetPos = Main.player[npc.target].Center;
                MoveToTarget(npc);
            }
            if (Main.player[npc.target].position.X > npc.position.X)
                npc.direction = 1;
            else
                npc.direction = 0;
        }
        /*
        public override void NPCLoot()
        {
            if (Main.rand.Next(4) == 0) Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<EtherialEssence>(), 1);
        }*/

        private void MirrorTeleport(NPC npc, bool burst)
        {
            if (burst && Main.player[npc.target].statLife > 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (Main.netMode != 1)
                    {
                        int N = NPC.NewNPC(NPC.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<EtherialSpiralShot>());
                        Main.npc[N].ai[0] = npc.whoAmI;
                        Main.npc[N].ai[1] = i;
                    }
                }
            }
            npc.position.X = Main.player[npc.target].position.X - (npc.position.X - Main.player[npc.target].position.X);
            npc.position.Y = Main.player[npc.target].position.Y - (npc.position.Y - Main.player[npc.target].position.Y);
            npc.velocity.X = -npc.velocity.X;
            npc.velocity.Y = -npc.velocity.Y;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter > 4.0)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight)
            {
                NPC.frame.Y = 0;
            }
        }

        private void MoveToTarget(NPC npc)
        {
            float dist = Vector2.Distance(targetPos, npc.Center);
            tVel = dist / 15;
            if (vMag < vMax && vMag < tVel)
            {
                vMag += vAccel;
                vMag = tVel;
            }

            if (vMag > tVel)
            {
                vMag = tVel;
            }

            if (vMag > vMax)
            {
                vMag = vMax;
            }

            if (dist != 0)
            {
                npc.velocity = npc.DirectionTo(targetPos) * vMag;
            }
        }
    }
}
