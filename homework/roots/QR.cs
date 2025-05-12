using System;

public class QR {
    public matrix Q, R;

    public QR(matrix A) {
        int m = A.size1, n = A.size2;
        Q = A.copy();
        R = new matrix(n, n);

        for (int i = 0; i < n; i++) {
            R[i, i] = Q[i].norm();
            Q[i] /= R[i, i];

            for (int j = i + 1; j < n; j++) {
                R[i, j] = Q[i] % Q[j];
                Q[j] -= Q[i] * R[i, j];
            }
        }
    }

    public vector solve(vector b) {
        vector y = Q % b;
        return backsub(R, y);
    }

    public static vector backsub(matrix R, vector y) {
        int n = y.size;
        vector x = new vector(n);
        for (int i = n - 1; i >= 0; i--) {
            double sum = 0;
            for (int k = i + 1; k < n; k++) sum += R[i, k] * x[k];
            x[i] = (y[i] - sum) / R[i, i];
        }
        return x;
    }

    public double det() {
        double product = 1.0;
        for (int i = 0; i < R.size1; i++) product *= R[i, i];
        return product;
    }

    public matrix inverse() {
        int n = Q.size1;
        matrix I = matrix.id(n);
        matrix B = new matrix(n, n);
        for (int i = 0; i < n; i++) {
            vector e = I[i];
            B[i] = backsub(R, Q % e);
        }
        return B;
    }
}
