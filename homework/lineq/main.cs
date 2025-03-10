using System;
using static System.Console;
using static System.Math;

class Program {
    static void Main() {
        int n = 4;
        var rnd = new Random();

        // Generate random square matrix A
        matrix A = new matrix(n, n);
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                A[i, j] = rnd.NextDouble() * 10;

        // Compute QR decomposition
        QR qr = new QR(A);

        // Compute inverse B
        matrix B = qr.inverse();

        // Verify AB ≈ I
        matrix I = A * B;
        matrix Identity = matrix.id(n);

        // Output results
        WriteLine("Matrix A:");
        A.print();
        WriteLine("\nInverse B:");
        B.print();
        WriteLine("\nProduct AB (Should be Identity):");
        I.print();
        WriteLine("\nIdentity Matrix:");
        Identity.print();

        // Check if AB is approximately the identity matrix
        WriteLine("\nIs AB ≈ I? " + I.approx(Identity));
    }
}

