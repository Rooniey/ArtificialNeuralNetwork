using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using NeuralNetwork.Model;

namespace NeuralNetwork
{
    public class Network
    {
        public List<Layer> Layers { get; private set; }
        public int InputSize { get; }

        public Network(int inputSize)
        {
            Layers = new List<Layer>();
            InputSize = inputSize;
        }

        public void AddLayer(Layer layer)
        {
            var previousSize = Layers.Count == 0 ? InputSize : Layers.Last().NeuronCount;
            layer.LayerUtility.InitLayer(layer, previousSize);
            Layers.Add(layer);         
        }

        public void Train(double learningRate, int epochs, List<TrainingElement> inputs)
        {
            for (var i = 0; i < epochs; i++)
            {
                for (var j = 0; j < inputs.Count; j++)
                {
                    var guess = ForwardPropagation(inputs[j].Input);

                    //an equation for the error in the output layer, δL
                    var outputLayer = Layers.Last();
                    var sigmoidDerivative =
                        outputLayer.ActivationFunction.CalculateDifferential(outputLayer.WeightedSum);                  //∇aC=(aL−y)
                    outputLayer.DeltaL = guess.Subtract(inputs[j].DesiredOutput).PointwiseMultiply(sigmoidDerivative);  //δL

                    //an equation for the error δl in terms of the error in the next layer, δl + 1
                    for (var k = Layers.Count - 2; k >= 0; k--)
                    {
                        var currentLayer = Layers[k];
                        sigmoidDerivative =
                            currentLayer.ActivationFunction.CalculateDifferential(currentLayer.WeightedSum);
                        Layers[k].DeltaL = Layers[k + 1].WeightMatrix
                                                        .Transpose()
                                                        .Multiply(Layers[k + 1].DeltaL)
                                                        .PointwiseMultiply(sigmoidDerivative);
                    }



                    

                    //                    CalculateDMatrices(inputs, j, guess);
                    //
                    //                    CalculateWeightDeltas(learningRate, inputs, j);
                    //
                    //                    UpdateWeights();
                }
            }
        }

        public Matrix<double> ForwardPropagation(Matrix<double> input)
        {
            var a = input;
            foreach (var layer in Layers)
            {
                a = layer.Propagate(a);
            }
            return a;
        }
    }
}
