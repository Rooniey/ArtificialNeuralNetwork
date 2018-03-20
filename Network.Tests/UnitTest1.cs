using System;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetwork;
using System.Collections.Generic;

namespace NetworkTests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        public void TestMethod1()
        {
            var f = new SigmoidFunction();
            var trix = Matrix<double>.Build.DenseOfArray(array: new double[,] { { 0, 1 }, { 2, 3 } });
            var diff = f.CalculateDifferential(trix);
            f.Calculate(trix);
        }

        //[TestMethod]
        public void PropagationTest()
        {
            var f = new SigmoidFunction();
            var trix = Matrix<double>.Build.DenseOfArray(array: new double[,] { { 0.1, 0.3 }, { 0.2, 0.4 } });
            var layer = new Layer(f, 2, new UnbiasedUtility());

            layer.LayerUtility.InitLayer(layer, 2);
            layer.WeightMatrix = trix;
            //layer.LayerUtility.Propagate(layer, Matrix<double>.Build.DenseOfArray(new double[,] { { 10, 1 } }));
        }

        //[TestMethod]
        public void PropagationOutputTest()
        {
            var net = new Network(2);
            var f = new SigmoidFunction();
            var trix = Matrix<double>.Build.DenseOfArray(array: new double[,] { { 0.1, 0.3 }, { 0.2, 0.4 } });
            var layer = new Layer(f, 2, new UnbiasedUtility());
            net.AddLayer(layer);
            net.AddLayer(new Layer(f, 3, new UnbiasedUtility()));

            layer.WeightMatrix = trix;
            net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 10, 1 } }));
        }

        //[TestMethod]
        public void LearningTest()
        {
            var net = new Network(2);

            net.AddLayer(new Layer(new SigmoidFunction(), 2, new BiasedUtility()));
            net.AddLayer(new Layer(new SigmoidFunction(), 2, new BiasedUtility()));
            net.AddLayer(new Layer(new SigmoidFunction(), 1, new BiasedUtility()));

            var trainingElems = new List<TrainingElement>();

            trainingElems.Add(new TrainingElement(new double[,] { { 0, 0 } }, new double[,] { { -1 } }));

            trainingElems.Add(new TrainingElement(new double[,] { { 0, 1 } }, new double[,] { { 1 } }));

            trainingElems.Add(new TrainingElement(new double[,] { { 1, 0 } }, new double[,] { { 1 } }));

            trainingElems.Add(new TrainingElement(new double[,] { { 1, 1 } }, new double[,] { { -1 } }));

            net.Train(0.5, 500, trainingElems);

            var outp = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 0, 0 } }));
            var outp2 = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 0 } }));
            var outp3 = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 0, 1 } }));
            var outp4 = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1 } }));
        }

        // [TestMethod]
        public void MultiplyExtensionTest()
        {
            var trix = Matrix<double>.Build.DenseOfArray(new double[,] { { 10, 1 }, { 20, 2 } });
            var ta = trix.ToArray();
        }

        //[TestMethod]
        public void MyTestMethod()
        {
            var net = new Network(2);
            net.AddLayer(new Layer(new SigmoidFunction(), 2, new UnbiasedUtility()));
            net.AddLayer(new Layer(new SigmoidFunction(), 1, new UnbiasedUtility()));

            var trainingElems = new List<TrainingElement>();
            trainingElems.Add(new TrainingElement(new double[,] { { 1, 1 } }, new double[,] { { 1 } }));

            net.Train(0.1, 10, trainingElems); 

        }

        [TestMethod]
        public void SimpleLearningTest()
        {
            var net = new Network(2);

            net.AddLayer(new Layer(new SigmoidFunction(), 1, new UnbiasedUtility()));

            var trainingElems = new List<TrainingElement>();

            
            Random rand = new Random();
            double x, y;
            for (int i = 0; i < 1000; i++)
            {
                x = rand.NextDouble() * 10;
                y = rand.NextDouble() * 10;
                trainingElems.Add(new TrainingElement(new double[,] {{x,y}}, new double[,] {{(2.0/3.0) * x < y ? 1 : 0}} ));
            }

            //trainingElems.Add(new TrainingElement(new double[,] { { 0, 1 } }, new double[,] { { 1 } }));

            //trainingElems.Add(new TrainingElement(new double[,] { { 1, 0 } }, new double[,] { { 1 } }));

            //trainingElems.Add(new TrainingElement(new double[,] { { 1, 1 } }, new double[,] { { -1 } }));

            net.Train(0.1, 1000, trainingElems);

            var outp = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 0, 1 } }));
            var outp2 = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 0 } }));
            var outp3 = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 5 } }));
            var outp4 = net.GetOutput(Matrix<double>.Build.DenseOfArray(new double[,] { { 4, 1 } }));
        }
    }
}