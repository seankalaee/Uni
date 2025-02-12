using System;
using System.IO;

class MachineEpsilon
{
    static void Main()
    {
        double x = 1.0;
        while ((1.0 + x) != 1.0) x /= 2;
        x *= 2; // Last valid step
        double epsilonDouble = x;

        float y = 1F;
        while ((float)(1F + y) != 1F) y /= 2;
        y *= 2; // Last valid step
        float epsilonFloat = y;

        string output = $"Double Epsilon: {epsilonDouble}\nFloat Epsilon: {epsilonFloat}\n";
        Console.WriteLine(output);
        File.WriteAllText("machineepsilon_output.txt", output);
    }
}
