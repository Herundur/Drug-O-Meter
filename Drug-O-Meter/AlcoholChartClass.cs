using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace AlcoholChart
{
    public class ViewModelChart
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Name = "Liter",
                    Values = new List<double> { 1, 1, 1, 0, 3, 1, 1, 2, 0, 0, 2, 0, 0, 0, 0, 1, 4, 0, 3, 0, 1, 2, 1, 2, 2, 0, 0, 1, 4, 2, 2 },
                    Stroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 2 },
                    Fill = new SolidColorPaint(SKColors.White.WithAlpha(50)),
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0,
                },
            };

        public Axis[] XAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Labels = new List<string> { "benedikt", "mama", "wachtel" },
                    IsVisible = false,
                }
            };

        public Axis[] YAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    IsVisible = false,
                }
            };
    }
}
