using System;
using System.IO;

public static class newton {
    public static vector minimize(Func<vector, double> f, vector x0, string outputFile, double acc = 1e-3) {
        var x = x0.copy();
        var file = new StreamWriter(outputFile);
        int steps = 0;

        while (steps++ < 2000) {
            vector grad = gradient.compute(f, x);
            if (grad.norm() < acc) break;

            matrix H = hessian.compute(f, x);
            vector dx = new QRdecomposition(H).solve(-grad);

            double lambda = 1;
            while (lambda >= 1.0 / 1024) {
                vector xNew = x + dx * lambda;
                if (f(xNew) < f(x)) break;
                lambda /= 2;
            }

            x += dx * lambda;
            file.WriteLine(x[0] + " " + x[1] + " " + f(x));
        }

        file.Close();
        return x;
    }
}
