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
            List<double[]> data = (List<double[]>)dg.GetData(@"C:\Users\Marek\Desktop\group_B.csv", ',');

            Values = new ChartValues<ObservablePoint>();

            for (var i = 0; i < 1500; i++)
            {
                Values.Add(new ObservablePoint(data[i][0], data[i][1]));
            }

            DataContext = this;
        }

        public ChartValues<ObservablePoint> Values { get; set; }

    }
}