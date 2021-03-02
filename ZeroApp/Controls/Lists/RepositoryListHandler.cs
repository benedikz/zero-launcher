using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroApp.Controls.Lists
{
    public class RepositoryListHandler
    {
        public readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"Addons\";

        public List<RepositoryObjectModel.Repository> GetAllRepositories(string path)
        {
            try
            {
                var m_Manager = new Mods();
                
                List<RepositoryObjectModel.Repository> repos = new List<RepositoryObjectModel.Repository>();

                foreach (string subfolder in Directory.GetDirectories(path))
                {
                    if (subfolder.Contains("#"))
                    {
                        string pack = subfolder.Remove(0, path.Length);
                        //Trace.WriteLine(subfolder + " " + pack);

                        repos.Add(m_Manager.GetModpack(pack));
                    }
                    else
                    {
                        continue;
                    }
                }

                return repos;
            }
            catch (Exception e)
            {
                Trace.WriteLine("[GetAllRepositories] Task Failed :: Exception [" + e + "] thrown.");
            }

            return new List<RepositoryObjectModel.Repository>();
        }
    }
}
