using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace VIP_Plugin.Handelers
{
    internal class CreateUser
    {
        public static void Ulozit(string Id, int classspawn, int mtfchaos, int blackout)
        {
            if(Directory.Exists(PluginMain.DatabasePath + "/database/") == false)
            {
                Directory.CreateDirectory(PluginMain.DatabasePath + "/database/");
            }

            string dir = PluginMain.DatabasePath + $"/database/{Id}";

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(dir, FileMode.Create);



            Seriazable.PlayerData data = new Seriazable.PlayerData(Id, classspawn, mtfchaos, blackout, DateTime.Today);
            formatter.Serialize(stream, data);
            stream.Close();
        }



    }
}
