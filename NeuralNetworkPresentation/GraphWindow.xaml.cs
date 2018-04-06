using System.Collections.Generic;
using System.Windows;
using OxyPlot;

namespace NeuralNetworkPresentation
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        public GraphWindow(string name, IList<DataPoint> points, string nameX, string nameY)
        {
            InitializeComponent();
            DataPoints = points;
            GraphName = name;
            NameX = nameX;
            NameY = nameY;
            DataContext = this;
        }

        public IList<DataPoint> DataPoints { get; private set; }
        public string GraphName { get; private set; }
        public string NameX { get; private set; }
        public string NameY { get; private set; }
    }
}
