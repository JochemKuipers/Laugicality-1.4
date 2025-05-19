using System;
using System.Collections.Generic;
using Laugicality.Content.Buffs;
using Laugicality.Content.Dusts;
using Laugicality.Content.Items.Consumables;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Laugicality.Content.Items.Weapons.Mystic;
using Laugicality.Content.Items.Loot;
using Laugicality.Content.Items.Materials;
using Laugicality.Content.NPCs.Obsidium;
using Laugicality.Content.Projectiles.Mystic.Conjuration;
using Laugicality.Content.Projectiles.Plague;
using Laugicality.Content.Projectiles.Ranged;
using Bubble = Laugicality.Content.Projectiles.Plague.Bubble;
using Laugicality.Utilities;
using Laugicality.Utilities.Players;
using Terraria.GameContent.ItemDropRules;
using static Laugicality.Utilities.BiomeConditions;

namespace Laugicality.Utilities.Globals
{
    public partial class LaugicalGlobalNPCs : GlobalNPC
    {
        public bool eFied = false;
        public bool mFied = false;//Mystified
        public bool hermes = false;
        public float mysticDamage = 1f;
        public int mysticCrit = 4;
        public bool ethSpawn = false;
        public bool lovestruck = false;
        public bool frigid = false;
        public bool bubbly = false;
        public bool dawn = false;
        public bool trueDawn = false;
        private int _dmg = 0;
        public int plays = 0;
        public int dmg2 = 0;
        public static int zTime = 0;
        public int zTimeInstanced = 0;
        public bool zImmune = false;
        public float xTemp = 0;
        public float yTemp = 0;
        public bool invin = false;
        public bool spored = false;
        public bool furious = false;
        public bool slimed = false;
        public bool frostbite = false;
        public bool spooked = false;
        public bool steamified = false;
        public bool incineration = false;
        public float damageMult = 1f;
        public int attacker = -1;
        public float DebuffDamageMult { get; set; } = 1f;
        public int JunglePlagueDuration { get; set; } = 0;
        public bool JunglePlague { get; set; } = false;
        public bool Orbital { get; set; } = false;

        public override void SetDefaults(NPC npc)
        {
            incineration = false;
            steamified = false;
            trueDawn = false;
            dawn = false;
            bubbly = false;
            spooked = false;
            frostbite = false;
            slimed = false;
            furious = false;
            spored = false;
            plays = 0;
            _dmg = 0;
            invin = npc.dontTakeDamage;
            dmg2 = npc.damage;
            damageMult = npc.takenDamageMultiplier;

            if (LaugicalityVars.zNPCs.Contains(npc.type))
            {
                zImmune = true;
            }
        }

        public override void ResetEffects(NPC npc)
        {
            incineration = false;
            steamified = false;
            trueDawn = false;
            dawn = false;
            bubbly = false;
            spooked = false;
            frostbite = false;
            slimed = false;
            furious = false;
            spored = false;
            eFied = false;
            mFied = false;
            hermes = false;
            lovestruck = false;
            frigid = false;
            mysticCrit = 4;
            Orbital = false;
            JunglePlague = false;

            npc.takenDamageMultiplier = damageMult;
            if (zTimeInstanced < zTime)
                zTimeInstanced = zTime;

            if (zTime > 0)
                zTime--;

            if (zTimeInstanced > 0)
                zTimeInstanced--;

            if (JunglePlagueDuration > 0)
                JunglePlagueDuration--;
        }

