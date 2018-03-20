using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class Network
    {
        private List<Layer> _layers;

        public int InputSize { get; set; }

        public List<Layer> Layers { get => _layers; set => _layers = value; }

        public Network(int inputSize)
        {
            _layers = new List<Layer>();
            InputSize = inputSize;
        }

        public void Train(double learningRate, int epochs, List<TrainingElement> inputs)
        {
            for (int i = 0; i < epochs; i++)
            {
                for (int j = 0; j < inputs.Count; j++)
                {
                    Matrix<double> guess = GetOutput(inputs[j].Input);

                    CalculateDMatrices(inputs, j, guess);

                    CalculateWeightDeltas(learningRate, inputs, j);

                    UpdateWeights();
                }
            }
        }

        private void UpdateWeights()
        {
            foreach (var layer in _layers)
            {
                layer.WeightMatrix = layer.WeightMatrix.Add(layer.WeightDeltaMatrix);
            }
        }

        private void CalculateDMatrices(List<TrainingElement> inputs, int j, Matrix<double> guess)
        {
            //D(last) = (Z(last) - Y)^T

            // D(last) = (Z(last) - Y) ^ T
            var finalErrorMatrix = guess.Subtract(inputs[j].DesiredOutput).Transpose();
        
            //_layers.Last().DeltaMatrix = inputs[j].DesiredOutput.Subtract(guess).Transpose();

            var weightMatrix = _layers.Last().WeightMatrix;
            var multiplyProduct = weightMatrix.Multiply(finalErrorMatrix);

            _layers.Last().DeltaMatrix = _layers.Last().LastDifferentialMatrix.PointwiseMultiply(finalErrorMatrix);

            //_layers.Last().DeltaMatrix = inputs[j].DesiredOutput.Subtract(guess).Transpose();

            for (int k = _layers.Count - 2; k >= 0; k--)
            {
                // D(i) = F(i) * (W(i) x D(i+1))
                weightMatrix = _layers[k+1].WeightMatrix;
                multiplyProduct = weightMatrix.Multiply(_layers[k + 1].DeltaMatrix.Transpose()).Transpose();

                _layers[k].DeltaMatrix = _layers[k].LastDifferentialMatrix.PointwiseMultiply(multiplyProduct);
            }
        }

        private void CalculateWeightDeltas(double learningRate, List<TrainingElement> inputs, int j)
        {
            //deltaW = -rate * (D(i) x inputMatrix)^T
            var weightFactor = _layers[0].DeltaMatrix.Transpose().Multiply(inputs[j].Input).Transpose();
            _layers[0].WeightDeltaMatrix = weightFactor.Multiply(-1 * learningRate);

            for (var k = 1; k < _layers.Count; k++)
            {
                var kupa = _layers[k].DeltaMatrix;
                var kupa2 = _layers[k - 1].LastOutputMatrix;
                weightFactor = _layers[k].DeltaMatrix.Transpose().Multiply(_layers[k-1].LastOutputMatrix).Transpose();
                _layers[k].WeightDeltaMatrix = weightFactor.Multiply(-1 * learningRate);
            }
        }

        public Matrix<double> GetOutput(Matrix<double> input)
        {
            Matrix<double> Z = input;
            foreach (var item in _layers)
            {
                Z = item.LayerUtility.Propagate(item, Z, item);
            }
            return Z;
        }

        public void AddLayer(Layer lay)
        {
            var previousSize = _layers.Count == 0 ? InputSize : _layers.Last().NeuronCount;
            _layers.Add(lay);
            lay.LayerUtility.InitLayer(lay, previousSize);
        }
    }
}