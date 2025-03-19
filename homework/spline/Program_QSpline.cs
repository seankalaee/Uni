using System;
using System.IO;

class Program_QSpline {
    static void Main() {
        double[] x = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        double[] y = new double[x.Length];

        for (int i = 0; i < x.Length; i++) y[i] = Math.Sin(x[i]);

        QSpline spline = new QSpline(x, y);

        using (StreamWriter output = new StreamWriter("qspline_results.txt"))
        using (StreamWriter testData = new StreamWriter("test_data.txt")) {
            output.WriteLine("# z, qspline(z), derivative(z), integral(z)");
            testData.WriteLine("# x, y(x)");

            for (double z = 1; z <= 9; z += 0.05) {
                double interp = spline.Evaluate(z);
                double derivative = spline.Derivative(z);
                double integral = spline.Integral(z);
                output.WriteLine($"{z} {interp} {derivative} {integral}");
            }

            for (int i = 0; i < x.Length; i++) {
                testData.WriteLine($"{x[i]} {y[i]}");
            }
        }

        Console.WriteLine("Quadratic spline computation finished.");
    }
}
