using System;
using static System.Math;

public static class LinearSpline {
    public static int binsearch(double[] x, double z) {
        /* Locates the interval for z by bisection */
        if (z < x[0] || z > x[x.Length - 1]) throw new Exception("binsearch: bad z");
        int i = 0, j = x.Length - 1;
        while (j - i > 1) {
            int mid = (i + j) / 2;
            if (z > x[mid]) i = mid; else j = mid;
        }
        return i;
    }

    public static double linterp(double[] x, double[] y, double z) {
        int i = binsearch(x, z);
        double dx = x[i + 1] - x[i];
        if (!(dx > 0)) throw new Exception("uups...");
        double dy = y[i + 1] - y[i];
        return y[i] + dy / dx * (z - x[i]);
    }

    public static double linterpInteg(double[] x, double[] y, double z) {
        int i = binsearch(x, z);
        double integral = 0;

        // Sum all full intervals up to i
        for (int j = 0; j < i; j++) {
            double dx = x[j + 1] - x[j];
            double dy = y[j + 1] - y[j];
            integral += y[j] * dx + 0.5 * dy * dx;
        }

        // Handle last segment using trapezoidal rule
        double dx_last = z - x[i];
        double slope = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
        double y_z = y[i] + slope * dx_last;
        integral += (y[i] + y_z) * dx_last / 2;

        return integral;
    }
}
