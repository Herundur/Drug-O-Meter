using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Diagnostics;

namespace Drug_O_Meter.MVVM.Model
{
    class BarChart
    {
        private static string folder;

        private static string Folder
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DrugOMeter/data"); }
        }
        public static List<double> barChartValues(string drug)
        {

            List<double> chartValues = new List<double> { 0, 0, 0, 0, 0, 0, 0 };
            List<string> files = Files.Names();

            foreach (string date in files)
            {
                drugConsumtion currentFile = Files.Read<drugConsumtion>($"{Folder}/{date}");
                var value = currentFile.GetType().GetProperty(drug).GetValue(currentFile);
                double currentValue = (double)value;
                //double entryValueRounded = Math.Round(floatValue, 1);

                if (currentValue != 0)
                {

                    DateTime dt = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    string dayName = dt.DayOfWeek.ToString();

                    switch (dayName) 
                    {
                        case "Monday":
                            chartValues[0] += currentValue;
                            break;
                        case "Tuesday":
                            chartValues[1] += currentValue;
                            break;
                        case "Wednesday":
                            chartValues[2] += currentValue;
                            break;
                        case "Thursday":
                            chartValues[3] += currentValue;
                            break;
                        case "Friday":
                            chartValues[4] += currentValue;
                            break;
                        case "Saturday":
                            chartValues[5] += currentValue;
                            break;
                        case "Sunday":
                            chartValues[6] += currentValue;
                            break;
                    }
                }

            }
            
            chartValues = chartValues.Select(x => Math.Round(x, 1)).ToList();
            return chartValues;
        }

        public static List<double> barChartValuesInPercent(string drug) 
        {
            List<double> chartValues = barChartValues(drug);
            List<double> chartValuesInPercent = new List<double>();

            double sum = 0;
            foreach (double value in chartValues)
            {
                sum += value;
            }

            double onePercent = sum / 100;

            foreach (double value in chartValues)
            {
                chartValuesInPercent.Add(Math.Round(value / onePercent, 1));
            }

            return chartValuesInPercent;
        }
    }


}
