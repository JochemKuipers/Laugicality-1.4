using System;
using Laugicality.Content.Buffs;
using Laugicality.Content.Dusts;
using Laugicality.Content.Items.Loot;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Laugicality.Utilities;
using Laugicality.Utilities.Players;
using Terraria.GameContent.ItemDropRules;

namespace Laugicality.Content.NPCs.Slybertron
{

    [AutoloadBossHead]
    public class Slybertron : ModNPC
    {
        public override void Load()
        {
            string SlybertronSay = this.GetLocalization("Chat.SlybertronSay").Value; // Phase 2 weapons activated.
            string SlybertronSay2 = this.GetLocalization("Chat.SlybertronSay2").Value; // Slybertron is coming for you.
        }

        public int spawned = 0;
        public Random rnd = new Random();
        public int phase = 0;
        public int numDefeats = 0;
        public int attackDuration = 0;
        public int attackDurationCl = 0;
        public int attackDurationXo = 0;
        public int damage = 0;
        public double attackDelay = 0;      //Current delay between attacks
        public double attackReload = 200;   //What the delay is reset to between attacks
        public double attack2Delay = 0;      //Current delay between attacks
        public double attack2Reload = 400;   //What the delay is reset to between attacks
        public double attackReloadSpeed = 1.0;
        public double reloadSpeed = 1.0;
        public int attack1 = 0;//1: Gearikans, 2: Coginator, 3: SteamStreams, 4:Electroshock
        public int attack2 = 0;//1: Loose Cog, 2: Steamy Shadow, 3: X-Out, 4:Gas Ball
        //Layer 1 stats
        public int steamStreamDmg = 0;
        public int steamStreamHits = 0;
        public int steamStreamShots = 0;
        public int coginatorDmg = 0;
        public int coginatorHits = 0;
        public int coginatorShots = 0;
        public int gearikanDmg = 0;
        public int gearikanHits = 0;
        public int gearikanShots = 0;
        public int electroShockDmg = 0;
        public int electroShockHits = 0;
        public int electroShockShots = 0;
        //Layer 2 Stats
        public int looseCogDmg = 0;
        public int looseCogHits = 0;
        public int looseCogShots = 0;
        public int steamShadowDmg = 0;
        public int steamShadowHits = 0;
        public int steamShadowShots = 0;
        public int xOutDmg = 0;
        public int xOutHits = 0;
        public int xOutShots = 0;
        public int gasBallDmg = 0;
        public int gasBallHits = 0;
        public int gasBallShots = 0;
        public bool stage2 = false;
        public bool bitherial = true;
        public int plays = 0;
        public bool fall = false;

        public override void SetStaticDefaults()
        {
            LaugicalityVars.eNPCs.Add(NPC.type);
            // DisplayName.SetDefault("Slybertron");
            //Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            fall = false;
            plays = 1;
            bitherial = true;
            attackReloadSpeed = 1.0;
            attackDelay = 300;      //Delay before first attack
            attackReload = 200;     //Resetting the reload speed
            attack2Delay = 300;      //Delay before first attack
            attack2Reload = 400;     //Resetting the reload speed
            phase = 1;
            NPC.width = 378;
            NPC.height = 194;
            NPC.damage = 100;
            NPC.defense = 30;
            NPC.aiStyle = 1;
            NPC.lifeMax = 60000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.npcSlots = 15f;
            NPC.value = 12f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            Music = MusicLoader.GetMusicSlot("Laugicality/Sounds/Music/Slybertron");
            damage = 40;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            plays = numPlayers;
            NPC.lifeMax = 80000 + numPlayers * 8000;
            NPC.damage = 140;
            damage = 50;
            attackReloadSpeed += .1;
        }


        public override void AI()
        {
            NPC.spriteDirection = 0;
            if(Main.rand.Next(6)== 0)Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, ModContent.DustType<TrainSteam>(), 0f, 0f);
            bitherial = true;
            //Despawn check
            if (Main.player[NPC.target].statLife == 0) { spawned = 0; NPC.aiStyle = 0; NPC.noTileCollide = true; }
            else
            {
                Vector2 delta = Main.player[NPC.target].Center - NPC.Center;
                float magnitude = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
                //Jump at you if too far away [x]
                if (Math.Abs(delta.X) > 1200 && NPC.aiStyle == 1) { NPC.aiStyle = 0; }
                if (Math.Abs(delta.X) < 600 && NPC.aiStyle == 0) { NPC.aiStyle = 1; }
                if (NPC.aiStyle == 0)
                {
                    if (delta.X > 0) { NPC.velocity.X = 8f; NPC.velocity.Y = -2f; }
                    if (delta.X < 0) { NPC.velocity.X = -8f; NPC.velocity.Y = -2f; }
                }
                if (NPC.aiStyle == 0 || fall == true)
                {
                    NPC.noTileCollide = true;
                }
                else
                {
                    NPC.noTileCollide = false;
                }
                //Jump at you if too far away [y]
                if (delta.Y < -300 && NPC.aiStyle == 1) { NPC.aiStyle = 0; }
                if (delta.Y > 200)
                    fall = true;
                else
                    fall = false;

                if (delta.Y > 40 && NPC.aiStyle == 0) { NPC.aiStyle = 1; }
                if (NPC.aiStyle == 0)
                {
                    if (delta.X > 2) { NPC.velocity.X = 2f; NPC.velocity.Y = -8f; }
                    if (delta.X < -2) { NPC.velocity.X = -2f; NPC.velocity.Y = -8f; }
                }
            }
            NPC.rotation = 0f;
            //Attack Durations
            if (attackDuration > 0) attackDuration += 1;
            if (attackDurationCl > 0) attackDurationCl += 1;

