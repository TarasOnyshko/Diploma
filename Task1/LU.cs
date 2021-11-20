using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class LU
    {
        public static (double[,] L, double[,] U) SequentialLU(double[,] matrix_A)
        {
            int n = matrix_A.GetLength(0);

            double[,] A = Copy(matrix_A);
            double[,] L = new double[n, n];

            for (int k = 0; k < n; k++)
            {
                L[k, k] = 1;
                for (int i = k + 1; i < n; i++)
                {
                    L[i, k] = A[i, k] / A[k, k];
                }
                for (int j = k + 1; j < n; j++)
                {
                    for (int i = k + 1; i < n; i++)
                    {
                        A[i, j] = A[i, j] - L[i, k] * A[k, j];
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    A[j, i] = 0;
                }
            }

            return (L, A);
        }

        public static (double[,] L, double[,] U) ParallelLU(double[,] matrix_A, int p = 2)
        {
            int n = matrix_A.GetLength(0);

            double[,] A = Copy(matrix_A);
            double[,] L = new double[n, n];

            for (int i = 0; i < n; i++) { L[i, i] = 1; }

            List<List<int>> numberOfRows = new List<List<int>>();
            for (int r = 0; r < p; r++)
            {
                numberOfRows.Add(new List<int>());
            }

            for (int i = 0; i < n; i++)
            {
                numberOfRows[i % p].Add(i);
            }

            for (int k = 0; k < n; k++)
            {
                Task.WaitAll(
                    numberOfRows.Select((indexes) =>
                    {
                        var workIndexes = indexes.Where((x) => x > k);
                        return Task.Factory.StartNew(() =>
                        {
                            foreach (var i in workIndexes)
                            {
                                L[i, k] = A[i, k] / A[k, k];
                            }
                        });
                    }).ToArray());

                Parallel.For(k + 1, n, (j) =>
                {
                    for (int i = k + 1; i < n; i++)
                    {
                        A[i, j] = A[i, j] - L[i, k] * A[k, j];
                    }
                });
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    A[j, i] = 0;
                }
            }

            return (L, A);
        }

        public static double[] Solve(double[,] L, double[,] U, double[] b)
        {
            var n = L.GetLength(0); var y = new double[n]; var x = new double[n];
            double sum;

            y[0] = b[0] / L[0, 0];

            for (int i = 1; i < n; i++)
            {
                sum = 0;
                for (int j = 0; j < i; j++)
                {
                    sum += L[i, j] * y[j];
                }
                y[i] = (b[i] - sum) / L[i, i];
            }

            x[n - 1] = y[n - 1] / U[n - 1, n - 1];

            for (int i = n - 2; i >= 0; i--)
            {
                sum = 0;
                for (int j = n - 1; j >= i + 1; j--)
                {
                    sum += x[j] * U[i, j];
                }
                x[i] = (y[i] - sum) / U[i, i];
            }

            return x;
        }

        public static double[] ParallelSolve(double[,] L, double[,] U, double[] b)
        {
            var n = L.GetLength(0); var y = new double[n]; var x = new double[n];

            for (int i = 0; i < n; i++)
            {
                Parallel.For(0, i, (j, state) =>
                {
                    b[i] -= L[i, j] * y[j];

                });

                y[i] = b[i] / L[i, i];
            }


            for (int i = n - 1; i >= 0; i--)
            {
                Parallel.For(i, n, (j, state) =>
                {
                    y[i] -= U[i, j] * x[j];
                });

                x[i] = y[i] / U[i, i];
            }

            return x;
        }

        public static double[,] Copy(double[,] arr)
        {
            int n = arr.GetLength(0);
            int m = arr.GetLength(1);

            double[,] arr_copy = new double[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    arr_copy[i, j] = arr[i, j];
                }
            }

            return arr_copy;
        }
    }
}
