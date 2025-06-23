using System;
using System.IO;

class main {
    static bool isClose(vector a, vector b, double tol = 1e-3) {
        return (a - b).norm() < tol;
    }

    static int CountLines(string filename) {
        return File.ReadAllLines(filename).Length;
    }

    static void Main(string[] args) {
        // Rosenbrock
        var x0rosen = new vector(0.5, 0.5);
        var rosenPath = "rosenbrock.txt";
        Console.WriteLine("Minimizing Rosenbrock...");
        var rosenMin = newton.minimize(functions.rosenbrock, x0rosen, rosenPath);
        Console.WriteLine($"Minimum at: {rosenMin[0]} {rosenMin[1]}");

        vector rosenExpected = new vector(1, 1);
        bool rosenCheck = isClose(rosenMin, rosenExpected);
        Console.WriteLine($"Success: Minimum is {(rosenCheck ? "correct" : "incorrect")}.");

        int rosenSteps = CountLines(rosenPath);
        using (var f = File.AppendText(rosenPath)) {
            f.WriteLine();
            f.WriteLine($"Final minimum: {rosenMin[0]} {rosenMin[1]}");
            f.WriteLine($"Expected minimum: {rosenExpected[0]} {rosenExpected[1]}");
            f.WriteLine($"Check: {(rosenCheck ? "PASS" : "FAIL")}");
            f.WriteLine($"Steps taken: {rosenSteps}");
        }

        // Himmelblau
        var x0himmel = new vector(3.5, 2.5);
        var himmelPath = "himmelblau.txt";
        Console.WriteLine("\nMinimizing Himmelblau...");
        var himmelMin = newton.minimize(functions.himmelblau, x0himmel, himmelPath);
        Console.WriteLine($"Minimum at: {himmelMin[0]} {himmelMin[1]}");

        vector himmelExpected = new vector(3, 2);
        bool himmelCheck = isClose(himmelMin, himmelExpected);
        Console.WriteLine($"Success: Minimum is {(himmelCheck ? "correct" : "incorrect")}.");

        int himmelSteps = CountLines(himmelPath);
        using (var f = File.AppendText(himmelPath)) {
            f.WriteLine();
            f.WriteLine($"Final minimum: {himmelMin[0]} {himmelMin[1]}");
            f.WriteLine($"Expected minimum: {himmelExpected[0]} {himmelExpected[1]}");
            f.WriteLine($"Check: {(himmelCheck ? "PASS" : "FAIL")}");
            f.WriteLine($"Steps taken: {himmelSteps}");
        }
    }
}
