using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Drug_O_Meter.MVVM.Model
{
    internal static class Files
    {
        public static void Write<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static T Read<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        public static List<string> Names()
        {
            List<string> fileNames = new List<string>();

            string[] fileEntries = Directory.GetFiles("../../../Data");

            foreach (string fileName in fileEntries)
            {
                fileNames.Add(fileName.Substring(14));
            }

            var orderedList = fileNames.OrderBy(x => DateTime.ParseExact(x, "dd.MM.yyyy", CultureInfo.InvariantCulture)).ToList();

            return orderedList;
        }

        public static string FirstFile()
        {
            List<string> files = Names();

            return files[0];
        }
    }
}
