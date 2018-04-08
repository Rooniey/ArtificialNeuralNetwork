using OxyPlot;
using System.Collections.Generic;
using System.Windows;
using OxyPlot.Wpf;

namespace NeuralNetworkPresentation
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        public GraphWindow(string name, string nameX, string nameY,  List<LineSeries> serieses)
        {
            InitializeComponent();
            GraphName = name;
            NameX = nameX;
            NameY = nameY;
            Plot.LegendTitle = "Legenda";
            foreach (var series in serieses)
            {               
                Plot.Series.Add(series);
            }
            DataContext = this;
        }

        public string GraphName { get; private set; }
        public string NameX { get; private set; }
        public string NameY { get; private set; }
    }
}