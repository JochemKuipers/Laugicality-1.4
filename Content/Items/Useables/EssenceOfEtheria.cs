using Laugicality.Content.Dusts;
using Laugicality.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Laugicality.Utilities.Base;
using Laugicality.Utilities.Players;
using Laugicality.Utilities;
using Terraria.Audio;

namespace Laugicality.Content.Items.Useables
{
    public class EssenceOfEtheria : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The High Priestess");
            // Tooltip.SetDefault("Allows you to enter and leave the Etherial at will, as long as no powerful creatures are present to stop you. \nCan place shadows of itself, which can be wired to enter the Etherial. \nAn odd number of wires need to be connected, or it will immediately switch back.");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 100;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.expert = true;
            Item.createTile = ModContent.TileType<HighPriestess>();
        }

        public override bool CanUseItem(Player player)
        {
            bool boss = false;
            for(int i = 0;  i < 200; i++)
            {
                if (Main.npc[i].boss && Main.npc[i].active)
                    boss = true;
            }
            return !boss;
        }

        public override bool? UseItem(Player player)
        {
            LaugicalityPlayer modPlayer = LaugicalityPlayer.Get(player);
            LaugicalityWorld.downedEtheria = !LaugicalityWorld.downedEtheria;
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            modPlayer.etherialTrail = 80;
            SoundEngine.PlaySound(new SoundStyle("Laugicality/Sounds/EtherialChange"), Item.position);
            return true;
        }
    }
}