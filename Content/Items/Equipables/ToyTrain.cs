using Laugicality.Content.Buffs;
using Laugicality.Content.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Laugicality.Utilities.Base;
using Laugicality.Utilities.Players;

namespace Laugicality.Content.Items.Equipables
{
    public class ToyTrain : LaugicalityItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Miniature Train Whistle");
			// Tooltip.SetDefault("Summons a Toy Train that follows you");
		}

		public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.shoot = ModContent.ProjectileType<ToyTrainProjectile>();
			Item.buffType = ModContent.BuffType<ToyTrainBuff>();
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.UseSound = SoundID.Item79;
        }
        

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}
	}
}