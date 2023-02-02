using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Drug_O_Meter.MVVM.Model
{
    internal class HomeViewData
    {
        public static string RandomQuote()
        {
            int randomIndex = new Random().Next(2);
            string[] quotes = { "“My grandpa started walking five miles a day when he was 60.\r\nNow he’s 97 years old and we have no clue where he is.”\r\n- Anonymous.“", "“" + "An eye for an eye will only make the whole world blind.“\n- Mahatma Ghandi" };

            return quotes[randomIndex];
        }
    }
}
