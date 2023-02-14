using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIP_Plugin.Seriazable;

namespace VIP_Plugin.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class Blackout : ICommand
    {
        /// <inheritdoc/>
        public string Command { get; } = "blackout";

        /// <inheritdoc/>
        public string[] Aliases { get; } = new[] { "bl" };

        /// <inheritdoc/>
        public string Description { get; } = "Activate a blackout for the whole facility";

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (Round.IsStarted == false)
            {
                response = "Wait for the round to start";
                return false;
            }
            if (Round.ElapsedTime.TotalSeconds > PluginMain.SecondsAfterStart)
            {
                response = $"You can't use your VIP because {PluginMain.SecondsAfterStart} seconds already elapsed";
                return false;
            }
            if(PluginMain.IsBlackOut== true)
            {
                response = "Blackout is already in progress";
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



            PlayerData playerdata = Handelers.LoadUser.Nacist(player.UserId);

            if (playerdata.DateTime == DateTime.Today)
            {
                if (playerdata.Blackout >= viprole.Blackout)
                {
                    response = "You used your VIP too many times today";
                    return false;
                }

                Handelers.CreateUser.Ulozit(playerdata.Id, playerdata.ClassSpawn, playerdata.MtfChaos, playerdata.Blackout + 1);

                Map.TurnOffAllLights(99999999);

                if (PluginMain.GiveFlaslightInBlackout == true)
                {
                    foreach (Player playerlist in Player.List)
                    {
                        playerlist.AddItem(ItemType.Flashlight);
                    }
                }
                PluginMain.IsBlackOut = true;

                response = "Done";
                return true;
            }
            else
            {

                Handelers.CreateUser.Ulozit(playerdata.Id, 0, 0, 1);

                Map.TurnOffAllLights(99999999);

                if (PluginMain.GiveFlaslightInBlackout == true)
                {
                    foreach (Player playerlist in Player.List)
                    {
                        playerlist.AddItem(ItemType.Flashlight);
                    }
                }

                PluginMain.IsBlackOut = true;

                response = "Done";
                return true;
            }
        }

        }
}
