using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Diagnostics;

namespace Drug_O_Meter.MVVM.Model
{
    class DataManager
    {
        private static string folder;

        private static string Folder
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DrugOMeter/data"); }
        }
        public static List<string> Labels()
        {
            var orderedList = Files.Names();
            var dates = new List<DateTime>();

            for (int i = 0; i < orderedList.Count(); i++)
            {
                if (orderedList[i].ToString() == orderedList.Last().ToString())
                {
                    dates.Add(DateTime.ParseExact(orderedList[i], "dd.MM.yyyy", CultureInfo.InvariantCulture));
                    break;
                }

                var start = DateTime.ParseExact(orderedList[i], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                var end = DateTime.ParseExact(orderedList[i + 1], "dd.MM.yyyy", CultureInfo.InvariantCulture);

                for (var dt = start; dt < end; dt = dt.AddDays(1))
                {
                    dates.Add(dt);
                }
            }

            var stringDates = new List<string>();
            dates.ForEach(date => stringDates.Add(date.ToString("dd.MM.yyyy")));

            return stringDates;
        }

        public static List<double> Values(string drug)
        {
            List<double> chartValueList = new List<double>();
            List<string> allDatesLabelsList = Labels();

            foreach (string date in allDatesLabelsList)
            {
                if (Files.Names().Contains(date))
                {
                    drugConsumtion currentFile = Files.Read<drugConsumtion>($"{Folder}/{date}");
                    var value = currentFile.GetType().GetProperty(drug).GetValue(currentFile);
                    double floatValue = (double)value;
                    chartValueList.Add(floatValue);
                }
                else
                {
                    chartValueList.Add(0);
                }
            }

            foreach (double entry in chartValueList.ToList())
            {
                if (chartValueList[0] != 0) break;

                chartValueList.RemoveAt(0);

            }
            return chartValueList;
        }
        public static List<double> MonthValues(out double literCount, string drug)
        {
            List<double> availableData = Values(drug).ToList();
            availableData.Reverse();

            List<double> data = new List<double>();
            int i;

            for (i = 1; i < 32; i++)
            {
                if (i <= availableData.Count)
                {
                    data.Add(availableData[i - 1]);
                }
                else if (31 >= availableData.Count)
                {
                    data.Add(0);
                }
            }

            double count = 0;

            foreach (double liter in data)
            {
                count += liter;
            }

            literCount = count;

            data.Reverse();
            return data;
        }

        public static List<string> LabelLast31Days()
        {

            DateTime today = DateTime.Today;
            List<DateTime> dates = new List<DateTime>();

            var start = DateTime.ParseExact(today.ToString("dd.MM.yyyy"), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var end = DateTime.ParseExact(today.AddDays(-31).ToString("dd.MM.yyyy"), "dd.MM.yyyy", CultureInfo.InvariantCulture);

            for (var dt = start; dt > end; dt = dt.AddDays(-1))
            {
                dates.Add(dt);
            }

            var stringDates = new List<string>();
            dates.ForEach(date => stringDates.Add(date.ToString("dd.MM.yyyy")));
            stringDates.Reverse();

            return stringDates;
        }

        public static double InTotal(string drug)
        {
            List<string> files = Files.Names();
            double count = 0;

            foreach (string file in files)
            {
                drugConsumtion currentFile = Files.Read<drugConsumtion>($"{Folder}/{file}");
                var value = currentFile.GetType().GetProperty(drug).GetValue(currentFile);
                double floatValue = (double)value;
                count += floatValue;
            }

            return (double)Math.Round(count, 2);
        }

        public static double AverageDay(string drug)
        {
            List<double> values = Values(drug);
            double sum = 0f;

            foreach (double value in values)
            {
                sum += value;
            }

            double arithmeticMean = sum / values.Count;

            return (double)Math.Round(arithmeticMean, 2);
        }

        public static string SoberSince(string drug)
        {
            List<double> values = Values(drug);
            values.Reverse();
            int counter = 0;
            string text = "BUG Tage";

            foreach (double value in values)
            {

                if (!values.Exists(num => num != 0))
                {
                    text = "x Tagen";
                    break;
                }

                if (value == 0)
                {
                    counter++;

                    if (values[counter] != 0)
                    {
                        switch (counter)
                        {
                            case 1:
                                text = $"einem Tag";
                                break;
                            default:
                                text = $"{counter.ToString()} Tagen";
                                break;
                        }
                        break;
                    }
                }
                else if (value != 0)
                {
                    text = $"0 Tagen";
                    break;
                }

            }

            return text;
        }

        public static string SoberLongestStreak(string drug)
        {
            List<double> values = Values(drug);
            values.Reverse();
            int currentIndex = 0;
            int counter = 0;
            int largestCount = 0;
            string text = "BUG Tage";

            foreach (var value in values)
            {
                if (!values.Exists(num => num != 0))
                {
                    text = "x Tagen";
                    break;
                }

                if (value == 0 && !(currentIndex == values.Count - 1))
                {
                    counter++;
                }
                else if (value != 0 && !(currentIndex == values.Count - 1))
                {
                    largestCount = counter > largestCount ? counter : largestCount;
                    counter = 0;
                }
                else if (currentIndex == values.Count - 1)
                {
                    largestCount = counter > largestCount ? counter : largestCount;

                    switch (largestCount)
                    {
                        case 1:
                            text = $"1 Tag";
                            break;
                        default:
                            text = $"{largestCount.ToString()} Tage";
                            break;
                    }

                    break;
                }
                currentIndex++;

            }

            return text;
        }

        public static List<List<string>> Entries(string drug)
        {
            List<List<string>> entries = new List<List<string>>();
            List<string> files = Files.Names();
            files.Reverse();

            int i = 0;

            foreach (string date in files)
            {
                drugConsumtion currentFile = Files.Read<drugConsumtion>($"{Folder}/{date}");
                var value = currentFile.GetType().GetProperty(drug).GetValue(currentFile);
                double floatValue = (double)value;

                if (floatValue != 0)
                {
                    List<string> list = new List<string>() {date, floatValue.ToString()};
                    entries.Add(list);
                    i += 1;
                }

            }


            //Entries get numbered
            foreach (List<string> list in entries)
            {
                list.Add(i.ToString());
                i -= 1;
            }

            return entries;
        }
    }
}
