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
        int counter = 0;

        public MainWindow()
        {
            List<string> fileNames = DirectoryClass.fileNames();

            if (!fileNames.Contains(todaysDate))
            {
                drugConsumtion consumtionToday = new drugConsumtion(todaysDate);
                DirectoryClass.WriteToFile<drugConsumtion>($"./data/{consumtionToday.Date}", consumtionToday);
            }

            InitializeComponent();
            
            alcoholChart.XAxes[0].Labels = DirectoryClass.allDatesLabels();
            alcoholChart.Series[0].Values = DirectoryClass.allDatesValues();
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(counter <= 0))
            {
                counter--;
            }
            count.Text = counter.ToString();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            count.Text = counter.ToString();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            string datePickerDate = datePicker.SelectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (!datePicker.SelectedDate.HasValue)
            {
                /* POPUP WINDOW */
                Trace.WriteLine("Theres no date specified in the date-picker.");
                return;
            }

            drugConsumtion addConsumtion  = new drugConsumtion(datePickerDate, counter * 500, 0);
            DirectoryClass.WriteToFile<drugConsumtion>($"./data/{addConsumtion.Date}", addConsumtion);
            alcoholChart.XAxes[0].Labels = DirectoryClass.allDatesLabels();
            alcoholChart.Series[0].Values = DirectoryClass.allDatesValues();

        }
    }
}
