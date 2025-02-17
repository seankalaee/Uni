using System;

class Program
{
    static void Main()
    {
        char[] splitDelimiters = { ' ', '\t', '\n' };
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            var numbers = line.Split(splitDelimiters, StringSplitOptions.RemoveEmptyEntries);
            foreach (var number in numbers)
            {
                double x = double.Parse(number);
                Console.Error.WriteLine($"{x}\t{Math.Sin(x)}\t{Math.Cos(x)}");
            }
        }
    }
}
