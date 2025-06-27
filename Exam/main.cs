using System;
using System.IO;
using System.Diagnostics;

class Program {
    static StreamWriter resultsWriter;
    static int sampleCount = 10000; // Number of samples used for global search

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

    static void Run(Func<vector, double> f, string name, vector a, vector b, string formula, string knownMinimum) {
        Console.WriteLine($"\n--- Running {name} ---");
        resultsWriter.WriteLine($"--- {char.ToUpper(name[0]) + name.Substring(1)} ---");
        resultsWriter.WriteLine(formula);

        (vector bestSample, double bestValue) = FindBestSample(f, a, b, sampleCount);
        Console.WriteLine($"Best sample: f({bestSample[0]:F4}, {bestSample[1]:F4}) = {bestValue:F6}");
        resultsWriter.WriteLine($"Best sample: f({bestSample[0]:F4}, {bestSample[1]:F4}) = {bestValue:F6}");

        string traceFile = $"trace_{name}.txt";
        string finalFile = $"final_{name}.txt";
        string bestFile = $"best_{name}.txt";

        vector minimum = newton.minimize(f, bestSample, traceFile);
        double result = f(minimum);
        Console.WriteLine($"Final result: f({minimum[0]:F6}, {minimum[1]:F6}) = {result:F6}");
        resultsWriter.WriteLine($"Final result: f({minimum[0]:F6}, {minimum[1]:F6}) = {result:F6}");

        resultsWriter.WriteLine(knownMinimum);
        resultsWriter.WriteLine();

        using (var writer = new StreamWriter(bestFile, false)) {
            writer.WriteLine($"{bestSample[0]} {bestSample[1]}");
        }

        using (var writer = new StreamWriter(finalFile, false)) {
            writer.WriteLine($"{minimum[0]} {minimum[1]} {result}");
        }

        string surfaceFile = $"{name}_surface.txt";
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
        }
    }

    static void Main() {
        resultsWriter = new StreamWriter("RESULTS.txt", false);

        resultsWriter.WriteLine($"Sample counts: {sampleCount}");
        resultsWriter.WriteLine();

        Func<vector, double> himmelblau = x => {
            double x1 = x[0], x2 = x[1];
            return Math.Pow(x1 * x1 + x2 - 11, 2) + Math.Pow(x1 + x2 * x2 - 7, 2);
        };

        Func<vector, double> rosenbrock = x => {
            double x1 = x[0], x2 = x[1];
            return Math.Pow(1 - x1, 2) + 100 * Math.Pow(x2 - x1 * x1, 2);
        };

        Func<vector, double> beale = x => {
            double x1 = x[0], x2 = x[1];
            return Math.Pow(1.5 - x1 + x1 * x2, 2)
                 + Math.Pow(2.25 - x1 + x1 * x2 * x2, 2)
                 + Math.Pow(2.625 - x1 + x1 * x2 * x2 * x2, 2);
        };

        Func<vector, double> booth = x => {
            double x1 = x[0], x2 = x[1];
            return Math.Pow(x1 + 2 * x2 - 7, 2) + Math.Pow(2 * x1 + x2 - 5, 2);
        };

        Run(himmelblau, "himmelblau", new vector(-5, -5), new vector(5, 5),
            "f(x,y) = (x² + y − 11)² + (x + y² − 7)²", "Known minima: (3,2), (-2.805,3.131), (-3.779,-3.283), (3.584,-1.848)");

        Run(rosenbrock, "rosenbrock", new vector(-3, -3), new vector(3, 3),
            "f(x,y) = (1 − x)² + 100(y − x²)²", "Known minimum: (1, 1)");

        Run(beale, "beale", new vector(-4.5, -4.5), new vector(4.5, 4.5),
            "f(x,y) = (1.5 − x + x·y)² + (2.25 − x + x·y²)² + (2.625 − x + x·y³)²", "Known minimum: (3, 0.5)");

        Run(booth, "booth", new vector(-10, -10), new vector(10, 10),
            "f(x,y) = (x + 2y − 7)² + (2x + y − 5)²", "Known minimum: (1, 3)");

        resultsWriter.Close();

        Console.WriteLine("\n All tasks complete.");
        Console.WriteLine("Generating plots...");

        foreach (var gpfile in new[] { "plotH.gp", "plotR.gp", "plotB.gp", "plotBooth.gp" }) {
            var psi = new ProcessStartInfo("gnuplot", gpfile);
            psi.UseShellExecute = false;
            Process.Start(psi);
        }
    }
}
