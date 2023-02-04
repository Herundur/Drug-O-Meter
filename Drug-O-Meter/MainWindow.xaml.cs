using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Drug_O_Meter.MVVM.Model;

namespace Drug_O_Meter
{

    public partial class MainWindow : Window
    {
        string todaysDate = DateTime.Now.ToString("dd.MM.yyyy");

        public MainWindow()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DrugOMeter/data");
            Directory.CreateDirectory(folder);

            List<string> fileNames = Files.Names();

            if (!fileNames.Contains(todaysDate))
            {

                drugConsumtion consumtionToday = new drugConsumtion(todaysDate);
                Files.Write<drugConsumtion>($"{folder}/{consumtionToday.Date}", consumtionToday);
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
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
