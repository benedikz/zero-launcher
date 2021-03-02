using System.Windows;
using System.Windows.Controls;

namespace ZeroApp.Controls.Lists
{
    /// <summary>
    /// Interakční logika pro RepositoryListItem.xaml
    /// </summary>
    public partial class RepositoryListItem : UserControl
    {
        public string R_Miniature
        {
            get { return (string)GetValue(R_MiniatureProperty); }
            set { SetValue(R_MiniatureProperty, value); }
        }

        public string R_Name
        {
            get { return (string)GetValue(R_NameProperty); }
            set { SetValue(R_NameProperty, value); }
        }

        public string R_Source
        {
            get { return (string)GetValue(R_SourceProperty); }
            set { SetValue(R_SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty R_MiniatureProperty =
            DependencyProperty.Register("R_Miniature", typeof(string), typeof(RepositoryListItem), new PropertyMetadata(@"pack://application:,,,/.zeroapp/media/box.png"));

        public static readonly DependencyProperty R_NameProperty =
            DependencyProperty.Register("R_Name", typeof(string), typeof(RepositoryListItem), new PropertyMetadata("Neznámý"));

        public static readonly DependencyProperty R_SourceProperty =
            DependencyProperty.Register("R_Source", typeof(string), typeof(RepositoryListItem), new PropertyMetadata("Neznámý zdroj"));

        public RepositoryListItem()
        {
            InitializeComponent();
        }
    }
}
