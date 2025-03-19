using System;
using System.IO;
using static System.Math;

class Program {
    static void Main() {
        double[] x = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        double[] y = new double[x.Length];

        for (int i = 0; i < x.Length; i++) y[i] = Cos(x[i]);

        using (StreamWriter output = new StreamWriter("output.txt"))
        using (StreamWriter cosData = new StreamWriter("cos_data.txt")) {
            output.WriteLine("# z, linterp(z), linterpInteg(z)");
            cosData.WriteLine("# x, cos(x)");

            for (double z = 0; z <= 9; z += 0.05) {
                double interp = LinearSpline.linterp(x, y, z);
                double integral = LinearSpline.linterpInteg(x, y, z);
                output.WriteLine($"{z} {interp} {integral}");
            }

            for (int i = 0; i < x.Length; i++) {
                cosData.WriteLine($"{x[i]} {y[i]}");
            }
        }

        // Generate improved GNUplot script
        using (StreamWriter plot = new StreamWriter("plot.gnu")) {
            plot.WriteLine("set terminal pngcairo enhanced");
            plot.WriteLine("set output 'plot.png'");
            plot.WriteLine("set title 'Linear Spline and its Integral'");
            plot.WriteLine("set xlabel 'x'");
            plot.WriteLine("set ylabel 'y'");
            plot.WriteLine("set grid");
            plot.WriteLine("set key outside");
            plot.WriteLine("set pointsize 1.8");  // Bigger points
            plot.WriteLine("set style line 1 lt 1 lw 2 lc rgb 'blue'");  // Linear spline style
            plot.WriteLine("set style line 2 lt 1 lw 2 lc rgb 'black'"); // Integral style
            plot.WriteLine("set style line 3 pt 7 ps 2 lc rgb 'red'");   // Cos data points
            plot.WriteLine("plot 'output.txt' using 1:2 with lines ls 1 title 'Linear Spline', \\");
            plot.WriteLine("     'output.txt' using 1:3 with lines ls 2 title 'Integral', \\");
            plot.WriteLine("     'cos_data.txt' using 1:2 with points ls 3 title 'Cos Data Points'");
        }

        Console.WriteLine("Computation finished. Run 'gnuplot plot.gnu' to generate the plot.");
    }
}
