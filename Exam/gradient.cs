using System;

public static class gradient {
    public static vector compute(Func<vector, double> f, vector x, double dx = 1e-7) {
        // Forward difference
        vector grad = new vector(x.size);
        for (int i = 0; i < x.size; i++) {
            double saved = x[i];
            x[i] += dx;
            grad[i] = (f(x) - f(x - dx * vector.e(i, x.size))) / dx;
            x[i] = saved;
        }
        return grad;
    }

    public static vector central(Func<vector, double> f, vector x, double dx = 1e-7) {
        vector grad = new vector(x.size);
        for (int i = 0; i < x.size; i++) {
            double saved = x[i];
            x[i] = saved + dx;
            double fPlus = f(x);
            x[i] = saved - dx;
            double fMinus = f(x);
            x[i] = saved;
            grad[i] = (fPlus - fMinus) / (2 * dx);
        }
        return grad;
    }
}
