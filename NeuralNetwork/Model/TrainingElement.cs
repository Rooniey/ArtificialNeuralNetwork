using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetwork.Model
{
    public class TrainingElement
    {
        public Matrix<double> Input { get; set; }
        public Matrix<double> DesiredOutput { get; set; }

        public TrainingElement(double[,] input, double[,] output)
        {
            Input = Matrix<double>.Build.DenseOfArray(input);
            DesiredOutput = Matrix<double>.Build.DenseOfArray(output);
        }

        public void AddInput(double[,] input)
        {
            Input = Matrix<double>.Build.DenseOfArray(input);
        }

        public void AddDesiredOutput(double[,] output)
        {
            DesiredOutput = Matrix<double>.Build.DenseOfArray(output);
        }
    }
}