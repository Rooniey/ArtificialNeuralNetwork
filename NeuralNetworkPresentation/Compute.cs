using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPresentation
{
    internal static class Compute
    {
        public static double GetAccuracy(int[,] confusionArray)
        {
            double sum = 0;
            double correct = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sum += confusionArray[i, j];
                    if (i == j) correct += confusionArray[i, j];
                }
            }

            return correct / sum;
        }

        public static double GetPrecision(int[,] confusionArray)
        {
            List<double> precisions = new List<double>();

            for (int i = 0; i < 3; i++)
            {
                double sum = 0;
                double correct = 0;
                for (int j = 0; j < 3; j++)
                {
                   sum += confusionArray[j, i];
                   if (i == j) correct += confusionArray[j, i];
                   
                }
                
                precisions.Add(correct/sum);
            }

            return precisions.Average();
        }
    }
}
