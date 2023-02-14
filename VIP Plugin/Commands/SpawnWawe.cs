using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using RemoteAdmin;
using Respawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIP_Plugin.Seriazable;

namespace VIP_Plugin.Commands
{

    [CommandHandler(typeof(ClientCommandHandler))]
    internal class SpawnWawe : ICommand
    {
        /// <inheritdoc/>
        public string Command { get; } = "spawnwawe";

        /// <inheritdoc/>
        public string[] Aliases { get; } = new[] { "sw" };

        /// <inheritdoc/>
        public string Description { get; } = "Force spawn a specific wawe (MTF/Chaos)";

        /// <inheritdoc/>
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
       {
            if (Round.IsStarted == false)
            {
                response = "Wait for the round to start";
                return false;
            }

            Player player = Player.Get(sender);
            VIPRoles viprole = new VIPRoles();

            foreach (VIPRoles viproles in PluginMain.viproles)
            {
                if (viproles.RoleName == player.GroupName)
                {
                    viprole = viproles;
                    break;
                }
            }

            if (viprole.RoleName == null)
            {
                response = "You don't have VIP";
                return false;
            }

            if (viprole.MtfChaos == 0)
            {
                response = "This server disabled this feature";
                return false;
            }

            if (arguments.Array.Length < 2)
            {
                response = "You didn't entered team name";
                return false;
            }

            SpawnableTeamType spawnteam;

            switch (arguments.Array[1])
            {
                case "mtf":
                    spawnteam = SpawnableTeamType.NineTailedFox;
                    break;
                case "chaos":
                    spawnteam = SpawnableTeamType.ChaosInsurgency;
                    break;
                default:
                    response = "You entered non existing team";
                    return false;
            }

            PlayerData playerdata = Handelers.LoadUser.Nacist(player.UserId);

            if (playerdata.DateTime == DateTime.Today)
            {
                if (playerdata.MtfChaos >= viprole.MtfChaos)
                {
                    response = "You used your VIP too many times today";
                    return false;
                }

                Handelers.CreateUser.Ulozit(playerdata.Id, playerdata.ClassSpawn , playerdata.MtfChaos + 1, playerdata.Blackout);

                Respawn.ForceWave(spawnteam);

                response = "Done";
                return true;
            }
            else
            {

                Handelers.CreateUser.Ulozit(playerdata.Id, 0, 1, 0);

                Respawn.ForceWave(spawnteam);

                response = "Done";
                return true;
            }
        }
        }
}
