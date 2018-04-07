using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetwork.ActivationFunctions
{
    public class IdentityFunction : IActivationFunction
    {
        public void Calculate(Matrix<double> input)
        {
            return;
        }

        public Matrix<double> CalculateDifferential(Matrix<double> input)
        {
            return input.Map(elem => 1d);
        }
    }
}