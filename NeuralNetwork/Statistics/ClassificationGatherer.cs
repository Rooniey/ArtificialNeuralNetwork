using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using NeuralNetwork.Model;

namespace NeuralNetwork.Statistics
{
    public class ClassificationGatherer : IStatisticGatherer
    {
        public void GatherStatistics(Network network)
        {
            double averageError = 0;
            double positiveV = 0;
            double positiveT = 0;

            //GET COUNT POSITIVES FOR TESTING SET
            foreach (var testElement in TestElements)
            {
                
                var output = network.ForwardPropagation(testElement.Input);

                int guessedClass = ConvertMatrixToClass(output);
                int rightClass = ConvertMatrixToClass(testElement.DesiredOutput);

                if (guessedClass == rightClass) positiveV++;

                var error = Network.MeanSquaredError(output, testElement.DesiredOutput);
                averageError += error;
                
            }

            AccuracyListV.Add(positiveV / TestElements.Count);
            averageError /= TestElements.Count;
            TestErrors.Add(averageError);


            //GET COUNT POSITIVES FOR TRAINING SET
            foreach (var trainElement in TrainingElements)
            {

                var output = network.ForwardPropagation(trainElement.Input);

                int guessedClass = ConvertMatrixToClass(output);
                int rightClass = ConvertMatrixToClass(trainElement.DesiredOutput);

                if (guessedClass == rightClass) positiveT++;
            }
            AccuracyListT.Add(positiveT / TrainingElements.Count);



        }

        public ClassificationGatherer(List<TrainingElement> train, List<TrainingElement> inputs)
        {
            TestElements = train;
            TrainingElements = inputs;
        }

        /// <summary>
        /// Convert classification network output to class index
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int ConvertMatrixToClass(Matrix<double> output)
        {
            var array = (output.ToArray());
            int max = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i,0] > array[max,0]) max = i;
            }

            max++; // Dopasowanie do zapisu klas w pliku

            return max;
        }

        public List<TrainingElement> TestElements { get; set; } = new List<TrainingElement>();
        public List<TrainingElement> TrainingElements { get; set; } = new List<TrainingElement>();
        public List<double> AccuracyListV { get; set; } = new List<double>();
        public List<double> AccuracyListT { get; set; } = new List<double>();
        public List<double> TestErrors { get; set; } = new List<double>();
    }
}
