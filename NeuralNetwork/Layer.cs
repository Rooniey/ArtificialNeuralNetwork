using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Layer
    {
        private IActivationFunction _activationFunction;
        private ILayerUtility _layerUtility;

        public Matrix<double> WeightMatrix { get; set; } // W
        public int NeuronCount { get; set; }
        public Matrix<double> LastOutputMatrix { get; set; } // Z
        public Matrix<double> LastDifferentialMatrix { get; set; } // F
        public IActivationFunction ActivationFunction { get => _activationFunction; set => _activationFunction = value; }
        public ILayerUtility LayerUtility { get => _layerUtility; set => _layerUtility = value; }        

        public Layer(IActivationFunction activationFunction, int neuronCount, ILayerUtility layerUtility)
        {
            _activationFunction = activationFunction;
            NeuronCount = neuronCount;
            _layerUtility = layerUtility;
        }

    }
}
