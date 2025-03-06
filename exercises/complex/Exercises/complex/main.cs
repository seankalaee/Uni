using System;
using static System.Console;
using static cmath; // Import complex math functions

class Program
{
    static void Main()
    {
        complex I = cmath.I;
        complex Pi = Math.PI;

        WriteLine("Complex Math Computations:");
        WriteLine($"√-1     = {sqrt(new complex(-1, 0))}");
        WriteLine($"√i      = {sqrt(I)}");
        WriteLine($"e^i     = {exp(I)}");
        WriteLine($"e^(iπ)  = {exp(I * Pi)}");
        WriteLine($"i^i     = {cmath.pow(I, I)}");
        WriteLine($"ln(i)   = {log(I)}");
        WriteLine($"sin(iπ) = {sin(I * Pi)}");

        // Extra: Hyperbolic functions
        WriteLine($"sinh(i) = {(exp(I) - exp(-I)) / 2}");
        WriteLine($"cosh(i) = {(exp(I) + exp(-I)) / 2}");
    }
}
