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

        public drugConsumtion(string date, float liters, float grams)
        {
            Date = date;
            Liters = liters;
            Grams = grams;
        }

        public string Date
        { get; set; }

        public float Liters
        { get; set; }

        public float Grams
        { get; set; }
    }
}
