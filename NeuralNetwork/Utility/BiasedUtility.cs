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
            layer.WeightsDeltas = Matrix<double>.Build.Dense(layer.NeuronCount, previousSize + 1);
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

        public void Backpropagate(Layer layer, Layer nextLayer)
        {
            var sigmoidDerivative =
                layer.ActivationFunction.CalculateDifferential(layer.WeightedSum);
            var withoutBiases = nextLayer.WeightMatrix.RemoveColumn(nextLayer.WeightMatrix.ColumnCount - 1);

            layer.DeltaL = withoutBiases
                .Transpose()
                .Multiply(nextLayer.DeltaL)
                .PointwiseMultiply(sigmoidDerivative);
        }

        public void UpdateLayer(Layer layer, Matrix<double> a, double learningRate, double momentum)
        {
            var delta = layer.DeltaL.Multiply(a.Transpose()).InsertColumn(layer.WeightMatrix.ColumnCount - 1, layer.DeltaL.Column(0)).Multiply(learningRate);
            var add = layer.WeightsDeltas.Multiply(momentum);
            layer.WeightsDeltas = delta.Add(add);
            layer.WeightMatrix = layer.WeightMatrix.Subtract(delta);
        }
    }
}