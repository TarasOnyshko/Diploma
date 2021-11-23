using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Task1
{
    public static class Solver
    {
        public delegate double[] Function(double[] x);
        public delegate double[,] DerivativeFunction(double[] x);
        public delegate double Function_i(double[] x, int i);

        public static (double[], int, double) Newton(double[] x0, Function f, DerivativeFunction df, double eps)
        {
            double[] xk = x0;
            double[] xkPrevious = new double[xk.Length];
            int i = 0;
            do
            {
                xkPrevious = (double[])xk.Clone();
                var d = df(xk);
                var vec = f(xk);
                xk = xk.Substract(df(xk).Inverse().Multiply(f(xk)));
                // xk - Df(xk)^(-1) * F(xk)
                //Console.WriteLine("Iteration: ", i);
                //Console.WriteLine("Xk: ", xk);
                //Console.WriteLine("----------------------------");
                   
                i++;
            }
            while (xk.Substract(xkPrevious).Norm() > eps);

            return (xk, i, f(xk).Norm());
        }

        public static (double[], int, double) NewtonSequential(double[] x0, Function f, DerivativeFunction df, double eps)
        {
            int n = x0.Length;
            double[] xk = x0;
            double[] xkPrevious = new double[n];
            double[,] Ak = df(xk).Inverse();
            double[,] Ak_1 = (double[,])Ak.Clone();
            double[,] I = MathLibrary.GetI(n);

            int i = 0;
            do
            {
                xkPrevious = (double[])xk.Clone();
                Ak = (double[,])Ak_1.Clone();

                xk = xk.Substract(Ak.Multiply(f(xk)));
                Ak_1 = Ak.Multiply(I.Multiply(2).Substract(df(xk).Multiply(Ak)));

                i++;
            }
            while (xk.Substract(xkPrevious).Norm() > eps);

            return (xk, i, f(xk).Norm());
        }

        public static (double[], int, double) NewtonParallel(double[] x0, Function f, DerivativeFunction df, double eps)
        {
            int n = x0.Length;
            double[] xk = x0;
            double[] xkPrevious = new double[n];
            double[,] Ak = df(xk).Inverse();
            double[,] Ak_1 = (double[,])Ak.Clone();
            double[,] I = MathLibrary.GetI(n);

            int i = 0;
            do
            {
                xkPrevious = (double[])xk.Clone();
                Ak = (double[,])Ak_1.Clone();

                xk = xk.Substract(Ak.Multiply(f(xk)));
                Ak_1 = Ak.Multiply(I.Multiply(2).Substract(df(xkPrevious).Multiply(Ak)));

                i++;
            }
            while (xk.Substract(xkPrevious).Norm() > eps);

            return (xk, i, f(xk).Norm());
        }

        public static (double[], int, double) Potra(double[] x0, double[] x1, double[] x2, Function f, Function_i fi, double eps)
        {
            double[] xk2 = x2;
            double[] xk1 = x1;
            double[] xk = x0;

            int i = 0;
            while (xk.Substract(xk1).Norm() > eps)
            {
                var xkCopy = (double[])xk.Clone();
                xk = xk.Substract(Fxy(fi, xkCopy, xk1).Add(Fxy(fi, xk2, xkCopy)).Substract(Fxy(fi, xk2, xk1)).Inverse().Multiply(f(xkCopy)));

                xk2 = (double[])xk1.Clone();
                xk1 = (double[])xkCopy.Clone();
                i++;
            }

            return (xk, i, f(xk).Norm());
        }

        public static (double[], int, double) PotraSequential(double[] x0, double[] x1, double[] x2, Function f, Function_i fi, double eps)
        {
            int n = x0.Length;

            double[] xk2 = x2;
            double[] xk1 = x1;
            double[] xk = x0;

            double[,] Ak = Fxy(fi, xk, xk1).Add(Fxy(fi, xk2, xk)).Substract(Fxy(fi, xk2, xk1)).Inverse();
            double[,] Ak_1 = (double[,])Ak.Clone();
            double[,] I = MathLibrary.GetI(n);

            int i = 0;
            while (xk.Substract(xk1).Norm() > eps)
            {
                Ak = (double[,])Ak_1.Clone();
                var xkCopy = (double[])xk.Clone();
                xk = xk.Substract(Ak.Multiply(f(xkCopy)));

                Ak_1 = Ak.Multiply(I.Multiply(2).Substract(Fxy(fi, xk, xk1).Add(Fxy(fi, xk2, xk)).Substract(Fxy(fi, xk2, xk1)).Multiply(Ak)));

                xk2 = (double[])xk1.Clone();
                xk1 = (double[])xkCopy.Clone();
                i++;
            }

            return (xk, i, f(xk).Norm());
        }

        public static (double[], int, double) PotraParallel(double[] x0, double[] x1, double[] x2, Function f, Function_i fi, double eps)
        {
            int n = x0.Length;

            double[] xk2 = x2;
            double[] xk1 = x1;
            double[] xk = x0;

            double[,] Ak = Fxy(fi, xk, xk1).Add(Fxy(fi, xk2, xk)).Substract(Fxy(fi, xk2, xk1)).Inverse();
            double[,] Ak_1 = (double[,])Ak.Clone();
            double[,] I = MathLibrary.GetI(n);

            int i = 0;
            while (xk.Substract(xk1).Norm() > eps)
            {
                Ak = (double[,])Ak_1.Clone();
                var xkCopy = (double[])xk.Clone();
                xk = xk.Substract(Ak.Multiply(f(xkCopy)));

                Ak_1 = Ak.Multiply(I.Multiply(2).Substract(Fxy(fi, xkCopy, xk1).Add(Fxy(fi, xk2, xkCopy)).Substract(Fxy(fi, xk2, xk1)).Multiply(Ak)));

                xk2 = (double[])xk1.Clone();
                xk1 = (double[])xkCopy.Clone();
                i++;
            }

            return (xk, i, f(xk).Norm());
        }

        public static (double[], int, double) DecompositionNewtonHords(double[] x0, double[] x1, double[] x2, Function f, Function g, DerivativeFunction df, Function_i gi, double eps)
        {
            int n = x0.Length;
           

            double[] xk2 = x2;
            double[] xk1 = x1;
            double[] xk = x0;

            double[] xkPrevious = new double[n];
            var Ak = new double[n, n];
            var Hx = new double[n];
            int i = 0;
            do
            {
                xkPrevious = (double[])xk.Clone();
                //Ak = df(xk).Add(Fxy(gi, xk, xk1)).Add(Fxy(gi, xk2, xk)).Substract(Fxy(gi,xk2, xk1));


                // Ak = F`(xk) + G(x^(k-1), x^k)
                Ak = df(xk).Add(Fxy(gi, xk, xk1));

                // H = F(x) + G(x)
                Hx = (f(xk).Add(g(xk)));
                for (int j = 0; j < Hx.Length; j++)
                {
                    Hx[j] *= -1;
                }

                //LU
                var LUData = LU.ParallelLU(Ak);
                var delta_x = LU.ParallelSolve(LUData.L, LUData.U, Hx);

                xk = xk.Add(delta_x);



                //xk = xk.Substract(Ak.Inverse().Multiply(f(xk).Add(g(xk))));

                //var fg_inv = (df(xk).Add(Fxy(gi, xk2, xk1)).Inverse());
                //xk = xk.Substract(fg_inv.Multiply(f(xk).Add(g(xk))));
                xk2 = (double[])xk1.Clone();
                xk1 = (double[])xkPrevious.Clone();
                Console.WriteLine("Iteration : " + i);
                Console.WriteLine(xk[0] + ", " + xk[1] + "," + xk[2]);

                Console.WriteLine("F(xk) : " + Hx[0] + "," + Hx[1] + "," + Hx[2]);

                i++;
            }
            while (xk.Substract(xkPrevious).Norm() > eps);

            
            return (xk, i, f(xk).Add(g(xk)).Norm());
        }

        //second overload
        public static (double[], int, double) DecompositionNewtonHords(double[] x0, double[] x1, double[] x2, Function f, DerivativeFunction df, Function_i gi, double eps)
        {
            int n = x0.Length;


            double[] xk2 = x2;
            double[] xk1 = x1;
            double[] xk = x0;

            double[] xkPrevious = new double[n];
            var Ak = new double[n, n];
            var Hx = new double[n];
            int i = 0;
            do
            {
                xkPrevious = (double[])xk.Clone();
                //Ak = df(xk).Add(Fxy(gi, xk, xk1)).Add(Fxy(gi, xk2, xk)).Substract(Fxy(gi,xk2, xk1));


                // Ak = F`(xk) + G(x^(k-1), x^k)
                Ak = df(xk).Add(Fxy(gi, xk, xk1));

                // H = F(x) + G(x)
                Hx = f(xk);
                for (int j = 0; j < Hx.Length; j++)
                {
                    Hx[j] *= -1;
                }

                //LU
                //var LUData = LU.ParallelLU(Ak);
                //var delta_x = LU.ParallelSolve(LUData.L, LUData.U, Hx);
                var LUData = LU.SequentialLU(Ak);
                var delta_x = LU.Solve(LUData.L, LUData.U, Hx);

                xk = xk.Add(delta_x);



                //xk = xk.Substract(Ak.Inverse().Multiply(f(xk).Add(g(xk))));

                //var fg_inv = (df(xk).Add(Fxy(gi, xk2, xk1)).Inverse());
                //xk = xk.Substract(fg_inv.Multiply(f(xk).Add(g(xk))));
                xk2 = (double[])xk1.Clone();
                xk1 = (double[])xkPrevious.Clone();
                //Console.WriteLine("Iteration : " + i);
                //Console.WriteLine(xk[0] + ", " + xk[1] + "," + xk[2]);

                //Console.WriteLine("F(xk) : " + Hx[0] + "," + Hx[1] + "," + Hx[2]);

                i++;
            }
            while (xk.Substract(xkPrevious).Norm() > eps);


            return (xk, i, f(xk).Norm());
        }

        public static (double[], int, double) DecompositionNewtonHordsParallel(double[] x0, double[] x1, double[] x2, Function f, DerivativeFunction df, Function_i gi, double eps)
        {
            int n = x0.Length;


            double[] xk2 = x2;
            double[] xk1 = x1;
            double[] xk = x0;

            double[] xkPrevious = new double[n];
            var Ak = new double[n, n];
            var Hx = new double[n];
            int i = 0;
            do
            {
                xkPrevious = (double[])xk.Clone();
                //Ak = df(xk).Add(Fxy(gi, xk, xk1)).Add(Fxy(gi, xk2, xk)).Substract(Fxy(gi,xk2, xk1));


                // Ak = F`(xk) + G(x^(k-1), x^k)
                Ak = df(xk).AddParallel(FxyParallel(gi, xk, xk1));

                // H = F(x) + G(x)
                Hx = f(xk);

                Parallel.For(0, Hx.Length, j =>
                {
                    Hx[j] *= -1;
                });



                //LU
                var LUData = LU.ParallelLU(Ak);
                var delta_x = LU.ParallelSolve(LUData.L, LUData.U, Hx);

                xk = xk.AddParallel(delta_x);



                //xk = xk.Substract(Ak.Inverse().Multiply(f(xk).Add(g(xk))));

                //var fg_inv = (df(xk).Add(Fxy(gi, xk2, xk1)).Inverse());
                
                //xk = xk.Substract(fg_inv.Multiply(f(xk).Add(g(xk))));
                xk2 = (double[])xk1.Clone();
                xk1 = (double[])xkPrevious.Clone();
                //Console.WriteLine("Iteration : " + i);
                //Console.WriteLine(xk[0] + ", " + xk[1] + "," + xk[2]);

                //Console.WriteLine("F(xk) : " + Hx[0] + "," + Hx[1] + "," + Hx[2]);

                i++;
            }
            while (xk.SubstractParallel(xkPrevious).Norm() > eps);


            return (xk, i, f(xk).Norm());
        }

        public static (double[], int, double) DecompositionPotra(double[] x0, double[] x1, double[] x2, Function f, Function g, Function_i fi, Function_i gi, double eps)
        {
            int n = x0.Length;
            double[] xk2 = x2;
            double[] xk1 = x1;
            double[] xk = x0;

            var Ak = new double[n, n];

            int i = 0;
            while (xk.Substract(xk1).Norm() > eps)
            {
                var xkPrevious = (double[])xk.Clone();
                Ak = Fxy(fi, xk, xk1).Add(Fxy(gi,xk,xk1)).Add(Fxy(fi, xk2, xk)).Add(Fxy(gi,xk2,xk)).Substract(Fxy(fi, xk2, xk1)).Substract(Fxy(gi,xk2,xk1));
                xk = xk.Substract(Ak.Inverse().Multiply(f(xkPrevious).Add(g(xkPrevious))));

                xk2 = (double[])xk1.Clone();
                xk1 = (double[])xkPrevious.Clone();
                i++;
            }

            return (xk, i, f(xk).Add(g(xk)).Norm());
        }

        // поділені різниці
        private static double[,] Fxy(Function_i fi, double[] x, double[] y)
        {
            var n = x.Length;
            var result = new double[n, n];

            for (int j = 0; j < n; j++)
            {
                var xModified = new double[x.Length];

                for (int i = 0; i <= j; i++)
                {
                    xModified[i] = x[i];
                }

                for (int i = j + 1; i < x.Length; i++)
                {
                    xModified[i] = y[i];
                }

                var yModified = (double[])xModified.Clone();
                yModified[j] = y[j];
                for (int i = 0; i < n; i++)
                {
                    result[i, j] = (fi(xModified, i) - fi(yModified, i)) / (x[j] - y[j]);
                }
            }

            return result;
        }

        // поділені різниці
        private static double[,] FxyParallel(Function_i fi, double[] x, double[] y)
        {
            var n = x.Length;
            var result = new double[n, n];

            // якщо розпаралелити, то ламаються обчислення
            for (int j = 0; j < n; j++)
            {
                var xModified = new double[x.Length];

                Parallel.For(0, j+1, i =>
                {
                    xModified[i] = x[i];
                });

                Parallel.For(j + 1, x.Length, i =>
                {
                    xModified[i] = y[i];
                });


                var yModified = (double[])xModified.Clone();
                yModified[j] = y[j];

                Parallel.For(0, n, i =>
                {
                    result[i, j] = (fi(xModified, i) - fi(yModified, i)) / (x[j] - y[j]);
                });

            }

            return result;
        }
    }
}

