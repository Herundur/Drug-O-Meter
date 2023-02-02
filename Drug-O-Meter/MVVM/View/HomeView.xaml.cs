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
using System.Windows.Threading;
using Drug_O_Meter.MVVM.Model;
using Drug_O_Meter.Properties;

namespace Drug_O_Meter.MVVM.View
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

            LiveTimeLabel.Content = DateTime.Now.ToString("HH:mm");
            DateTime.Now.ToString("HH:mm");
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();
            HomeDate.Text = DateTime.Now.ToString("dd MMMM yyyy");

            QuoteTextBlock.Text = HomeViewData.RandomQuote();

            greetingsUser.Text = $"Hallo, {Properties.Settings.Default.Username}";
        }

        void timer_Tick(object sender, EventArgs e)
        {
            LiveTimeLabel.Content = DateTime.Now.ToString("HH:mm");
        }
    }
}
