using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZeroApp.Controls.Lists;
using ZeroApp.Windows.Dialogs;
using ZeroApp.ZeroCore;

namespace ZeroApp
{
    /// <summary>
    /// Interakční logika pro LauncherWindow.xaml
    /// </summary>
    public partial class LauncherWindow : Window
    {
        public static string USER = "OFFLINE";

        public static bool isAuthenticated = false;

        //public static int currProgress = 0;

        // Observable Collections (Collections of UserControls in Lists)
        public ObservableCollection<ZeroApp.Controls.Lists.RepositoryListItem> Repos { get; set; }
        public ObservableCollection<ZeroApp.Controls.Lists.ModListItem> Mods { get; set; }
        public ObservableCollection<ZeroApp.Controls.Lists.ParametersListItem> Parameters { get; set; }

        // UNIVERSAL DECLARATION OF LIST OF PARAMETERS
        List<ParametersObjectModel.Parameter> parameters = new List<ParametersObjectModel.Parameter>();
        // UNIVERSAL DECLARATION OF REPOSITORY OBJECT
        List<RepositoryObjectModel.Repository> repos = new List<RepositoryObjectModel.Repository>();

        // Timestamp (STARTUP)
        readonly static Int32 startupTimestamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        public LauncherWindow()
        {
            InitializeComponent();
        }

        private void AppWindow_Loaded(object sender, RoutedEventArgs e)
        {   
            // Initialize Trace Listener
            Trace.Listeners.Add(new TextWriterTraceListener(".logs/zeroapp.log"));

            // Start Discord Rich Presence
            RPCManager.Initialize("638122033632509973");
            RPCManager.UpdatePresence("Maturita Edition", "Je v launcheru", startupTimestamp, 0, "infisync", "a", "test", "b");

            // Initialize Repositories List & Parameters List
            var uc_Handler = new UserConfig();
            var r_ListHandler = new ZeroApp.Controls.Lists.RepositoryListHandler();
            this.repos = r_ListHandler.GetAllRepositories(r_ListHandler.path);
            var p_ListHandler = new ZeroApp.Controls.Lists.ParametersListHandler();
            this.parameters = p_ListHandler.GetParameters();
            
            // Update GUI
            UpdateParamsList();
            UpdateRepoList();

            // Update USER in GUI
            if (uc_Handler.GetKeys("User").Key[2].Value != "" )
            {
                USER = uc_Handler.GetKeys("User").Key[2].Value;
                isAuthenticated = true;
            }

            // Visibility
            ParametersPanel.Visibility = Visibility.Visible;
            
            string key_IsSetup = uc_Handler.GetKeys("User").Key[0].Value;
            Trace.WriteLine("[IsSetUp] " + key_IsSetup);
            if (key_IsSetup == "false")
            {
                Dialog_Setup setupDialog = new Dialog_Setup();
                System.Windows.Media.Effects.BlurEffect blur = new System.Windows.Media.Effects.BlurEffect();
                this.Effect = blur;
                setupDialog.Owner = GetWindow(this);
                setupDialog.ShowDialog();
            }

            // Process Timer
            System.Timers.Timer p_Timer = new System.Timers.Timer();
            p_Timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            p_Timer.Interval = 7500;
            p_Timer.Enabled = true;
        }
        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if ( Process.GetProcessesByName("arma3_x64").Length > 0 )
            {
                RPCManager.UpdatePresence("Maturita Edition", "Hraje Arma 3 (x64)", startupTimestamp, 0, "infisync", "", "test", "");
                
            }

            if (Process.GetProcessesByName("arma3").Length > 0)
            {
                RPCManager.UpdatePresence("Maturita Edition", "Hraje Arma 3", startupTimestamp, 0, "infisync", "", "test", "");
            }

            if (Process.GetProcessesByName("arma3_be").Length > 0)
            {
                RPCManager.UpdatePresence("Maturita Edition", "Hraje Arma 3 (BattlEye)", startupTimestamp, 0, "infisync", "", "test", "");
            }
        }

        private void AppMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void AuthExitButton_Click(object sender, RoutedEventArgs e)
        {
            RPCManager.ShutdownPresence();
            Trace.Flush();
            this.Close();
            Application.Current.Shutdown();
        }

