using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string dataFile = "gamma_data.txt";
        string gnuplotScript = "gamma_plot.gnuplot";
        string outputImage = "gamma_plot.png";

        double[] xValues = Enumerable.Range(1, 5).Select(i => (double)i).ToArray();
        double[] gammaValues = xValues.Select(gamma).ToArray();

        double[] tabulatedX = Enumerable.Range(1, 5).Select(i => (double)i).ToArray();
        double[] tabulatedGamma = tabulatedX.Select(n => Factorial(n - 1)).ToArray();

        using (StreamWriter writer = new StreamWriter(dataFile))
        {
            writer.WriteLine("# x   Gamma(x) (Computed)   Factorial(x-1) (Tabulated)");
            for (int i = 0; i < tabulatedX.Length; i++)
            {
                double computed = gammaValues[i];
                writer.WriteLine($"{tabulatedX[i]}   {computed}   {tabulatedGamma[i]}");
            }
        }

        using (StreamWriter script = new StreamWriter(gnuplotScript))
        {
            script.WriteLine("set terminal pngcairo enhanced size 800,600");
            script.WriteLine($"set output '{outputImage}'");
            script.WriteLine("set title 'Gamma Function Approximation vs Factorial'");
            script.WriteLine("set xlabel 'x'");
            script.WriteLine("set ylabel 'Gamma(x)'");
            script.WriteLine("set grid");
            script.WriteLine("plot \\");
            script.WriteLine($"    '{dataFile}' using 1:2 with lines lw 2 title 'Computed Gamma(x)', \\");
            script.WriteLine($"    '{dataFile}' using 1:3 with points pt 7 lc 'red' title 'Factorial(x-1)'");
        }

        Process.Start("gnuplot", gnuplotScript);

        Console.WriteLine($"Plot saved as {outputImage}");
    }

    static double gamma(double x)
    {
        if (x < 0) return Math.PI / (Math.Sin(Math.PI * x) * gamma(1 - x));
        if (x < 9) return gamma(x + 1) / x;

        double lnGamma = Math.Log(2 * Math.PI) / 2 + (x - 0.5) * Math.Log(x) - x
                        + (1.0 / 12) / x - (1.0 / 360) / (x * x * x) + (1.0 / 1260) / (x * x * x * x * x);
        return Math.Exp(lnGamma);
    }

    static double Factorial(double n)
    {
        if (n <= 1) return 1;
        return n * Factorial(n - 1);
    }
}
