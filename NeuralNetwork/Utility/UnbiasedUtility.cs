using MathNet.Numerics.LinearAlgebra;
using NeuralNetwork.Model;

namespace NeuralNetwork.Utility
{
    public class UnbiasedUtility : ILayerUtility
    {
        public void InitLayer(Layer layer, int previousSize)
        {
            layer.WeightMatrix = Matrix<double>.Build.Random(layer.NeuronCount, previousSize);
            layer.WeightedSum = Matrix<double>.Build.Dense(layer.NeuronCount, 1);
            layer.Activation = Matrix<double>.Build.Dense(layer.NeuronCount, 1);
        }

        public Matrix<double> Propagate(Layer layer, Matrix<double> aPrev)
        {
            //calculate weighted sum
            var a = layer.WeightMatrix.Multiply(aPrev);
            a.CopyTo(layer.WeightedSum);

            //calculate output "in-place" and save it
            layer.ActivationFunction.Calculate(a);
            a.CopyTo(layer.Activation);

            return a;
        }
    }
}