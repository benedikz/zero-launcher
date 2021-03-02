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
    /// Interakční logika pro ParametersListItem.xaml
    /// </summary>
    public partial class ParametersListItem : UserControl
    {
        public string P_Name
        {
            get { return (string)GetValue(P_NameProperty); }
            set { SetValue(P_NameProperty, value); }
        }

        public string P_IsActive
        {
            get { return (string)GetValue(P_IsActiveProperty); }
            set { SetValue(P_IsActiveProperty, value); }
        }

        public Visibility P_HasValue
        {
            get { return (Visibility)GetValue(P_HasValueProperty); }
            set { SetValue(P_HasValueProperty, value); }
        }

        public string P_Value
        {
            get { return (string)GetValue(P_ValueProperty); }
            set { SetValue(P_ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty P_NameProperty =
            DependencyProperty.Register("P_Name", typeof(string), typeof(ParametersListItem), new PropertyMetadata(@"???"));

        public static readonly DependencyProperty P_IsActiveProperty =
            DependencyProperty.Register("P_IsActive", typeof(string), typeof(ParametersListItem), new PropertyMetadata("false"));

        public static readonly DependencyProperty P_HasValueProperty =
            DependencyProperty.Register("P_HasValue", typeof(Visibility), typeof(ParametersListItem), new PropertyMetadata(Visibility.Collapsed));

        public static readonly DependencyProperty P_ValueProperty =
            DependencyProperty.Register("P_Value", typeof(string), typeof(ParametersListItem), new PropertyMetadata(""));

        public ParametersListItem()
        {
            InitializeComponent();
        }
    }
}
