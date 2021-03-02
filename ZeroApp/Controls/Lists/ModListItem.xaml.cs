using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZeroApp.Controls.Lists
{
    /// <summary>
    /// Interakční logika pro ModListItem.xaml
    /// </summary>
    public partial class ModListItem : UserControl
    {
        public string m_Miniature
        {
            get { return (string)GetValue(M_MiniatureProperty); }
            set { SetValue(M_MiniatureProperty, value); }
        }

        public string m_Name
        {
            get { return (string)GetValue(M_NameProperty); }
            set { SetValue(M_NameProperty, value); }
        }

        public string m_ID
        {
            get { return (string)GetValue(M_IDProperty); }
            set { SetValue(M_IDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty M_MiniatureProperty =
            DependencyProperty.Register("Miniature", typeof(string), typeof(RepositoryListItem), new PropertyMetadata(@"pack://application:,,,/.zeroapp/media/box.png"));

        public static readonly DependencyProperty M_NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(RepositoryListItem), new PropertyMetadata("Neznámý"));

        public static readonly DependencyProperty M_IDProperty =
            DependencyProperty.Register("ID", typeof(string), typeof(RepositoryListItem), new PropertyMetadata("..."));

        public ModListItem()
        {
            InitializeComponent();
        }
    }
}
