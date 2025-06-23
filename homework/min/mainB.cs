using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

class Program {
    static void Main() {
        List<double> energy = new List<double>();
        List<double> signal = new List<double>();
        List<double> error = new List<double>();

        char[] separators = { ' ', '\t' };
        string line;

        while ((line = Console.ReadLine()) != null) {
            if (line.StartsWith("#")) continue;
            string[] words = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length < 3) continue;
            energy.Add(double.Parse(words[0]));
            signal.Add(double.Parse(words[1]));
            error.Add(double.Parse(words[2]));
        }

        Func<vector, double> deviation = delegate (vector v) {
            double m = v[0], gamma = v[1], A = v[2];
            double sum = 0;
            for (int i = 0; i < energy.Count; i++) {
                double E = energy[i];
                double F = A / ((E - m) * (E - m) + (gamma * gamma) / 4);
                double diff = (F - signal[i]) / error[i];
                sum += diff * diff;
            }
            return sum;
        };

        vector start = new vector(125.5, 2.5, 6.5); // reasonable guess for m, Γ, A
        string traceFile = "higgs_trace.txt";
        vector result = newton.minimize(deviation, start, traceFile);

        // Print fitted parameters
        WriteLine("Best fit parameters:");
        WriteLine("m     = {0:F4}", result[0]);
        WriteLine("Gamma = {0:F4}", result[1]);
        WriteLine("A     = {0:F4}", result[2]);

        // Write fit curve to file for plotting
        StreamWriter fit = new StreamWriter("higgs_fit.txt");
        for (double E = 100; E <= 160; E += 0.5) {
            double F = result[2] / ((E - result[0]) * (E - result[0]) + (result[1] * result[1]) / 4);
            fit.WriteLine("{0} {1}", E, F);
        }
        fit.Close();
    }
}
