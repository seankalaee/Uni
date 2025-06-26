using System;
using System.IO;
using System.Diagnostics;

class Program {
    static (vector, double) FindBestSample(Func<vector, double> f, vector a, vector b, int N) {
        double bestVal = double.PositiveInfinity;
        vector bestX = null;
        Random rand = new Random();
        for (int i = 0; i < N; i++) {
            vector x = new vector(a.size);
            for (int j = 0; j < a.size; j++) x[j] = a[j] + rand.NextDouble() * (b[j] - a[j]);
            double val = f(x);
            if (val < bestVal) {
                bestVal = val;
                bestX = x.copy();
            }
        }
        return (bestX, bestVal);
    }

    static void Run(Func<vector, double> f, string name, vector a, vector b) {
        Console.WriteLine($"\n--- Running {name} ---");
        (vector bestSample, double bestValue) = FindBestSample(f, a, b, 10000);
        Console.WriteLine($"Best sample: f({bestSample[0]:F4}, {bestSample[1]:F4}) = {bestValue:F6}");

        string traceFile = $"trace_{name}.txt";
        string finalFile = $"final_{name}.txt";
        string bestFile = $"best_{name}.txt";
        
        vector minimum = newton.minimize(f, bestSample, traceFile);
        double result = f(minimum);
        Console.WriteLine($"Final result: f({minimum[0]:F6}, {minimum[1]:F6}) = {result:F6}");

        using (var writer = new StreamWriter(bestFile, false)) {
            writer.WriteLine($"{bestSample[0]} {bestSample[1]}");
            writer.Flush();
        }

        using (var writer = new StreamWriter(finalFile, false)) {
            writer.WriteLine($"{minimum[0]} {minimum[1]} {result}");
            writer.Flush();
        }

        string surfaceFile = name == "himmelblau" ? "himmelblau_surface.txt" : "rosenbrock_surface.txt";
        GenerateSurfaceFile(f, a, b, surfaceFile, 100);
    }

    static void GenerateSurfaceFile(Func<vector, double> f, vector a, vector b, string filename, int steps = 100) {
        using (StreamWriter file = new StreamWriter(filename)) {
            double x_min = a[0], x_max = b[0];
            double y_min = a[1], y_max = b[1];

            for (int i = 0; i <= steps; i++) {
                double x = x_min + i * (x_max - x_min) / steps;
                for (int j = 0; j <= steps; j++) {
                    double y = y_min + j * (y_max - y_min) / steps;
                    double z = f(new vector(x, y));
                    file.WriteLine($"{x} {y} {z}");
                }
                file.WriteLine(); // blank line for gnuplot
            }

            file.Flush();
        }
    }

    static void Main() {
        Func<vector, double> himmelblau = x => {
            double x1 = x[0], x2 = x[1];
            return Math.Pow(x1 * x1 + x2 - 11, 2) + Math.Pow(x1 + x2 * x2 - 7, 2);
        };

        Func<vector, double> rosenbrock = x => {
            double x1 = x[0], x2 = x[1];
            return Math.Pow(1 - x1, 2) + 100 * Math.Pow(x2 - x1 * x1, 2);
        };

        Run(himmelblau, "himmelblau", new vector(-5, -5), new vector(5, 5));
        Run(rosenbrock, "rosenbrock", new vector(-3, -3), new vector(3, 3));

        Console.WriteLine("\n✅ All tasks complete.");
        Console.WriteLine("Generating plots...");

        var gnuplotH = new ProcessStartInfo("gnuplot", "plotH.gp");
        var gnuplotR = new ProcessStartInfo("gnuplot", "plotR.gp");
        gnuplotH.UseShellExecute = false;
        gnuplotR.UseShellExecute = false;
        Process.Start(gnuplotH);
        Process.Start(gnuplotR);
    }
}
