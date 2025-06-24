using System;
using System.IO;

class mainB {
    public static void Main() {
        // Define the activation function f(x) = x·exp(-x²)
        Func<double, double> f = delegate(double x) {
            return x * Math.Exp(-x * x);
        };

        // Define the target function g(x) = cos(5x - 1)·exp(-x²)
        Func<double, double> g = delegate(double x) {
            return Math.Cos(5 * x - 1) * Math.Exp(-x * x);
        };

        int n = 15; // Number of neurons
        ann ann_network = new ann(n, f);

        // Create training data
        int N = 100;
        double[] xs = new double[N];
        for (int i = 0; i < N; i++) {
            xs[i] = -1 + 2.0 * i / (N - 1);
        }

        // Train the network
        ann_network.train(xs, g);

        // Output ANN response and derivatives to file
        StreamWriter output = new StreamWriter("fitB.txt");
        for (double x = -1; x <= 1; x += 1.0 / 128) {
            double y = ann_network.response(x);
            double dy = ann_network.derivative(x);
            double ddy = ann_network.second_derivative(x);
            double integral = ann_network.integral(x);
            output.WriteLine($"{x} {y} {dy} {ddy} {integral}");
        }
        output.Close();

        // Generate plot
        System.Diagnostics.Process.Start("gnuplot", "plotB.gp");
    }
}
