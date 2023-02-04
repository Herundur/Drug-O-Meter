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
    public partial class CannabisView : UserControl
    {

        float counter = 0;
        float litersLast31Days;
        string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DrugOMeter/data");
        public CannabisView()
        {

            InitializeComponent();

            cannabisChart.Series[0].Values = DataManager.MonthValues(out litersLast31Days, "Grams");

            literLast31DaysTextBlock.Text = $"{litersLast31Days.ToString()} Joints";
            LitersInTotalTextBlock.Text = $"{DataManager.InTotal("Grams").ToString()} Joints";
            LitersPerDayTextBlock.Text = $"Ø {DataManager.AverageDay("Grams").ToString()} Joints";
            SoberSinceTextBox.Text = DataManager.SoberSince("Grams");
            SoberLongestSteakTextBox.Text = DataManager.SoberLongestStreak("Grams");
        }
            
            private void removeButton_Click(object sender, RoutedEventArgs e)
            {
                if (counter > 0)
                {
                counter -= 0.5f;
                }
                count.Text = $"{counter.ToString()}J";
            }

            private void addButton_Click(object sender, RoutedEventArgs e)
            {
                if (counter < 99)
                {
                    counter += 0.5f;
                }
                count.Text = $"{counter.ToString()}J";
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
                drugConsumtion addConsumtion  = new drugConsumtion(datePickerDate, todaysFile.Liters, counter);

                Files.Write<drugConsumtion>($"{folder}/{addConsumtion.Date}", addConsumtion);

                cannabisChart.Series[0].Values = DataManager.MonthValues(out litersLast31Days, "Grams");

                literLast31DaysTextBlock.Text = $"{litersLast31Days.ToString()} Joints";
                LitersInTotalTextBlock.Text = $"{DataManager.InTotal("Grams").ToString()} Joints";
                LitersPerDayTextBlock.Text = $"Ø {DataManager.AverageDay("Grams").ToString()} Joints";
                SoberSinceTextBox.Text = DataManager.SoberSince("Grams");
                SoberLongestSteakTextBox.Text = DataManager.SoberLongestStreak("Grams");

        }
    }
}
