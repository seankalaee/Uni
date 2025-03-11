using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        double rmax = 10.0;
        double dr = 0.3;

        if (args.Length == 2)
        {
            rmax = double.Parse(args[0]);
            dr = double.Parse(args[1]);
        }

        int npoints = (int)(rmax / dr) - 1;
        double[,] H = Hamiltonian.BuildHamiltonian(npoints, rmax, dr);
        double[] eigenvalues = new double[npoints];
        double[,] eigenvectors = new double[npoints, npoints];

        Jacobi.Cyclic(H, eigenvalues, eigenvectors);
        NormalizeEigenvectors(eigenvectors, dr);

        // Save wavefunction to file
        SaveWavefunction("wavefunction.txt", eigenvectors, npoints, dr);

        // Output results
        string output = "Eigenvalues:\n";
        for (int i = 0; i < 5; i++)  
            output += eigenvalues[i] + "\n";

        Console.WriteLine(output);
        File.WriteAllText("output.txt", output);
    }

    static void NormalizeEigenvectors(double[,] V, double dr)
    {
        int n = V.GetLength(0);

        for (int k = 0; k < n; k++)
        {
            double norm = 0;
            for (int i = 0; i < n; i++)
                norm += V[i, k] * V[i, k] * dr;

            norm = Math.Sqrt(norm);

            for (int i = 0; i < n; i++)
                V[i, k] /= norm;
        }
    }

    static void SaveWavefunction(string filename, double[,] V, int npoints, double dr)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            for (int i = 0; i < npoints; i++)
            {
                double r = dr * (i + 1);
                writer.WriteLine($"{r} {V[i, 0]}");
            }
        }
    }
}
