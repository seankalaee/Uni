using System;

public static class Relativity
{
    public static Func<double, double[], double[]> MakeOrbitODE(double epsilon)
    {
        return (phi, y) =>
        {
            double u = y[0];
            double up = y[1];
            return new double[] {
                up,                   // y0' = y1
                1 - u + epsilon * u * u  // y1' = 1 - y0 + ε*y0²
            };
        };
    }
}
