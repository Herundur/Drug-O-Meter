﻿ using System;
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

        public drugConsumtion(string date, float liters, float grams)
        {
            this.Date = date;
            this.Liters = liters;
            this.Grams = grams;
        }

        public string Date
        { get; set; }

        public float Liters
        { get; set; }

        public float Grams 
        { get; set; }
    }
}
