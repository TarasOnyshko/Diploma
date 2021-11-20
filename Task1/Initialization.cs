using System;
using System.Collections.Generic;
using System.Text;

namespace Task1
{
    public static class Initialization
    {
        public static double[] Initialize(int n, double value1, double value2)
        {
            var result = new double[n];

            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 1)
                {
                    result[i] = value1;
                }
                else
                {
                    result[i] = value2;
                }

            }
            return result;
        }

        public static double[] Initialize(int n, double value)
        {
            var result = new double[n];

            for (int i = 0; i < n; i++)
            {
                result[i] = value;
            }
            return result;
        }
    }
}
