using System;
using System.Collections.Generic;
using System.Text;

namespace Task1
{
    public static class OperationsIO
    {
        public static void DisplayResults(double[] result, int iterations, double norm)
        {
            Console.WriteLine($"Iterations count {iterations}");
            Console.WriteLine($"Last Norm: {norm}");
            result.DisplayVector();
            Console.WriteLine();
        }

        public static void DisplayResultsWithMoreDigits(double[] result, int iterations, double norm)
        {
            Console.WriteLine($"Iterations count {iterations}");
            Console.WriteLine($"Last Norm: {norm}");
            result.DisplayVectorWithMoreDigits();
            Console.WriteLine();
        }

        public static void DisplayVector(this double[] vector)
        {
            Console.Write("Xk: ");
            foreach (var a in vector)
            {
                Console.Write(String.Format("{0:0.0000}", a) + " ");
            }
            Console.WriteLine();
        }

        public static void DisplayVectorWithMoreDigits(this double[] vector)
        {
            Console.Write("Xk: ");
            foreach (var a in vector)
            {
                Console.Write(String.Format("{0:0.00000000000000}", a) + " ");
            }
            Console.WriteLine();
        }

        public static void DisplayMatrix(this double[,] matrix)
        {
            Console.Write("Matrix: ");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(String.Format("{0:0.0000}", matrix[i,j]) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
