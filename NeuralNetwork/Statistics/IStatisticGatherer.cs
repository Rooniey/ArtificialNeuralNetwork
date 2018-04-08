using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.Model;

namespace NeuralNetwork.Statistics
{
    public interface IStatisticGatherer
    {
        void GatherStatistics(Network network);

    }
}
