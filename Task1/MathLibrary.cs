using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public static class MathLibrary
    {
        //public static double[] Substract(this double[] x_k, double[] x_kk)
        //{
        //    var result = new double[x_k.Length];
        //    for (int i = 0; i < x_k.Length; i++)
        //    {
        //        result[i] = x_k[i] - x_kk[i];
        //    }

        //    //Parallel.For(0, x_k.Length, count => {
        //    //        result[count] = x_k[count] - x_kk[count];  
        //    //});
        //    return result;
        //}
        public static double[] Substract(this double[] x_k, double[] x_kk)
        {
            var result = new double[x_k.Length];


            Parallel.For(0, x_k.Length, count =>
            {
                result[count] = x_k[count] - x_kk[count];
            });
            return result;
        }

        public static double[,] Multiply(this double[,] matrix1, double[,] matrix2)
        {
            var matrix1Rows = matrix1.GetLength(0);
            var matrix1Cols = matrix1.GetLength(1);
            var matrix2Cols = matrix2.GetLength(1);

            double[,] multiplication = new double[matrix1Rows, matrix2Cols];

            for (int i = 0; i < matrix1Rows; i++)
            {
                for (int j = 0; j < matrix2Cols; j++)
                {
                    for (int k = 0; k < matrix1Cols; k++)
                    {
                        multiplication[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return multiplication;
        }

        public static double[] Multiply(this double[,] matrix, double[] vector)
        {
            var vectorAsMatrix = new double[vector.Length, 1];
            for (int i = 0; i < vector.Length; i++)
            {
                vectorAsMatrix[i, 0] = vector[i];
            }

            var result = Multiply(matrix, vectorAsMatrix);
            var multiplication = new double[vectorAsMatrix.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                multiplication[i] = result[i, 0];
            }
            return multiplication;
        }

        public static double[,] Multiply(this double[,] matrix, double scalar)
        {
            var matrixRows = matrix.GetLength(0);
            var matrixCols = matrix.GetLength(1);
            var multiplication = new double[matrixRows, matrixCols];

            //for (int i = 0; i < matrixRows; i++)
            //{
            //    for (int j = 0; j < matrixCols; j++)
            //    {
            //        multiplication[i, j] = matrix[i, j] * scalar;
            //    }
            //}
            Parallel.For(0, matrixRows, i =>
            {
                {
                    for (int j = 0; j < matrixCols; j++)
                    {
                        multiplication[i, j] = matrix[i, j] * scalar;
                    }
                }
            });

            return multiplication;
        }

        public static double[,] Add(this double[,] matrix1, double[,] matrix2)
        {
            var matrixRows = matrix1.GetLength(0);
            var matrixCols = matrix1.GetLength(1);

            double[,] addition = new double[matrixRows, matrixCols];

            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixCols; j++)
                {

                    addition[i, j] = matrix1[i, j] + matrix2[i, j];

                }
            }
            return addition;
        }

        public static double[] Add(this double[] vector1, double[] vector2)
        {
            double[] addition = new double[vector1.Length];

            for (int i = 0; i < vector1.Length; i++)
            {
                addition[i] = vector1[i] + vector2[i];
            }
            //Parallel.For(1, vector1.Length, count => {
            //    addition[count] = vector1[count] + vector2[count];
            //});
            return addition;
        }

        public static double[,] Substract(this double[,] matrix1, double[,] matrix2)
        {
            var matrixRows = matrix1.GetLength(0);
            var matrixCols = matrix1.GetLength(1);

            double[,] substraction = new double[matrixRows, matrixCols];

            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixCols; j++)
                {

                    substraction[i, j] = matrix1[i, j] - matrix2[i, j];

                }
                //Parallel.For(0, matrixCols, count => {
                //    substraction[i, count] = matrix1[i, count] - matrix2[i, count];
                //});
            }
            return substraction;
        }

        public static double[,] Inverse(this double[,] matrix)
        {
            int n = matrix.GetLength(0);

            double[,] inversedMatrix = new double[n, n];
            for (int i = 0; i < n; i++)
                inversedMatrix[i, i] = 1;

            double[,] doubledMatrix = new double[n, 2 * n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    doubledMatrix[i, j] = matrix[i, j];
                    doubledMatrix[i, j + n] = inversedMatrix[i, j];
                }
            //Parallel.For(0, n, count => {
            //        doubledMatrix[i, count] = matrix[i, count];
            //        doubledMatrix[i, count + n] = inversedMatrix[i, count];
            //    });
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < 2 * n; i++)
                    doubledMatrix[k, i] = doubledMatrix[k, i] / matrix[k, k];
                for (int i = k + 1; i < n; i++)
                {
                    double K = doubledMatrix[i, k] / doubledMatrix[k, k];
                    for (int j = 0; j < 2 * n; j++)
                        doubledMatrix[i, j] = doubledMatrix[i, j] - doubledMatrix[k, j] * K;
                }
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        matrix[i, j] = doubledMatrix[i, j];
            }

            for (int k = n - 1; k > -1; k--)
            {
                for (int i = 2 * n - 1; i > -1; i--)
                    doubledMatrix[k, i] = doubledMatrix[k, i] / matrix[k, k];
                for (int i = k - 1; i > -1; i--)
                {
                    double K = doubledMatrix[i, k] / doubledMatrix[k, k];
                    for (int j = 2 * n - 1; j > -1; j--)
                        doubledMatrix[i, j] = doubledMatrix[i, j] - doubledMatrix[k, j] * K;
                }
            }
            //System.Threading.Thread.Sleep(0);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    inversedMatrix[i, j] = doubledMatrix[i, j + n];

            return inversedMatrix;
        }

        //public static double[,] Inverse(this double[,] matrix)
        //{
        //    int n = matrix.GetLength(0);

        //    double[,] inversedMatrix = new double[n, n];
        //    for (int i = 0; i < n; i++)
        //        inversedMatrix[i, i] = 1;

        //    double[,] doubledMatrix = new double[n, 2 * n];
        //    for (int i = 0; i < n; i++)
        //        for (int j = 0; j < n; j++)
        //        {
        //            doubledMatrix[i, j] = matrix[i, j];
        //            doubledMatrix[i, j + n] = inversedMatrix[i, j];
        //        }

        //    Parallel.For(0, n, k =>
        //    {
        //        for (int i = 0; i < 2 * n; i++)
        //            doubledMatrix[k, i] = doubledMatrix[k, i] / matrix[k, k];
        //        for (int i = k + 1; i < n; i++)
        //        {
        //            double K = doubledMatrix[i, k] / doubledMatrix[k, k];
        //            for (int j = 0; j < 2 * n; j++)
        //                doubledMatrix[i, j] = doubledMatrix[i, j] - doubledMatrix[k, j] * K;
        //        }
        //        for (int i = 0; i < n; i++)
        //            for (int j = 0; j < n; j++)
        //                matrix[i, j] = doubledMatrix[i, j];
        //    }
        //    );

        //    for (int k = n - 1; k > -1; k--)
        //    {
        //        for (int i = 2 * n - 1; i > -1; i--)
        //            doubledMatrix[k, i] = doubledMatrix[k, i] / matrix[k, k];
        //        for (int i = k - 1; i > -1; i--)
        //        {
        //            double K = doubledMatrix[i, k] / doubledMatrix[k, k];
        //            for (int j = 2 * n - 1; j > -1; j--)
        //                doubledMatrix[i, j] = doubledMatrix[i, j] - doubledMatrix[k, j] * K;
        //        }
        //    }
        //    //System.Threading.Thread.Sleep(0);
        //    for (int i = 0; i < n; i++)
        //        for (int j = 0; j < n; j++)
        //            inversedMatrix[i, j] = doubledMatrix[i, j + n];

        //    return inversedMatrix;
        //}

        public static double Norm(this double[] x)
        {
            var max = Math.Abs(x[0]);
            for (int i = 1; i < x.Length; i++)
            {
                if (Math.Abs(x[i]) > max)
                {
                    max = Math.Abs(x[i]);
                }
            }

            return max;
        }

        public static double[,] GetI(int n)
        {
            var matrix = new double[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1;
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }

            return matrix;
        }
    }
}
