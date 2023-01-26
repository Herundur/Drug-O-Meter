using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Drug_O_Meter
{
    class AlcoholData
    {
        public static List<double> MonthValues(out double literCount)  
        {
            List<double> availableData = DirectoryClass.allDatesValues().ToList();
            //availableData.ForEach(i => Trace.Write(i));
            availableData.Reverse();

            List<double> data = new List<double>();
            int i;
            
            for (i = 0; i < 31; i++)
            {
                if (i >= availableData.Count || 31 < availableData.Count)
                {
                    data.Add(0);
                }
                else
                {
                    data.Add(availableData[i]);
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

       /* public static int literCountLast31Days()
        {

        }
       */
    }
}
