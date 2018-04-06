using MathNet.Numerics.LinearAlgebra;
using NeuralNetwork.Model;

namespace NeuralNetwork.Utility
{
    public interface ILayerUtility
    {
        void InitLayer(Layer layer, int prevousSize);

        Matrix<double> Propagate(Layer layer, Matrix<double> a);

        void Backpropagate(Layer layer, Layer nextLayer);

        void UpdateLayer(Layer layer, Matrix<double> a, double learningRate, double momentum);

    }
}