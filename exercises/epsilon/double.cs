using System;
using System.IO;
using static System.Math;

class DoubleComparison
{
    static bool Approx(double a, double b, double acc = 1e-9, double eps = 1e-9)
    {
        if (Abs(a - b) <= acc) return true;
        if (Abs(a - b) <= Max(Abs(a), Abs(b)) * eps) return true;
        return false;
    }

    static void Main()
    {
        double d1 = 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1;
        double d2 = 8 * 0.1;

        string output = $"d1 = {d1:e15}\n";
        output += $"d2 = {d2:e15}\n";
        output += $"d1 == d2 ? {d1 == d2}\n";
        output += $"Approx(d1, d2) ? {Approx(d1, d2)}\n";

        Console.WriteLine(output);
        File.WriteAllText("double_output.txt", output);
    }
}
