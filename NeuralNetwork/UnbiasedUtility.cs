using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetwork
{
    public class UnbiasedUtility : ILayerUtility
    {
        public void InitLayer(Layer layer, int previousSize)
        {
            layer.WeightMatrix = Matrix<double>.Build.Random(previousSize, layer.NeuronCount);
            layer.LastOutputMatrix = Matrix<double>.Build.Dense(layer.NeuronCount, 1);
            layer.LastDifferentialMatrix = Matrix<double>.Build.Dense(layer.NeuronCount, 1);
        }

        public Matrix<double> Propagate(Layer layer, Matrix<double> X, Layer nextLayer)
        {
            //calculate weighted sum
            var S = X.Multiply(layer.WeightMatrix);

            //calculate and save  differential product for later backpropagation
            layer.LastDifferentialMatrix = layer.ActivationFunction.CalculateDifferential(S);

            //calculate output and save it
            layer.ActivationFunction.Calculate(S);
            layer.LastOutputMatrix = S;

            return layer.LastOutputMatrix;
        }
    }
}