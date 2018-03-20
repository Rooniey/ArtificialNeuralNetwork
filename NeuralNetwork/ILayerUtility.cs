using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetwork
{
    public interface ILayerUtility
    {
        void InitLayer(Layer layer, int prevousSize);

        Matrix<double> Propagate(Layer layer, Matrix<double> X, Layer nextLayer);
    }
}