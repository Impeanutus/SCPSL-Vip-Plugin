using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIP_Plugin.Seriazable
{
    [Serializable]
    public class PlayerData
    {
        public string Id;
        public DateTime DateTime;
        public int ClassSpawn;
        public int MtfChaos;
        public int Blackout;

        public PlayerData(string id, int classspawn, int mtfchaos, int blackout, DateTime dateTime) 
        {
            Id = id;
            ClassSpawn = classspawn;
            MtfChaos= mtfchaos;
            Blackout = blackout;
            DateTime = dateTime;
        }
    }
}
