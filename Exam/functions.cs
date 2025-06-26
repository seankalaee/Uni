using System;
using static System.Math;

public abstract class function {
    public abstract double invoke(vector x);
}

public class himmelblau : function {
    public override double invoke(vector x) {
        double x0 = x[0], x1 = x[1];
        return Pow(x0 * x0 + x1 - 11, 2) + Pow(x0 + x1 * x1 - 7, 2);
    }
}

public class rosenbrock : function {
    public override double invoke(vector x) {
        double x0 = x[0], x1 = x[1];
        return Pow(1 - x0, 2) + 100 * Pow(x1 - x0 * x0, 2);
    }
}
