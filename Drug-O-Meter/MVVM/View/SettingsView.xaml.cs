using Drug_O_Meter.MVVM.Model;
using System.Windows;
using System.Windows.Controls;

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
            startedCollecting.Text = Files.FirstFile();
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
