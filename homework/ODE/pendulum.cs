using System;
using static System.Math;

public static class Pendulum
{
    public static Func<double, double[], double[]> MakePendulum(double b, double c)
    {
        return (t, y) =>
        {
            double theta = y[0];
            double omega = y[1];
            return new double[] { omega, -b * omega - c * Sin(theta) };
        };
    }
}
