using System;
using System.IO;

class MinMax
{
    static void Main()
    {
        int i = 1;
        while (i + 1 > i) i++;
        int maxInt = i;

        i = -1;
        while (i - 1 < i) i--;
        int minInt = i;

        string output = $"Max int: {maxInt}\nMin int: {minInt}\n";
        Console.WriteLine(output);
        File.WriteAllText("minmax_output.txt", output);
    }
}
