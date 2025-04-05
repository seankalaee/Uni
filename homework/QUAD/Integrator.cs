using System;
using static System.Math;

public static class Integrator {
    // Public entry point with evals counter (non-optional, required last)
    public static double Integrate(Func<double, double> f, double a, double b, double acc, double eps, ref int evals) {
        double h = b - a;
        double f2 = f(a + 2 * h / 6); evals++;
        double f3 = f(a + 4 * h / 6); evals++;
        return IntegrateRecursive(f, a, b, acc, eps, f2, f3, ref evals);
    }

    // Recursive adaptive integrator using higher and lower order rules
    private static double IntegrateRecursive(Func<double, double> f, double a, double b, double acc, double eps, double f2, double f3, ref int evals) {
        double h = b - a;
        double f1 = f(a + h / 6); evals++;
        double f4 = f(a + 5 * h / 6); evals++;

        double Q = (2 * f1 + f2 + f3 + 2 * f4) * h / 6;
        double q = (f1 + f2 + f3 + f4) * h / 4;
        double err = Abs(Q - q);

        if (err < acc + eps * Abs(Q)) return Q;

        double mid = (a + b) / 2;
        double f2L = f(a + (mid - a) * 2 / 6); evals++;
        double f3L = f(a + (mid - a) * 4 / 6); evals++;
        double f2R = f(mid + (b - mid) * 2 / 6); evals++;
        double f3R = f(mid + (b - mid) * 4 / 6); evals++;

        return IntegrateRecursive(f, a, mid, acc / Sqrt(2), eps, f2L, f3L, ref evals) +
               IntegrateRecursive(f, mid, b, acc / Sqrt(2), eps, f2R, f3R, ref evals);
    }

    // Error function implemented via its integral representation
    public static double Erf(double z, double acc, double eps) {
        int dummy = 0;
        if (z < 0)
            return -Erf(-z, acc, eps);
        else if (z <= 1)
            return 2 / Sqrt(PI) * Integrate(x => Exp(-x * x), 0, z, acc, eps, ref dummy);
        else
            return 1 - 2 / Sqrt(PI) * Integrate(t => Exp(-Pow(z + (1 - t) / t, 2)) / (t * t), 0, 1, acc, eps, ref dummy);
    }

    // Clenshaw–Curtis variable transformation integrator with protection for x → 0
    public static double ClenshawCurtisIntegrate(Func<double, double> f, double a, double b, double acc, double eps, ref int evals)
    {
        Func<double, double> transformed = theta =>
        {
            double x = (a + b) / 2 + (b - a) / 2 * Cos(theta);
            double dx_dtheta = (b - a) / 2 * Sin(theta);
            double safe_x = x < 1e-15 ? 1e-15 : x; // protect from log(0) or division by 0
            return f(safe_x) * dx_dtheta;
        };
        return Integrate(transformed, 0, PI, acc, eps, ref evals);
    }
}
