using Drug_O_Meter.MVVM.Model;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Drug_O_Meter.MVVM.View
{
    /// <summary>
    /// Interaction logic for DiscoveryView.xaml
    /// </summary>
    public partial class AlcoholView : UserControl
    {

        float counter = 0;
        float litersLast31Days;
        string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DrugOMeter/data");
        public AlcoholView()
        {
            
            InitializeComponent();

            alcoholChart.Series[0].Values = DataManager.MonthValues(out litersLast31Days, "Liters");
            literLast31DaysTextBlock.Text = $"{litersLast31Days.ToString()} Liter";
            LitersInTotalTextBlock.Text = $"{DataManager.InTotal("Liters").ToString()} Liter";
            LitersPerDayTextBlock.Text = $"Ø {DataManager.AverageDay("Liters").ToString()} Liter";
            SoberSinceTextBox.Text = DataManager.SoberSince("Liters");
            SoberLongestSteakTextBox.Text = DataManager.SoberLongestStreak("Liters");
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
                string todaysDate = DateTime.Now.ToString("dd.MM.yyyy");

                drugConsumtion todaysFile = Files.Read<drugConsumtion>($"{folder}/{todaysDate}");
                drugConsumtion addConsumtion = new drugConsumtion(datePickerDate, counter, todaysFile.Grams);

                Files.Write<drugConsumtion>($"{folder}/{addConsumtion.Date}", addConsumtion);
                alcoholChart.Series[0].Values = DataManager.MonthValues(out litersLast31Days, "Liters");
                literLast31DaysTextBlock.Text = $"{litersLast31Days.ToString()} Liter";
                LitersInTotalTextBlock.Text = $"{DataManager.InTotal("Liters").ToString()} Liter";
                LitersPerDayTextBlock.Text = $"Ø {DataManager.AverageDay("Liters").ToString()} Liter";
                SoberSinceTextBox.Text = DataManager.SoberSince("Liters");
                SoberLongestSteakTextBox.Text = DataManager.SoberLongestStreak("Liters");

        }
    }
}
