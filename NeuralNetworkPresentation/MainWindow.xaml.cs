using System;
using NeuralNetwork.ActivationFunctions;
using NeuralNetwork.Model;
using NeuralNetwork.Utility;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MathNet.Numerics.LinearAlgebra;
using NeuralNetwork.DataService;
using NeuralNetwork.Statistics;
using OxyPlot;
using OxyPlot.Wpf;
using OxyPlot.Series;

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

            var layerToAdd = $"{NumberOfNeurons.Text} {(sigmoid ? "S" : "I")} {(bias ? "B" : "U")}";
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

        private void Button_Click_ShowGraph(object sender, RoutedEventArgs e)
        {
            GraphWindow gw = new GraphWindow(GraphName.Text, "epoch", "error", SeriesList);
            gw.Show();
            SeriesList.Clear();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Layers.Items.Clear();
        }

        public List<OxyPlot.Wpf.Series> SeriesList { get; set; } = new List<OxyPlot.Wpf.Series>();
        public List<OxyPlot.Wpf.Series> SeriesList2 { get; set; } = new List<OxyPlot.Wpf.Series>();

        private void Button_Click_AddTask(object sender, RoutedEventArgs e)
        {
            TrainingParameters td = new TrainingParameters
            {
                DesiredError = double.Parse(DesiredErr.Text, CultureInfo.InvariantCulture),
                Epochs = int.Parse(NumberOfEpochs.Text),
                LearningRate = double.Parse(LearningRat.Text, CultureInfo.InvariantCulture),
                Momentum = double.Parse(Moment.Text, CultureInfo.InvariantCulture),
                Iterat = int.Parse(Iterations.Text)
            };

            var selection = TaskOption.SelectionBoxItem.ToString().ToLower();

            if (selection.Contains("transformation"))
            {
                DataGetter dg = new DataGetter();
                List<TrainingElement> data = (dg.GetSetOfData(TrainPath.Text, Network.InputSize));
                TransformationScenario(data, td);
            }
            else if (selection.Contains("approximation"))
            {
                DataGetter dg = new DataGetter();
                List<TrainingElement> data = (dg.GetSetOfData(TrainPath.Text, Network.InputSize));
                ApproximationScenario(dg, data, td);
            }
            else if (selection.Contains("classification"))
            {
                //get wanted inputs
                DataGetter dg = new DataGetter();

                var chosenInputs = new bool[] { FirstInput.IsChecked.Value, SecondInput.IsChecked.Value, ThirdInput.IsChecked.Value, FourthInput.IsChecked.Value};
                List<TrainingElement> data = dg.GetSetOfDataWithChosenInputs(TrainPath.Text, chosenInputs);
                List<TrainingElement> testData = dg.GetSetOfDataWithChosenInputs(TestPath.Text, chosenInputs);
                
                ClassificationScenario(td, data, testData);

            }
        }

        private void ClassificationScenario(TrainingParameters td, List<TrainingElement> data, List<TrainingElement> testData)
        {
            //translate class numbers to network-intelligible matrices
            for (int i = 0; i < data.Count; i++)
            {
                var output = data[i].DesiredOutput;
                switch (output.At(0, 0))
                {
                    case 1:
                        output = Matrix<double>.Build.DenseOfArray(new double[,] { { 1 }, { 0 }, { 0 } });
                        break;
                    case 2:
                        output = Matrix<double>.Build.DenseOfArray(new double[,] { { 0 }, { 1 }, { 0 } });
                        break;
                    case 3:
                        output = Matrix<double>.Build.DenseOfArray(new double[,] { { 0 }, { 0 }, { 1 } });
                        break;
                }
                data[i].DesiredOutput = output;
            }

            for (int i = 0; i < testData.Count; i++)
            {
                var output = testData[i].DesiredOutput;
                switch (output.At(0, 0))
                {
                    case 1:
                        output = Matrix<double>.Build.DenseOfArray(new double[,] { { 1 }, { 0 }, { 0 } });
                        break;
                    case 2:
                        output = Matrix<double>.Build.DenseOfArray(new double[,] { { 0 }, { 1 }, { 0 } });
                        break;
                    case 3:
                        output = Matrix<double>.Build.DenseOfArray(new double[,] { { 0 }, { 0 }, { 1 } });
                        break;
                }
                testData[i].DesiredOutput = output;
            }


            Network.Gatherer = new ClassificationGatherer(testData, data);

            int iterations = td.Iterat;
            List<double> errorsAverage = new List<double>(new double[td.Epochs]);
            List<double> testerErrorAverage = new List<double>(new double[td.Epochs]);
            List<double> accuracyAveragesV = new List<double>(new double[td.Epochs]);
            List<double> accuracyAveragesT = new List<double>(new double[td.Epochs]);

            for (int i = 0; i < iterations; i++)
            {
                Network.Train(td.LearningRate, td.Epochs, td.Momentum, data);


                for (int j = 0; j < Network.Errors.Count; j++)
                {
                    errorsAverage[j] += Network.Errors[j];
                    testerErrorAverage[j] += ((ClassificationGatherer)Network.Gatherer).TestErrors[j];
                    accuracyAveragesV[j] += ((ClassificationGatherer)Network.Gatherer).AccuracyListV[j];
                    accuracyAveragesT[j] += ((ClassificationGatherer)Network.Gatherer).AccuracyListT[j];
                }
            }

            //Calculate average properties
            for (int j = 0; j < Network.Errors.Count; j++)
            {
                errorsAverage[j] /= iterations;
                testerErrorAverage[j] /= iterations;
                accuracyAveragesV[j] /= iterations;
                accuracyAveragesT[j] /= iterations;
            }

            IList<DataPoint> points = new List<DataPoint>();
            IList<DataPoint> pointsTestError = new List<DataPoint>();
            IList<DataPoint> pointsAccurracyV = new List<DataPoint>();
            IList<DataPoint> pointsAccuracyT = new List<DataPoint>();
            for (int i = 0; i < Network.Errors.Count; i++)
            {
                points.Add(new DataPoint(i + 1, errorsAverage[i]));
                pointsTestError.Add(new DataPoint(i + 1, testerErrorAverage[i]));
                pointsAccuracyT.Add(new DataPoint(i + 1, accuracyAveragesT[i]));
                pointsAccurracyV.Add(new DataPoint(i + 1, accuracyAveragesV[i]));
            }

            //calculate error matrix
            List<Tuple<int, int>> DesiredAndActualOutputs = new List<Tuple<int, int>>();
            foreach (var telem in testData)
            {

                var desired = ((ClassificationGatherer)Network.Gatherer).ConvertMatrixToClass(telem.DesiredOutput);
                var gotten = ((ClassificationGatherer)Network.Gatherer).ConvertMatrixToClass(Network.ForwardPropagation(telem.Input));
                DesiredAndActualOutputs.Add(new Tuple<int, int>(desired, gotten));
            }

            int[,] ErrorMatrix = new int[3, 3];

            foreach (var tupel in DesiredAndActualOutputs)
            {
                ErrorMatrix[tupel.Item1 - 1, tupel.Item2 - 1]++;
            }

            WindowDataGrid wdg = new WindowDataGrid(ErrorMatrix);
            wdg.Show();


            SeriesList.Add(new OxyPlot.Wpf.LineSeries
            {
                ItemsSource = points,
                Title = $"Training error Learning rate: {td.LearningRate}, momentum: {td.Momentum}, hidden neuron count: {Network.Layers[0].NeuronCount}"
            });
            SeriesList.Add(new OxyPlot.Wpf.LineSeries
            {
                ItemsSource = pointsTestError,
                Title = $"Validation error Learning rate: {td.LearningRate}, momentum: {td.Momentum}, hidden neuron count: {Network.Layers[0].NeuronCount}"
            });

            SeriesList2.Add(new OxyPlot.Wpf.LineSeries
            {
                ItemsSource = pointsAccuracyT,
                Title = $"Training set accuracy Learning rate: {td.LearningRate}, momentum: {td.Momentum}, hidden neuron count: {Network.Layers[0].NeuronCount}"
            });
            SeriesList2.Add(new OxyPlot.Wpf.LineSeries
            {
                ItemsSource = pointsAccurracyV,
                Title = $"Validation set accuracy Learning rate: {td.LearningRate}, momentum: {td.Momentum}, hidden neuron count: {Network.Layers[0].NeuronCount}"
            });
        }

        private void ApproximationScenario(DataGetter dg, List<TrainingElement> data, TrainingParameters td)
        {
            List<double> errorsAverage = new List<double>(new double[td.Epochs]);
            List<double> testerErrorAverage = new List<double>(new double[td.Epochs]);

            int iterations = td.Iterat;

            Network.Gatherer = new ApproximationGatherer(dg.GetSetOfData(TestPath.Text, Network.InputSize));

            for (int j = 0; j < iterations; j++)
            {
                Network.Train(td.LearningRate, td.Epochs, td.Momentum, data, td.DesiredError);

                for (int i = 0; i < Network.Errors.Count; i++)
                {
                    errorsAverage[i] += Network.Errors[i];
                    testerErrorAverage[i] += ((ApproximationGatherer)Network.Gatherer).TestErrors[i];
                }

            }

            for (int j = 0; j < errorsAverage.Count; j++)
            {
                errorsAverage[j] /= iterations;
                testerErrorAverage[j] /= iterations;
            }

            IList<DataPoint> points = new List<DataPoint>();
            IList<DataPoint> testPoints = new List<DataPoint>();

            for (int i = 0; i < Network.Errors.Count; i++)
            {
                points.Add(new DataPoint(i + 1, errorsAverage[i]));
                testPoints.Add(new DataPoint(i + 1, testerErrorAverage[i]));
            }

            SeriesList.Add(new OxyPlot.Wpf.LineSeries
            {
                ItemsSource = points,
                Title = $"Training error (Learning rate: {td.LearningRate}, momentum: {td.Momentum}, hidden neuron count: {Network.Layers[0].NeuronCount})"
            });


            SeriesList.Add(new OxyPlot.Wpf.LineSeries
            {
                ItemsSource = testPoints,
                Title = $"Validation Error (Learning rate: {td.LearningRate}, momentum: {td.Momentum}, hidden neuron count: {Network.Layers[0].NeuronCount})"
            });
        }

        private void TransformationScenario(List<TrainingElement> data, TrainingParameters td)
        {
            List<double> errorsAverage = new List<double>(new double[td.Epochs]);

            int iterations = td.Iterat;

            Network.Gatherer = new TransformationGatherer();

            for (int j = 0; j < iterations; j++)
            {
                Network.Train(td.LearningRate, td.Epochs, td.Momentum, data, td.DesiredError);

                for (int i = 0; i < Network.Errors.Count; i++)
                {
                    errorsAverage[i] += Network.Errors[i];
                }
            }

            for (int j = 0; j < errorsAverage.Count; j++)
            {
                errorsAverage[j] /= iterations;
            }



            IList<DataPoint> points = new List<DataPoint>();
            for (int i = 0; i < Network.Errors.Count; i++)
            {
                points.Add(new DataPoint(i + 1, errorsAverage[i]));
            }

            SeriesList.Add(new OxyPlot.Wpf.LineSeries
            {
                ItemsSource = points,
                Title = $"Learning rate: {td.LearningRate}, momentum: {td.Momentum}, hidden neuron count: {Network.Layers[0].NeuronCount}"
            });
        }

        private void Button_Click_ShowFunctionGraph(object sender, RoutedEventArgs e)
        {
           TrainingParameters td = new TrainingParameters
            {
                DesiredError = double.Parse(DesiredErr.Text, CultureInfo.InvariantCulture),
                Epochs = int.Parse(NumberOfEpochs.Text),
                LearningRate = double.Parse(LearningRat.Text, CultureInfo.InvariantCulture),
                Momentum = double.Parse(Moment.Text, CultureInfo.InvariantCulture)
            };

            DataGetter dg = new DataGetter();
            List<TrainingElement> testing = dg.GetSetOfData(TestPath.Text, Network.InputSize);
            IList<DataPoint> testingPoints = new List<DataPoint>();

            foreach (var train in testing)
            {
                var x = train.Input.At(0, 0);
                var y = train.DesiredOutput.At(0, 0);
                testingPoints.Add(new DataPoint(x, y));
            }

            List<TrainingElement> training = dg.GetSetOfData(TrainPath.Text, Network.InputSize);
            IList<ScatterPoint> trainingPoints = new List<ScatterPoint>();
            foreach (var train in training)
            {
                var x = train.Input.At(0, 0);
                var y = train.DesiredOutput.At(0, 0);
                trainingPoints.Add(new ScatterPoint(x, y, 5));
            }

            testingPoints = testingPoints.OrderBy(elem => elem.X).ToList();
            //trainingPoints = trainingPoints.OrderBy(elem => elem.X).ToList();

            IList<DataPoint> testingp = new List<DataPoint>();
            

            for (double i = -4; i <= 4; i += 0.1)
            {
                var losowe = Network.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] { { i } }));
                testingp.Add(new DataPoint(i, losowe.At(0, 0)));
            }

            List<OxyPlot.Wpf.Series> serieses = new List<OxyPlot.Wpf.Series>() {
                new OxyPlot.Wpf.LineSeries
                {
                    ItemsSource = testingp,
                    Title = $"Approximation Learning rate: {td.LearningRate}, momentum: {td.Momentum}, hidden neuron count: {Network.Layers[0].NeuronCount}"
                },
                new OxyPlot.Wpf.LineSeries
                {
                    ItemsSource = testingPoints,
                    Title = $"Original function"
                },
                new OxyPlot.Wpf.ScatterSeries
                {
                    ItemsSource = trainingPoints,
                    Title = $"Training points",
                    MarkerSize = 5
                }                
            };
            GraphWindow gw = new GraphWindow("Functions comparison", "x", "y", serieses);
            gw.Show();



        }

        private void Button_Click_ShowAccuracyGraph(object sender, RoutedEventArgs e)
        {
            GraphWindow gw = new GraphWindow("Accuracy over epochs", "epoch", "accuracy", SeriesList2);
            gw.Show();
            SeriesList2.Clear();
        }

        private void TaskOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
   
            var option = e.AddedItems[0].ToString().ToLower();
            if (option.Contains("transformation"))
            {
                TrainPath.Text = "transformation.txt";
                TestPath.Text = "";
            }
            else if (option.Contains("approximation"))
            {
                TrainPath.Text = "approximation1.txt";
                TestPath.Text = "approximation_test.txt";
            }
            else if (option.Contains("classification"))
            {
                TrainPath.Text = "classification.txt";
                TestPath.Text = "classification_test.txt";
            }
        }

        
    }
}