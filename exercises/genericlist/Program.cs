using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var list = new GenList<double[]>();  // Create generic list
        char[] delimiters = { ' ', '\t' };

        string line;
        while ((line = Console.ReadLine()) != null)
        {
            var words = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            int n = words.Length;
            var numbers = new double[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = double.Parse(words[i]);  // Convert to double
            }

            list.Add(numbers);  // Add row to list
        }

        // Print in exponential format
        for (int i = 0; i < list.Count; i++)
        {
            var numbers = list[i];
            foreach (var number in numbers)
            {
                Console.Write($"{number:0.00e+00} ");
            }
            Console.WriteLine();
        }
    }
}
