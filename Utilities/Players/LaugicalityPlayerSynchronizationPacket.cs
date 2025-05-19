﻿using System.IO;
using Laugicality.Utilities.Focuses;
using Terraria;
using Terraria.ID;
using WebmilioCommons.Packets;
using Laugicality.Utilities;

namespace Laugicality.Utilities.Players
{
    public class LaugicalityPlayerSynchronizationPacket : ModPlayerNetworkPacket<LaugicalityPlayer>
    {
        protected override bool PostReceive(BinaryReader reader, int fromWho)
        {
            if (!IsResponse && Main.netMode == NetmodeID.MultiplayerClient)
            {
                IsResponse = true;
                Send(Main.myPlayer, Player.whoAmI);
            }

            return base.PostReceive(reader, fromWho);
        }


        public string Focus
        {
            get => ModPlayer.Focus == null ? "" : ModPlayer.Focus.UnlocalizedName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                ModPlayer.Focus = FocusManager.Instance[value];
            }
        }

        public bool IsResponse { get; set; }
    }
}