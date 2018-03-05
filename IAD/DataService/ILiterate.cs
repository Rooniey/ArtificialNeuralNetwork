using System.Collections.Generic;

namespace IAD.DataService
{
    public interface ILiterate
    {
        IEnumerable<double[]> GetData(string filePath, char delimeter);
    }
}
