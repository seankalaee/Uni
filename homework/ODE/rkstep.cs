using System;

public static class RK
{
    public static (double[] yh, double[] dy) rkstep12(
        Func<double, double[], double[]> f,
        double x,
        double[] y,
        double h)
    {
        var k0 = f(x, y);
        var ytemp = new double[y.Length];
        for (int i = 0; i < y.Length; i++) ytemp[i] = y[i] + k0[i] * (h / 2);
        var k1 = f(x + h / 2, ytemp);

        var yh = new double[y.Length];
        var dy = new double[y.Length];
        for (int i = 0; i < y.Length; i++)
        {
            yh[i] = y[i] + k1[i] * h;
            dy[i] = (k1[i] - k0[i]) * h;
        }

        return (yh, dy);
    }
}
