using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;


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

        public static List<float> Values(string drug)
        {
            List<float> chartValueList = new List<float>();
            List<string> allDatesLabelsList = Labels();

            foreach (string date in allDatesLabelsList)
            {
                if (Files.Names().Contains(date))
                {
                    drugConsumtion currentFile = Files.Read<drugConsumtion>($"{Folder}/{date}");
                    var value = currentFile.GetType().GetProperty(drug).GetValue(currentFile);
                    float floatValue = (float)value;
                    chartValueList.Add(floatValue);
                }
                else
                {
                    chartValueList.Add(0);
                }
            }
            return chartValueList;
        }
        public static List<float> MonthValues(out float literCount, string drug)
        {
            List<float> availableData = Values(drug).ToList();
            availableData.Reverse();

            List<float> data = new List<float>();
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

            float count = 0;

            foreach (float liter in data)
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

        public static float InTotal(string drug)
        {
            List<string> files = Files.Names();
            float count = 0;

            foreach (string file in files)
            {
                drugConsumtion currentFile = Files.Read<drugConsumtion>($"{Folder}/{file}");
                var value = currentFile.GetType().GetProperty(drug).GetValue(currentFile);
                float floatValue = (float)value;
                count += floatValue;
            }

            return count;
        }

        public static float AverageDay(string drug)
        {
            List<float> values = Values(drug);
            float sum = 0f;

            foreach (float value in values)
            {
                sum += value;
            }

            float arithmeticMean = sum / values.Count;

            return (float)Math.Round(arithmeticMean, 2);
        }

        public static string SoberSince(string drug)
        {
            List<float> values = Values(drug);
            values.Reverse();
            int counter = 0;
            string text = "BUG Tage";

            foreach (float value in values)
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
            List<float> values = Values(drug);
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
    }
}
