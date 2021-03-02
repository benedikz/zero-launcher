using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroApp.ZeroCore
{
    class UserConfig
    {
        public ConfigObjectModel.Keys GetKeys(string user)
        {
            var f_Synchronization = new FileSynchronization();
            string u_Config = f_Synchronization.ReadIndex(AppDomain.CurrentDomain.BaseDirectory + @"Data\Client\" + user + ".Config.xml");

            // Process XML into Object
            var xmlDeserializer = new XMLSerialization();
            // using ParametersObjectModel.Parameters;
            ConfigObjectModel.Keys uc = xmlDeserializer.Deserialize<ConfigObjectModel.Keys>(u_Config);

            return uc;
        }

        public void SetKeys(string user, ConfigObjectModel.Keys keys)
        {
            var f_Synchronization = new FileSynchronization();
            var xmlSerializer = new XMLSerialization();

            string uc_XML = xmlSerializer.Serialize<ConfigObjectModel.Keys>(keys);

            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"Data\Client\" + user + ".Config.xml"))
            {
                sw.WriteLine(uc_XML);
            }
        }

        public string GetSavedUser(ConfigObjectModel.Keys userconfig)
        {
            if ( userconfig.Key[3].Value != "" )
            {
                return userconfig.Key[3].Value;
            }
            return "OFFLINE";
        }
    }
}