        public override void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            plays = numPlayers;
            dmg2 = npc.damage;
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (LaugicalityPlayer.Get(spawnInfo.Player).zoneObsidium)
            {
                pool.Clear();
                float spawnMod = .25f;
                if (!Main.hardMode)
                {
                    pool.Add(ModContent.NPCType<ObsidiumSkull>(), 0.10f * spawnMod);
                    //pool.Add(ModContent.NPCType<ObsidiumDriller>(), 0.05f * spawnMod);
                    pool.Add(NPCID.Skeleton, 0.25f * spawnMod);
                    pool.Add(NPCID.BlackSlime, 0.2f * spawnMod);
                    pool.Add(NPCID.MotherSlime, 0.2f * spawnMod);
                    pool.Add(NPCID.Hellbat, 0.2f * spawnMod);

                    if (LaugicalityWorld.downedRagnar)
                    {
                        pool.Add(ModContent.NPCType<MagmatipedeHead>(), 0.05f * spawnMod);
                        //pool.Add(ModContent.NPCType<MagmaCaster>(), 0.30f * spawnMod);
                    }
                }
                else
                {
                    pool.Add(ModContent.NPCType<ObsidiumSkull>(), 0.05f * spawnMod);
                    //pool.Add(ModContent.NPCType<MoltenSlime>(), 0.2f * spawnMod);
                    pool.Add(ModContent.NPCType<MoltiochHead>(), 0.015f * spawnMod);
                    pool.Add(ModContent.NPCType<MoltenSoul>(), 0.015f * spawnMod);
                    pool.Add(NPCID.SkeletonArcher, 0.25f * spawnMod);
                    pool.Add(NPCID.GiantBat, 0.25f * spawnMod);
                    //pool.Add(NPCID.Giant, 0.25f * spawnMod);
                    if (LaugicalityWorld.downedRagnar)
                    {
                        pool.Add(ModContent.NPCType<MagmatipedeHead>(), 0.015f * spawnMod);
                        //pool.Add(ModContent.NPCType<MagmaCaster>(), 0.20f * spawnMod);
                        pool.Add(ModContent.NPCType<LavaTitan>(), 0.01f * spawnMod);
                    }
                }
            }
            return;
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            if (npc.type == NPCID.ScorpionBlack && LaugicalityWorld.downedEtheria)
                chat = "Why hello there.";
            base.GetChat(npc, ref chat);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (spored)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= (int)(2);
                if (damage < 2)
                {
                    damage = (2);
                }
            }
            if (slimed)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= (int)(2);
                if (damage < 2)
                {
                    damage = (2);
                }
            }
            if (furious)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= (int)(8);
                if (damage < 8)
                {
                    damage = (8);
                }
            }
            if (incineration)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= (int)(8);
                if (damage < 8)
                {
                    damage = (8);
                }
            }
            if (hermes)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= (int)(4);
                if (damage < 4)
                {
                    damage = (4);
                }
            }

            if (spooked)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= (int)(24);
                if (damage < 24)
                {
                    damage = (24);
                }
            }

            if (frostbite)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= (int)(320);
                if (damage < 320)
                {
                    damage = (320);
                }
            }
            if (steamified)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= (int)(80);
                if (damage < 80)
                {
                    damage = (80);
                }
            }
            if (npc.boss == false)
            {
                if (frigid)
                {
                    npc.velocity.X *= 0;
                    npc.velocity.Y *= 0;
                }
            }
            if(bubbly)
            {
                if (Main.rand.Next(1 * 60) == 0 && Main.netMode != 1)
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (-1 + 2 * Main.rand.Next(2)) * 4, Main.rand.Next(-5, 2), ModContent.ProjectileType<Bubble>(), 20, 3f, Main.myPlayer);
            }
            if (dawn)
            {
                if (Main.rand.Next(1 * 60) == 0 && Main.netMode != 1)
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (-1 + 2 * Main.rand.Next(2)) * 4, Main.rand.Next(-5, 2), ModContent.ProjectileType<GoldenBubble>(), 20, 3f, Main.myPlayer);
            }
            if (trueDawn)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= (int)(24);
                if (damage < 24)
                {
                    damage = (24);
                }
                if (Main.rand.Next(1 * 60) == 0 && Main.netMode != 1)
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (-1 + 2 * Main.rand.Next(2)) * 4, Main.rand.Next(-5, 2), ModContent.ProjectileType<TrueDawnSpark>(), 40, 3f, Main.myPlayer);
            }
            if (JunglePlague)
            {
                if (Main.rand.Next(1 * 60) == 0 && Main.netMode != 1)
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (-1 + 2 * Main.rand.Next(2)) * 4, Main.rand.Next(-5, 2), ModContent.ProjectileType<JunglePlagueSpore>(), 75, 3f, Main.myPlayer);
            }
            if (Orbital)
            {
                npc.knockBackResist = -5f;
            }
            if(DebuffDamageMult > 1)
            {
                npc.lifeRegen = (int)(npc.lifeRegen * DebuffDamageMult);
            }
            if (damage < npc.lifeRegen)
                damage = npc.lifeRegen;
        }
        public override bool PreAI(NPC npc)
        {
            if (zTimeInstanced > 0 && zImmune == false)
                return false;
            return true;
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (eFied)
            {
                if (Main.rand.Next(13) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<TrainSteam>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.8f, 0.8f);
            }
            if (spored)
            {
                if (Main.rand.Next(13) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<ShroomDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.8f, 0.8f);
            }
            if (slimed)
            {
                if (Main.rand.Next(13) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 116, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                //Lighting.AddLight(npc.position, 0.1f, 0.8f, 0.8f);
            }
            if (mFied)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<Lightning>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.8f, 0.8f);
            }
            if (incineration)
            {
                if (Main.rand.Next(8) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<Magma>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.8f, 0.4f, 0f);
            }
            if (hermes)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<HermesDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.8f, 0.8f);
            }
            if (lovestruck && !npc.boss)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<HeartDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.8f, 0.8f);
            }
            if (frigid)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<Frost>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.8f, 0.8f);
            }

            if (furious)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<Magma>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.8f, 0.2f);
            }
            if (bubbly)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<Content.Dusts.Bubble>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 2f;
                    }
                }
            }
            if (dawn || trueDawn)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<GoldenBubbleDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 2f;
                    }
                }
            }
            if (frostbite)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<EtherialDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.0f, 0.4f, 0.6f);
            }
            if (steamified)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<Steam>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.4f, 0.4f, 0.4f);
            }
            if (spooked)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<SpookedDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.4f, 0.0f, 0.4f);
            }
            if (Orbital)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<SpookedDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.4f, 0.0f, 0.4f);
            }
        }

        public override void PostAI(NPC npc)
        {
            //Za Warudo
            if ((zTimeInstanced > 0 && zImmune == false) || frigid)
            {
                npc.velocity.X *= 0;
                npc.velocity.Y *= 0;
                if (xTemp == 0 || yTemp == 0)
                {
                    xTemp = npc.position.X;
                    yTemp = npc.position.Y;
                }
                else
                {
                    npc.position.X = xTemp;
                    npc.position.Y = yTemp;
                }
            }
            else
            {
                xTemp = 0;
                yTemp = 0;
            }


            if (npc.life > npc.lifeMax)
                npc.lifeMax = npc.life;

            //Za Warudo
            if (zTimeInstanced > 0 && zImmune == false)
            {
                npc.velocity.X *= 0;
                npc.velocity.Y *= 0;
            }

        }

        public override void OnKill(NPC npc)
        {
            //Debuffs
            if (furious)
            {
                if (Main.netMode != 1)
                {
                    float mag = 6f;
                    float theta2 = (float)(Main.rand.NextDouble() * 2 * Math.PI);
                    int damage = 80;
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (float)Math.Cos(theta2) * mag, (float)Math.Sin(theta2) * mag, ModContent.ProjectileType<ObsidiumArrowHead>(), damage, 3f, Main.myPlayer);
                    theta2 = (float)(Main.rand.NextDouble() * 2 * Math.PI);
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (float)Math.Cos(theta2) * mag, (float)Math.Sin(theta2) * mag, ModContent.ProjectileType<ObsidiumArrowHead>(), damage, 3f, Main.myPlayer);
                    theta2 = (float)(Main.rand.NextDouble() * 2 * Math.PI);
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (float)Math.Cos(theta2) * mag, (float)Math.Sin(theta2) * mag, ModContent.ProjectileType<ObsidiumArrowHead>(), damage, 3f, Main.myPlayer);
                    theta2 = (float)(Main.rand.NextDouble() * 2 * Math.PI);
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (float)Math.Cos(theta2) * mag, (float)Math.Sin(theta2) * mag, ModContent.ProjectileType<ObsidiumArrowHead>(), damage, 3f, Main.myPlayer);
                    theta2 = (float)(Main.rand.NextDouble() * 2 * Math.PI);
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (float)Math.Cos(theta2) * mag, (float)Math.Sin(theta2) * mag, ModContent.ProjectileType<ObsidiumArrowHead>(), damage, 3f, Main.myPlayer);
                }
            }
            if (bubbly)
            {
                if (Main.netMode != 1)
                {
                    int rand = Main.rand.Next(3, 7);
                    for (int i = 0; i < rand; i++)
                        Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (-1 + 2 * Main.rand.Next(2)) * 4, Main.rand.Next(-5, 2), ModContent.ProjectileType<Bubble>(), 20, 3f, Main.myPlayer);
                }
            }
            if (dawn)
            {
                if (Main.netMode != 1)
                {
                    int rand = Main.rand.Next(3, 7);
                    for (int i = 0; i < rand; i++)
                    {
                        float mag = 8f;
                        float theta2 = (float)(Main.rand.NextDouble() * 2 * Math.PI);
                        int damage = 32;
                        Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (float)Math.Cos(theta2) * mag, (float)Math.Sin(theta2) * mag, ModContent.ProjectileType<DawnSpark>(), damage, 3f, Main.myPlayer);
                    }
                }
            }
            if (trueDawn)
            {
                if (Main.netMode != 1)
                {
                    int rand = Main.rand.Next(4, 9);
                    for (int i = 0; i < rand; i++)
                    {
                        float mag = 8f;
                        float theta2 = (float)(Main.rand.NextDouble() * 2 * Math.PI);
                        int damage = 45;
                        Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (float)Math.Cos(theta2) * mag, (float)Math.Sin(theta2) * mag, ModContent.ProjectileType<TrueDawnSpark>(), damage, 3f, Main.myPlayer);
                    }
                }
            }
            if (JunglePlague)
            {
                if (Main.netMode != 1)
                {
                    int rand = Main.rand.Next(3, 7);
                    for (int i = 0; i < rand; i++)
                        Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (-1 + 2 * Main.rand.Next(2)) * 4, Main.rand.Next(-5, 2), ModContent.ProjectileType<JunglePlagueSporeSpread>(), 75, 3f, Main.myPlayer);
                }
            }
            if (steamified)
            {
                if (Main.netMode != 1)
                {
                    int rand = Main.rand.Next(4, 7);
                    for (int i = 0; i < rand; i++)
                    {
                        float mag = 8f;
                        float theta2 = (float)(Main.rand.NextDouble() * 2 * Math.PI);
                        int damage = 1000;
                        Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, (float)Math.Cos(theta2) * mag, (float)Math.Sin(theta2) * mag, ModContent.ProjectileType<VulcanConjuration>(), damage, 3f, Main.myPlayer);
                    }
                }
            }
            if (attacker != -1)
            {
                if (LaugicalityPlayer.Get(Main.player[attacker]).EtherCog)
                {
                    Main.player[attacker].AddBuff(ModContent.BuffType<Annihilation>(), 10 * 60, false);
                    LaugicalityPlayer.Get(Main.player[attacker]).AnnihilationDamageBoost += .2f;
                }
            }
            if (plays == 0)
                plays = 1;
            if (lovestruck && !npc.boss)
            {
                Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 58); //Drop Hearts
                if (Main.rand.Next(1, 3) == 1)
                {
                    Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 58);
                }
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            var HellHardmode = new LeadingConditionRule(new BiomeConditions.InMultipleBiomes(
            new IDRNC(IDRNC.BossType.WallOfFlesh, true),
            new BiomeConditions.Underworld()));

            var SkyHardmode = new LeadingConditionRule(new BiomeConditions.InMultipleBiomes(
            new IDRNC(IDRNC.BossType.WallOfFlesh, true),
            new BiomeConditions.Sky()));

            var _zoneObsidiumHardmode = new LeadingConditionRule(new BiomeConditions.InMultipleBiomes(
            new IDRNC(IDRNC.BossType.WallOfFlesh, true),
            new InModBiome<ZoneObsidium>()));

            //Soul Drops
            SkyHardmode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SoulOfSought>(), 3));
            npcLoot.Add(SkyHardmode);

            HellHardmode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SoulOfHaught>(), 3));
            npcLoot.Add(HellHardmode);

            _zoneObsidiumHardmode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SoulOfHaught>(), 3));
            npcLoot.Add(_zoneObsidiumHardmode);

            //Misc Materials
            if (npc.type == 113)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NullShard>(), 1, 1, 4));
            }
            if (npc.type == 4)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TastyMorsel>()));
            }
            if (npc.type == NPCID.IceQueen)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoyalIce>()));
            }
            if (npc.type == NPCID.GoblinSummoner)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Shadowflame>(), 1, 2, 5));
            }
            if (npc.type == NPCID.Mothron)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SaturnsRings>(), 4));
            }
        }
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
    }
}