using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.Model;

namespace NeuralNetwork.Statistics
{
    public class ApproximationGatherer : IStatisticGatherer
    {
        public void GatherStatistics(Network network)
        {
            double averageError = 0;
            foreach (var testElement in TestElements)
            {
                var output = network.ForwardPropagation(testElement.Input);

                var error = Network.MeanSquaredError(output, testElement.DesiredOutput);
                averageError += error;
                
            }

            averageError /= TestElements.Count;
            TestErrors.Add(averageError);
        }

        public ApproximationGatherer(List<TrainingElement> trainingElements)
        {
            TestElements = trainingElements;
        }

        public List<TrainingElement> TestElements { get; set; } = new List<TrainingElement>();

        public List<double> TestErrors { get; set; } = new List<double>();
    }
}
