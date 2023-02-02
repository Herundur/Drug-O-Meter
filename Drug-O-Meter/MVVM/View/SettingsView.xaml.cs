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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Drug_O_Meter.MVVM.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();

            usernameTextbox.Text = Properties.Settings.Default.Username;
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Username = usernameTextbox.Text;

            Properties.Settings.Default.Save();
        }
        private void saveSettings_Click(object sender, RoutedEventArgs e)
        {

            SaveSettings();
        }
    }
}
