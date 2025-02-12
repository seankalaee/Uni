using System;
using System.IO;

class Tiny
{
    static void Main()
    {
        double epsilon = Math.Pow(2, -52);
        double tiny = epsilon / 2;

        double a = 1 + tiny + tiny;
        double b = tiny + tiny + 1;

        string output = $"a == b ? {a == b}\n";
        output += $"a > 1 ? {a > 1}\n";
        output += $"b > 1 ? {b > 1}\n";

        Console.WriteLine(output);
        File.WriteAllText("tiny_output.txt", output);
    }
}
