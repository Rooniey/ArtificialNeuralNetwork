using System.Collections.Generic;

namespace NeuralNetwork.DataService
{
    public interface ILiterate
    {
        IEnumerable<double[]> GetData(string filePath, char delimeter);
    }
}