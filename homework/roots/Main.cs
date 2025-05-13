using System;
using System.Collections.Generic;
using System.IO;

class main {
    static void Main() {
        using (var output = new StreamWriter("Output.txt")) {

            // --- Newton's method: Rosenbrock function ---
            output.WriteLine("Rosenbrock gradient root:");
            Func<vector, vector> rosenbrock_grad = v => {
                double x = v[0], y = v[1];
                return new vector(-2 * (1 - x) - 400 * x * (y - x * x),
                                  200 * (y - x * x));
            };
            vector start1 = new vector(0.5, 0.5);
            vector result1 = Newton.newton(rosenbrock_grad, start1);
            output.WriteLine($"‖f(x)‖ = {rosenbrock_grad(result1).norm()}");
            output.WriteLine();

            // --- Newton's method: Himmelblau function ---
            output.WriteLine("Himmelblau gradient root:");
            Func<vector, vector> himmelblau_grad = v => {
                double x = v[0], y = v[1];
                return new vector(4 * x * (x * x + y - 11) + 2 * (x + y * y - 7),
                                  2 * (x * x + y - 11) + 4 * y * (x + y * y - 7));
            };
            vector start2 = new vector(5.0, 5.0);
            vector result2 = Newton.newton(himmelblau_grad, start2);
            output.WriteLine($"‖f(x)‖ = {himmelblau_grad(result2).norm()}");
            output.WriteLine();

            // --- Hydrogen atom ground state energy ---
            output.WriteLine("Hydrogen atom ground state:");
            Func<double, double> M = Hydrogen.M;
            double E0 = Bisection(M, -1.0, -0.1, 1e-6);
            output.WriteLine($"Computed E₀ = {E0}");
            output.WriteLine($"Exact E₀ = -0.5");
            output.WriteLine($"Error = {Math.Abs(E0 + 0.5)}");

            // --- Save wavefunctions to file ---
            using (var wf = new StreamWriter("wavefunction.dat")) {
                var result = Hydrogen.radial_solution(E0);
                var rvals = result.Item1;
                var fvals = result.Item2;
                for (int i = 0; i < rvals.Count; i++) {
                    double r = rvals[i];
                    double f = fvals[i];
                    double exact = Hydrogen.exact(r);
                    wf.WriteLine($"{r} {f} {exact}");
                }
            }
            output.WriteLine();

            // --- Convergence study ---
            output.WriteLine("Convergence study:");
            double[] rmins = { 1e-1, 5e-1 };
            double[] rmaxs = { 4, 8 };
            double[] accs = { 1e-1, 1e-2 };
            double[] epss = { 1e-1, 1e-2 };

            foreach (double rmin in rmins)
            foreach (double rmax in rmaxs)
            foreach (double acc in accs)
            foreach (double eps in epss) {
                Hydrogen.rmin = rmin;
                Hydrogen.rmax = rmax;
                Hydrogen.acc = acc;
                Hydrogen.eps = eps;

                double fa = M(-1.0), fb = M(-0.1);
                if (fa * fb > 0) {
                    output.WriteLine($"rmin={rmin} rmax={rmax} acc={acc} eps={eps} => Root not bracketed");
                    continue;
                }

                double E = Bisection(M, -1.0, -0.1, 1e-6);
                double err = Math.Abs(E + 0.5);
                output.WriteLine($"rmin={rmin} rmax={rmax} acc={acc} eps={eps} => E={E} err={err}");
            }
        }
    }

    public static double Bisection(Func<double, double> f, double a, double b, double tol) {
        double fa = f(a), fb = f(b);
        if (fa * fb > 0) throw new Exception("Root not bracketed");
        while (b - a > tol) {
            double c = (a + b) / 2;
            double fc = f(c);
            if (fa * fc < 0) { b = c; fb = fc; }
            else             { a = c; fa = fc; }
        }
        return (a + b) / 2;
    }
}
