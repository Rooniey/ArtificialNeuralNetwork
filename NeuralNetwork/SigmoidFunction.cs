using MathNet.Numerics.LinearAlgebra;
using System;

namespace NeuralNetwork
{
    public class SigmoidFunction : IActivationFunction
    {
        private double sigmoid(double x)
        {
            return (1 / (1 + Math.Pow(Math.E, -1 * x)));
        }

        public void Calculate(Matrix<double> input)
        {
            //input.MapInplace(elem => 1 / (1 + Math.Pow(Math.E, -1 * elem)), Zeros.Include);
            input.MapInplace(sigmoid, Zeros.Include);
        }

        public Matrix<double> CalculateDifferential(Matrix<double> input)
        {
            return input.Map(elem => sigmoid(elem) * (1 - sigmoid(elem)));
        }
    }
}