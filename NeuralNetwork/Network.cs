using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Train(double learningRate, int epochs, List<Matrix<double> > inputs)//maybe otheres
        {
            for(int i = 0; i < epochs; i++)
            {
                
                //TODO
                //idea for trainingSet STRUCT!!!!!!importando

            }
        }

        public Matrix<double> GetOutput(Matrix<double> input)
        {

            Matrix<double> Z = input;
            foreach (var item in _layers)
            {
                Z = item.LayerUtility.Propagate(item, Z);
     
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
