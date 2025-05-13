using System;
using static System.Math;

public static class Newton {
    public static vector newton(
        Func<vector, vector> f,
        vector start,
        double acc = 1e-2,
        vector dx = null,
        double lambda_min = 1.0 / 128
    ) {
        vector x = start.copy();
        vector fx = f(x), z = null, fz = null;

        do {
            if (fx.norm() < acc) break;

            matrix J = Jacobian.compute(f, x, fx, dx);
            var QR = new QR(J);
            vector Dx = QR.solve(-fx);

            double lambda = 1;
            do {
                z = x + lambda * Dx;
                fz = f(z);
                if (fz.norm() < (1 - lambda / 2) * fx.norm()) break;
                if (lambda < lambda_min) break;
                lambda /= 2;
            } while (true);

            x = z;
            fx = fz;
        } while (true);

        return x;
    }
}
