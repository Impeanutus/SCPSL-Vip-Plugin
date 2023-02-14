using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Server = Exiled.Events.Handlers.Server;
using Mapa = Exiled.Events.Handlers.Map;

namespace VIP_Plugin
{
    public class PluginMain : Plugin<Config>
    {

        public static List<VIPRoles> viproles;
        public static int SecondsAfterStart;
        public static bool IsBlackOut;
        public static bool GiveFlaslightInBlackout;
        public static string DatabasePath;

        private Handelers.BlackoutHandeler blackoutHandeler;

        public override void OnEnabled()
        {
            viproles = Config.VIPRoles;
            SecondsAfterStart = Config.TimeAfterStart;
            blackoutHandeler = new Handelers.BlackoutHandeler();
            GiveFlaslightInBlackout = Config.GiveFlashlightInBlackout;
            DatabasePath = Config.DatabasePath;

            Enable();
        }

        public override void OnDisabled()
        {
            Disable();
        }

        public void Enable()
        {
            Server.WaitingForPlayers += blackoutHandeler.OnWaitingForPlayers;
            Server.RespawningTeam += blackoutHandeler.OnRespawningTeam;
        }

        public void Disable()
        {
            Server.WaitingForPlayers -= blackoutHandeler.OnWaitingForPlayers;
            Server.RespawningTeam -= blackoutHandeler.OnRespawningTeam;
        }
    }
}
