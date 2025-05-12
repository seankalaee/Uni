using System;

public static class RK {
    public static (double[] yh, double[] dy) rkstep12(Func<double, double[], double[]> f, double x, double[] y, double h) {
        int n = y.Length;
        double[] k0 = f(x, y);
        double[] yt = new double[n];
        for (int i = 0; i < n; i++) yt[i] = y[i] + k0[i] * h / 2;
        double[] k1 = f(x + h / 2, yt);
        double[] yh = new double[n], dy = new double[n];
        for (int i = 0; i < n; i++) {
            yh[i] = y[i] + k1[i] * h;
            dy[i] = (k1[i] - k0[i]) * h;
        }
        return (yh, dy);
    }
}
