using System;
using System.IO;
using static System.Math;

class Program {
    static void Main() {
        StreamWriter output = new StreamWriter("Results.txt");
        StreamWriter check = new StreamWriter("Checks.txt");

        output.WriteLine("Testing known integrals:\n");
        check.WriteLine("Integral Result | Expected | Error | Status");

        double acc = 1e-6;
        double eps = 0.0;

        Test(output, check, x => Sqrt(x), 0, 1, 2.0 / 3, "∫₀¹ sqrt(x) dx", acc, eps);
        Test(output, check, x => 1 / Sqrt(x), 0, 1, 2.0, "∫₀¹ 1/sqrt(x) dx", acc, eps);
        Test(output, check, x => Sqrt(1 - x * x), 0, 1, PI / 4, "∫₀¹ sqrt(1 − x²) dx", acc, eps);
        Test(output, check, x => Log(x) / Sqrt(x), 0, 1, -4.0, "∫₀¹ ln(x)/sqrt(x) dx", acc, eps);

        double erfExact = 0.84270079294971486934;
        double erfResult = Integrator.Erf(1, acc, eps);
        double erfError = Abs(erfExact - erfResult);

        output.WriteLine($"\nComputed erf(1) with acc={acc:G}: {erfResult}");
        output.WriteLine($"Expected:                       {erfExact}");
        check.WriteLine($"erf(1)".PadRight(30) + $"{erfResult,15:0.00000000} {erfExact,15:0.00000000} {erfError,10:0.00E+00} {(erfError < acc ? "PASSED" : "FAILED")}");

        // Clenshaw–Curtis test comparison
        output.WriteLine("\nClenshaw–Curtis Comparison (eval counts):");

        Func<double, double> f1 = x => 1 / Sqrt(x);
        int std1 = 0, cc1 = 0;
        double standard1 = Integrator.Integrate(f1, 0, 1, acc, eps, ref std1);
        double clenshaw1 = Integrator.ClenshawCurtisIntegrate(f1, 0, 1, acc, eps, ref cc1);

        output.WriteLine("∫₀¹ 1/√x dx");
        output.WriteLine($"Standard result:        {standard1}, evaluations: {std1}");
        output.WriteLine($"Clenshaw–Curtis result: {clenshaw1}, evaluations: {cc1}");

        Func<double, double> f2 = x => Log(x) / Sqrt(x);
        int std2 = 0, cc2 = 0;
        double standard2 = Integrator.Integrate(f2, 0, 1, acc, eps, ref std2);
        double clenshaw2 = Integrator.ClenshawCurtisIntegrate(f2, 0, 1, acc, eps, ref cc2);

        output.WriteLine("\n∫₀¹ ln(x)/√x dx");
        output.WriteLine($"Standard result:        {standard2}, evaluations: {std2}");
        output.WriteLine($"Clenshaw–Curtis result: {clenshaw2}, evaluations: {cc2}");

        output.Close();
        check.Close();

        // ==== Generate error_data.txt for plot ====
        using (StreamWriter errplot = new StreamWriter("error_data.txt")) {
            for (double a = 1e-1; a >= 1e-8; a /= 10) {
                double approx = Integrator.Erf(1, a, 0.0);
                double error = Abs(approx - erfExact);
                errplot.WriteLine($"{a} {error}");
            }
        }
    }

    static void Test(StreamWriter output, StreamWriter check, Func<double, double> f, double a, double b, double expected, string label, double acc, double eps) {
        int evals = 0;
        double result = Integrator.Integrate(f, a, b, acc, eps, ref evals);
        double error = Abs(result - expected);
        output.WriteLine($"{label}: {result} (expected {expected}), error = {error:G}, evals = {evals}");
        check.WriteLine($"{label.PadRight(30)} {result,15:0.00000000} {expected,15:0.00000000} {error,10:0.00E+00} {(error < acc ? "PASSED" : "FAILED")}");
    }
}
