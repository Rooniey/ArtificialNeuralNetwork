using NeuralNetwork.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace NeuralNetwork.DataService
{
    public class DataGetter : ILiterate
    {
        public IEnumerable<double[]> GetData(string filePath, char delimeter)
        {
            List<double[]> data = new List<double[]>();

            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var row = line.Split(delimeter);
                        var rowD = new double[row.Length];
                        for (var i = 0; i < row.Length; i++)
                        {
                            rowD[i] = double.Parse(row[i], CultureInfo.InvariantCulture);
                        }
                        data.Add(rowD);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return data;
        }

        public List<TrainingElement> GetSetOfData(string path, int numberOfInputs)
        {
            IEnumerable<double[]> data = GetData(path, ' ');
            List<TrainingElement> setData = new List<TrainingElement>();

            foreach (var example in data)
            {
                var input = new double[numberOfInputs, 1];
                var output = new double[example.Length - numberOfInputs, 1];
                var j = 0;
                for (var i = 0; i < example.Length; i++)
                {
                    if (i < numberOfInputs)
                    {
                        input[i,0] = example[i];
                    }
                    else
                    {
                        output[j, 0] = example[i];
                        j++;
                    }
                }
                setData.Add(new TrainingElement(input, output));
            }

            return setData;
        }

        public List<TrainingElement> GetSetOfDataWithOneOutput(string path, int numberOfInputs)
        {
            IEnumerable<double[]> data = GetData(path, ' ');
            List<TrainingElement> setData = new List<TrainingElement>();

            foreach (var example in data)
            {
                var input = new double[numberOfInputs, 1];
                var output = new double[1, 1];
                for (var i = 0; i < example.Length; i++)
                {
                    if (i < numberOfInputs)
                    {
                        input[i, 0] = example[i];
                    }
                    else
                    {
                        output[0, 0] = example[example.Length - 1];
                        break;
                    }
                }
                setData.Add(new TrainingElement(input, output));
            }

            return setData;
        }

        public List<TrainingElement> GetSetOfDataWithChosenInputs(string path,bool[] chosenInputs)
        {
            IEnumerable<double[]> data = GetData(path, ' ');
            List<TrainingElement> setData = new List<TrainingElement>();

            int numberOfInputs = 0;

            foreach(var bit in chosenInputs)
            {
                if (bit) numberOfInputs++;
            }


            foreach (var example in data)
            {
                var input = new double[numberOfInputs, 1];
                var output = new double[1, 1];
                int j = 0;
                for (int i = 0; i < chosenInputs.Length; i++)
                {
                    if (chosenInputs[i])
                    {
                        input[j, 0] = example[i];
                        j++;
                    }
                }
                output[0, 0] = example[example.Length - 1];
                setData.Add(new TrainingElement(input, output));
            }

            return setData;
        }
    }
}