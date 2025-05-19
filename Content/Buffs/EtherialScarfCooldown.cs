using Terraria;
using Laugicality.Utilities.Base;
using Laugicality.Utilities.Players;
using Terraria.ID;

namespace Laugicality.Content.Buffs
{
	public class EtherialScarfCooldown : LaugicalityBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fragmented Soul");
			// Description.SetDefault("Your Etherial Scarf has to regenerate before it can save you again.");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
        

		public override void Update(Player player, ref int buffIndex)
		{
			LaugicalityPlayer.Get(player).EtherialScarfCooldown = true;
		}
	}
}
