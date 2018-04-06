using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetwork.ActivationFunctions
{
    public interface IActivationFunction
    {
        void Calculate(Matrix<double> input);

        Matrix<double> CalculateDifferential(Matrix<double> input);
    }
}