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

using AlcoholChart;
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
            List<string> fileNames = DirectoryClass.fileNames();

            if (!fileNames.Contains(todaysDate))
            {
                drugConsumtion consumtionToday = new drugConsumtion(todaysDate);
                DirectoryClass.WriteToFile<drugConsumtion>($"./data/{consumtionToday.Date}", consumtionToday);
            }
           
            InitializeComponent();
            
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
            Trace.WriteLine("asdasd");
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

    }
}
