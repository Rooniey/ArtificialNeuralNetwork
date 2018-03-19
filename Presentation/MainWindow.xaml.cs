using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using IAD.DataService;
using LiveCharts;
using LiveCharts.Defaults;

namespace Wpf.CartesianChart.ScatterPlot
{
    /// <summary>
    /// Interaction logic for ScatterExample.xaml
    /// </summary>
    public partial class ScatterExample : UserControl
    {
        public ScatterExample()
        {
            
            InitializeComponent();

            DataGetter dg = new DataGetter();
            List<double[]> data = (List<double[]>)dg.GetData("group_B.csv", ',');
            List<double[]> data1 = (List<double[]>)dg.GetData("group_A.csv", ',');
            Values = new ChartValues<ObservablePoint>();
            Values1 = new ChartValues<ObservablePoint>();

            Perceptron.learn();

            for (var i = 0; i < 500; i++)
            {
                Values.Add(new ObservablePoint(data[i][0], data[i][1]));
                Values1.Add(new ObservablePoint(data1[i][0], data1[i][1]));
            }

            DataContext = this;
        }

        public ChartValues<ObservablePoint> Values { get; set; }
        public ChartValues<ObservablePoint> Values1 { get; set; }

    }

    internal class Perceptron
    {
        internal static void learn()
        {
            return;
        }
    }
}