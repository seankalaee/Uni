using System;

public class Jacobi
{
    public static void TimesJ(double[,] A, int p, int q, double theta)
    {
        double c = Math.Cos(theta);
        double s = Math.Sin(theta);
        int n = A.GetLength(0);

        for (int i = 0; i < n; i++)
        {
            double aip = A[i, p];
            double aiq = A[i, q];
            A[i, p] = c * aip - s * aiq;
            A[i, q] = s * aip + c * aiq;
        }
    }

    public static void JTimes(double[,] A, int p, int q, double theta)
    {
        double c = Math.Cos(theta);
        double s = Math.Sin(theta);
        int n = A.GetLength(0);

        for (int j = 0; j < n; j++)
        {
            double apj = A[p, j];
            double aqj = A[q, j];
            A[p, j] = c * apj - s * aqj;
            A[q, j] = s * apj + c * aqj;
        }
    }

    public static void Cyclic(double[,] A, double[] w, double[,] V)
    {
        int n = A.GetLength(0);
        int maxIterations = 1000; // Prevent infinite loop
        double tolerance = 1e-9;  // Stop when changes are minimal

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                V[i, j] = (i == j) ? 1.0 : 0.0;
        }

        bool changed;
        int iterations = 0;

        do
        {
            changed = false;
            iterations++;

            for (int p = 0; p < n - 1; p++)
            {
                for (int q = p + 1; q < n; q++)
                {
                    double apq = A[p, q], app = A[p, p], aqq = A[q, q];
                    double theta = 0.5 * Math.Atan2(2 * apq, aqq - app);
                    double c = Math.Cos(theta), s = Math.Sin(theta);
                    double new_app = c * c * app - 2 * s * c * apq + s * s * aqq;
                    double new_aqq = s * s * app + 2 * s * c * apq + c * c * aqq;

                    // Only apply rotation if values change significantly
                    if (Math.Abs(new_app - app) > tolerance || Math.Abs(new_aqq - aqq) > tolerance)
                    {
                        changed = true;
                        TimesJ(A, p, q, theta);
                        JTimes(A, p, q, -theta);
                        TimesJ(V, p, q, theta);
                    }
                }
            }

            Console.WriteLine($"Iteration {iterations}: Max off-diagonal value = {FindMaxOffDiagonal(A)}");

        } while (changed && iterations < maxIterations);

        if (iterations >= maxIterations)
            Console.WriteLine("Warning: Jacobi did not fully converge.");

        for (int i = 0; i < n; i++)
            w[i] = A[i, i]; // Extract eigenvalues
    }

    private static double FindMaxOffDiagonal(double[,] A)
    {
        int n = A.GetLength(0);
        double maxVal = 0.0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                maxVal = Math.Max(maxVal, Math.Abs(A[i, j]));

        return maxVal;
    }
}
