using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public interface ILayerUtility
    {
        void InitLayer(Layer layer, int prevousSize);

        Matrix<double> Propagate(Layer layer, Matrix<double> X);
    }
}
