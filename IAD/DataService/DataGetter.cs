using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace IAD.DataService
{
    public class DataGetter : ILiterate
    {
        public IEnumerable<double[]> GetData(string filePath, char delimeter)
        {
            List<double[]> data = new List<double[]>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    string[] row;
                    double[] rowD;
                    while ((line = sr.ReadLine()) != null)
                    {
                        row = line.Split(delimeter);
                        rowD = new double[row.Length];
                        for(int i = 0; i < row.Length; i++)
                        {
                            rowD[i] = Double.Parse(row[i], CultureInfo.InvariantCulture);
                        }
                        data.Add(rowD);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return data;
        }

    }
}
