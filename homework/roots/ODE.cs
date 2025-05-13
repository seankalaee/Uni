using System;
using System.Collections.Generic;
using static System.Math;

public static class ODE {

    // Runge-Kutta 2nd/3rd order step function
    public static Tuple<vector, vector> rkstep23(
        Func<double, vector, vector> F,
        double x,
        vector y,
        double h
    ) {
        vector k0 = F(x, y);
        vector k1 = F(x + h / 2, y + k0 * (h / 2));
        vector k2 = F(x + 3 * h / 4, y + k1 * (3 * h / 4));

        vector ka = k0 * (2.0 / 9) + k1 * (3.0 / 9) + k2 * (4.0 / 9);
        vector kb = k1;

        vector yh = y + ka * h;
        vector er = (ka - kb) * h;

        return new Tuple<vector, vector>(yh, er);
    }

    // Adaptive ODE solver using rkstep23
    public static Tuple<List<double>, List<vector>> driver(
        Func<double, vector, vector> F,
        double a,
        vector ya,
        double b,
        double acc = 1e-2,
        double eps = 1e-2,
        double h = 0.01,
        List<double> xlist = null,
        List<vector> ylist = null
    ) {
        if (a > b) throw new Exception("ODE.driver: a > b");

        double x = a;
        vector y = ya;

        if (xlist == null) xlist = new List<double>();
        if (ylist == null) ylist = new List<vector>();

        xlist.Add(x);
        ylist.Add(y);

        while (true) {
            if (x >= b) break;
            if (x + h > b) h = b - x;

            Tuple<vector, vector> step = rkstep23(F, x, y, h);
            vector yh = step.Item1;
            vector erv = step.Item2;

            double tol = Max(acc, yh.norm() * eps) * Sqrt(h / (b - a));
            double err = erv.norm();

            if (err < tol) {
                x += h;
                y = yh;
                xlist.Add(x);
                ylist.Add(y);
            }

            h *= Min(Pow(tol / err, 0.25) * 0.95, 2);
        }

        return new Tuple<List<double>, List<vector>>(xlist, ylist);
    }
}
