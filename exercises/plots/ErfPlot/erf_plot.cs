using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string dataFile = "erf_data.txt";
        string gnuplotScript = "erf_plot.gnuplot";
        string outputImage = "erf_plot.png";

        // Generate x values
        double[] xValues = Enumerable.Range(0, 36).Select(i => i * 0.1).ToArray();
        double[] erfValues = xValues.Select(erf).ToArray();

        // Tabulated values
        double[] tabulatedX = { 0, 0.02, 0.04, 0.06, 0.08, 0.1, 0.2, 0.3, 0.4, 0.5, 
                                0.6, 0.7, 0.8, 0.9, 1, 1.1, 1.2, 1.3, 1.4, 1.5, 
                                1.6, 1.7, 1.8, 1.9, 2, 2.1, 2.2, 2.3, 2.4, 2.5, 3, 3.5 };
        double[] tabulatedErf = { 0, 0.022564575, 0.045111106, 0.067621594, 0.090078126, 0.112462916,
                                  0.222702589, 0.328626759, 0.428392355, 0.520499878, 0.603856091, 0.677801194, 
                                  0.742100965, 0.796908212, 0.842700793, 0.880205070, 0.910313978, 0.934007945, 
                                  0.952285120, 0.966105146, 0.976348383, 0.983790459, 0.989090502, 0.992790429, 
                                  0.995322265, 0.997020533, 0.998137154, 0.998856823, 0.999311486, 0.999593048, 
                                  0.999977910, 0.999999257 };

        // Write computed and tabulated values to a file
        using (StreamWriter writer = new StreamWriter(dataFile))
        {
            writer.WriteLine("# x   erf(x) (Computed)   erf(x) (Tabulated)");
            for (int i = 0; i < tabulatedX.Length; i++)
            {
                double computed = i < xValues.Length ? erfValues[i] : double.NaN;
                writer.WriteLine($"{tabulatedX[i]}   {computed}   {tabulatedErf[i]}");
            }
        }

        // Write Gnuplot script
        using (StreamWriter script = new StreamWriter(gnuplotScript))
        {
            script.WriteLine("set terminal pngcairo enhanced size 800,600");
            script.WriteLine($"set output '{outputImage}'");
            script.WriteLine("set title 'Error Function Approximation vs Tabulated Values'");
            script.WriteLine("set xlabel 'x'");
            script.WriteLine("set ylabel 'erf(x)'");
            script.WriteLine("set grid");
            script.WriteLine("plot \\");
            script.WriteLine($"    '{dataFile}' using 1:2 with lines lw 2 title 'Approximation', \\");
            script.WriteLine($"    '{dataFile}' using 1:3 with points pt 7 lc 'red' title 'Tabulated Values'");
        }

        // Run Gnuplot
        Process.Start("gnuplot", gnuplotScript);

        Console.WriteLine($"Plot saved as {outputImage}");
    }

    static double erf(double x)
    {
        if (x < 0) return -erf(-x);
        double[] a = { 0.254829592, -0.284496736, 1.421413741, -1.453152027, 1.061405429 };
        double t = 1 / (1 + 0.3275911 * x);
        double sum = t * (a[0] + t * (a[1] + t * (a[2] + t * (a[3] + t * a[4]))));
        return 1 - sum * Math.Exp(-x * x);
    }
}
