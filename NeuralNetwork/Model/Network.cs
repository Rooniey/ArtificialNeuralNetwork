using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetwork.Model
{
    public class Network
    {
        public List<Layer> Layers { get; set; }
        public int InputSize { get; set; }

        public Network(int inputSize)
        {
            Layers = new List<Layer>();
            InputSize = inputSize;
        }

//        public void Train(double learningRate, int epochs, List<TrainingElement> inputs)
//        {
//            for (int i = 0; i < epochs; i++)
//            {
//                for (int j = 0; j < inputs.Count; j++)
//                {
//                    Matrix<double> guess = GetOutput(inputs[j].Input);
//
//                    CalculateDMatrices(inputs, j, guess);
//
//                    CalculateWeightDeltas(learningRate, inputs, j);
//
//                    UpdateWeights();
//                }
//            }
//        }
//
//        private void UpdateWeights()
//        {
//            foreach (var layer in Layers)
//            {
//                layer.WeightMatrix = layer.WeightMatrix.Add(layer.WeightDeltaMatrix);
//            }
//        }
//
//        private void CalculateDMatrices(List<TrainingElement> inputs, int j, Matrix<double> guess)
//        {
//            //D(last) = (Z(last) - Y)^T
//
//            // D(last) = (Z(last) - Y) ^ T
//            var finalErrorMatrix = guess.Subtract(inputs[j].DesiredOutput).Transpose();
//        
//            //_layers.Last().DeltaMatrix = inputs[j].DesiredOutput.Subtract(guess).Transpose();
//
//            var weightMatrix = Layers.Last().WeightMatrix;
//            var multiplyProduct = weightMatrix.Multiply(finalErrorMatrix);
//
//            Layers.Last().DeltaMatrix = Layers.Last().LastDifferentialMatrix.PointwiseMultiply(finalErrorMatrix);
//
//            //_layers.Last().DeltaMatrix = inputs[j].DesiredOutput.Subtract(guess).Transpose();
//
//            for (int k = Layers.Count - 2; k >= 0; k--)
//            {
//                // D(i) = F(i) * (W(i) x D(i+1))
//                weightMatrix = Layers[k+1].WeightMatrix;
//                multiplyProduct = weightMatrix.Multiply(Layers[k + 1].DeltaMatrix.Transpose()).Transpose();
//
//                Layers[k].DeltaMatrix = Layers[k].LastDifferentialMatrix.PointwiseMultiply(multiplyProduct);
//            }
//        }
//
//        private void CalculateWeightDeltas(double learningRate, List<TrainingElement> inputs, int j)
//        {
//            //deltaW = -rate * (D(i) x inputMatrix)^T
//            var weightFactor = Layers[0].DeltaMatrix.Transpose().Multiply(inputs[j].Input).Transpose();
//            Layers[0].WeightDeltaMatrix = weightFactor.Multiply(-1 * learningRate);
//
//            for (var k = 1; k < Layers.Count; k++)
//            {
//                var kupa = Layers[k].DeltaMatrix;
//                var kupa2 = Layers[k - 1].LastOutputMatrix;
//                weightFactor = Layers[k].DeltaMatrix.Transpose().Multiply(Layers[k-1].LastOutputMatrix).Transpose();
//                Layers[k].WeightDeltaMatrix = weightFactor.Multiply(-1 * learningRate);
//            }
//        }
//
//        public Matrix<double> GetOutput(Matrix<double> input)
//        {
//            Matrix<double> Z = input;
//            foreach (var item in Layers)
//            {
//                Z = item.LayerUtility.Propagate(item, Z, item);
//            }
//            return Z;
//        }
//
//        public void AddLayer(Layer lay)
//        {
//            var previousSize = Layers.Count == 0 ? InputSize : Layers.Last().NeuronCount;
//            Layers.Add(lay);
//            lay.LayerUtility.InitLayer(lay, previousSize);
//        }
    }
}