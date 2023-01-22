using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace AlcoholChart
{
    public class ViewModel
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Name = "Liter",
                    Values = new List<double> { 1, 2, 3 },
                    Fill = null
                },
            };

        public Axis[] XAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Labels = new List<string> { "benedikt", "mama", "wachtel" },
                }
            };
    }
}
