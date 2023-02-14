using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using VIP_Plugin.Seriazable;

namespace VIP_Plugin.Handelers
{
    internal class LoadUser
    {
        public static PlayerData Nacist(string Id)
        {
            string dir = PluginMain.DatabasePath + $"/database/{Id}";

            if (File.Exists(dir))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(dir, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;

                stream.Close();

                return data;
            }
            else
            {
                Seriazable.PlayerData playerdata = new Seriazable.PlayerData(Id, 0, 0, 0, DateTime.Today);

                return playerdata;
            }
        }

    }
}
