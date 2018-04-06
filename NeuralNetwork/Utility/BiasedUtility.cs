using MathNet.Numerics.LinearAlgebra;
using NeuralNetwork.Model;

namespace NeuralNetwork.Utility
{
    public class BiasedUtility : ILayerUtility
    {
        public void InitLayer(Layer layer, int previousSize)
        {
            layer.WeightMatrix = Matrix<double>.Build.Random(layer.NeuronCount, previousSize + 1);
            layer.WeightedSum = Matrix<double>.Build.Dense(layer.NeuronCount, 1);
            layer.Activation = Matrix<double>.Build.Dense(layer.NeuronCount, 1);
        }

        public Matrix<double> Propagate(Layer layer, Matrix<double> aPrev)
        {
            //insert row simulating bias input
            var aPrevWithBias = aPrev.Clone().InsertRow(aPrev.RowCount, Vector<double>.Build.DenseOfArray(new double[] { 1 }));

            //calculate weighted sum
            var a = layer.WeightMatrix.Multiply(aPrevWithBias);
            a.CopyTo(layer.WeightedSum);

            //calculate output "in-place" and save it
            layer.ActivationFunction.Calculate(a);
            a.CopyTo(layer.Activation);

            return a;
        }
    }
}