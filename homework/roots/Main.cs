using System;
using System.Collections.Generic;
using System.IO;
using static System.Math;

public class MainProgram {
    public static void Main() {
        // ===============================
        // Part 1: Root-Finding (Newton)
        // ===============================
        StreamWriter output = new StreamWriter("RosenbrockHimmelblau.txt", false);

        Func<vector, vector> rosenbrockGradient = v => {
            double x = v[0], y = v[1];
            return new vector(
                -2 * (1 - x) - 400 * x * (y - x * x),
                200 * (y - x * x)
            );
        };

        Func<vector, vector> himmelblauGradient = v => {
            double x = v[0], y = v[1];
            return new vector(
                4 * x * (x * x + y - 11) + 2 * (x + y * y - 7),
                2 * (x * x + y - 11) + 4 * y * (x + y * y - 7)
            );
        };

        vector rosen_start = new vector(-1.0, 1.0);
        vector himmel_start = new vector(4.0, 0.0);

        output.WriteLine("Finding minimum of Rosenbrock function:");
        vector rosen_result = newton_solver.newton(rosenbrockGradient, rosen_start, 1e-6);
        output.WriteLine($"Found minimum at: ({rosen_result[0]}, {rosen_result[1]})\n");

        output.WriteLine("Finding minimum of Himmelblau function:");
        vector himmel_result = newton_solver.newton(himmelblauGradient, himmel_start, 1e-6);
        output.WriteLine($"Found minimum at: ({himmel_result[0]}, {himmel_result[1]})");

        output.Close();

        // ===============================
        // Part 2: Hydrogen Shooting
        // ===============================
        StreamWriter hydro_out = new StreamWriter("hydrogen_data.txt", false);
        double r_min = 1e-3;
        double r_max = 8.0;
        double energy_guess_a = -0.8;
        double energy_guess_b = -0.3;
        double acc = 1e-4;

        Func<double, double[], double[]> schrodinger(double E) {
            return (r, y) => new double[] {
                y[1],
                -2 * ( -1 / r - E ) * y[0]
            };
        }

        double shoot(double E) {
            double[] y0 = { r_min - r_min * r_min, 1 - 2 * r_min }; // Initial conditions
            var (xlist, ylist) = Driver.driver12(schrodinger(E), (r_min, r_max), y0, h: 0.05, acc: 1e-6, eps: 1e-6);
            return ylist[ylist.Count - 1][0]; // return f_E(r_max)
        }

        double bisection(Func<double, double> f, double a, double b, double tol = 1e-6) {
            double fa = f(a), fb = f(b);
            if (fa * fb > 0) throw new Exception("Root not bracketed");
            while (b - a > tol) {
                double mid = (a + b) / 2;
                double fmid = f(mid);
                if (fa * fmid < 0) { b = mid; fb = fmid; }
                else { a = mid; fa = fmid; }
            }
            return (a + b) / 2;
        }

        double E0 = bisection(shoot, energy_guess_a, energy_guess_b, acc);
        Console.WriteLine($"Ground state energy found: E0 ≈ {E0}");

        // Solve again to write f(r)
        double[] y00 = { r_min - r_min * r_min, 1 - 2 * r_min };
        var (R, Y) = Driver.driver12(schrodinger(E0), (r_min, r_max), y00, h: 0.05, acc: 1e-6, eps: 1e-6);

        for (int i = 0; i < R.Count; i++) {
            double r = R[i];
            double f_num = Y[i][0];
            double f_exact = r * Exp(-r);
            hydro_out.WriteLine($"{r,10:F6} {f_num,15:E6} {f_exact,15:E6}");
        }

        hydro_out.Close();
    }
}
