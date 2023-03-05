﻿using Drug_O_Meter.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Drug_O_Meter.MVVM.View
{
    public class AlcoholEntry
    {
        public string Date { get; set; }
        public string Value { get; set; }
        public string Nummer { get; set; }
        public string Beer { get; set; }
        public string IconKind { get; set; }
    }
    public partial class AlcoholView : UserControl
    {

        float counter = 0;
        float litersLast31Days;
        string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DrugOMeter/data");

        public AlcoholView()
        {
            
            InitializeComponent();

            List<List<string>> entries = DataManager.Entries("Liters");
            List<AlcoholEntry> items = new List<AlcoholEntry>();

            foreach (List<string> entry in entries)
            {
               string icon = String.Empty;
               items.Add(new AlcoholEntry() { Date = entry[0], Value = $"{entry[1]} Liter", Nummer = $"Eintrag Nr. {entry[2]}", Beer = BeerType(entry[1], out icon), IconKind = icon  });
            }
            recentEntries.ItemsSource = items;

           /* alcoholChart.Series[0].Values = DataManager.MonthValues(out litersLast31Days, "Liters");
            literLast31DaysTextBlock.Text = $"{litersLast31Days.ToString()} Liter";
            LitersInTotalTextBlock.Text = $"{DataManager.InTotal("Liters").ToString()} Liter";
            LitersPerDayTextBlock.Text = $"Ø {DataManager.AverageDay("Liters").ToString()} Liter";
            SoberSinceTextBox.Text = DataManager.SoberSince("Liters");
            SoberLongestSteakTextBox.Text = DataManager.SoberLongestStreak("Liters");*/
        }
        
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Math.Round(counter, 1) > 0)
            {
                counter -= 0.1f;
                Math.Round(counter, 1);
            }
            count.Text = $"{Math.Round(counter, 1).ToString()}L";
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (counter < 99)
            {
                counter += 0.1f;
            }
            count.Text = $"{Math.Round(counter, 1).ToString()}L";
        }

        public void updateContent()
        {
            List<List<string>> entries = DataManager.Entries("Liters");
            List<AlcoholEntry> items = new List<AlcoholEntry>();

            foreach (List<string> entry in entries)
            {
                string icon = String.Empty;
                items.Add(new AlcoholEntry() { Date = entry[0], Value = $"{entry[1]} Liter", Nummer = $"Eintrag Nr. {entry[2]}", Beer = BeerType(entry[1], out icon), IconKind = icon });
            }
           
            recentEntries.ItemsSource = items;
            //alcoholChart.Series[0].Values = DataManager.MonthValues(out litersLast31Days, "Liters");
            //literLast31DaysTextBlock.Text = $"{litersLast31Days.ToString()} Liter";
            //LitersInTotalTextBlock.Text = $"{DataManager.InTotal("Liters").ToString()} Liter";
            //LitersPerDayTextBlock.Text = $"Ø {DataManager.AverageDay("Liters").ToString()} Liter";
            //SoberSinceTextBox.Text = DataManager.SoberSince("Liters");
            //SoberLongestSteakTextBox.Text = DataManager.SoberLongestStreak("Liters");
        }
            
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            float roundedCounter = (float)Math.Round(counter, 1);


            if (!datePicker.SelectedDate.HasValue)
            {

                //Trace.WriteLine("Theres no date specified in the date-picker.");
                return;
            }

            string datePickerDate = datePicker.SelectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (Files.Names().Contains(datePickerDate))
            {
                drugConsumtion fileFromStatedDate = Files.Read<drugConsumtion>($"{folder}/{datePickerDate}");
                drugConsumtion addConsumtion = new drugConsumtion(datePickerDate, roundedCounter, fileFromStatedDate.Grams);
                Files.Write<drugConsumtion>($"{folder}/{addConsumtion.Date}", addConsumtion);
            }
            else 
            {
                drugConsumtion addConsumtion = new drugConsumtion(datePickerDate, roundedCounter, 0);
                Files.Write<drugConsumtion>($"{folder}/{addConsumtion.Date}", addConsumtion);
            }

            updateContent();

        }

        public string BeerType(string beerAmount, out string iconKind)
        {
            string type = String.Empty;
            iconKind = String.Empty;

            switch (float.Parse(beerAmount))
            {
                case <= (float)0.3:
                    type = "Seidl";
                    iconKind = "Beer";
                    break;
                case < 1:
                    type = "Hoibe";
                    iconKind = "GlassMugVariant";
                    break;
                case >= 1:
                    type = "Moss";
                    iconKind = "GlassMug";
                    break;

            }

            return type;
        }
    }
}
