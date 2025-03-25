using System;
using System.Collections.Generic;
using static System.Math;

public static class Driver
{
    public static Tuple<List<double>, List<double[]>> driver12(
        Func<double, double[], double[]> f,
        (double, double) interval,
        double[] yinit,
        double h = 0.125,
        double acc = 0.01,
        double eps = 0.01)
    {
        var (a, b) = interval;
        double x = a;
        double[] y = (double[])yinit.Clone();

        List<double> xlist = new List<double> { x };
        List<double[]> ylist = new List<double[]> { (double[])y.Clone() };

        while (true)
        {
            if (x >= b) break;
            if (x + h > b) h = b - x;

            var (yh, dy) = RK.rkstep12(f, x, y, h);

            double err = Norm(dy);
            double tol = (acc + eps * Norm(yh)) * Sqrt(h / (b - a));

            if (err <= tol)
            {
                x += h;
                y = yh;
                xlist.Add(x);
                ylist.Add((double[])y.Clone());
            }

            h *= Min(Pow(tol / err, 0.25) * 0.95, 2);
        }

        return Tuple.Create(xlist, ylist);
    }

    private static double Norm(double[] v)
    {
        double sum = 0;
        foreach (var vi in v) sum += vi * vi;
        return Sqrt(sum);
    }
}
