using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZeroApp.ZeroCore;

namespace ZeroApp.Windows.Dialogs
{
    /// <summary>
    /// Interakční logika pro Dialog_Setup.xaml
    /// </summary>
    public partial class Dialog_Setup : Window
    {
        // UNIVERSAL DECLARATION OF USER CONFIG OBJECT
        List<ConfigObjectModel.Key> keys = new List<ConfigObjectModel.Key>();

        public Dialog_Setup()
        {
            InitializeComponent();
        }

        private void Button_ExitDialog_Click(object sender, RoutedEventArgs e)
        {
            GetWindow(Application.Current.MainWindow).Effect = null;
            var mainWindow = new LauncherWindow();
            
            // Shutdown Presence
            RPCManager.ShutdownPresence();

            // Shutdown Application
            ZeroApp.App.Current.Shutdown();
        }

        private void Button_Continue_Click(object sender, RoutedEventArgs e)
        {
            string gamedir = Input_ApplicationLocation.Text;
            Trace.WriteLine(Input_ApplicationLocation.Text);
            if ((gamedir.Length > 0) && (gamedir.EndsWith("arma3.exe") || gamedir.EndsWith("arma3_x64.exe")) || gamedir.EndsWith("arma3battleye.exe"))
            {
                var uc_Handler = new ZeroCore.UserConfig();
                SaveUserConfig("User", gamedir);

                GetWindow(Application.Current.MainWindow).Effect = null; 
                var mainWindow = new LauncherWindow();
                this.Close();
            }
        }

        private void Button_GetDirectory_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog f_Dialog = new Microsoft.Win32.OpenFileDialog();
            f_Dialog.DefaultExt = ".exe";
            f_Dialog.Filter = "EXE Files (*.exe)|*.exe";
            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = f_Dialog.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = f_Dialog.FileName;
                Input_ApplicationLocation.Text = filename; 
            }
        }

        private void SaveUserConfig(string user, string GameDirectory)
        {
            var XmlSerializer = new XMLSerialization();
            var uc_Handler = new UserConfig();
            ConfigObjectModel.Keys new_config = new ConfigObjectModel.Keys();

            // Automatically set "IsSetup" to true and set GameDirectory
            ConfigObjectModel.Key isSetupKey = new ConfigObjectModel.Key() { Name = "IsSetup", Value = "true" };
            ConfigObjectModel.Key gameDirectoryKey = new ConfigObjectModel.Key() { Name = "GameDirectory", Value = GameDirectory };
            ConfigObjectModel.Key usernameKey = new ConfigObjectModel.Key() { Name = "SavedUsername", Value = "" };
            ConfigObjectModel.Key passwordKey = new ConfigObjectModel.Key() { Name = "SavedPassword", Value = "" };
            new_config.Key.Add(isSetupKey);
            new_config.Key.Add(gameDirectoryKey);
            new_config.Key.Add(usernameKey);
            new_config.Key.Add(passwordKey);

            uc_Handler.SetKeys("User", new_config);
        }
    }
}
