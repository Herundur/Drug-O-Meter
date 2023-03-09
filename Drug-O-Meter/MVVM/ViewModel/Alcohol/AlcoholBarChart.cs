using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System.Collections.Generic;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using Drug_O_Meter.MVVM.Model;

namespace Drug_O_Meter.MVVM.ViewModel.Alcohol
{
    class ViewModelAlcoholBarChart
    {
        public ISeries[] Series { get; set; } =
        {
        new ColumnSeries<double>
        {
            Name = "Liter",
            Values = new List<double> { 1 },
            Rx = 50,
            Ry = 50,
            MaxBarWidth = 16,
            GroupPadding = 1,
            TooltipLabelFormatter = (chartPoint) => $"{new string[] {"M", "D", "M", "D", "F", "S", "S" }[chartPoint.Context.Index]}: {chartPoint.PrimaryValue}L/{BarChart.barChartValuesInPercent("Liters")[chartPoint.Context.Index]}%",
        },
    };

        public Axis[] XAxes { get; set; } =
        {
        new Axis
        {
            IsVisible = false,
        }

    };

        public Axis[] YAxes { get; set; } =
{
        new Axis
        {
            IsVisible = false,
        }

    };
    }
}

