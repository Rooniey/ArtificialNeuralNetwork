using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetwork
{
    public static class MatrixMathExtension
    {
        public static Matrix<double> OurPointwiseMultiply(this Matrix<double> thisMatrix, Matrix<double> otherMatrix)
        {
            double[,] toReturn = otherMatrix.ToArray();
            var thisArray = thisMatrix.ToArray();

            var l0 = toReturn.GetLength(0);
            var l1 = toReturn.GetLength(1);

            for (int j = 0; j < toReturn.GetLength(1); j++)
            {
                for (int i = 0; i < toReturn.GetLength(0); i++)
                {
                    toReturn[i, j] *= thisArray[0, j];
                }
            }

            return Matrix<double>.Build.DenseOfArray(toReturn);
        }
    }
}