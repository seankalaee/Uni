using System;
using System.IO;
using System.Collections.Generic;

class RelativityProgram
{
    static void Main()
    {
        // Define test cases
        var cases = new[]
        {
            new { Epsilon = 0.0, Y0 = new double[] { 1.0, 0.0 },     Label = "circular" },
            new { Epsilon = 0.0, Y0 = new double[] { 1.0, -0.5 },    Label = "elliptical" },
            new { Epsilon = 0.01, Y0 = new double[] { 1.0, -0.5 },   Label = "precessing" }
        };

        foreach (var test in cases)
        {
            Console.WriteLine($"Running test: {test.Label} orbit (ε={test.Epsilon})");

            Func<double, double[], double[]> ode = Relativity.MakeOrbitODE(test.Epsilon);

            var result = Driver.driver12(ode, (0, 50 * Math.PI), test.Y0);

            List<double> phis = result.Item1;
            List<double[]> ylist = result.Item2;

            string filename = $"relativity_{test.Label}.txt";
            using (StreamWriter writer = new StreamWriter(filename))
            {
                for (int i = 0; i < phis.Count; i++)
                {
                    double phi = phis[i];
                    double u = ylist[i][0];
                    writer.WriteLine($"{phi} {u}");
                }
            }

            Console.WriteLine($"  → Output saved to: {filename}");
        }

        Console.WriteLine("All orbits complete.");
    }
}
