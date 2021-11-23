using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public static class Functions
    {
        // ЦЕ по суті H(x) = Fx+Gx
        public static double[] F(double[] x)
        {
            int n = x.Length;

            double[] f = new double[n];

            f[0] = x[0] * (3 - 2 * x[0]) - 2 * x[1] + 1 + Math.Abs(x[0] - x[1]);

            for (int i = 1; i < n - 1; i++)
            {
                f[i] = x[i] * (3 - 2 * x[0]) - x[i - 1] - 2 * x[i + 1] + 1 + Math.Abs(x[i] - x[i + 1] - x[i - 1]);
            }

            f[n - 1] = x[n - 1] * (3 - 2 * x[n - 1]) - x[n - 2] + 1 + Math.Abs(x[n - 1] - x[n - 2]);

            return f;
        }

        // poХidna iz F ale tilky po F, bez G
        public static double[,] DF(double[] x)
        {
            int n = x.Length;

            double[,] df = new double[n, n];

            df[0, 0] = 3.0 - 4.0 * x[0];
            df[0, 1] = -2.0;
            for (int i = 1; i < n - 1; i++)
            {
                df[i, i - 1] = (-1.0);
                df[i, i] = 3.0 - 4.0 * x[i];
                df[i, i + 1] = -2.0;
            }

            df[n - 1, n - 1] = 3.0 - 4 * x[n - 1];
            df[n - 1, n - 2] = -1.0;

            return df;
        }

        // яке I, таке G рівняня і вертаємо
        public static double Gi(double[] x, int i)
        {
            int n = x.Length;

            if (i == 0)
            {
                return Math.Abs(x[0] - x[1]);
            }
            else if (i >= 1 && i <= n - 2)
            {
                return Math.Abs(x[i] - x[i + 1] - x[i - 1]);
            }
            else
            {
                return Math.Abs(x[n - 1] - x[n - 2]);
            }

        }

        public static double[] F_Parallel(double[] x)
        {
            int n = x.Length;

            double[] f = new double[n];

            f[0] = x[0] * (3 - 2 * x[0]) - 2 * x[1] + 1 + Math.Abs(x[0] - x[1]);

            Parallel.For(1, n - 1, i =>
            {
                f[i] = x[i] * (3 - 2 * x[0]) - x[i - 1] - 2 * x[i + 1] + 1 + Math.Abs(x[i] - x[i + 1] - x[i - 1]);
            });


            f[n - 1] = x[n - 1] * (3 - 2 * x[n - 1]) - x[n - 2] + 1 + Math.Abs(x[n - 1] - x[n - 2]);

            return f;
        }

        public static double[,] DF_Parallel(double[] x)
        {
            int n = x.Length;

            double[,] df = new double[n, n];

            df[0, 0] = 3.0 - 4.0 * x[0];
            df[0, 1] = -2.0;

            Parallel.For(1, n - 1, i =>
            {
                df[i, i - 1] = (-1.0);
                df[i, i] = 3.0 - 4.0 * x[i];
                df[i, i + 1] = -2.0;
            });

            df[n - 1, n - 1] = 3.0 - 4 * x[n - 1];
            df[n - 1, n - 2] = -1.0;

            return df;
        }


        public static double[] G(double[] x)
        {
            int n = x.Length;

            double[] f = new double[n];

            f[0] = Math.Abs(x[0] - x[1]);

            for (int i = 1; i < n - 1; i++)
            {
                f[i] = Math.Abs(x[i] - x[i + 1] - x[i - 1]);
            }

            f[n - 1] =  Math.Abs(x[n - 1] - x[n - 2]);

            return f;
        }

        public static double[] F1(double[] x)
        {
            int n = x.Length;

            double[] f = new double[n];

            f[0] = Math.Pow((3.0 - 2.0 * x[0]) * x[0] - 2.0 * x[1] + 1.0, 2.0);
            for (int i = 1; i < n - 1; i++)
            {
                f[i] = Math.Pow((3.0 - 2.0 * x[i]) * x[i] - x[i - 1] - 2.0 * x[i + 1] + 1.0, 2.0);
            }
            f[n - 1] = Math.Pow((3.0 - 2.0 * x[n - 1]) * x[n - 1] - x[n - 2] + 1.0, 2.0);

            return f;
        }

        public static double[,] DF1(double[] x)
        {
            int n = x.Length;

            double[,] df = new double[n, n];

            df[0, 0] = 2.0 * ((3.0 - 2.0 * x[0]) * x[0] - 2.0 * x[1] + 1.0) * (3.0 - 4.0 * x[0]);
            df[0, 1] = 2.0 * ((3.0 - 2.0 * x[0]) * x[0] - 2.0 * x[1] + 1.0) * (-2.0);
            for (int i = 1; i < n - 1; i++)
            {
                df[i, i - 1] = 2.0 * ((3.0 - 2.0 * x[i]) * x[i] - x[i - 1] - 2.0 * x[i + 1] + 1.0) * (-1.0);
                df[i, i] = 2.0 * ((3.0 - 2.0 * x[i]) * x[i] - x[i - 1] - 2.0 * x[i + 1] + 1.0) * (3.0 - 4.0 * x[i]);
                df[i, i + 1] = 2.0 * ((3.0 - 2.0 * x[i]) * x[i] - x[i - 1] - 2.0 * x[i + 1] + 1.0) * (-2.0);
            }
            df[n - 1, n - 1] = 2.0 * ((3.0 - 2.0 * x[n - 1]) * x[n - 1] - x[n - 2] + 1.0) * (3.0 - 4.0 * x[n - 1]);
            df[n - 1, n - 2] = 2.0 * ((3.0 - 2.0 * x[n - 1]) * x[n - 1] - x[n - 2] + 1.0) * (-1.0);

            return df;
        }

        public static double F1i(double[] x, int i)
        {
            int n = x.Length;

            if (i == 0)
            {
                return Math.Pow((3.0 - 2.0 * x[0]) * x[0] - 2.0 * x[1] + 1.0, 2.0);
            }
            else if (i >= 1 && i <= n - 2)
            {
                return Math.Pow((3.0 - 2.0 * x[i]) * x[i] - x[i - 1] - 2.0 * x[i + 1] + 1.0, 2.0);
            }
            else
            {
                return Math.Pow((3.0 - 2.0 * x[n - 1]) * x[n - 1] - x[n - 2] + 1.0, 2.0);
            }
        }

        public static double[] F2(double[] x)
        {
            double[] result = new double[x.Length];
            result[0] = 10.0 * (x[1] - Math.Pow(x[0], 2));
            result[1] = 1.0 - x[0];
            return result;
        }

        public static double[,] DF2(double[] x)
        {
            int n = x.Length;
            double[,] result = new double[n, n];

            result[0, 0] = 10 * (-2 * x[0]);
            result[0, 1] = 10.0;
            result[1, 0] = -1.0;
            result[1, 1] = 0.0;

            return result;
        }

        public static double F2i(double[] x, int i)
        {
            int n = x.Length;
            if (i == 0)
            {
                return 10.0 * (x[1] - Math.Pow(x[0], 2));
            }
            else
            {
                return 1.0 - x[0];
            }
        }
    }
}
