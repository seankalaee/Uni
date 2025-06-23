using System;
using System.IO;

class mainC {
    static void Main() {
        Func<vector, double> rosenbrock = v => Math.Pow(1 - v[0], 2) + 100 * Math.Pow(v[1] - v[0] * v[0], 2);
        Func<vector, double> himmelblau = v => Math.Pow(v[0]*v[0] + v[1] - 11, 2) + Math.Pow(v[0] + v[1]*v[1] - 7, 2);

        vector startRosen = new vector(-2, 8);
        vector startHimmel = new vector(3.5, 2);

        Console.WriteLine("Running central difference minimization...");

        vector minRosen = minimizeCentral(rosenbrock, startRosen, "central_rosen.txt");
        vector minHimmel = minimizeCentral(himmelblau, startHimmel, "central_himmel.txt");

        using(var outFile = new StreamWriter("centraldiff.txt")) {
            outFile.WriteLine("Rosenbrock:");
            outFile.WriteLine($"Final minimum: {minRosen[0]} {minRosen[1]}");
            outFile.WriteLine("Expected minimum: 1 1");
            outFile.WriteLine($"Check: {(Math.Abs(minRosen[0] - 1) < 1e-2 && Math.Abs(minRosen[1] - 1) < 1e-2 ? "PASS" : "FAIL")}");
            outFile.WriteLine();

            outFile.WriteLine("Himmelblau:");
            outFile.WriteLine($"Final minimum: {minHimmel[0]} {minHimmel[1]}");
            outFile.WriteLine("Expected minimum: 3 2");
            outFile.WriteLine($"Check: {(Math.Abs(minHimmel[0] - 3) < 1e-2 && Math.Abs(minHimmel[1] - 2) < 1e-2 ? "PASS" : "FAIL")}");
        }
    }

    static vector minimizeCentral(Func<vector, double> f, vector x0, string path, double acc = 1e-3) {
        var x = x0.copy();
        var file = new StreamWriter(path);
        int steps = 0;

        while (steps++ < 1000) {
            vector grad = gradient.central(f, x);
            if (grad.norm() < acc) break;

            matrix H = hessian.central(f, x);
            vector dx = new QRdecomposition(H).solve(-grad);

            double lambda = 1;
            while (lambda >= 1.0 / 1024) {
                vector xNew = x + dx * lambda;
                if (f(xNew) < f(x)) break;
                lambda /= 2;
            }

            x += dx * lambda;
            file.WriteLine($"{x[0]} {x[1]} {f(x)}");
        }

        file.Close();
        return x;
    }
}
