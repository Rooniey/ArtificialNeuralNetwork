using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetwork.ActivationFunctions;
using NeuralNetwork.Model;
using NeuralNetwork.Utility;
using System;
using System.Collections.Generic;
using Layer = NeuralNetwork.Model.Layer;
using Network = NeuralNetwork.Model.Network;

namespace NetworkTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLayersInitializers()
        {
            var layer = new Layer(3, new SigmoidFunction(), new UnbiasedUtility());
            var network = new Network(2);
            network.AddLayer(layer);
        }

        [TestMethod]
        public void TestTrainingElement()
        {
            var trainingElems =
                new List<TrainingElement> { new TrainingElement(new double[,] { { 1 }, { 1 } }, new double[,] { { 23 } }) };
        }

        //        [TestMethod] XOR TEST
        //        public void TestGetOutput()
        //        {
        ////            var layer1 = new Layer(2, new SigmoidFunction(), new BiasedUtility());
        //            var layer2 = new Layer(2, new SigmoidFunction(), new BiasedUtility());
        //            var layer3 = new Layer(1, new SigmoidFunction(), new BiasedUtility());
        //
        //            var network = new Network(2);
        ////            network.AddLayer(layer1);
        //            network.AddLayer(layer2);
        //            network.AddLayer(layer3);
        ////            var input = new double[,] { { 2, 3, 1 }, { 4, 2, 1 } };
        //            //layer1.WeightMatrix = Matrix<double>.Build.DenseOfArray(input);
        //            //layer2.WeightMatrix = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1, 1 } });
        //            var trainingElems = new List<TrainingElement>();
        //
        //            trainingElems.Add(new TrainingElement(new double[,] {{0}, {0}}, new double[,] {{0}}));
        //
        //            trainingElems.Add(new TrainingElement(new double[,] {{0}, {1}}, new double[,] {{1}}));
        //
        //            trainingElems.Add(new TrainingElement(new double[,] {{1}, {0}}, new double[,] {{1}}));
        //
        //            trainingElems.Add(new TrainingElement(new double[,] {{1}, {1}}, new double[,] {{0}}));
        //
        //            network.Train(5, 3000, trainingElems);
        //            var kupa = network.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] {{1}, {1}}));
        //            Console.WriteLine($"5 300 1 1 {kupa}");
        //            var kupa1 = network.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] {{0}, {1}}));
        //            Console.WriteLine($"0 1 {kupa1}");
        //            var kup2a = network.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] {{1}, {0}}));
        //            Console.WriteLine($"1 0 {kup2a}");
        //            var kupa3 = network.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] {{0}, {0}}));
        //            Console.WriteLine($"0 0 {kupa3}");
        //
        //        }

        [TestMethod]
        public void TestGetOutputTransform()
        {
            //            var layer1 = new Layer(2, new SigmoidFunction(), new BiasedUtility());
            var layer2 = new Layer(3, new SigmoidFunction(), new BiasedUtility());
            var layer3 = new Layer(4, new SigmoidFunction(), new BiasedUtility());

            var network = new Network(4);
            //            network.AddLayer(layer1);
            network.AddLayer(layer2);
            network.AddLayer(layer3);
            //            var input = new double[,] { { 2, 3, 1 }, { 4, 2, 1 } };
            //layer1.WeightMatrix = Matrix<double>.Build.DenseOfArray(input);
            //layer2.WeightMatrix = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1, 1 } });
            var trainingElems = new List<TrainingElement>();

            trainingElems.Add(new TrainingElement(new double[,] { { 1 }, { 0 }, { 0 }, { 0 } }, new double[,] { { 1 }, { 0 }, { 0 }, { 0 } }));

            trainingElems.Add(new TrainingElement(new double[,] { { 0 }, { 1 }, { 0 }, { 0 } }, new double[,] { { 0 }, { 1 }, { 0 }, { 0 } }));

            trainingElems.Add(new TrainingElement(new double[,] { { 0 }, { 0 }, { 1 }, { 0 } }, new double[,] { { 0 }, { 0 }, { 1 }, { 0 } }));

            trainingElems.Add(new TrainingElement(new double[,] { { 0 }, { 0 }, { 0 }, { 1 } }, new double[,] { { 0 }, { 0 }, { 0 }, { 1 } }));

            network.Train(0.5, 1000, 0.85, trainingElems);
            var kupa0 = network.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] { { 1 }, { 0 }, { 0 }, { 0 } }));
            Console.WriteLine($"5 300 1 1 {kupa0}");
            var kupa1 = network.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] { { 0 }, { 1 }, { 0 }, { 0 } }));
            Console.WriteLine($"0 1 {kupa1}");
            var kup2a = network.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] { { 0 }, { 0 }, { 1 }, { 0 } }));
            Console.WriteLine($"1 0 {kup2a}");
            var kupa3 = network.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] { { 0 }, { 0 }, { 0 }, { 1 } }));
            Console.WriteLine($"0 0 {kupa3}");
        }

        ////[TestMethod]
        //public void PropagationTest()
        //{
        //    var f = new SigmoidFunction();
        //    var trix = Matrix<double>.Build.DenseOfArray(array: new double[,] { { 0.1, 0.3 }, { 0.2, 0.4 } });
        //    var layer = new Layer(f, 2, new UnbiasedUtility());

        //    layer.LayerUtility.InitLayer(layer, 2);
        //    layer.WeightMatrix = trix;
        //    //layer.LayerUtility.Propagate(layer, Matrix<double>.Build.DenseOfArray(new double[,] { { 10, 1 } }));
        //}

        ////[TestMethod]
        //public void PropagationOutputTest()
        //{
        //    var net = new Network(2);
        //    var f = new SigmoidFunction();
        //    var trix = Matrix<double>.Build.DenseOfArray(array: new double[,] { { 0.1, 0.3 }, { 0.2, 0.4 } });
        //    var layer = new Layer(f, 2, new UnbiasedUtility());
        //    net.AddLayer(layer);
        //    net.AddLayer(new Layer(f, 3, new UnbiasedUtility()));

        //    layer.WeightMatrix = trix;
        //    net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 10, 1 } }));
        //}

        ////[TestMethod]
        //public void LearningTest()
        //{
        //    var net = new Network(2);

        //    net.AddLayer(new Layer(new SigmoidFunction(), 2, new BiasedUtility()));
        //    net.AddLayer(new Layer(new SigmoidFunction(), 2, new BiasedUtility()));
        //    net.AddLayer(new Layer(new SigmoidFunction(), 1, new BiasedUtility()));

        //    var trainingElems = new List<TrainingElement>();

        //    trainingElems.Add(new TrainingElement(new double[,] { { 0, 0 } }, new double[,] { { -1 } }));

        //    trainingElems.Add(new TrainingElement(new double[,] { { 0, 1 } }, new double[,] { { 1 } }));

        //    trainingElems.Add(new TrainingElement(new double[,] { { 1, 0 } }, new double[,] { { 1 } }));

        //    trainingElems.Add(new TrainingElement(new double[,] { { 1, 1 } }, new double[,] { { -1 } }));

        //    net.Train(0.5, 500, trainingElems);

        //    var outp = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 0, 0 } }));
        //    var outp2 = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 0 } }));
        //    var outp3 = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 0, 1 } }));
        //    var outp4 = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1 } }));
        //}

        //// [TestMethod]
        //public void MultiplyExtensionTest()
        //{
        //    var trix = Matrix<double>.Build.DenseOfArray(new double[,] { { 10, 1 }, { 20, 2 } });
        //    var ta = trix.ToArray();
        //}

        //        [TestMethod]
        //        public void MyTestMethod()
        //        {
        //        //    var net = new Network(2);
        //        //    net.AddLayer(new Layer(new SigmoidFunction(), 2, new UnbiasedUtility()));
        //        //    net.AddLayer(new Layer(new SigmoidFunction(), 1, new UnbiasedUtility()));
        //
        //            var trainingElems = new List<TrainingElement>();
        //            trainingElems.Add(new TrainingElement(new double[,] { { 1 }, { 1 } }, new double[,] { {23} }));
        //
        //        //    net.Train(0.1, 10, trainingElems);

        [TestMethod]
        public void SimpleLearningTest()
        {
            var net = new Network(2);

            net.AddLayer(new Layer(1, new SigmoidFunction(), new UnbiasedUtility()));

            var trainingElems = new List<TrainingElement>();

            Random rand = new Random();
            double x, y;
            for (int i = 0; i < 1000; i++)
            {
                x = rand.NextDouble() * 10;
                y = rand.NextDouble() * 10;
                trainingElems.Add(new TrainingElement(new double[,] { { x }, { y } },
                    new double[,] { { (2.0 / 3.0) * x < y ? 1 : 0 } }));
            }

            //trainingElems.Add(new TrainingElement(new double[,] { { 0, 1 } }, new double[,] { { 1 } }));

            //trainingElems.Add(new TrainingElement(new double[,] { { 1, 0 } }, new double[,] { { 1 } }));

            //trainingElems.Add(new TrainingElement(new double[,] { { 1, 1 } }, new double[,] { { -1 } }));

            net.Train(0.1, 100, 0, trainingElems);

            var outp1 = net.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] { { 0 }, { 1 } })); //0
            var outp2 = net.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] { { 1 }, { 0 } })); //1
            var outp3 = net.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] { { 2 }, { 5 } })); //0
            var outp4 = net.ForwardPropagation(Matrix<double>.Build.DenseOfArray(new double[,] { { 4 }, { 1 } })); //1
        }
    }
}