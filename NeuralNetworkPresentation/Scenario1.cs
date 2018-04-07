
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.Model;
using NeuralNetworkPresentation.Statistics;

namespace NeuralNetworkPresentation
{
    public class Scenario1
    {
        public Network NeuralNetwork { get; set; }
        public List<TrainingElement> TrainingSet { get; set; }

        private IStatiscticable _statistics;

        public Scenario1(Network net, List<TrainingElement> train, TrainingData td, IStatiscticable stat)
        {
            NeuralNetwork = net;
            TrainingSet = train;
            _statistics = stat;
            
        }

        public void Run()
        {
           
        }
    }

    
}
