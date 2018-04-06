using MathNet.Numerics.LinearAlgebra;
using NeuralNetwork.ActivationFunctions;
using NeuralNetwork.Utility;

namespace NeuralNetwork.Model
{
    public class Layer
    {
        public Matrix<double> WeightMatrix { get; set; } // W
        public Matrix<double> WeightedSum { get; set; } // Z
        public Matrix<double> Activation { get; set; } // A
        public Matrix<double> DeltaL { get; set; } //δL
        public Matrix<double> WeightsDeltas { get; set; }

        public int NeuronCount { get; set; }

        public ILayerUtility LayerUtility { get; }
        public IActivationFunction ActivationFunction { get; }

        public Layer(int neuronCount, IActivationFunction activationFunction, ILayerUtility layerUtility)
        {
            ActivationFunction = activationFunction;
            NeuronCount = neuronCount;
            LayerUtility = layerUtility;
        }

        public Matrix<double> Propagate(Matrix<double> a)
        {
            return LayerUtility.Propagate(this, a);
        }

        public void Backpropagate(Layer nextLayer)
        {
            LayerUtility.Backpropagate(this, nextLayer);
        }

        public void UpdateLayer(Matrix<double> prevA, double learningRate, double momentum)
        {
            LayerUtility.UpdateLayer(this, prevA, learningRate, momentum);
        }
    }
}
