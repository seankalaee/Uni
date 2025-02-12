using System;
using System.IO;
using static System.Console;
using static System.Math;

static class main
{
    public static void Print(this double x, string s = "")
    {
        Write(s);
        WriteLine(x);
    }

    static int Main()
    {
        using (StreamWriter writer = new StreamWriter("task_output_tests.txt"))
        {
            var rnd = new Random();
            var u = new vec(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble());
            var v = new vec(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble());

            writer.WriteLine($"u={u}");
            writer.WriteLine($"v={v}");
            writer.WriteLine();

            vec t;

            t = new vec(-u.x, -u.y, -u.z);
            writer.WriteLine($"-u = {(-u)}");
            writer.WriteLine($"t  = {t}");
            if (vec.Approx(t, -u))
                writer.WriteLine("test 'unary -' passed\n");

            t = new vec(u.x - v.x, u.y - v.y, u.z - v.z);
            writer.WriteLine($"u-v = {u - v}");
            writer.WriteLine($"t   = {t}");
            if (vec.Approx(t, u - v))
                writer.WriteLine("test 'operator-' passed\n");

            t = new vec(u.x + v.x, u.y + v.y, u.z + v.z);
            writer.WriteLine($"u+v = {u + v}");
            writer.WriteLine($"t   = {t}");
            if (vec.Approx(t, u + v))
                writer.WriteLine("test 'operator+' passed\n");

            double c = rnd.NextDouble();
            t = new vec(u.x * c, u.y * c, u.z * c);
            vec tmp = u * c;
            writer.WriteLine($"u*c = {tmp}");
            writer.WriteLine($"t   = {t}");
            if (vec.Approx(t, u * c))
                writer.WriteLine("test 'operator*' passed\n");

            double d = u.x * v.x + u.y * v.y + u.z * v.z;
            double dotProduct = vec.Dot(u, v);
            writer.WriteLine($"u⋅v = {dotProduct}");
            writer.WriteLine($"d   = {d}");
            if (vec.Approx(d, dotProduct))
                writer.WriteLine("test 'dot product' passed\n");
        }

        return 0;
    }
}
