using System;
using System.IO;

class mainA {
	static void Main() {
		// Target function
		Func<double, double> g = delegate (double x) {
			return Math.Cos(5 * x - 1) * Math.Exp(-x * x);
		};

		// Evaluation points
		int N = 200;
		double[] xs = new double[N];
		double dx = 2.0 / (N - 1);
		for (int i = 0; i < N; i++) {
			xs[i] = -1 + i * dx;
		}

		// Create and train ANN
		int neurons = 15;
		ann net = new ann(neurons, x => x * Math.Exp(-x * x));  // Gaussian wavelet
		net.train(xs, g);

		// Write output
		using (StreamWriter output = new StreamWriter("fitA.txt")) {
			for (int i = 0; i < N; i++) {
				double x = xs[i];
				double y_true = g(x);
				double y_pred = net.response(x);
				output.WriteLine($"{x} {y_true} {y_pred}");
			}
		}

		Console.WriteLine("Generating plot...");
		System.Diagnostics.Process.Start("gnuplot", "plotA.gp");
	}
}
