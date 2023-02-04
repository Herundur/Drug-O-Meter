using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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

using Drug_O_Meter.MVVM.ViewModel;
using Drug_O_Meter.MVVM.Model;
using HarfBuzzSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;

namespace Drug_O_Meter
{

    public partial class MainWindow : Window
    {
        string todaysDate = DateTime.Now.ToString("dd.MM.yyyy");

        public MainWindow()
        {
            List<string> fileNames = Files.Names();

            if (!fileNames.Contains(todaysDate))
            {
                drugConsumtion consumtionToday = new drugConsumtion(todaysDate);
                Files.Write<drugConsumtion>($"../../../Data/{consumtionToday.Date}", consumtionToday);
            }
            InitializeComponent();
            Trace.WriteLine(Directory.GetCurrentDirectory());

        }

        private void exitWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void minimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void DragWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
