using System;

public class vec
{
    public double x, y, z;
    public vec() { x = y = z = 0; }
    public vec(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public static vec operator *(vec v, double c) => new vec(v.x * c, v.y * c, v.z * c);
    public static vec operator *(double c, vec v) => v * c;
    public static vec operator /(vec v, double c) => new vec(v.x / c, v.y / c, v.z / c);
    public static vec operator +(vec u, vec v) => new vec(u.x + v.x, u.y + v.y, u.z + v.z);
    public static vec operator -(vec u, vec v) => new vec(u.x - v.x, u.y - v.y, u.z - v.z);
    public static vec operator -(vec u) => new vec(-u.x, -u.y, -u.z);

    public void Print(string s = "") => Console.WriteLine($"{s}{x} {y} {z}");

    public double Dot(vec other) => this.x * other.x + this.y * other.y + this.z * other.z;
    public static double Dot(vec u, vec v) => u.Dot(v);

    public static bool Approx(double a, double b, double acc = 1e-9, double eps = 1e-9)
    {
        if (Math.Abs(a - b) < acc) return true;
        if (Math.Abs(a - b) < (Math.Abs(a) + Math.Abs(b)) * eps) return true;
        return false;
    }
    public bool Approx(vec other) => Approx(x, other.x) && Approx(y, other.y) && Approx(z, other.z);
    public static bool Approx(vec u, vec v) => u.Approx(v);

    public override string ToString() => $"{x} {y} {z}";
}
