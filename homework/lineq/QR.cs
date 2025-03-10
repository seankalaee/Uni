using System;
using static System.Console;
using static System.Math;

public class QR {
    public matrix Q, R;

    // Constructor: Performs QR decomposition using Modified Gram-Schmidt
    public QR(matrix A) {
        int m = A.size1, n = A.size2;
        Q = A.copy();
        R = new matrix(n, n);

        for (int i = 0; i < n; i++) {
            R[i, i] = Q[i].norm();  // Compute norm of the column
            Q[i] /= R[i, i];  // Normalize column

            for (int j = i + 1; j < n; j++) {
                R[i, j] = Q[i] % Q[j];  // Compute dot product
                Q[j] -= Q[i] * R[i, j];  // Orthogonalize
            }
        }
    }

    // Solves Ax = b using QR decomposition (Qx = b => Rx = Q^T b)
    public vector solve(vector b) {
        vector y = Q % b;  // Compute Q^T * b
        return backsub(R, y);  // Solve Rx = y
    }

    // Computes determinant of matrix A (using product of diagonal elements of R)
    public double det() {
        double product = 1.0;
        for (int i = 0; i < R.size1; i++) product *= R[i, i];
        return product;
    }

    // Part B, Computes matrix inverse using QR decomposition
    public matrix inverse() {
        int n = Q.size1;
        matrix I = matrix.id(n);  // Identity matrix
        matrix B = new matrix(n, n);

        for (int i = 0; i < n; i++) {
            vector e = I[i];  // Extract i-th column of identity
            vector x_sol = backsub(R, Q % e);  // Solve Rx = Q^T e
            B[i] = x_sol;  // Store in inverse matrix
        }
        return B;
    }

    // Back-substitution solver for upper triangular matrix R
    public static vector backsub(matrix U, vector c) {
        int n = c.size;
        vector x = new vector(n);
        for (int i = n - 1; i >= 0; i--) {
            double sum = 0;
            for (int k = i + 1; k < n; k++) sum += U[i, k] * x[k];
            x[i] = (c[i] - sum) / U[i, i];
        }
        return x;
    }
}
