 using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Drug_O_Meter
{
    [Serializable]
    internal class drugConsumtion
    {
        public drugConsumtion(string date)
        {
            this.Date = date;
        }

        public drugConsumtion(string date, double liters, double grams)
        {
            this.Date = date;
            this.Liters = liters;
            this.Grams = grams;
        }

        public string Date
        { get; set; }

        public double Liters
        { get; set; }

        public double Grams 
        { get; set; }
    }
}