            //Phases
            string SlybertronSay = this.GetLocalization("Chat.SlybertronSay").Value;
            if (NPC.life < NPC.lifeMax * .67 && phase == 1 ) { phase = 2; Main.NewText(SlybertronSay, 250, 0, 0);  //this is the message that will appear when the npc is killed  , 200, 200, 55 is the text color
                SoundEngine.PlaySound(SoundID.Roar, NPC.position);
            }
            if (NPC.life < NPC.lifeMax * .33 && phase == 2) { phase = 3; attackReloadSpeed += .1; }
            if (NPC.life < NPC.lifeMax * .20 && phase == 3) { phase = 4; }//Only to apply phase 3 knowledge
            if (NPC.life < NPC.lifeMax * .10 && phase == 4 && Main.expertMode)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                attackReloadSpeed += .1;
                phase = 5;
                NPC.life = (int)(NPC.lifeMax * .25);
                string SlybertronSay2 = this.GetLocalization("Chat.SlybertronSay2").Value;
                Main.NewText(SlybertronSay2, 250, 0, 0);  //this is the message that will appear when the npc is killed  , 200, 200, 55 is the text color
            }
            //Picking attack 1
            if (attackDelay > 0) attackDelay -= attackReloadSpeed;
            else {//Pick attack 

                //Picking an attack
                if (Main.rand.NextDouble() <= .25) attack1 = 1;//25% chance to do attack 1
                else
                {
                    if (Main.rand.NextDouble() <= .33) attack1 = 2; //33% of 75% is 25%
                    else
                    {
                        if (Main.rand.NextDouble() <= .5) { attack1 = 3; }
                        else attack1 = 4;
                    }
                }
            }//end Pick attack
            if (phase > 1)
            {
                //Picking attack 2
                if (attack2Delay > 0) attack2Delay -= attackReloadSpeed;
                else
                {//Pick attack 

                    //Picking an attack
                    if (Main.rand.NextDouble() <= .25) attack2 = 1;//25% chance to do attack 1
                    else
                    {
                        if (Main.rand.NextDouble() <= .33) attack2 = 2; //33% of 75% is 25%
                        else
                        {
                            if (Main.rand.NextDouble() <= .5) { attack2 = 3; }
                            else attack2 = 4;
                        }
                    }
                }//end Pick attack
            }

