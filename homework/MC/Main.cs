using System;
using static System.Math;
using System.IO;

public class Program {
    public static void Main() {

        bool skipSingular = true;
        bool skipQuasi = false;

        // === UNIT CIRCLE ===
        vector a = new vector(2);
        vector b = new vector(1.0, 1.0);

        Func<vector, double> unitCircle = v => (v[0]*v[0] + v[1]*v[1] <= 1) ? 1 : 0;
        double exactCircle = PI / 4;

        // === GAUSSIAN 2D ===
        vector ga = new vector(-2.0, -2.0);
        vector gb = new vector( 2.0,  2.0);

        Func<vector, double> gaussian2D = v => Exp(-v[0]*v[0] - v[1]*v[1]);
        double exactGaussian = Pow(Sqrt(PI) * SpecialFunctions.erf(2), 2);

        // === PSEUDO-MC ===
        using (StreamWriter writer = new StreamWriter("circle_data.txt")) {
            for (int exp = 1; exp <= 5; exp++) {
                int N = (int)Pow(10, exp);
                var res = MonteCarlo.plainmc(unitCircle, a, b, N);
                double actualError = Abs(res.Item1 - exactCircle);
                writer.WriteLine($"{N} {res.Item2} {actualError}");
            }
        }

        using (StreamWriter writer = new StreamWriter("gaussian_data.txt")) {
            for (int exp = 1; exp <= 5; exp++) {
                int N = (int)Pow(10, exp);
                var res = MonteCarlo.plainmc(gaussian2D, ga, gb, N);
                double actualError = Abs(res.Item1 - exactGaussian);
                writer.WriteLine($"{N} {res.Item2} {actualError}");
            }
        }

        Console.WriteLine("Circle + Gaussian results written to circle_data.txt and gaussian_data.txt");

        // === QUASI-MC (HALTON) ===
        if (!skipQuasi) {
            using (StreamWriter writer = new StreamWriter("quasi_data.txt")) {
                writer.WriteLine("# N    Circle_Error    Gaussian_Error");
                for (int exp = 1; exp <= 5; exp++) {
                    int N = (int)Pow(10, exp); // <== removed the conditional cap

                    var t1 = System.Diagnostics.Stopwatch.StartNew();
                    var circleQ = QuasiMonteCarlo.haltonmc(unitCircle, a, b, N);
                    t1.Stop();

                    double circleQError = Abs(circleQ.Item1 - exactCircle);
                    Console.WriteLine($"[quasi] Circle N={N}, time={t1.Elapsed.TotalSeconds:F2}s");

                    var t2 = System.Diagnostics.Stopwatch.StartNew();
                    var gaussianQ = QuasiMonteCarlo.haltonmc(gaussian2D, ga, gb, N);
                    t2.Stop();

                    double gaussianQError = Abs(gaussianQ.Item1 - exactGaussian);
                    Console.WriteLine($"[quasi] Gaussian N={N}, time={t2.Elapsed.TotalSeconds:F2}s");

                    writer.WriteLine($"{N} {circleQError} {gaussianQError}");
                }
            }

            Console.WriteLine("Quasi-MC results written to quasi_data.txt");
        }
        else Console.WriteLine("Quasi-MC skipped (set skipQuasi = false to enable)");

        // === SINGULAR INTEGRAL ===
        if (!skipSingular) {
            Console.WriteLine("Singular integral skipped.");
            return;
        }

        vector a3 = new vector(0.0, 0.0, 0.0);
        vector b3 = new vector(PI, PI, PI);

        Func<vector, double> singularFunc = v => 1.0 / (PI*PI*PI * (1 - Cos(v[0]) * Cos(v[1]) * Cos(v[2])));
        double exactSingular = Pow(SpecialFunctions.gamma(0.25), 4) / (4 * Pow(PI, 3));

        int N3 = unchecked((int)10_000_000L);

        Console.WriteLine($"Estimating singular integral with N = {N3}...");

        var timer = System.Diagnostics.Stopwatch.StartNew();
        var singularResult = MonteCarlo.plainmc(singularFunc, a3, b3, N3);
        timer.Stop();

        double singularEstimate = singularResult.Item1;
        double singularEstError = singularResult.Item2;
        double singularActualError = Abs(singularEstimate - exactSingular);

        using (StreamWriter sw = new StreamWriter("singular_check.txt")) {
            sw.WriteLine("Singular Integral Estimation");
            sw.WriteLine($"N = {N3}");
            sw.WriteLine($"Estimated Result : {singularEstimate}");
            sw.WriteLine($"Estimated Error  : {singularEstError}");
            sw.WriteLine($"Actual Error     : {singularActualError}");
            sw.WriteLine($"Exact Value      : {exactSingular}");
            sw.WriteLine($"Elapsed Time     : {timer.Elapsed.TotalSeconds:F2} seconds");
        }

        Console.WriteLine("Singular integral result written to singular_check.txt");
    }
}

public static class SpecialFunctions {
    public static double erf(double x) {
        double sign = Math.Sign(x);
        x = Math.Abs(x);
        double a1 =  0.254829592, a2 = -0.284496736, a3 =  1.421413741;
        double a4 = -1.453152027, a5 =  1.061405429, p =  0.3275911;
        double t = 1.0 / (1.0 + p * x);
        double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Exp(-x * x);
        return sign * y;
    }

    public static double gamma(double x) {
        double[] p = {
            676.5203681218851, -1259.1392167224028,
            771.32342877765313, -176.61502916214059,
            12.507343278686905, -0.13857109526572012,
            9.9843695780195716e-6, 1.5056327351493116e-7
        };

        if (x < 0.5)
            return PI / (Sin(PI * x) * gamma(1 - x));
        x -= 1;
        double a = 0.99999999999980993;
        for (int i = 0; i < p.Length; i++)
            a += p[i] / (x + i + 1);
        double t = x + p.Length - 0.5;
        return Sqrt(2 * PI) * Pow(t, x + 0.5) * Exp(-t) * a;
    }
}
