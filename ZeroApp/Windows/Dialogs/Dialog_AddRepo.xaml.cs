using System;
using System.Collections.Generic;
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

namespace ZeroApp.Windows.Dialogs
{
    /// <summary>
    /// Interakční logika pro Dialog_AddRepo.xaml
    /// </summary>
    public partial class Dialog_AddRepo : Window
    {
        public Dialog_AddRepo()
        {
            InitializeComponent();
        }

        private void Button_ExitDialog_Click(object sender, RoutedEventArgs e)
        {
            CloseDialog();
            var mainWindow = new LauncherWindow();
        }

        private void Button_Continue_Click(object sender, RoutedEventArgs e)
        {
                var m_Manager = new Mods();
                string url = Input_SourceRepository.Text;
                
                // Ověření správnosti a omezení URL
                if(url.Length > 1 && (url.StartsWith("https://") || url.StartsWith("http://")) )
                {
                    if( url.EndsWith("index.xml") )
                    {
                        m_Manager.DownloadRepository(url);
                        CloseDialog();
                    }
                }
                else
                {
                    Input_SourceRepository.Text = "https://";
                    Input_SourceRepository.CaretIndex = 7;
                }
        }

        private void CloseDialog()
        {
            GetWindow(Application.Current.MainWindow).Effect = null;
            this.Close();
        }
    }
}
