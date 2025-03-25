using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        double b = 0.25, c = 5.0;

        // Create the function f(t, y) representing the pendulum system
        Func<double, double[], double[]> f = Pendulum.MakePendulum(b, c);

        // Initial condition: theta(0) = pi - 0.1, omega(0) = 0
        double[] y0 = new double[] { Math.PI - 0.1, 0.0 };

        // Call the adaptive RK driver
        Tuple<List<double>, List<double[]>> result = Driver.driver12(f, (0, 10), y0);

        List<double> xlist = result.Item1;
        List<double[]> ylist = result.Item2;

        // Write the output to a file for plotting
        using (StreamWriter writer = new StreamWriter("data.txt"))
        {
            for (int i = 0; i < xlist.Count; i++)
            {
                double t = xlist[i];
                double theta = ylist[i][0];
                double omega = ylist[i][1];
                writer.WriteLine($"{t} {theta} {omega}");
            }
        }

        Console.WriteLine("Done. Output written to data.txt");
    }
}
