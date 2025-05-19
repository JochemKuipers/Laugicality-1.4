using Terraria;
using Terraria.ID;
using System.IO;
using Laugicality.Content.Items.Loot;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace Laugicality.Content.NPCs.Obsidium
{
	class MoltiochHead : Moltioch
    {
		public override string Texture { get { return "Laugicality/Content/NPCs/Obsidium/MoltiochHead"; } }

		public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.width = 110;
            NPC.height = 110;
            NPC.damage = 60;
            NPC.defense = 12;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.buffImmune[24] = true;
        }

		public override void Init()
		{
			base.Init();
			head = true;
		}
        /*
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (LaugicalityWorld.obsidiumTiles > 150 && LaugicalityWorld.downedRagnar && Main.hardMode)
                return SpawnCondition.Cavern.Chance * 0.25f;
            else return 0f;
        }*/

        int _attackCounter = 0;

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(_attackCounter);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			_attackCounter = reader.ReadInt32();
		}

		public override void CustomBehavior()
		{

		}
	}

	class MoltiochBody : Moltioch
    {
		public override string Texture { get { return "Laugicality/Content/NPCs/Obsidium/MoltiochBody"; } }

		public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.width = 110;
            NPC.height = 110;
            NPC.damage = 30;
            NPC.defense = 38;
            NPC.lifeMax = 500;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.buffImmune[24] = true;
        }
    }

	class MoltiochTail : Moltioch
    {
		public override string Texture { get { return "Laugicality/Content/NPCs/Obsidium/MoltiochTail"; } }

		public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.width = 110;
            NPC.height = 110;
            NPC.damage = 40;
            NPC.defense = 28;
            NPC.lifeMax = 4000;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.buffImmune[24] = true;
        }

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}
    
	public abstract class Moltioch : Worm
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Moltioch");
		}

		public override void Init()
		{
			minLength = 9;
			maxLength = 12;
			tailType = ModContent.NPCType<MoltiochTail>();
			bodyType = ModContent.NPCType<MoltiochBody>();
			headType = ModContent.NPCType<MoltiochHead>();
			speed = 20f;
			turnSpeed = 0.2f;
            NPC.buffImmune[24] = true;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MagmaticCrystal>(), 1));
        }
    }
}