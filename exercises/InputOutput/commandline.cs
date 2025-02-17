using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        foreach (var arg in args)
        {
            var words = arg.Split(':');
            if (words[0] == "-numbers")
            {
                var numbers = words[1].Split(',');
                foreach (var number in numbers)
                {
                    double x = double.Parse(number);
                    Console.WriteLine($"{x}\t{Math.Sin(x)}\t{Math.Cos(x)}");
                }
            }
        }
    }
}
