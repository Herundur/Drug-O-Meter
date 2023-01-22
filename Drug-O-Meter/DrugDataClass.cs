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

        public drugConsumtion(string date, int milliliters, int grams)
        {
            this.Date = date;
            this.Milliliters = milliliters;
            this.Grams = grams;
        }

        public string Date
        { get; set; }

        public int Milliliters
        { get; set; }

        public int Grams 
        { get; set; }
    }
}
