using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using PlayerRoles;
using RemoteAdmin;
using VIP_Plugin.Seriazable;

namespace VIP_Plugin.Commands
{

    [CommandHandler(typeof(ClientCommandHandler))]
    public class Spawn : ICommand
    {
        /// <inheritdoc/>
        public string Command { get; } = "spawn";

        /// <inheritdoc/>
        public string[] Aliases { get; } = new[] { "spawn"};

        /// <inheritdoc/>
        public string Description { get; } = "Spawn VIP as a specific role: spawn <Role name>";

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if(Round.IsStarted == false)
            {
                response = "Wait for the round to start";
                return false;
            }
            if(Round.ElapsedTime.TotalSeconds > PluginMain.SecondsAfterStart)
            {
                response = $"You can't use your VIP because {PluginMain.SecondsAfterStart} seconds already elapsed";
                return false;
            }


            Player player = Player.Get(sender);
            VIPRoles viprole = new VIPRoles();

            foreach (VIPRoles viproles in PluginMain.viproles)
            {
                if(viproles.RoleName == player.GroupName)
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
            if (viprole.ClassSpawn == 0)
            {
                response = "This server disabled this feature";
                return false;
            }


            if (arguments.Array.Length < 2)
            {
                response = "You didn't entered class name";
                return false;
            }

            RoleTypeId Role;
            switch (arguments.Array[1])
            {
                case "scp049":
                    Role = RoleTypeId.Scp049;
                    break;
                case "scp079":
                    Role = RoleTypeId.Scp079;
                    break;
                case "scp096":
                    Role = RoleTypeId.Scp096;
                    break;
                case "scp106":
                    Role = RoleTypeId.Scp106;
                    break;
                case "scp173":
                    Role = RoleTypeId.Scp173;
                    break;
                case "scp939":
                    Role = RoleTypeId.Scp939;
                    break;
                case "guard":
                    Role = RoleTypeId.FacilityGuard;
                    break;
                case "classd":
                    Role = RoleTypeId.ClassD;
                    break;
                case "scientist":
                    Role = RoleTypeId.Scientist;
                    break;
                default:
                    response = "You entered non existing class";
                    return false;
            }

            PlayerData playerdata = Handelers.LoadUser.Nacist(player.UserId);

            if(playerdata.DateTime == DateTime.Today)
            {
                if(playerdata.ClassSpawn >= viprole.ClassSpawn)
                {
                    response = "You used your VIP too many times today";
                    return false;
                }

                Handelers.CreateUser.Ulozit(playerdata.Id, playerdata.ClassSpawn + 1, playerdata.MtfChaos, playerdata.Blackout);

                player.Role.Set(Role);

                response = "Done";
                return true;
            }
            else
            {

                Handelers.CreateUser.Ulozit(playerdata.Id, 1, 0, 0);

                player.Role.Set(Role);

                response = "Done";
                return true;
            }
        }

        }
}
