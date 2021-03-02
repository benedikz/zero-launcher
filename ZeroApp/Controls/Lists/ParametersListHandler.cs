using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroApp.ZeroCore;

namespace ZeroApp.Controls.Lists
{
    public class ParametersListHandler
    {
        public readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"Data\";

        public void GetAllParameters(ParametersObjectModel.Parameters source)
        {
            try
            {
                Trace.WriteLine(" Listing " + source.Parameter.Count() + " [PARAMETERS]");
                for (int i = 0; i < source.Parameter.Count(); i++)
                {
                    Trace.Write("[P] | " + source.Parameter[i].Active + " | " + source.Parameter[i].Key);
                    if (!(source.Parameter[i].Value == "null"))
                    {
                        Trace.Write(source.Parameter[i].Value + "\n");
                    }
                    else
                    {
                        Trace.Write("\n");
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("[GetAllParameters] Task Failed :: Exception [" + e + "] thrown.");
            }
        }

        public List<ParametersObjectModel.Parameter> GetParameters()
        {
            try
            {
                var p_Manager = new Parameters();

                List<ParametersObjectModel.Parameter> parameters = new List<ParametersObjectModel.Parameter>();

                ParametersObjectModel.Parameters p = p_Manager.GetParameters("Arma3");
                
                for (int i = 0; i < p.Parameter.Count(); i++)
                {
                    parameters.Add(p.Parameter[i]);
                }

                /*
                
                */

                return parameters;
            }
            catch (Exception e)
            {
                Trace.WriteLine("[GetParameters] Task Failed :: Exception [" + e + "] thrown.");
            }

            return new List<ParametersObjectModel.Parameter>();
        }
    }
}
