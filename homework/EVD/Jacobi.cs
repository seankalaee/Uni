using System;

public static class jacobi
{
    // Multiplies matrix A by Jacobi rotation matrix J(p, q, theta) from the right: A ← A * J
    public static void timesJ(matrix A, int p, int q, double theta)
    {
        double c = Math.Cos(theta), s = Math.Sin(theta);
        for (int i = 0; i < A.size1; i++)
        {
            double aip = A[i, p], aiq = A[i, q];
            A[i, p] = c * aip - s * aiq;
            A[i, q] = s * aip + c * aiq;
        }
    }

    // Multiplies matrix A by Jacobi rotation matrix J(p, q, theta) from the left: A ← J^T * A
    public static void Jtimes(matrix A, int p, int q, double theta)
    {
        double c = Math.Cos(theta), s = Math.Sin(theta);
        for (int j = 0; j < A.size2; j++)
        {
            double apj = A[p, j], aqj = A[q, j];
            A[p, j] = c * apj + s * aqj;
            A[q, j] = -s * apj + c * aqj;
        }
    }

    // Jacobi Eigenvalue Algorithm with cyclic sweeps
    public static (vector, matrix) cyclic(matrix M)
    {
        int n = M.size1;
        matrix A = M.copy();    // Copy input matrix to avoid modifying it
        matrix V = matrix.id(n); // Identity matrix to store eigenvectors
        vector w = new vector(n); // Vector to store eigenvalues
        bool changed;

        // Perform cyclic sweeps until no significant changes occur
        do
        {
            changed = false;
            for (int p = 0; p < n - 1; p++)
            {
                for (int q = p + 1; q < n; q++)
                {
                    double apq = A[p, q], app = A[p, p], aqq = A[q, q];
                    double theta = 0.5 * Math.Atan2(2 * apq, aqq - app); // Compute rotation angle
                    double c = Math.Cos(theta), s = Math.Sin(theta);

                    // Compute new diagonal elements after rotation
                    double new_app = c * c * app - 2 * s * c * apq + s * s * aqq;
                    double new_aqq = s * s * app + 2 * s * c * apq + c * c * aqq;

                    // If diagonal elements changed, perform rotation
                    if (new_app != app || new_aqq != aqq)
                    {
                        changed = true;
                        timesJ(A, p, q, theta); // A ← A * J
                        Jtimes(A, p, q, -theta); // A ← J^T * A
                        timesJ(V, p, q, theta); // V ← V * J (update eigenvectors)
                    }
                }
            }
        } while (changed);

        // Copy diagonal elements into eigenvalue vector
        for (int i = 0; i < n; i++)
            w[i] = A[i, i];

        return (w, V);
    }
}
