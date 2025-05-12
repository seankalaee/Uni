using System;
using static System.Math;

public static class Jacobian {
    public static matrix compute(Func<vector, vector> f, vector x, vector fx = null, vector dx = null) {
        int n = x.size;
        if (dx == null) dx = x.map(xi => Max(Abs(xi), 1) * Pow(2, -26));
        if (fx == null) fx = f(x);
        matrix J = new matrix(n, n);

        for (int j = 0; j < n; j++) {
            x[j] += dx[j];
            vector dfx = f(x) - fx;
            for (int i = 0; i < n; i++) J[i, j] = dfx[i] / dx[j];
            x[j] -= dx[j];
        }

        return J;
    }
}
