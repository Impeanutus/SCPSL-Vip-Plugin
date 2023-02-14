using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;

namespace VIP_Plugin.Handelers
{
    internal class BlackoutHandeler
    {
        public void OnWaitingForPlayers()
        {
            PluginMain.IsBlackOut = false;
        }

        public void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            if (PluginMain.GiveFlaslightInBlackout == true && PluginMain.IsBlackOut == true)
            {
            Timing.CallDelayed(2, () =>
            {
                foreach (Player player in ev.Players)
                {
                    player.AddItem(ItemType.Flashlight);
                }
            });
            }
        }

    }
}
