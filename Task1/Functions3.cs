using System;
using System.Collections.Generic;
using System.Text;

namespace Task1
{
    public static class Functions3
    {
        public static double[] F1(double[] x)
        {
            double[] result = new double[x.Length];
            result[0] = 3.0 * x[0] * x[0] * x[1] - Math.Pow(x[1], 2) - 1;
            result[1] = Math.Pow(x[0], 4) + x[0] * Math.Pow(x[1], 3) - 1;

            return result;
        }
        public static double[] G1(double[] x)
        {
            double[] result = new double[x.Length];
            result[0] = Math.Abs(x[0] - 1);
            result[1] = Math.Abs(x[1]);

            return result;
        }
        public static double[,] DF1(double[] x)
        {
            int n = x.Length;
            double[,] result = new double[n, n];

            result[0, 0] = 6.0 * x[0] * x[1];
            result[0, 1] = 3.0 * x[0] * x[0] - 2.0 * x[1];
            result[1, 0] = 4.0 * Math.Pow(x[0], 3) + Math.Pow(x[1], 3);
            result[1, 1] = 3.0 * x[1] * x[1] * x[0];

            return result;
        }
        public static double F1i(double[] x, int i)
        {
            if (i == 0)
            {
                return 3.0 * x[0] * x[0] * x[1] - Math.Pow(x[1], 2) - 1;
            }
            else
            {
                return Math.Pow(x[0], 4) + x[0] * Math.Pow(x[1], 3) - 1;
            }
        }
        public static double G1i(double[] x, int i)
        {
            if (i == 0)
            {
                return Math.Abs(x[0] - 1);
            }
            else
            {
                return Math.Abs(x[1]);
            }
        }

        public static double[] F2(double[] x)
        {
            double[] result = new double[x.Length];
            result[0] = Math.Pow(x[2], 2.0) * (1.0 - x[1]) - x[0] * x[1]; // z*z*(1-y-x*y)
            //result[0] = Math.Pow(x[2], 2.0) * (1.0 - x[1]) - x[0] * x[1]*x[4]; // z*z*(1-y-x*y*c)
            result[1] = Math.Pow(x[2], 2.0) * (Math.Pow(x[0], 3.0) - x[0]) - Math.Pow(x[1], 2.0); // z*z*(x^3-x)-y*y
            result[2] = 6.0 * x[0] * Math.Pow(x[1], 3.0) + Math.Pow(x[1], 2.0) * Math.Pow(x[2], 2.0) - x[0] * Math.Pow(x[1], 2.0) * x[2];
            // 6*x*y^3+y*y*z*z-x*y*y*z

            //result[3] = x[1] * x[1] - x[3] * x[3] * x[3];
            //// y*y-h+x^3
            //result[4] = Math.Pow(x[1], 3.0) + (1.0 - x[3]) - x[0] * x[4]; // y^3+ 1-h*c)

            return result;

                //x[0] - x
                //x[1] - y
                //x[2] - z
                //x[3] - h
                //x[4] - c
        }
        public static double[] G2(double[] x)
        {
            double[] result = new double[x.Length];
            result[0] = Math.Abs(x[1] - x[2] * x[2]); //y-z*z
            result[1] = Math.Abs(3.0 * x[1] * x[1] - x[2] * x[2] + 1.0); // 3*y*y-z*z+1
            result[2] = Math.Abs(x[0] + x[2] - x[1]); //x+z-y

            //result[3] = Math.Abs(2 * x[1] - x[3]); //y+y-h
            //result[4] = Math.Abs(3 + x[2]*x[2] - x[4]); //3+z*z-c

            return result;
        }
        public static double[,] DF2(double[] x)
        {
            int n = x.Length;
            double[,] result = new double[n, n];

            result[0, 0] = -x[1];
            result[0, 1] = -Math.Pow(x[2], 2.0) - x[0];
            result[0, 2] = 2.0 * x[2] * (1.0 - x[1]);

            //result[0, 3] = 0;
            //result[0, 4] = x[0]*x[1];

            result[1, 0] = 3 * Math.Pow(x[2], 2) * Math.Pow(x[0], 2) - Math.Pow(x[0], 2);
            result[1, 1] = -2.0 * x[1];
            result[1, 2] = 2.0 * x[2] * (Math.Pow(x[0], 3.0) - x[0]);

            //result[1, 3] = 0;
            //result[1, 4] = 0;

            result[2, 0] = 6.0 * Math.Pow(x[1], 3.0) - Math.Pow(x[1], 2.0) * x[2];
            result[2, 1] = 18.0 * x[0] * Math.Pow(x[1], 2.0) + 2.0 * x[1] * Math.Pow(x[2], 2.0) - 2.0 * x[0] * x[1] * x[2];
            result[2, 2] = Math.Pow(x[1], 2.0) * 2.0 * x[2] - x[0] * Math.Pow(x[1], 2.0);

            //result[2, 3] = 0;
            //result[2, 4] = 0;

            //result[3, 0] = 3 * x[0];
            //result[3, 1] = 2;
            //result[3, 2] = 0;
            //result[3, 3] = -1;
            //result[3, 4] = x[3];

            return result;
        }
        public static double F2i(double[] x, int i)
        {
            if (i == 0)
            {
                return Math.Pow(x[2], 2.0) * (1.0 - x[1]) - x[0] * x[1];
            }
            else if (i == 1)
            {
                return Math.Pow(x[2], 2.0) * (Math.Pow(x[0], 3.0) - x[0]) - Math.Pow(x[1], 2.0);
            }

            //else if (i == 3)
            //{
            //    return x[1] * x[1] - x[3] * x[3] * x[3];
            //}
            //else if (i == 4)
            //{
            //    return Math.Pow(x[1], 3.0) + (1.0 - x[3]) - x[0] * x[4];
            //}


            else
            {
                return 6.0 * x[0] * Math.Pow(x[1], 3.0) + Math.Pow(x[1], 2.0) * Math.Pow(x[2], 2.0) - x[0] * Math.Pow(x[1], 2.0) * x[2];
            }
            
        }
        public static double G2i(double[] x, int i)
        {
            if (i == 0)
            {
                return Math.Abs(x[1] - x[2] * x[2]);
            }
            else if (i == 1)
            {
                return Math.Abs(3.0 * x[1] * x[1] - x[2] * x[2] + 1.0);
            }


            //else if (i == 3)
            //{
            //    return Math.Abs(2 * x[1] - x[3]);
            //}
            //else if (i == 4)
            //{
            //    return Math.Abs(3 + x[2] * x[2] - x[4]);
            //}


            else
            {
                return Math.Abs(x[0] + x[2] - x[1]);
            }
        }
    }
}
