using System;
using System.IO;
using static System.Math;

public class QSpline {
    private vector x, y, b, c;

    public QSpline(vector xs, vector ys) {
        x = xs;
        y = ys;
        int n = x.size;

        b = new vector(n - 1);
        c = new vector(n - 1);
        vector h = new vector(n - 1);
        vector p = new vector(n - 1);

        for (int i = 0; i < n - 1; i++) {
            h[i] = x[i + 1] - x[i];
            p[i] = (y[i + 1] - y[i]) / h[i];
        }

        c[0] = 0;
        for (int i = 0; i < n - 2; i++) {
            c[i + 1] = (p[i + 1] - p[i] - c[i] * h[i]) / h[i + 1];
        }
        c[n - 2] /= 2;

        for (int i = n - 3; i >= 0; i--) {
            c[i] = (p[i + 1] - p[i] - c[i + 1] * h[i + 1]) / h[i];
        }

        for (int i = 0; i < n - 1; i++) {
            b[i] = p[i] - c[i] * h[i];
        }

        using (StreamWriter qsplineFile = new StreamWriter("qspline_output.txt")) {
            qsplineFile.WriteLine("# Interval [x_i, x_i+1], b_i, c_i");
            for (int i = 0; i < n - 1; i++) {
                qsplineFile.WriteLine($"{x[i]} {x[i + 1]} {b[i]:F6} {c[i]:F6}");
            }
        }
    }

    private int BinSearch(double z) {
        int i = 0, j = x.size - 1;
        while (j - i > 1) {
            int mid = (i + j) / 2;
            if (z > x[mid]) i = mid;
            else j = mid;
        }
        return i;
    }

    public double Evaluate(double z) {
        int i = BinSearch(z);
        double dx = z - x[i];
        return y[i] + b[i] * dx + c[i] * dx * dx;
    }

    public double Derivative(double z) {
        int i = BinSearch(z);
        double dx = z - x[i];
        return b[i] + 2 * c[i] * dx;
    }

    public double Integral(double z) {
        int i = BinSearch(z);
        double integral = 0;

        for (int j = 0; j < i; j++) {
            double dx = x[j + 1] - x[j];
            integral += y[j] * dx + 0.5 * b[j] * dx * dx + (c[j] * dx * dx * dx) / 3;
        }

        double dx_last = z - x[i];
        integral += y[i] * dx_last + 0.5 * b[i] * dx_last * dx_last + (c[i] * dx_last * dx_last * dx_last) / 3;

        return integral;
    }
}
