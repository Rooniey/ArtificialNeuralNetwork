using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NeuralNetworkPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var x = new List<double>() { 1, 2, 3, 4, 5, 6, 7 };
            var y = new List<double>() { 0.23, 3, 4, 2, 5.67, 5.34, 5.78 };
            var points = new List<DataPoint>();
            for (int i = 0; i < x.Count; i++)
            {
                points.Add(new DataPoint(x[i], y[i]));
            }
            var gr = new GraphWindow("Wykres bledu", points, "nr epoki", "blad");
            gr.Show();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
