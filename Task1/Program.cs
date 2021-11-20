using System;

namespace Task1
{
    class Program
    {
        public static double eps = 0.0000001;
        static void Main(string[] args)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            //One_1(); // 10 рівнянь
            //watch.Stop();
            //Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds + 100} ms");

            // One_2(); // 10

            ////One_3();

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Three_3();
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            Console.ReadKey();
        }

      
        public static void One_1()
        {
            double[] x0 = Initialization.Initialize(2, -1.0);
            var (result, iterations, norm) = Solver.Newton(x0, Functions.F1, Functions.DF1, eps);
            Console.WriteLine("(1.1) NEWTON:");
            OperationsIO.DisplayResults(result, iterations, norm);

            //x0 = Initialization.Initialize(10, -1.0);
            //var x1 = Initialization.Initialize(10, -0.9999);
            //var x2 = Initialization.Initialize(10, -0.9998);
            //(result, iterations, norm) = Solver.Potra(x0, x1, x2, Functions.F1, Functions.F1i, eps);
            //Console.WriteLine("(1.1) Potra:");
            //OperationsIO.DisplayResults(result, iterations, norm);
        }

        public static void Two_1()
        {
            var x0 = new double[2] { -1.2, 1.0 };
            var (result, iterations, norm) = Solver.Newton(x0, Functions.F2, Functions.DF2, eps);
            OperationsIO.DisplayResults(result, iterations, norm);

            var x1 = new double[2] { -1.1999, 1.0001 };
            var x2 = new double[2] { -1.1998, 1.0002 };
            (result, iterations, norm) = Solver.Potra(x0, x1, x2, Functions.F2, Functions.F2i, eps);

            OperationsIO.DisplayResults(result, iterations, norm);
        }
        
        public static void One_2()
        {
            double[] x0 = Initialization.Initialize(10, -1.0);
            var (result, iterations, norm) = Solver.Newton(x0, Functions.F1, Functions.DF1, eps);
            //Console.WriteLine("(2.1) NEWTON:");
            //OperationsIO.DisplayResults(result, iterations, norm);

            (result, iterations, norm) = Solver.NewtonSequential(x0, Functions.F1, Functions.DF1, eps);
            Console.WriteLine("(2.1) SEQUENTIAL NEWTON WITH APPROXIMATION:");
            OperationsIO.DisplayResults(result, iterations, norm);

            (result, iterations, norm) = Solver.NewtonParallel(x0, Functions.F1, Functions.DF1, eps);
            Console.WriteLine("(2.1) PARALLEL NEWTON WITH APPROXIMATION:");
            OperationsIO.DisplayResults(result, iterations, norm);

            x0 = Initialization.Initialize(10, -1.0);
            var x1 = Initialization.Initialize(10, -0.9999);
            var x2 = Initialization.Initialize(10, -0.9998);
            //(result, iterations, norm) = Solver.Potra(x0, x1, x2, Functions.F1, Functions.F1i, eps);
            //Console.WriteLine("(2.1) POTRA:");
            //OperationsIO.DisplayResults(result, iterations, norm);

            (result, iterations, norm) = Solver.PotraSequential(x0, x1, x2, Functions.F1, Functions.F1i, eps);
            Console.WriteLine("(2.1) SEQUENTIAL POTRA WITH APPROXIMATION:");
            OperationsIO.DisplayResults(result, iterations, norm);

            (result, iterations, norm) = Solver.PotraParallel(x0, x1, x2, Functions.F1, Functions.F1i, eps);
            Console.WriteLine("(2.1) PARALLEL POTRA WITH APPROXIMATION:");
            OperationsIO.DisplayResults(result, iterations, norm);
        }

