using System.Collections.Generic;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Drug_O_Meter.MVVM.Model;

namespace Drug_O_Meter.MVVM.ViewModel.Cannabis
{

    public class ViewModelCannabisChart
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Name = "Joints",
                    Values = new List<double> { 1 },
                    Stroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 2 },
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0,
                    TooltipLabelFormatter =
                        (chartPoint) => $"{DataManager.LabelLast31Days()[chartPoint.Context.Index]}: {chartPoint.PrimaryValue}J's",

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
