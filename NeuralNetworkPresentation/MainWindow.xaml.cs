using NeuralNetwork.ActivationFunctions;
using NeuralNetwork.Model;
using NeuralNetwork.Utility;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using NeuralNetwork.DataService;
using OxyPlot;

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
            var neurons = (TextBox)this.FindName("NumberOfNeurons");
            var s = ((CheckBox)this.FindName("Sigmoid"));
            var sigmoid = false;
            if (s?.IsChecked != null)
            {
                sigmoid = s.IsChecked.Value;
            }

            var b = (CheckBox)this.FindName("Bias");
            var bias = false;
            if (b?.IsChecked != null)
            {
                bias = b.IsChecked.Value;
            }

            var layerToAdd = $"{NumberOfNeurons.Text} {(sigmoid ? "S" : "I")} { (bias ? "B" : "U")}";
            var list = (ListBox)this.FindName("Layers");
            list.Items.Add(layerToAdd);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var numberOfNeurons = int.Parse(InputNumber.Text);
            Network = new Network(numberOfNeurons);

            foreach (var item in Layers.Items)
            {
                var properties = item.ToString().Split(' ');
                var neurons = int.Parse(properties[0]);
                IActivationFunction activation;
                if (properties[1] == "S")
                {
                    activation = new SigmoidFunction();
                }
                else
                {
                    activation = new IdentityFunction();
                }

                ILayerUtility utility;
                if (properties[2] == "B")
                {
                    utility = new BiasedUtility();
                }
                else
                {
                    utility = new UnbiasedUtility();
                }
                Network.AddLayer(new Layer(neurons, activation, utility));
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<TrainingElement> data = (new DataGetter().GetSetOfData(TrainPath.Text, Network.InputSize));

            TrainingParameters td = new TrainingParameters
            {
                DesiredError = double.Parse(DesiredErr.Text, CultureInfo.InvariantCulture),
                Epochs = int.Parse(NumberOfEpochs.Text),
                LearningRate = double.Parse(LearningRat.Text, CultureInfo.InvariantCulture),
                Momentum = double.Parse(Moment.Text, CultureInfo.InvariantCulture)
            };


            if (TaskOption.SelectionBoxItem.ToString().ToLower().Contains("transformation"))
            {
                List<double> errorsAverage = new List<double>();
                for (int i = 0; i < 10; i++)
                {
                    Network.Train(td.LearningRate, td.Epochs, td.Momentum, data, td.DesiredError);

                }
                IList<DataPoint> points = new List<DataPoint>();
                for (int i = 0; i < Network.Errors.Count; i++)
                {
                    points.Add(new DataPoint(i + 1, Network.Errors[i]));
                }
                GraphWindow gw = new GraphWindow($"Quadratic error graph (epochs: {td.Epochs}, lr: {td.LearningRate}, mom: {td.Momentum}", points, "epoch", "error");
                gw.Show();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Layers.Items.Clear();
        }
    }


}