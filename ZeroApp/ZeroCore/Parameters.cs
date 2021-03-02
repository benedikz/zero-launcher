using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroApp.ZeroCore
{
    class Parameters
    {
        public ParametersObjectModel.Parameters GetParameters(string app_name)
        {
            var f_Synchronization = new FileSynchronization();

            // Read "Data/%PACK_NAME%/App.Parameters.xml
            string p_Source = f_Synchronization.ReadIndex(AppDomain.CurrentDomain.BaseDirectory + @"Data\Apps\" + app_name + @"\App.Parameters.xml");

            // Process XML into Object
            var xmlDeserializer = new XMLSerialization();
            // using ParametersObjectModel.Parameters;
            ParametersObjectModel.Parameters p = xmlDeserializer.Deserialize<ParametersObjectModel.Parameters>(p_Source);

            return p;
        }

        public void SetParameters(string app_name, ParametersObjectModel.Parameters parameters)
        {
            var f_Synchronization = new FileSynchronization();

            var xmlDeserializer = new XMLSerialization();

            string p_XML = xmlDeserializer.Serialize<ParametersObjectModel.Parameters>(parameters);

            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"Data\Apps\" + app_name + @"\App.Parameters.xml"))
            {
                sw.WriteLine(p_XML);
            }
        }
    }
}
