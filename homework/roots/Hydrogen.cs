using System;
using System.Collections.Generic;

public static class Hydrogen {
    // Parameters for integration and accuracy
    public static double rmin = 1e-5;
    public static double rmax = 8;
    public static double acc = 1e-6;
    public static double eps = 1e-6;

    // Returns the Schrödinger equation as a first-order system
    public static Func<double, vector, vector> schrodinger(double E) {
        return (r, f) => {
            double dfdr = f[1];                          // f'
            double d2fdr2 = -2 * (E + 1 / r) * f[0];     // f''
            return new vector(dfdr, d2fdr2);             // Return vector of first-order system
        };
    }

    // Integrate the Schrödinger equation and return f(rmax)
    public static vector integrate(double E) {
        vector f0 = new vector(rmin - rmin * rmin, 1 - 2 * rmin); // Initial condition
        var result = ODE.driver(schrodinger(E), rmin, f0, rmax, acc: acc, eps: eps);
        List<double> xlist = result.Item1;
        List<vector> ylist = result.Item2;
        return ylist[ylist.Count - 1]; // Final value at rmax
    }

    // Return full wavefunction f(r) and r values as two lists
    public static Tuple<List<double>, List<double>> radial_solution(double E) {
        vector f0 = new vector(rmin - rmin * rmin, 1 - 2 * rmin); // Initial condition
        var result = ODE.driver(schrodinger(E), rmin, f0, rmax, acc: acc, eps: eps);
        List<double> xlist = result.Item1;
        List<vector> ylist = result.Item2;

        List<double> rvals = new List<double>();
        List<double> fvals = new List<double>();

        for (int i = 0; i < xlist.Count; i++) {
            rvals.Add(xlist[i]);
            fvals.Add(ylist[i][0]);
        }

        return new Tuple<List<double>, List<double>>(rvals, fvals);
    }

    // Function used in root finding: return f(rmax) for energy E
    public static double M(double E) {
        return integrate(E)[0];
    }

    // Analytic hydrogen ground state wavefunction (up to normalization)
    public static double exact(double r) {
        return r * Math.Exp(-r);
    }
}
