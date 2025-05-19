using Laugicality.Content.Items.Loot;
using Laugicality.Content.NPCs.Etheria;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Laugicality.Utilities;
using Terraria.GameContent.ItemDropRules;
using Terraria.Audio;

namespace Laugicality.Content.NPCs.Etherial.BossFights
{
    public class SuperServant : ModNPC
    {
        public bool bitherial = false;
        public bool etherial = true;
        int _delay = 0;
        int _index = 0;
        Vector2 _targetPos;
        public float tVel = 0f;
        public float vel = 0f;
        public float vMax = 10f;
        public float vAccel = .2f;
        public float vMag = 0f;
        float _theta = 0;
        int _targetType = 0;

        public override void SetDefaults()
        {
            _targetType = 0;
            vMag = 0f;
            vMax = 14f;
            tVel = 0f;
            _index = 0;
            _delay = 0;
            LaugicalityVars.etherial.Add(NPC.type);
            NPC.width = 36;
            NPC.height = 36;
            NPC.damage = 40;
            NPC.defense = 80;
            NPC.lifeMax = 4000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 5;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }
        
        public override void AI()
        {
            MovementType(NPC);
        }
        
        private void MovementType(NPC npc)
        {
            _delay++;
            if (_delay > 480)
            {
                _delay = Main.rand.Next(0, 120);
                MirrorTeleport(npc, false);
            }
        }


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EtherialEssence>()));
        }

        private void MirrorTeleport(NPC npc, bool burst)
        {
            SoundEngine.PlaySound(new SoundStyle("Laugicality/EtherialChange"), NPC.position);
            if (burst && Main.player[npc.target].statLife > 1)
            {
                for (int i = 0; i < 8; i++)
                {

                    if (Main.netMode != 1)
                    {
                        int n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<EtherialSpiralShot>());
                        Main.npc[n].ai[0] = npc.whoAmI;
                        Main.npc[n].ai[1] = i;
                    }
                }
            }
            npc.position.X = Main.player[npc.target].position.X - (npc.position.X - Main.player[npc.target].position.X);
            npc.position.Y = Main.player[npc.target].position.Y - (npc.position.Y - Main.player[npc.target].position.Y);
            npc.velocity.X = -npc.velocity.X;
            npc.velocity.Y = -npc.velocity.Y;
        }
    }
}
