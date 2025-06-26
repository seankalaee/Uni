using System;
using static System.Math;

public static class QuasiMonteCarlo {
    static readonly int[] primes = { 2, 3 }; // Hardcoded to avoid slow primes

    public static (double, double) haltonmc(Func<vector, double> f, vector a, vector b, int N) {
        int dim = a.size;
        if (dim > primes.Length)
            throw new Exception("Halton sequence debug version supports only 2D for now");

        double V = 1; for (int i = 0; i < dim; i++) V *= b[i] - a[i];
        double sum = 0, sum2 = 0;

        var x = new vector(dim);
        for (int i = 0; i < N; i++) {
            if (i % 1000 == 0) Console.WriteLine($"  [haltonmc] progress: {i}/{N}");

            for (int k = 0; k < dim; k++) {
                double hk = Halton(i + 1, primes[k]); // skip index 0
                x[k] = a[k] + hk * (b[k] - a[k]);
            }

            if (i % 5000 == 0)
                Console.WriteLine($"    sample x = ({x[0]:F4}, {x[1]:F4})");

            double fx;
            try {
                fx = f(x);
            } catch (Exception e) {
                Console.WriteLine($"    ERROR evaluating f(x) at i={i}, x=({x[0]}, {x[1]}): {e.Message}");
                throw;
            }

            sum += fx;
            sum2 += fx * fx;
        }

        double mean = sum / N;
        double sigma = Sqrt(sum2 / N - mean * mean);
        return (mean * V, sigma * V / Sqrt(N));
    }

    static double Halton(int index, int b) {
        double f = 1.0, r = 0.0;
        while (index > 0) {
            f = f / b;
            r += f * (index % b);
            index /= b;
        }
        return r;
    }
}