        private void WindowHeadbar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            // Ověření
            try
            {
                // Get UserConfig
                var uc_Handler = new UserConfig();
                ConfigObjectModel.Keys userConfig = uc_Handler.GetKeys("User");
                var p_Handler = new Parameters();
                ParametersObjectModel.Parameters parameters = p_Handler.GetParameters("Arma3");
                var m_Handler = new Mods();
                RepositoryObjectModel.Repository modpack = m_Handler.GetModpack(repos[ReposListBox.SelectedIndex].Modpacks.Modpack.ID);

                if ( userConfig.Key[2].Value.Length != 0 && userConfig.Key[3].Value.Length != 0 )
                {
                    string login = Authorization.Login(userConfig.Key[2].Value, userConfig.Key[3].Value);
                    if (login.Contains("\"token\""))
                    {
                        isAuthenticated = true;
                    }
                    else
                    {
                        Trace.WriteLine("INVALID CREDENTIALS " + login);

                        AuthWindow authDialog = new AuthWindow();
                        System.Windows.Media.Effects.BlurEffect blur = new System.Windows.Media.Effects.BlurEffect();
                        this.Effect = blur;
                        authDialog.Owner = Window.GetWindow(this);
                        authDialog.ShowDialog();
                    }
                } 
                else
                {
                    AuthWindow authDialog = new AuthWindow();
                    System.Windows.Media.Effects.BlurEffect blur = new System.Windows.Media.Effects.BlurEffect();
                    this.Effect = blur;
                    authDialog.Owner = Window.GetWindow(this);
                    authDialog.ShowDialog();
                }

                string startup_params = String.Empty;
                string mods = String.Empty;

                for (int i = 0; i < parameters.Parameter.Count(); i++)
                {
                    if (parameters.Parameter[i].Active == "true")
                    {
                        if (parameters.Parameter[i].Value != "null" && !(String.IsNullOrWhiteSpace(parameters.Parameter[i].Value)))
                        {
                            startup_params += parameters.Parameter[i].Key + parameters.Parameter[i].Value + " ";
                        }
                        else
                        {
                            startup_params += parameters.Parameter[i].Key + " ";
                        }
                    }
                }

                mods += "-mod=\"";
                for (int i = 0; i < modpack.Addons.Addon.Count(); i++)
                {
                    mods += AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + repos[ReposListBox.SelectedIndex].Modpacks.Modpack.ID + @"\" + modpack.Addons.Addon[i].Name + ";";
                }
                mods += "\"";

                // Set CurrentUser Label
                CurrentUser.Content = USER;

                string arguments;
                if ( !String.IsNullOrWhiteSpace(repos[ReposListBox.SelectedIndex].Modpacks.Modpack.IP) && !String.IsNullOrWhiteSpace(repos[ReposListBox.SelectedIndex].Modpacks.Modpack.Port) )
                {
                    string connect = "-connect=" + repos[ReposListBox.SelectedIndex].Modpacks.Modpack.IP + " -port=" + repos[ReposListBox.SelectedIndex].Modpacks.Modpack.Port;
                    if (ConnectCheckBox.IsChecked.Value == true)
                    {
                        arguments = mods + " " + "-name=" + USER + " " + startup_params + " " + connect;
                        Trace.WriteLine("[PLAY] Auto-connect to:\n" + repos[ReposListBox.SelectedIndex].Modpacks.Modpack.IP + ":" + repos[ReposListBox.SelectedIndex].Modpacks.Modpack.Port + "\n" + repos[ReposListBox.SelectedIndex].Modpacks.Modpack.Password);

                        if (repos[ReposListBox.SelectedIndex].Modpacks.Modpack.Password != "")
                        {
                            arguments += " -password=" + repos[ReposListBox.SelectedIndex].Modpacks.Modpack.Password;
                        }
                    }
                    else
                    {
                        arguments = mods + " " + "-name=" + USER + " " + startup_params;
                        Trace.WriteLine("[PLAY] Do Not Connect");
                    }
                }
                else
                {
                    arguments = mods + " " + "-name=" + USER + " " + startup_params;
                    Trace.WriteLine("[PLAY] Do Not Connect");
                }

                if ( isAuthenticated )
                {
                    Launch(userConfig.Key[1].Value, arguments);
                }
            }
            catch (Exception)
            {
                AuthWindow authDialog = new AuthWindow();
                System.Windows.Media.Effects.BlurEffect blur = new System.Windows.Media.Effects.BlurEffect();
                this.Effect = blur;
                authDialog.Owner = Window.GetWindow(this);
                authDialog.ShowDialog();
            }
        }

        private void Launch(string full_path, string arguments)
        {
            Trace.WriteLine("[USER] " + USER);
            
            Process process = new Process();
            process.StartInfo.FileName = System.IO.Path.GetFileName(full_path);
            process.StartInfo.Arguments = arguments;
            process.StartInfo.WorkingDirectory = full_path.Remove(full_path.Length - System.IO.Path.GetFileName(full_path).Length, System.IO.Path.GetFileName(full_path).Length);
            process.StartInfo.UseShellExecute = true;
            process.Start();
            
            Trace.WriteLine("#### LAUNCH ####\nSpouštěcí soubor: " + System.IO.Path.GetFileName(full_path) + "\nAdresář: " + full_path.Remove(full_path.Length - System.IO.Path.GetFileName(full_path).Length, System.IO.Path.GetFileName(full_path).Length) + "\nArgumenty:\n" + arguments);
            MessageBox.Show("#### LAUNCH ####\nSpouštěcí soubor: " + System.IO.Path.GetFileName(full_path) + "\nAdresář: " + full_path.Remove(full_path.Length - System.IO.Path.GetFileName(full_path).Length, System.IO.Path.GetFileName(full_path).Length) + "\nArgumenty:\n" + arguments);
        }

        private void syncButton_Click(object sender, RoutedEventArgs e)
        {
            this.MainProgressBar.Value = 0;
            this.MainProgressBar.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 174, 0));

            var thread = new Thread(Synchronize);
            thread.Start(ReposListBox.SelectedIndex);
        }

        private void Synchronize(object data)
        {
            try
            {
                var m_Manager = new Mods();
                var f_Synchronization = new FileSynchronization();
                var r_ListHandler = new RepositoryListHandler();

                //Načtení a ověření balíčku
                var index = (int)data;
                var max = (int)repos[index].Addons.Addon.Count;
                var current = (string)repos[index].Modpacks.Modpack.ID;
                this.SetProgressBarMax(max);

                m_Manager.DownloadRepository(repos[index].Modpacks.Modpack.Source + "/index.xml");
                m_Manager.VerifyModpacks();

                RepositoryObjectModel.Repository repo = m_Manager.GetModpack(current);

                // Get all modpacks in ./Addons directory
                string[] packs = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + current);
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
                        m_Manager.DownloadMod(repo, i);
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.MainProgressBar.Value = i));
                    }
                }

                // Mazání neexistujících modů v kořenu
                m_Manager.DeleteDirectoriesExcept(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + current, m_Manager.GetDirectoryExcludes(repo));
                // Mazání souborů v adresáři modů
                m_Manager.GetAllFiles(AppDomain.CurrentDomain.BaseDirectory + @"Addons\" + current, repo);

                for (int i = 0; i < repos[index].Addons.Addon.Count; i++)
                {
                    // Verify integrity of mod
                    m_Manager.VerifyMod(repos[index].Modpacks.Modpack.ID, i);

                    

                    this.SetProgressBarVal(i + 1);
                    this.CheckCompletedProgress((i + 1), max);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("[Synchronize] Task Failed :: Exception [" + ex + "] thrown.");
            }
        }

        private void SetProgressBarVal(int val)
        {
            Application.Current.Dispatcher.BeginInvoke(
              DispatcherPriority.Background,
              new Action(() => this.MainProgressBar.Value = val));
        }

        private void CheckCompletedProgress(int val, int max)
        {
            if (val == max)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.MainProgressBar.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(59, 179, 0))));
            }
        }

        private void SetProgressBarMax(int max)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.MainProgressBar.Maximum = max));
        }

        private void AddRepository_Click(object sender, RoutedEventArgs e)
        {
            Dialog_AddRepo addRepoDialog = new Dialog_AddRepo();
            System.Windows.Media.Effects.BlurEffect blur = new System.Windows.Media.Effects.BlurEffect();
            this.Effect = blur;
            addRepoDialog.Owner = GetWindow(this);
            addRepoDialog.ShowDialog();
        }

        private void Button_RemoveRepository_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ReposListBox.SelectedIndex > 0)
                {
                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + @"Addons/" + repos[ReposListBox.SelectedIndex].Modpacks.Modpack.ID, true);
                    Trace.WriteLine("Smazán balíček módů (" + repos[ReposListBox.SelectedIndex].Modpacks.Modpack.ID + ")");
                }
                
                this.UpdateRepoList();
            }
            catch (Exception exe)
            {
                Trace.WriteLine("[GetAllRepositories] Task Failed :: Exception [" + exe + "] thrown.");
            }
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            if (ParametersPanel.Width < 836)
            {
                RPCManager.UpdatePresence("Maturita Edition", "Nastavuje hru", startupTimestamp, 0, "infisync", "", "test", "");
                CurrentUser.Content = USER;

                var storyBoardIn = (Storyboard)this.Resources["Parameters_FadeIn"];
                storyBoardIn.Begin();

                // Refresh (get) list of Parameters
                var p_Handler = new ZeroCore.Parameters();
                ParametersObjectModel.Parameters p = p_Handler.GetParameters("Arma3");
                
                this.UpdateParamsList();
                this.UpdateRepoList();
            }
            else
            {
                RPCManager.UpdatePresence("Maturita Edition", "Je v launcheru", startupTimestamp, 0, "infisync", "", "test", "");
                var storyBoardIn = (Storyboard)this.Resources["Parameters_FadeOut"];
                storyBoardIn.Begin();
            }

        }

        private void ModpackGrid_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ModsGrid_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ReposListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ( ReposListBox.SelectedIndex >= 0 )
                {
                    var r_ListHandler = new ZeroApp.Controls.Lists.RepositoryListHandler();
                    this.repos = r_ListHandler.GetAllRepositories(r_ListHandler.path);

                    int index = ReposListBox.SelectedIndex;

                    Mods = new ObservableCollection<ZeroApp.Controls.Lists.ModListItem>();

                    for (int i = 0; i < repos[index].Addons.Addon.Count; i++)
                    {
                        Mods.Add(new ZeroApp.Controls.Lists.ModListItem() { m_Miniature = "", m_Name = repos[index].Addons.Addon[i].Name, m_ID = "..." });
                    }

                    ModsListBox.ItemsSource = Mods;
                }
            }
            catch (Exception x)
            {
                Trace.WriteLine("[GetAllRepositories] Task Failed :: Exception [" + x + "] thrown.");
            }
        }

        public void UpdateRepoList()
        {
            ReposListBox.SelectedIndex = 0;

            var r_ListHandler = new ZeroApp.Controls.Lists.RepositoryListHandler();
            this.repos = r_ListHandler.GetAllRepositories(r_ListHandler.path);

            Repos = new ObservableCollection<ZeroApp.Controls.Lists.RepositoryListItem>();

            foreach (RepositoryObjectModel.Repository Repository in repos)
            {
                Repos.Add(new ZeroApp.Controls.Lists.RepositoryListItem() { R_Miniature = @"pack://application:,,,/.zeroapp/media/box.png", R_Name = Repository.Modpacks.Modpack.Name, R_Source = Repository.Modpacks.Modpack.Source });
            }

            ReposListBox.ItemsSource = Repos;
        }

        public void UpdateParamsList()
        {
            // Deselect List Index
            ParamsListBox.SelectedIndex = 0;

            // Get Parameters From File
            var p_ListHandler = new ZeroApp.Controls.Lists.ParametersListHandler();
            this.parameters = p_ListHandler.GetParameters();
            
            // Load Parameters Into ObservableCollection
            Parameters = new ObservableCollection<ZeroApp.Controls.Lists.ParametersListItem>();

            // Add Each Parameter Into ListBox
            foreach (ParametersObjectModel.Parameter par in parameters)
            {
                if (!(par.Value == "null"))
                {
                    Parameters.Add(new ZeroApp.Controls.Lists.ParametersListItem() { P_Name = par.Name, P_IsActive = par.Active, P_HasValue = Visibility.Visible, P_Value = par.Value });
                }
                else
                {
                    Parameters.Add(new ZeroApp.Controls.Lists.ParametersListItem() { P_Name = par.Name, P_IsActive = par.Active, P_HasValue = Visibility.Collapsed, P_Value = par.Value });
                }
            }

            ParamsListBox.ItemsSource = Parameters;
        }

        private void Button_RefreshReposList_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateRepoList();
        }

        private void Button_RefreshParamsList_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateParamsList();
        }

        private void SaveParameters(string app_name, List<ParametersObjectModel.Parameter> parameters, ObservableCollection<Controls.Lists.ParametersListItem> Collection_Parameters)
        {
            var XmlSerializer = new XMLSerialization();
            ParametersObjectModel.Parameters new_parameters = new ParametersObjectModel.Parameters();

            for (int i = 0; i < parameters.Count(); i++)
            {
                // Update Values
                if (Collection_Parameters[i].P_HasValue == Visibility.Visible && Collection_Parameters[i].P_Value != parameters[i].Value)
                {
                    Trace.WriteLine("[" + Collection_Parameters[i].Name + "] " + "XML: " + parameters[i].Value + " |  GUI: " + Collection_Parameters[i].P_Value );
                    parameters[i].Value = Collection_Parameters[i].P_Value;
                }
                else
                {
                    //Trace.WriteLine("[" + Collection_Parameters[i].Name + "] XML value equals to GUI value.");
                }

                // Enable those, that User enabled via GUI
                // Collection_Parameters[i].P_Active.IsChecked == TRUE
                if ( (Collection_Parameters[i].P_Active.IsChecked.Value) && parameters[i].Active != "true")
                {
                    parameters[i].Active = "true";
                    Trace.WriteLine("[" + Collection_Parameters[i].Name + "] Enabled by user.");
                }

                // Disable those, that User disabled via GUI
                // !(Collection_Parameters[i].P_Active.IsChecked) == FALSE
                if ( !(Collection_Parameters[i].P_Active.IsChecked.Value) && parameters[i].Active != "false")
                {
                    parameters[i].Active = "false";
                    Trace.WriteLine("[" + Collection_Parameters[i].Name + "] Disabled by user.");
                }

                // Active = parameters[i].Active, Key = parameters[i].Key, Name = parameters[i].Name, Value = parameters[i].Value
                new_parameters.Parameter.Add(parameters[i]);
            }

            string xml = XmlSerializer.Serialize<ParametersObjectModel.Parameters>(new_parameters);
            //Trace.WriteLine(xml);

            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"Data\Apps\" + app_name + @"\App.Parameters.xml"))
            {
                sw.WriteLine(xml);
            }
        }

        private void Button_SaveParams_Click(object sender, RoutedEventArgs e)
        {
            SaveParameters("Arma3", this.parameters, this.Parameters);
        }

        private void Button_Credits_Click(object sender, RoutedEventArgs e) {}

        private void Button_ChangeDir_Click(object sender, RoutedEventArgs e)
        {
            Dialog_Setup setupDialog = new Dialog_Setup();
            System.Windows.Media.Effects.BlurEffect blur = new System.Windows.Media.Effects.BlurEffect();
            this.Effect = blur;
            setupDialog.Owner = GetWindow(this);
            setupDialog.ShowDialog();
        }

        private void Button_DeleteUserData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var uc_Handler = new UserConfig();

                ConfigObjectModel.Keys userConfig = uc_Handler.GetKeys("User");
                userConfig.Key[2].Value = "";
                userConfig.Key[3].Value = "";
                uc_Handler.SetKeys("User", userConfig);
                CurrentUser.Content = "OFFLINE";

                Trace.WriteLine("[DeleteUserData] Vymazána uživatelská data v User.Parameters.xml");
            }
            catch (Exception x)
            {
                Trace.WriteLine("[DeleteUserData] Task Failed :: " + x);
            }
        }

        private void Button_GoToAddons_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Addons/");
        }
    }
}
