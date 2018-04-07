using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Model
{
    public class Network
    {
        public List<Layer> Layers { get; private set; }
        public int InputSize { get; }
        public List<double> Errors { get; set; }

        public Network(int inputSize)
        {
            Layers = new List<Layer>();
            InputSize = inputSize;
            Errors = new List<double>();
        }

        public void AddLayer(Layer layer)
        {
            var previousSize = Layers.Count == 0 ? InputSize : Layers.Last().NeuronCount;
            layer.LayerUtility.InitLayer(layer, previousSize);
            Layers.Add(layer);
        }

        public void Train(double learningRate, int epochs, double momentum, List<TrainingElement> inputs, double desiredError = 0)
        {
            ResetLayers();
            Errors.Clear();
            for (var i = 0; i < epochs; i++)
            {
                //TODO check error
                List<double> epochsErrors = new List<double>();
                for (var j = 0; j < inputs.Count; j++)
                {
                    var guess = ForwardPropagation(inputs[j].Input);

                    epochsErrors.Add(MeanSquaredError(guess, inputs[j].Input));

                    //an equation for the error in the output layer, δL
                    var outputLayer = Layers.Last();
                    var sigmoidDerivative =
                        outputLayer.ActivationFunction.CalculateDifferential(outputLayer.WeightedSum);                  //∇aC=(aL−y)
                    outputLayer.DeltaL = guess.Subtract(inputs[j].DesiredOutput).PointwiseMultiply(sigmoidDerivative);  //δL

                    //an equation for the error δl in terms of the error in the next layer, δl + 1
                    for (var k = Layers.Count - 2; k >= 0; k--)
                    {
                        Layers[k].Backpropagate(Layers[k + 1]);
                    }

                    Layers.First().UpdateLayer(inputs[j].Input, learningRate, momentum);

                    for (var k = 1; k < Layers.Count; k++)
                    {
                        Layers[k].UpdateLayer(Layers[k - 1].Activation, learningRate, momentum);
                    }
                }
                Errors.Add(epochsErrors.Sum() / inputs.Count);
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

        private double MeanSquaredError(Matrix<double> guesses, Matrix<double> desiredOuputs)
        {
            var diff = guesses.Subtract(desiredOuputs);
            diff.MapInplace(elem => elem * elem);
            double sum = 0;
            for (var i = 0; i < diff.RowCount; i++)
            {
                sum += diff.At(i, 0);
            }

            return sum;
        }

        private void ResetLayers()
        {
            var previousSize = InputSize;
            foreach (var t in Layers)
            {
                t.LayerUtility.InitLayer(t, previousSize);
                previousSize = t.NeuronCount;
            }
        }
        
      
    }
}