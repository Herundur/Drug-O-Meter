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
using Drug_O_Meter.MVVM.Model;

namespace Drug_O_Meter.MVVM.ViewModel
{

    public class ViewModelChart
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<float>
                {
                    Name = "Liter",
                    Values = new List<float> { 1 },
                    Stroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 2 },
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0,
                    TooltipLabelFormatter =
                        (chartPoint) => $"{AlcoholData.LabelLast31Days()[chartPoint.Context.Index]}: {chartPoint.PrimaryValue}L",

                },
            };

        public Axis[] XAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Labels = new List<string> { "1" },
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
