/* (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty. */
using System;
using static System.Math;
public static partial class cmath{ /* complex math */

public static readonly complex I = new complex(0,1);

public static double   arg(complex z) => Math.Atan2(z.Im,z.Re);
public static complex  exp(complex z)
	=> Math.Exp(z.Re)*(Math.Cos(z.Im)+I*Math.Sin(z.Im));
public static complex  sin(complex z) => (exp(I*z)-exp(-I*z))/2/I;
public static complex  cos(complex z) => (exp(I*z)+exp(-I*z))/2;
public static complex  log(complex z) => Math.Log(abs(z))+I*arg(z);
public static complex sqrt(complex z) => Math.Sqrt(abs(z))*exp(I*arg(z)/2);
public static double   abs(complex z){
	double x=Math.Abs(z.Re),y=Math.Abs(z.Im);
	if(x==0 && y==0) return 0;
	else if(x>y){ double t=y/x; return x*sqrt(1+t*t); }
	else        { double t=x/y; return y*sqrt(t*t+1); }
	}
public static complex pow(this complex z, double x ) => exp(log(z)*x);
public static complex pow(this complex z, complex w) => exp(log(z)*w);

public static double exp(double x){return Math.Exp(x);}
public static double sin(double x){return Math.Sin(x);}
public static double cos(double x){return Math.Cos(x);}
public static double abs(double x){return Math.Abs(x);}
public static double log(double x){return Math.Log(x);}
public static double sqrt(double x){return Math.Sqrt(x);}
public static double pow(this double x, double y){return Math.Pow (x,y);}
public static double pow(this double x, int n   ){return Math.Pow (x,n);}

public static bool approx
(this double x, double y, double abserr=1e-9, double relerr=1e-9){
	return complex.approx(x,y,abserr,relerr);
}
public static bool approx
(this double x, complex y, double abserr=1e-9, double relerr=1e-9){
	return complex.approx(x,y,abserr,relerr);
}

// xtenstions
public static void print(this double x,string s){ Console.WriteLine(s+x); }
public static void print(this double x)         { x.print(""); }
public static void print(this complex z)
	{Console.WriteLine("({0},{1})",z.Re,z.Im);}
public static void print(this complex z, string s)
	{Console.Write(s);z.print();}
public static void printf(this complex z,string s)
	{Console.WriteLine(s,z.Re,z.Im);}

}// cmath
