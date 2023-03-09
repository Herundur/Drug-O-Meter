using System;

namespace Drug_O_Meter
{
    [Serializable]
    internal class drugConsumtion
    {
        public drugConsumtion(string date)
        {
            Date = date;
        }

        public drugConsumtion(string date, double liters, double grams)
        {
            Date = date;
            Liters = liters;
            Grams = grams;
        }

        public string Date
        { get; set; }

        public double Liters
        { get; set; }

        public double Grams
        { get; set; }
    }
}
