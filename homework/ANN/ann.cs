using System;
using System.IO;

public class ann {
	public int n;
	public Func<double, double> f;
	public double[] a, b, w;

	public ann(int n, Func<double, double> f) {
		this.n = n;
		this.f = f;
		a = new double[n];
		b = new double[n];
		w = new double[n];

		Random rand = new Random(1);
		for (int i = 0; i < n; i++) {
			a[i] = -1 + 2.0 * i / (n - 1);
			b[i] = 0.3 + 0.2 * rand.NextDouble();
			w[i] = 0.5 * (2 * rand.NextDouble() - 1);
		}
	}

	public double response(double x) {
		double sum = 0;
		for (int i = 0; i < n; i++) {
			sum += f((x - a[i]) / b[i]) * w[i];
		}
		return sum;
	}

	public double derivative(double z) {
	  	double sum = 0;
    		for (int i = 0; i < n; i++) {
        		double arg = (z - a[i]) / b[i];
        		double df = (1 - 2 * arg * arg) * Math.Exp(-arg * arg);
        		sum += w[i] * df / b[i];
    		}
    		return sum;
	}


	public double integral(double z) {
		double sum = 0;
    		for (int i = 0; i < n; i++) {
        		double arg = (z - a[i]) / b[i];
        		double integral = -0.5 * Math.Exp(-arg * arg);
        		sum += w[i] * integral * b[i];
    		}
    		return sum;
	}
	
	public double second_derivative(double z) {
    		double sum = 0;
    		for (int i = 0; i < n; i++) {
        		double arg = (z - a[i]) / b[i];
        		double d2f = (-6 * arg + 4 * arg * arg * arg) * Math.Exp(-arg * arg);
        		sum += w[i] * d2f / (b[i] * b[i]);
    		}
    		return sum;
	}



	public double cost(double[] xs, Func<double, double> target) {
		double sum = 0;
		foreach (double x in xs) {
			double diff = response(x) - target(x);
			sum += diff * diff;
		}
		return sum / xs.Length;
	}

	public void train(double[] xs, Func<double, double> target) {
		vector p = new vector(3 * n);
		for (int i = 0; i < n; i++) {
			p[i] = a[i];
			p[n + i] = b[i];
			p[2 * n + i] = w[i];
		}

		Func<vector, double> C = delegate (vector v) {
			for (int i = 0; i < n; i++) {
				a[i] = v[i];
				b[i] = v[n + i];
				w[i] = v[2 * n + i];
			}
			return cost(xs, target);
		};

		Console.WriteLine("Training ANN...");
		qnewton(C, ref p, acc: 1e-4);

		for (int i = 0; i < n; i++) {
			a[i] = p[i];
			b[i] = p[n + i];
			w[i] = p[2 * n + i];
		}

		Console.WriteLine("Trained ANN parameters:");
		for (int i = 0; i < n; i++) {
			Console.WriteLine($"Neuron {i}: a = {a[i]:F3}, b = {b[i]:F3}, w = {w[i]:F3}");
		}
	}

	public static void qnewton(Func<vector, double> f, ref vector x, double acc = 1e-3) {
		int n = x.size;
		matrix B = matrix.id(n);
		vector grad = gradient(f, x);
		int maxSteps = 2000;

		for (int steps = 0; steps < maxSteps && grad.norm() > acc; steps++) {
			vector dx = -B * grad;
			double lambda = 1.0;
			while (f(x + lambda * dx) > f(x) + 1e-4 * lambda * grad.dot(dx)) {
				lambda /= 2;
				if (lambda < 1e-8) break;
			}
			vector s = lambda * dx;
			vector x_new = x + s;
			vector grad_new = gradient(f, x_new);
			vector y = grad_new - grad;
			vector u = s - B * y;
			double uy = u.dot(y);
			if (Math.Abs(uy) > 1e-12) B.update(u, y, 1.0 / uy);
			x = x_new;
			grad = grad_new;

			if (steps % 100 == 0)
				Console.WriteLine($"Step {steps}: cost = {f(x):F6}, grad.norm = {grad.norm():F6}");
		}

		if (grad.norm() > acc)
			Console.WriteLine("Warning: qnewton stopped early without convergence after 2000 steps.");
	}

	public static vector gradient(Func<vector, double> f, vector x, double dx = 1e-8) {
		vector grad = new vector(x.size);
		for (int i = 0; i < x.size; i++) {
			double xi = x[i];
			x[i] = xi + dx;
			double fx1 = f(x);
			x[i] = xi - dx;
			double fx2 = f(x);
			x[i] = xi;
			grad[i] = (fx1 - fx2) / (2 * dx);
		}
		return grad;
	}
}