        public static void Two_2()
        {
            var x0 = new double[2] { -1.2, 1.0 };
            var (result, iterations, norm) = Solver.Newton(x0, Functions.F2, Functions.DF2, eps);
            //Console.WriteLine("NEWTON:");
            //OperationsIO.DisplayResults(result, iterations, norm);

            (result, iterations, norm) = Solver.NewtonSequential(x0, Functions.F2, Functions.DF2, eps);
            Console.WriteLine("SEQUENTIAL NEWTON WITH APPROXIMATION:");
            OperationsIO.DisplayResults(result, iterations, norm);

            (result, iterations, norm) = Solver.NewtonParallel(x0, Functions.F2, Functions.DF2, eps);
            Console.WriteLine("PARALLEL NEWTON WITH APPROXIMATION:");
            OperationsIO.DisplayResults(result, iterations, norm);

            var x1 = new double[2] { -1.1999, 1.0001 };
            var x2 = new double[2] { -1.1998, 1.0002 };
            (result, iterations, norm) = Solver.Potra(x0, x1, x2, Functions.F2, Functions.F2i, eps);
            //Console.WriteLine("POTRA:");
            //OperationsIO.DisplayResults(result, iterations, norm);

            (result, iterations, norm) = Solver.PotraSequential(x0, x1, x2, Functions.F2, Functions.F2i, eps);
            Console.WriteLine("SEQUENTIAL POTRA WITH APPROXIMATION:");
            OperationsIO.DisplayResults(result, iterations, norm);

            (result, iterations, norm) = Solver.PotraParallel(x0, x1, x2, Functions.F2, Functions.F2i, eps);
            Console.WriteLine("PARALLEL POTRA WITH APPROXIMATION:");
            OperationsIO.DisplayResults(result, iterations, norm);
        }

        public static void One_3()
        {
            var x0 = new double[2] { -1.0, -1.0 }; 
            var x1 = new double[2];
            var x2 = new double[2];

            for (int i = 0; i < x0.Length; i++)
            {
                x1[i] += 0.0001;
                x2[i] += 0.0002;
            }
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var (result, iterations, norm) = Solver.DecompositionNewtonHords(x0, x1, x2, Functions3.F1, Functions3.G1, Functions3.DF1, Functions3.G1i, eps);

            watch.Stop(); ;
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine("Decomposition Newton-Potra:");
            OperationsIO.DisplayResultsWithMoreDigits(result, iterations, norm);

            (result, iterations, norm) = Solver.DecompositionPotra(x0, x1, x2, Functions3.F1, Functions3.G1, Functions3.F1i, Functions3.G1i, eps);
            Console.WriteLine("Decomposition Potra:");
            OperationsIO.DisplayResultsWithMoreDigits(result, iterations, norm);
        }

        public static void Three_3()
        {
            var x0 = new double[3] { -1.5, 2.5, 4.0 };
            var x1 = new double[3];
            var x2 = new double[3];
            //var x0 = new double[5] { -1.5, 2.5, 4.0, 3.0, 1.0 };
            //var x1 = new double[5];
            //var x2 = new double[5];
            for (int i = 0; i < x0.Length; i++)
            {
                x1[i] += 0.0001;
                x2[i] += 0.0002;
            }
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            var (result, iterations, norm) = Solver.DecompositionNewtonHords(x0, x1, x2, Functions.F, Functions3.DF2, Functions3.G2i, eps);

            //var (result, iterations, norm) = Solver.DecompositionNewtonHords(x0, x1, x2, Functions3.F2, Functions3.G2, Functions3.DF2, Functions3.G2i, eps);
            //watch.Stop();
            //Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine("(3.1) Decomposition Newton-Hords:");
            OperationsIO.DisplayResultsWithMoreDigits(result, iterations, norm);

            //(result, iterations, norm) = Solver.DecompositionPotra(x0, x1, x2, Functions3.F2, Functions3.G2, Functions3.F2i, Functions3.G2i, eps);
            //Console.WriteLine("(3.1) Decomposition Potra:");
            //OperationsIO.DisplayResultsWithMoreDigits(result, iterations, norm);
        }
    }
}
