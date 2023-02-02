using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Drug_O_Meter.MVVM.Model
{
    internal static class DirectoryClass
    {
        public static void WriteToFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static T ReadFromFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        public static List<string> fileNames()
        {
            List<string> fileNames = new List<string>();

            string[] fileEntries = Directory.GetFiles("./data");

            foreach (string fileName in fileEntries)
            {
                fileNames.Add(fileName.Substring(7));
            }

            var orderedList = fileNames.OrderBy(x => DateTime.ParseExact(x, "dd.MM.yyyy", CultureInfo.InvariantCulture)).ToList();

            return orderedList;
        }

        public static List<string> allDatesLabels()
        {
            var orderedList = fileNames();
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

        public static List<float> allDatesValues()
        {
            List<float> chartValueList = new List<float>();
            List<string> allDatesLabels = DirectoryClass.allDatesLabels();

            foreach (string date in allDatesLabels)
            {
                if (fileNames().Contains(date))
                {
                    drugConsumtion currentFile = ReadFromFile<drugConsumtion>($"./data/{date}");
                    chartValueList.Add(currentFile.Liters);
                }
                else
                {
                    chartValueList.Add(0);
                }
            }
            //chartValueList.ForEach(i => Trace.Write(i));
            return chartValueList;
        }
    }
}
