using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace VIP_Plugin
{
    public class VIPRoles
    {
        public string RoleName { get; set; }
        public int ClassSpawn { get; set; }
        public int MtfChaos { get; set; }

        public int Blackout { get; set; }
    }


    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = true;

        [Description("Configure how many seconds players have to choose their VIP advantage after round starts (Doesnt apply on force wave)")]
        public int TimeAfterStart { get; set; } = 15;

        [Description("Give player flashlight when they spawn in blackout")]
        public bool GiveFlashlightInBlackout { get; set; } = true;

        [Description("Path to databases")]
        public string DatabasePath { get; set; } = Directory.GetCurrentDirectory();

        [Description("Configure your VIP roles")]
        public List<VIPRoles> VIPRoles { get; private set; } = new List<VIPRoles>
        {
           new VIPRoles()
           {
               RoleName="CHAD",
               ClassSpawn=1,
               MtfChaos=2,
               Blackout=3,
           }

        };

    }
}