            //Attacks
            if (attack1 == 1 && Main.netMode != 1)//Gearikans
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4 + Main.rand.Next(4), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -1 * (4 + Main.rand.Next(4)), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4 + Main.rand.Next(4), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -1 * (4 + Main.rand.Next(4)), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4 + Main.rand.Next(4), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -1 * (4 + Main.rand.Next(4)), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4 + Main.rand.Next(4), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -1 * (4 + Main.rand.Next(4)), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4 + Main.rand.Next(4), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -1 * (4 + Main.rand.Next(4)), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4 + Main.rand.Next(4), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -1 * (4 + Main.rand.Next(4)), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4 + Main.rand.Next(4), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -1 * (4 + Main.rand.Next(4)), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4 + Main.rand.Next(4), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -1 * (4 + Main.rand.Next(4)), 1 - Main.rand.Next(8), ModContent.ProjectileType<Gearikan>(), damage / 2, 3f, Main.myPlayer);
                attack1 = 0;
                gearikanShots += 1;
                attackDelay = attackReload;
                
            }
            if (attack1 == 2 && Main.netMode != 1)//Coginator
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Coginator>(), damage, 3f, Main.myPlayer);
                attack1 = 0;
                coginatorShots += 1;
                attackDelay = attackReload;
            }
            if (attack1 == 3 && Main.netMode != 1)//Steam Stream
            {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 3, -10, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -3, -10, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 10, -3, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -10, -3, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    attackDuration = 1;
                    attack1 = 0;
                    steamStreamShots += 1;
                    attackDelay = attackReload;
            }
            /*if (attack1 == 3)
                Main.PlaySound(SoundID.Item34, (int)npc.position.X, (int)npc.position.Y);*/

            if (attackDuration == 30)
                {
                    SoundEngine.PlaySound(SoundID.Item34, NPC.position);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 3, -10, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -3, -10, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 10, -3, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -10, -3, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                }
                if (attackDuration == 60)
                {
                    SoundEngine.PlaySound(SoundID.Item34, NPC.position);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 3, -10, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -3, -10, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 10, -3, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -10, -3, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                }
                if (attackDuration == 90)
                {
                    SoundEngine.PlaySound(SoundID.Item34, NPC.position);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 3, -10, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -3, -10, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 10, -3, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -10, -3, ModContent.ProjectileType<SteamStream>(), damage, 3f, Main.myPlayer);
                }
                if (attackDuration > 90)
                {
                    attackDuration = 0;
                }
            
            if (attack1 == 4 && Main.netMode != 1)//Electroshock
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 12, 0, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 10, 2, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 8, 4, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 6, 6, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4, 8, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 2, 10, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 0, 12, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -2, 10, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -4, 8, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 6, 6, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -8, 4, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -10, 2, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -12, 0, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 10, -2, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 8, -4, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 6, -6, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4, -8, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 2, -10, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 0, -12, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -2, -10, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -4, -8, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 6, -6, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -8, -4, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -10, -2, ModContent.ProjectileType<Electroshock>(), damage, 3f, Main.myPlayer);
                attack1 = 0;
                electroShockShots += 1;
                attackDelay = attackReload;
            }
            if (attack1 == 4)
                SoundEngine.PlaySound(SoundID.Item33, NPC.position);

            //Attack Layer 2
            if (attack2 == 1 && Main.netMode != 1)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<CogLoose>(), damage, 3f, Main.myPlayer);
                attack2 = 0;
                attackDurationCl = 1;
                attack2Delay = attack2Reload;
            }

            if (attackDurationCl == 30)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<CogLoose>(), damage, 3f, Main.myPlayer);
            }
            if (attackDurationCl == 60)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<CogLoose>(), damage, 3f, Main.myPlayer);
            }
            if (attackDurationCl == 90)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<CogLoose>(), damage, 3f, Main.myPlayer);
            }
            if (attackDurationCl > 90)
            {
                attackDurationCl = 0;
            }
            if (attack2 == 2 && Main.netMode != 1)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X+120, NPC.Center.Y, 0, 0, ModContent.ProjectileType<SteamyShadow>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X-120, NPC.Center.Y, 0, 0, ModContent.ProjectileType<SteamyShadow>(), damage, 3f, Main.myPlayer);
                attack2 = 0;
                attack2Delay = attack2Reload;
            }
            if (attack2 == 3 && Main.netMode != 1)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4, 4, ModContent.ProjectileType<XOut>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, 4, -4, ModContent.ProjectileType<XOut>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -4, -4, ModContent.ProjectileType<XOut>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X, NPC.Center.Y, -4, 4, ModContent.ProjectileType<XOut>(), damage, 3f, Main.myPlayer);
                attack2 = 0;
                attack2Delay = attack2Reload;
            }
            if (attack2 == 3)
                SoundEngine.PlaySound(SoundID.Item33, NPC.position);

            if (attack2 == 4 && Main.netMode != 1)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X + 120, NPC.Center.Y, 8, 0, ModContent.ProjectileType<GasBall>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center.X - 120, NPC.Center.Y, -8, 0, ModContent.ProjectileType<GasBall>(), damage, 3f, Main.myPlayer);
                attack2 = 0;
                attack2Delay = attack2Reload;
            }
            if (attack2 == 4)
                SoundEngine.PlaySound(SoundID.Item34, NPC.position);

        }


        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.expertMode)
            {
                int debuff = ModContent.BuffType<Steamy>();
                if (debuff >= 0)
                {
                    target.AddBuff(debuff, 90, true);
                }
            }
        }

        public override void OnKill()
        {
            LaugicalityPlayer modPlayer = LaugicalityPlayer.Get(Main.LocalPlayer);
            spawned = 0;
            LaugicalityWorld.downedSlybertron = true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var _Etheria = new LeadingConditionRule(new IDRNC(IDRNC.BossType.Etheria, true));

            _Etheria.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Etherworks>()));
            npcLoot.Add(_Etheria);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SteamBar>(), 1, 15, 30));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfFraught>(),1, 20, 40));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<SlybertronTreasureBag>(), 1));
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 499;
        }
        /*
        public override void FindFrame(int frameHeight)
        {
            if (npc.life < npc.lifeMax * .5)
            {
                npc.frame.Y = frameHeight;
            }
            else
            {
                npc.frame.Y = 0;
            }
        }*/


        /*
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}*/
    }
}
