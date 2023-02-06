using Drug_O_Meter.MVVM.Model;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Drug_O_Meter.MVVM.View
{

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

        public void updateContent()
        {
            alcoholChart.Series[0].Values = DataManager.MonthValues(out litersLast31Days, "Liters");
            literLast31DaysTextBlock.Text = $"{litersLast31Days.ToString()} Liter";
            LitersInTotalTextBlock.Text = $"{DataManager.InTotal("Liters").ToString()} Liter";
            LitersPerDayTextBlock.Text = $"Ø {DataManager.AverageDay("Liters").ToString()} Liter";
            SoberSinceTextBox.Text = DataManager.SoberSince("Liters");
            SoberLongestSteakTextBox.Text = DataManager.SoberLongestStreak("Liters");
        }
            
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {

            if (!datePicker.SelectedDate.HasValue)
            {

                //Trace.WriteLine("Theres no date specified in the date-picker.");
                return;
            }

            string datePickerDate = datePicker.SelectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (Files.Names().Contains(datePickerDate))
            {
                drugConsumtion fileFromStatedDate = Files.Read<drugConsumtion>($"{folder}/{datePickerDate}");
                drugConsumtion addConsumtion = new drugConsumtion(datePickerDate, counter, fileFromStatedDate.Grams);
                Files.Write<drugConsumtion>($"{folder}/{addConsumtion.Date}", addConsumtion);
            }
            else 
            {
                drugConsumtion addConsumtion = new drugConsumtion(datePickerDate, counter, 0);
                Files.Write<drugConsumtion>($"{folder}/{addConsumtion.Date}", addConsumtion);
            }

            updateContent();

        }
    }
}
