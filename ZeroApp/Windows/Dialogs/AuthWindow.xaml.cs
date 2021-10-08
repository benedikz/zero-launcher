using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZeroApp
{
    /// <summary>
    /// Interakční logika pro AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        /// <summary>
        /// Static Application Variables
        /// </summary>
        public static string API_URL = ConfigurationManager.AppSettings["URL_API"];
        public static string API_DOM = Regex.Match(API_URL, @"(http:|https:)\/\/(.*?)\/").ToString();
        private static string SERVERCONFIG_REMOTEURL = ConfigurationManager.AppSettings["URL_Repository"];

        public AuthWindow()
        {
            InitializeComponent();
        }

        private void LoginActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string i_username = LoginUsernameInput.Text;
                string i_password = LoginUserPinInput.Password;

                if (Authorization.Login(i_username, i_password).Contains("\"token\""))
                {
                    if (CheckBox_RememberMe.IsChecked == true)
                    {
                        var uc_Handler = new ZeroCore.UserConfig();
                        ZeroCore.ConfigObjectModel.Keys uc = uc_Handler.GetKeys("User");

                        uc.Key[2].Value = i_username;
                        uc.Key[3].Value = i_password;

                        uc_Handler.SetKeys("User", uc);
                    }
                    else
                    {

                    }

                    LauncherWindow.USER = i_username;
                    LauncherWindow.isAuthenticated = true;

                    CloseDialog();
                }
                else
                {
                    // Selhání
                    Trace.WriteLine("SELHÁNÍ");
                }

            }
            catch (Exception exc)
            {
                Trace.WriteLine("[LoginActionButton_Click] Task Failed :: Exception[" + exc + "] thrown.");
            }
        }

        private void Button_ExitDialog_Click(object sender, RoutedEventArgs e)
        {
            //CloseDialog();
        }

        private void CloseDialog()
        {
            GetWindow(Application.Current.MainWindow).Effect = null;
            this.Close();
        }
    }
}
