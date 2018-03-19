using System;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetwork;

namespace NetworkTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var f = new SigmoidFunction();
            var trix = Matrix<double>.Build.DenseOfArray(array: new double[,] {{ 0, 1 },{ 2,3} });
            var diff = f.CalculateDifferential(trix);
            f.Calculate(trix);
        }

        [TestMethod]
        public void PropagationTest()
        {
            var f = new SigmoidFunction();
            var trix = Matrix<double>.Build.DenseOfArray(array: new double[,] { { 0.1, 0.3 }, { 0.2, 0.4 } });
            var layer = new Layer(f, 2, new UnbiasedUtility());

            layer.LayerUtility.InitLayer(layer, 2);
            layer.WeightMatrix = trix;
            layer.LayerUtility.Propagate(layer, Matrix<double>.Build.DenseOfArray(new double[,] {{ 10,1 }}));
        }
        [TestMethod]
        public void PropagationOutputTest()
        {
            var net = new Network(2);
            var f = new SigmoidFunction();
            var trix = Matrix<double>.Build.DenseOfArray(array: new double[,] { { 0.1, 0.3 }, { 0.2, 0.4 } });
            var layer = new Layer(f, 2, new UnbiasedUtility());
            net.AddLayer(layer);
            net.AddLayer(new Layer(f, 3, new UnbiasedUtility()));


            layer.WeightMatrix = trix;
            net.GetOutput( Matrix<double>.Build.DenseOfArray(new double[,] { { 10, 1 } }));
        }

    }
}
