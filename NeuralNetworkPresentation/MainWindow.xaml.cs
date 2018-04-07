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
using NeuralNetwork.Model;


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
            
            

        }

        public Network Network { get; set; }
        public List<Layer> NetworkLayers { get; set; }
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var neurons = (TextBlock)this.FindName("NumberOfNeurons");
            var s = ((CheckBox)this.FindName("Sigmoid"));
            var sigmoid = false;
            if (s?.IsChecked != null)
            {
                sigmoid = s.IsChecked.Value;
            }
            
            var b = (CheckBox) this.FindName("Bias");
            var bias = false;
            if (b?.IsChecked != null)
            {
                bias = b.IsChecked.Value;
            }

            var layerToAdd = $"{NumberOfNeurons.Text} {(sigmoid ? "S" : "I")} { (bias ? "B" : "U")}";
            var list = (ListView)this.FindName("Layers")

        }
    }
}
