using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using OxyPlot.Wpf;

namespace NeuralNetworkPresentation
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        public GraphWindow(string name, string nameX, string nameY,  List<OxyPlot.Wpf.Series> serieses)
        {
            
            InitializeComponent();
            GraphName = name;
            NameX = nameX;
            NameY = nameY;
            Plot.LegendTitleFontSize = 20;
            Plot.LegendFontSize = 27;
 
            
            foreach (var series in serieses)
            {
                //if(series.GetType() == new LineSeries().GetType())((LineSeries)series).StrokeThickness = 5;
                
                if(series is OxyPlot.Wpf.LineSeries) ((OxyPlot.Wpf.LineSeries)series).StrokeThickness = 5;
                Plot.Series.Add(series);
            }
            DataContext = this;
        }

        public string GraphName { get; private set; }
        public string NameX { get; private set; }
        public string NameY { get; private set; }
    }
}