using System;

class EigenTest
{
    static void Main()
    {
        int n = 4;
        Random rand = new Random();
        matrix A = new matrix(n, n);

        // Generate a random symmetric matrix
        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                double value = rand.NextDouble();
                A[i, j] = value;
                A[j, i] = value;
            }
        }

        Console.WriteLine("\nTesting Eigenvalue Decomposition:");
        (vector w, matrix V) = jacobi.cyclic(A);

        matrix D = new matrix(n, n);
        for (int i = 0; i < n; i++)
            D[i, i] = w[i];

        matrix VT = V.transpose();
        matrix VTAV = VT * A * V;
        matrix VDVt = V * D * VT;
        matrix VTV = VT * V;
        matrix VVT = V * VT;

        Console.WriteLine("Check V^T A V == D:");
        VTAV.print();
        Console.WriteLine("Check V D V^T == A:");
        VDVt.print();
        Console.WriteLine("Check V^T V == I:");
        VTV.print();
        Console.WriteLine("Check V V^T == I:");
        VVT.print();
    }
}
