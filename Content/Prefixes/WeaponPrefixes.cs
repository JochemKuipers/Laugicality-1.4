﻿using Terraria.ModLoader;

namespace Laugicality.Content.Prefixes
{
    public class CarefulPrefix : ModPrefix
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Careful");
        }
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 1.33333f;
            useTimeMult = 1.15f;
            shootSpeedMult = 1.15f;
            critBonus = 15;
        }
    }
    public class KnowledgeablePrefix : ModPrefix
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Knowledgeable");
        }
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            manaMult = .2f;
            critBonus = 20;
        }
    }
    public class HallowedPrefix : ModPrefix
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hallowed");
        }
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            useTimeMult = .65f;
            critBonus = 15;
        }
    }
    public class ColossalPrefix : ModPrefix
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Colossal");
        }
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            scaleMult = 1.5f;
            damageMult = 1.25f;
            useTimeMult = 1.25f;
        }
    }   
}