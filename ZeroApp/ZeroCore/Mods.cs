using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static ZeroApp.RepositoryObjectModel;

namespace ZeroApp
{
    class Mods
    {
        /// <summary>
        /// VerifyAddons scans local "Addons" directory for modpacks and checks whether each one of them contain corresponding
        /// index and ~~mods~~. If index.xml is corrupt or missing, the modpack will be automatically deleted.
        /// </summary>
        public void VerifyModpacks()
        {
            try
            {
                string[] modpacks = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory +  @"Addons\", "#*", SearchOption.TopDirectoryOnly);
                Trace.WriteLine("[VerifyModpacks] Nalezeno " + modpacks.Length + " modpack(ů).");

                // If there is any modpack in the directory
                if (modpacks.Length != 0)
                {
                    foreach (string modpack in modpacks)
                    {
                        // If there is a modpack index in the directory
                        if (System.IO.File.Exists(modpack + @"\index.xml"))
                        {
                            Trace.WriteLine("[VerifyModpacks] Nalezen {index.xml} v modpacku: " + modpack);
                        }
                        else
                        {
                            Trace.WriteLine("[VerifyModpacks] Modpack " + modpack + " neobsahuje index. " + modpack + " bude smazán ...");
                            // V této části můžeme složku (balíček) smazat, protože bez indexu je v podstatě k ničemu a jen něco rozbije.
                            Directory.Delete(modpack, true);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("[VerifyModpacks] Task Failed :: Exception [" + e.ToString() + "] thrown.");
            }
        }

        /// <summary>
        /// Verification of specific mod's contents based upon index.xml. If local files do not match the size or Date of Last Edit, they will be downloaded again from Repo.
        /// </summary>
        /// <param name="pack_name"></param>
        /// <param name="index"></param>
        public void VerifyMod(string pack_name, int index)
        {
            Repository repo = GetModpack(pack_name);
            
            try
            {
                // If a folder of this mod identified by this index exists
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + pack_name + @"\" + repo.Addons.Addon[index].Name))
                {
                    Trace.WriteLine("Nalezen mod: " + repo.Addons.Addon[index].Name);

                    // Check all files mentioned within <Files>
                    for ( int i = 0; i < repo.Addons.Addon[index].Files.File.Count(); i++ )
                    {
                        bool exists = System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + pack_name + @"\" + repo.Addons.Addon[index].Files.File[i].Path);
                        //bool exists = files.Any(s => s.Contains(repo.Addons.Addon[index].Files.File[i].Path));
                        if (exists)
                        {
                            Trace.WriteLine(Path.GetFileName(repo.Addons.Addon[index].Files.File[i].Path) + " <- Existuje");
                        }
                        else
                        {
                            Trace.WriteLine(Path.GetFileName(repo.Addons.Addon[index].Files.File[i].Path) + " <- Neexistuje");

                            // Download file
                            DownloadFile(repo, index, i);
                        }

                        // If file's date is older or size is smaller or bigger than in actual index.xml, then redownload files or delete them
                        //Trace.WriteLine(" |- Velikost souboru na PC: " + new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + pack_name + @"\" + repo.Addons.Addon[index].Files.File[i].Path).Length + " bajtů");
                        //Trace.WriteLine(" |- Velikost v index.xml  : " + repo.Addons.Addon[index].Files.File[i].Size + " bajtů");
                        if ( new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + pack_name + @"\" + repo.Addons.Addon[index].Files.File[i].Path).Length == repo.Addons.Addon[index].Files.File[i].Size )
                        {
                            Trace.WriteLine(" |--- Velikost je stejná");

                            DateTime filemtime = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + pack_name + @"\" + repo.Addons.Addon[index].Files.File[i].Path).LastWriteTime;
                            DateTime i_filemtime = Static.TimestampToDate(repo.Addons.Addon[index].Files.File[i].LastChange);
                            //Trace.WriteLine(" |- Datum posledního zápisu v index.xml : " + i_filemtime.ToString("F"));
                            //Trace.WriteLine(" |- Datum posledního zápisu : " + filemtime.ToString("F"));
                            // Compare datetime of Last Write of file against datetime in index.xml
                            int diff = DateTime.Compare(filemtime, i_filemtime);

                            if (diff < 0)
                            {
                                Trace.WriteLine(" |--- Datum poslední úpravy je novější než u lokálního souboru");

                                // Delete and redownload from index.xml
                                DeleteFile(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + pack_name + @"\" + repo.Addons.Addon[index].Files.File[i].Path);
                                DownloadFile(repo, index, i);
                            }
                            else if (diff == 0) { Trace.WriteLine(" |--- Data poslední úpravy se shodují"); }
                            else
                            {
                                Trace.WriteLine(" |--- Index obsahuje starší datum úpravy než lokální soubor.");
                            }

                        }
                        else
                        {
                            Trace.WriteLine(" |--- Velikost je odlišná.");

                            // Delete and redownload from index.xml
                            DeleteFile(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + pack_name + @"\" + repo.Addons.Addon[index].Files.File[i].Path);
                            DownloadFile(repo, index, i);
                        }
                    }
                }
                else
                {
                    // Create directory
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + pack_name + @"\" + repo.Addons.Addon[index].Name);

                    // Download all files within mod
                    for ( int i = 0; i < repo.Addons.Addon[index].Files.File.Count; i++ )
                    {
                        Trace.WriteLine("Stahuji soubor: " + repo.Addons.Addon[index].Files.File[i].Path + " | " + Static.TimestampToDate(repo.Addons.Addon[index].Files.File[i].LastChange) + " | " + repo.Addons.Addon[index].Files.File[i].Size + " bytes") ;
                    }
                }

            }
            catch (Exception e)
            {
                Trace.WriteLine("[VerifyMod] Task Failed :: Exception [" + e.ToString() + "] thrown.");
            }
        }

        /// <summary>
        /// DeepClean method searches through repository for any files or folders, that should not be included
        /// in the Repository already and deletes them.
        /// </summary>
        /// <param name="pack_name"></param>
        public void GetAllFiles(string dir, RepositoryObjectModel.Repository repo)
        {
            try
            {
                List<string> file_list = new List<string>();
                List<string> excludes = GetFileExcludes(repo);

                string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + repo.Modpacks.Modpack.ID, "*.*", SearchOption.AllDirectories);
                foreach (string f in files)
                {
                    file_list.Add(f);
                }

                foreach (string file in file_list)
                {
                    if (!excludes.Contains(file))
                    {
                        Trace.WriteLine("Smazat " + file);
                        System.IO.File.Delete(file);
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("[DeepClean] Exception Occured when cleaning '" + dir + "' [\n" + e + "\n]");
            }
        }

        public List<string> GetFileExcludes(RepositoryObjectModel.Repository currentRepo)
        {
            List<string> files = new List<string>();

            for (int i = 0; i < currentRepo.Addons.Addon.Count; i++)
            {
                for (int j = 0; j < currentRepo.Addons.Addon[i].Files.File.Count; j++)
                {
                    files.Add(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + currentRepo.Modpacks.Modpack.ID + @"\" + currentRepo.Addons.Addon[i].Files.File[j].Path.Replace('/', '\\'));
                }
            }

            // Index anti-delete patch
            files.Add(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + currentRepo.Modpacks.Modpack.ID + @"\index.xml");

            return files;
        }

        public void DeleteFilesExcept(string directory, List<string> excludes)
        {
            var files = System.IO.Directory.GetFiles(directory).Where(x => !excludes.Contains(x));
            foreach (var file in files)
            {
                System.IO.File.Delete(file);
                Trace.WriteLine("[DeleteFilesExcept] Deleted " + file);
            }
        }

        public List<string> GetDirectoryExcludes(RepositoryObjectModel.Repository currentRepo)
        {
            List<string> directories = new List<string>();

            for (int i = 0; i < currentRepo.Addons.Addon.Count; i++)
            {
                directories.Add(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + currentRepo.Modpacks.Modpack.ID + @"\" + currentRepo.Addons.Addon[i].Name);
            }

            return directories;
        }

        public void DeleteDirectoriesExcept(string directory, List<string> excludes)
        {
            var dirs = System.IO.Directory.GetDirectories(directory).Where(x => !excludes.Contains(x));
            foreach (var dir in dirs)
            {
                System.IO.Directory.Delete(dir, true);
                Trace.WriteLine("[DeleteDirectoriesExcept] Deleted " + dir);
            }
        }

        /// <summary>
        /// LoadModpack processes an index.xml of specific pack, from which it downloads any mod within that index.
        /// </summary>
        /// <param name="pack_name"></param>
        /*
        public void LoadModpack(string pack_name)
        {
            var main = new LauncherWindow();
            Repository repo = GetModpack(pack_name);

            // Get all modpacks in ./Addons directory
            string[] packs = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + pack_name);
            int packs_count = packs.Length;

            // Check if every addon in index.xml is in pack directory

            for (int i = 0; i < repo.Addons.Addon.Count; i++)
            {
                bool exists = packs.Any(s => s.Contains(repo.Addons.Addon[i].Name));
                if (exists)
                {
                    Trace.WriteLine(repo.Addons.Addon[i].Name + " <- Existuje");
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.MainProgressBar.Value = i));
                }
                else
                {
                    Trace.WriteLine(repo.Addons.Addon[i].Name + " <- Neexistuje");

                    // Download this mod from URL in index.xml
                    DownloadMod(repo, i);
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.MainProgressBar.Value = i));
                }
            }
        }
        */

        /// <summary>
        /// Reads index.xml of specified pack, then creates and returns instance of Repository object
        /// </summary>
        /// <param name="pack_name"></param>
        /// <returns></returns>
        public RepositoryObjectModel.Repository GetModpack(string pack_name)
        {
            var f_Synchronization = new FileSynchronization();

            // Read "Addons/%PACK_NAME%/index.xml
            string l_Index = f_Synchronization.ReadIndex(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + pack_name + @"\index.xml");

            // Process string into data
            var xmlSerializer = new XMLSerialization();
            // using ZeroApp.RepositoryObjectModel;
            Repository repo = xmlSerializer.Deserialize<Repository>(l_Index);

            return repo;
        }

        /************ DOWNLOADING **********/
        
        /// <summary>
        /// Downloads index.xml from specified URL onto local ./Addons folder
        /// </summary>
        /// <param name="url">URL of Repository</param>
        public void DownloadRepository(string url)
        {
            var f_Synchronization = new FileSynchronization();
            var xmlSerializer = new XMLSerialization();

            string r_Index = f_Synchronization.DownloadIndex(url);
            Repository repo = xmlSerializer.Deserialize<Repository>(r_Index);

            // Create modpack folder
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + repo.Modpacks.Modpack.ID);
            // Download index.xml
            Web.DownloadFile(url, AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + repo.Modpacks.Modpack.ID + @"\index.xml");
            Trace.WriteLine("[DownloadRepository] Repo downloaded from: " + url);

        }

        /// <summary>
        /// Downloads a mod (creates folder and downloads all the mod's files) from specified repository with corresponding index
        /// </summary>
        /// <param name="repository">Instance name of repository</param>
        /// <param name="index">Int32 index is used as mod's ID number</param>
        public void DownloadMod(RepositoryObjectModel.Repository repository, int index)
        {
            string name = repository.Addons.Addon[index].Name; 

            try
            {
                // Create mod folder
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + repository.Modpacks.Modpack.ID + @"\" + name);
                // Download all files from mod
                for (int i = 0; i < repository.Addons.Addon[index].Files.File.Count; i++)
                {
                    DownloadFile(repository, index, i);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("[DownloadMod] Task Failed :: Exception [" + e.ToString() + "] thrown.");
            }
        }

        /// <summary>
        /// Downloads specific file of a mod
        /// </summary>
        /// <param name="repository">Object Repository (serialized index.xml)</param>
        /// <param name="addon_index">Index number of addon</param>
        /// <param name="file_index">Index number of file inside an addon[index]</param>
        public void DownloadFile(RepositoryObjectModel.Repository repository, int addon_index, int file_index)
        {
            try
            {
                // Create directories to this file & Download it
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + repository.Modpacks.Modpack.ID + @"\" + Path.GetDirectoryName(repository.Addons.Addon[addon_index].Files.File[file_index].Path));
                
                Trace.WriteLine("[DownloadFile] Downloading from: " + repository.Modpacks.Modpack.Source + @"/" + repository.Addons.Addon[addon_index].Files.File[file_index].Path);
                Trace.WriteLine("[DownloadFile] To -> " + AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + repository.Modpacks.Modpack.ID + @"\" + repository.Addons.Addon[addon_index].Files.File[file_index].Path.Replace('/', '\\'));
                
                string from = repository.Modpacks.Modpack.Source + @"/" + repository.Addons.Addon[addon_index].Files.File[file_index].Path;
                string to = AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + repository.Modpacks.Modpack.ID + @"\" + repository.Addons.Addon[addon_index].Files.File[file_index].Path.Replace('/', '\\');

                Web.DownloadFile(from, to);
            }
            catch (Exception e)
            {
                Trace.WriteLine("[DownloadMod] Task Failed :: Exception [" + e.ToString() + "] thrown.");
            }
        }

        public void DeleteFile(string path)
        {
            try
            {
                Trace.WriteLine("[DeleteFile] Deleting " + path);
                System.IO.File.Delete(path);
            }
            catch (Exception e)
            {
                Trace.WriteLine("[DeleteFile] Task Failed :: Exception [" + e.ToString() + "] thrown.");
            }   
        }
    }
}
