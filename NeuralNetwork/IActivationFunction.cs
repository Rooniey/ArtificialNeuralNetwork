using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public interface IActivationFunction
    {
        void Calculate(Matrix<double> input);

        Matrix<double> CalculateDifferential(Matrix<double> input);
    }
}
