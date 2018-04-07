using MathNet.Numerics.LinearAlgebra;
using System;

namespace NeuralNetwork.ActivationFunctions
{
    public class SigmoidFunction : IActivationFunction
    {
        private double Sigmoid(double x)
        {
            return (1 / (1 + Math.Pow(Math.E, -1 * x)));
        }

        public void Calculate(Matrix<double> input)
        {
            //input.MapInplace(elem => 1 / (1 + Math.Pow(Math.E, -1 * elem)), Zeros.Include);
            input.MapInplace(Sigmoid, Zeros.Include);
        }

        public Matrix<double> CalculateDifferential(Matrix<double> input)
        {
            return input.Map(elem => Sigmoid(elem) * (1 - Sigmoid(elem)));
        }
    }
}