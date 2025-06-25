using System;
using static System.Math;

class main {
    static void Main(string[] args) {
        Console.WriteLine($"Sqrt(2) = {Sqrt(2)}");
        Console.WriteLine($"2^(1/5) = {Pow(2, 1.0 / 5)}");
        Console.WriteLine($"e^π = {Pow(E, PI)}");
        Console.WriteLine($"π^e = {Pow(PI, E)}");

        for (int i = 1; i <= 10; i++) {
            double gamma = sfuns.fgamma(i);
            double lngamma = sfuns.lngamma(i);
            Console.WriteLine($"Γ({i}) = {gamma}, lnΓ({i}) = {lngamma}");
        }
    }
}
