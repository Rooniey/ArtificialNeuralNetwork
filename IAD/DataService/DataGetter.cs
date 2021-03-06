﻿using System;
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
    }
}