using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetwork
{
    public struct TrainingElement
    {
        public Matrix<double> Input;
        public Matrix<double> DesiredOutput;

        public TrainingElement(double[,] input, double[,] output)
        {
            Input = Matrix<double>.Build.DenseOfArray(input); //, Matrix<double>.Build.DenseOfArray(new double[,] { { 0 } })));
            DesiredOutput = Matrix<double>.Build.DenseOfArray(output);
        }

        //maybe useful
        public void AddInput(double[,] input)
        {
            Input = Matrix<double>.Build.DenseOfArray(input); //, Matrix<double>.Build.DenseOfArray(new double[,] { { 0 } })));
        }

        public void AddDesiredOutput(double[,] output)
        {
            DesiredOutput = Matrix<double>.Build.DenseOfArray(output);
        }
    }
}