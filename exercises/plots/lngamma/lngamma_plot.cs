using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string dataFile = "lngamma_data.txt";
        string gnuplotScript = "lngamma_plot.gnuplot";
        string outputImage = "lngamma_plot.png";

        double[] xValues = Enumerable.Range(1, 10).Select(i => (double)i).ToArray();
        double[] lngammaValues = xValues.Select(lngamma).ToArray();

        double[] tabulatedX = Enumerable.Range(1, 10).Select(i => (double)i).ToArray();
        double[] tabulatedLnGamma = tabulatedX.Select(n => Math.Log(Factorial(n - 1))).ToArray();

        // Write computed and tabulated values to a file
        using (StreamWriter writer = new StreamWriter(dataFile))
        {
            writer.WriteLine("# x   ln(Gamma(x)) (Computed)   ln(Factorial(x-1)) (Tabulated)");
            for (int i = 0; i < tabulatedX.Length; i++)
            {
                double computed = lngammaValues[i];
                writer.WriteLine($"{tabulatedX[i]}   {computed}   {tabulatedLnGamma[i]}");
            }
        }

        // Write Gnuplot script
        using (StreamWriter script = new StreamWriter(gnuplotScript))
        {
            script.WriteLine("set terminal pngcairo enhanced size 800,600");
            script.WriteLine($"set output '{outputImage}'");
            script.WriteLine("set title 'Log Gamma Function Approximation vs ln(Factorial)'");
            script.WriteLine("set xlabel 'x'");
            script.WriteLine("set ylabel 'ln(Gamma(x))'");
            script.WriteLine("set grid");
            script.WriteLine("plot \\");
            script.WriteLine($"    '{dataFile}' using 1:2 with lines lw 2 title 'Computed ln(Gamma(x))', \\");
            script.WriteLine($"    '{dataFile}' using 1:3 with points pt 7 lc 'red' title 'ln(Factorial(x-1))'");
        }

        // Run Gnuplot
        Process.Start("gnuplot", gnuplotScript);

        Console.WriteLine($"Plot saved as {outputImage}");
    }

    static double lngamma(double x)
    {
        if (x <= 0) throw new ArgumentException("lngamma: x<=0");
        if (x < 9) return lngamma(x + 1) - Math.Log(x);
        return x * Math.Log(x + 1 / (12 * x - 1 / x / 10)) - x + Math.Log(2 * Math.PI / x) / 2;
    }

    static double Factorial(double n)
    {
        if (n <= 1) return 1;
        return n * Factorial(n - 1);
    }
}
