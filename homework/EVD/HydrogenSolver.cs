using System;
using System.IO;
using System.Diagnostics;

class HydrogenSolver
{
    static void Main()
    {
        string outputFile = "Hydrogen.txt";
        using (StreamWriter file = new StreamWriter(outputFile))
        {
            // Write header for GNUplot
            file.WriteLine("# Δr     ε0");
            
            double rmax = 10.0;
            double[] dr_values = { 0.1, 0.2, 0.3, 0.5, 1.0 }; // Step size variations
            
            foreach (double dr in dr_values)
            {
                int npoints = (int)(rmax / dr) - 1;
                vector r = new vector(npoints);
                for (int i = 0; i < npoints; i++)
                    r[i] = dr * (i + 1);

                matrix H = Hamiltonian.BuildMatrix(npoints, r, dr);
                (vector eigenvalues, _) = jacobi.cyclic(H);

                file.WriteLine($"{dr:F3}   {eigenvalues[0]:F6}");  // Numerical format for GNUplot
            }
        }

        Console.WriteLine("Hydrogen results saved to Hydrogen.txt");

        // Generate GNUplot script and run it
        GenerateGnuplotScript();
        RunGnuplot();
    }

    static void GenerateGnuplotScript()
    {
        using (StreamWriter gp = new StreamWriter("plot_hydrogen.gp"))
        {
            gp.WriteLine("set terminal png");
            gp.WriteLine("set output 'convergence_dr.png'");
            gp.WriteLine("set xlabel 'Δr'");
            gp.WriteLine("set ylabel 'ε0'");
            gp.WriteLine("set title 'Ground state energy vs. Δr'");
            gp.WriteLine("set xrange [0:*]");  // Allow auto-scaling for x
            gp.WriteLine("set grid");
            gp.WriteLine("plot 'Hydrogen.txt' using 1:2 with linespoints title 'Energy'");
        }

        Console.WriteLine("GNUplot script generated.");
    }

    static void RunGnuplot()
    {
        Process gnuplot = new Process();
        gnuplot.StartInfo.FileName = "gnuplot";
        gnuplot.StartInfo.Arguments = "plot_hydrogen.gp";
        gnuplot.StartInfo.RedirectStandardOutput = true;
        gnuplot.StartInfo.UseShellExecute = false;
        gnuplot.StartInfo.CreateNoWindow = true;

        gnuplot.Start();
        gnuplot.WaitForExit();

        Console.WriteLine("GNUplot execution completed.");
    }
}
