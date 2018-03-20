using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetwork
{
    public class BiasedUtility : ILayerUtility
    {
        public void InitLayer(Layer layer, int previousSize)
        {
            layer.WeightMatrix = Matrix<double>.Build.Random(previousSize + 1, layer.NeuronCount);
            layer.LastOutputMatrix = Matrix<double>.Build.Dense(layer.NeuronCount, 1);
            layer.LastDifferentialMatrix = Matrix<double>.Build.Dense(layer.NeuronCount, 1);
        }

        public Matrix<double> Propagate(Layer layer, Matrix<double> input, Layer nextLayer)
        {
            //insert row simulating biased input
            var X = input.Clone().InsertColumn(input.ColumnCount, Vector<double>.Build.DenseOfArray(new double[] { 1 }));

            //calculate weighted sum
            var S = X.Multiply(layer.WeightMatrix);

            //calculate and save  differential product for later backpropagation
            var _differentialMatrix = S.Clone();
            
            // F.append(fdif())
            //if (nextLayer != null)
            //{
            //    var biasWeights = nextLayer.WeightMatrix.AsRowArrays();
            //    _differentialMatrix.InsertColumn(layer.LastDifferentialMatrix.ColumnCount,
            //        Vector<double>.Build.DenseOfArray(biasWeights[biasWeights.Length - 1]));
            //}

            layer.LastDifferentialMatrix =layer.ActivationFunction.CalculateDifferential(_differentialMatrix);
            //calculate output and save it
            layer.ActivationFunction.Calculate(S);
            layer.LastOutputMatrix = S;

            return layer.LastOutputMatrix;
        }
    }
}