using System;

public static class hessian {
    public static matrix compute(Func<vector, double> f, vector x, double dx = 1e-5) {
        int n = x.size;
        matrix H = new matrix(n, n);
        for (int i = 0; i < n; i++) {
            double saved_i = x[i];
            x[i] = saved_i + dx;
            double f_ip = f(x);
            x[i] = saved_i - dx;
            double f_im = f(x);
            x[i] = saved_i;
            H[i, i] = (f_ip - 2 * f(x) + f_im) / (dx * dx);
            for (int j = i + 1; j < n; j++) {
                double saved_j = x[j];
                x[i] = saved_i + dx;
                x[j] = saved_j + dx;
                double f_pp = f(x);
                x[j] = saved_j - dx;
                double f_pm = f(x);
                x[i] = saved_i - dx;
                x[j] = saved_j + dx;
                double f_mp = f(x);
                x[j] = saved_j - dx;
                double f_mm = f(x);
                x[i] = saved_i;
                x[j] = saved_j;
                double mixed = (f_pp - f_pm - f_mp + f_mm) / (4 * dx * dx);
                H[i, j] = mixed;
                H[j, i] = mixed;
            }
        }
        return H;
    }

    public static matrix central(Func<vector, double> f, vector x, double dx = 1e-5) {
        return compute(f, x, dx); // Same method reused for central
    }
}
