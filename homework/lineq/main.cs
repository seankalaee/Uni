using System;
using static System.Console;
using static System.Math;

class Program {
    static void Main() {
        int n = 4;
        int m = 6; // A tall matrix for QR decomposition test
        var rnd = new Random();

        // Generate a random tall matrix A (m > n)
        matrix A_tall = new matrix(m, n);
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                A_tall[i, j] = rnd.NextDouble() * 10;

        // Perform QR decomposition on the tall matrix
        QR qr_tall = new QR(A_tall);

        // Check decomposition properties
        WriteLine("Checking QR decomposition:");
        WriteLine($"Is R upper triangular? {qr_tall.isUpperTriangular()}");
        WriteLine($"Is Q orthogonal? {qr_tall.isOrthogonal()}");
        WriteLine($"Does QR reconstruct A? {qr_tall.isQRDecompositionCorrect(A_tall)}");

        // Generate a random square matrix A
        matrix A_square = new matrix(n, n);
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                A_square[i, j] = rnd.NextDouble() * 10;

        // Compute QR decomposition
        QR qr_square = new QR(A_square);

        // Generate a random vector b
        vector b = new vector(n);
        for (int i = 0; i < n; i++)
            b[i] = rnd.NextDouble() * 10;

        // Solve QRx = b
        vector x = qr_square.solve(b);
        vector Ax = A_square * x;

        // Output results for Ax ≈ b
        WriteLine("\nChecking QR solve:");
        WriteLine($"Does Ax ≈ b? {Ax.approx(b)}");

        // Compute inverse B
        matrix B = qr_square.inverse();

        // Verify AB ≈ I
        matrix I = A_square * B;
        matrix Identity = matrix.id(n);

        // Output results
        WriteLine("\nMatrix A:");
        A_square.print();
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
