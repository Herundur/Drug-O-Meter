using Drug_O_Meter.MVVM.Model;
using LiveChartsCore.SkiaSharpView.SKCharts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for DiscoveryView.xaml
    /// </summary>
    public partial class AlcoholView : UserControl
    {

        float counter = 0;
        float litersLast31Days;
        public AlcoholView()
        {
            InitializeComponent();


            //alcoholChart.XAxes[0].Labels = DirectoryClass.allDatesLabels();
            alcoholChart.Series[0].Values = AlcoholData.MonthValues(out litersLast31Days);
            literLast31DaysTextBlock.Text = $"{litersLast31Days.ToString()} Liter";
            LitersInTotalTextBlock.Text = $"{AlcoholData.LitersInTotal().ToString()} Liter";
            LitersPerDayTextBlock.Text = $"Ø {AlcoholData.LitersAverageDay().ToString()} Liter";
            SoberSinceTextBox.Text = AlcoholData.SoberSince();
            SoberLongestSteakTextBox.Text = AlcoholData.SoberLongestStreak();
        }
        
          private void removeButton_Click(object sender, RoutedEventArgs e)
            {
                if (counter > 0)
                {
                counter -= 0.5f;
                }
                count.Text = $"{counter.ToString()}L";
            }

            private void addButton_Click(object sender, RoutedEventArgs e)
            {
                if (counter < 99)
                {
                    counter += 0.5f;
                }
                count.Text = $"{counter.ToString()}L";
        }

            private void updateButton_Click(object sender, RoutedEventArgs e)
            {
                

                if (!datePicker.SelectedDate.HasValue)
                {

                    //Trace.WriteLine("Theres no date specified in the date-picker.");
                    return;
                }

                string datePickerDate = datePicker.SelectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);

                drugConsumtion addConsumtion  = new drugConsumtion(datePickerDate, counter, 0);
                DirectoryClass.WriteToFile<drugConsumtion>($"./data/{addConsumtion.Date}", addConsumtion);
                alcoholChart.Series[0].Values = AlcoholData.MonthValues(out litersLast31Days);
                literLast31DaysTextBlock.Text = $"{litersLast31Days.ToString()} Liter";
                LitersInTotalTextBlock.Text = $"{AlcoholData.LitersInTotal().ToString()} Liter";
                LitersPerDayTextBlock.Text = $"Ø {AlcoholData.LitersAverageDay().ToString()} Liter";
                SoberSinceTextBox.Text = AlcoholData.SoberSince();
                SoberLongestSteakTextBox.Text = AlcoholData.SoberLongestStreak();

        }
    }
}
