using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using OxyPlot;
using RareCrewCSharp.Models;

public class ChartService
{
    public void GeneratePieChart(List<EmployeeWorkStats> data, string filePath)
    {
        var plotModel = new PlotModel { Title = "Employee Work Hours" };

        var pieSeries = new PieSeries
        {
            StrokeThickness = 1,
            Diameter = 0.5,
            InsideLabelPosition = 0.7,
            AngleSpan = 360,
            StartAngle = 0
        };

        foreach (var item in data)
        {
            pieSeries.Slices.Add(new PieSlice(item.Name, item.TotalTimeWorked));
        }

        plotModel.Series.Add(pieSeries);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            var exporter = new PngExporter { Width = 600, Height = 400 };
            exporter.Export(plotModel, stream);
        }
    }
}
