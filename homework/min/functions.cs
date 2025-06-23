using System;

public static class functions {
    public static double rosenbrock(vector v) {
        double x = v[0], y = v[1];
        return Math.Pow(1 - x, 2) + 100 * Math.Pow(y - x * x, 2);
    }

    public static double himmelblau(vector v) {
        double x = v[0], y = v[1];
        return Math.Pow(x * x + y - 11, 2) + Math.Pow(x + y * y - 7, 2);
    }
}
