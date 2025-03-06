// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System;
using SM=System.Math;
using System.Globalization;

public partial struct complex{

// data
private double re,im;

// getters
public double Re => re; // {get{return re;}}
public double Im => im; // {get{return im;}}

// constructors
public complex(double x){ this.re = x; this.im = 0; }
public complex(double x, double y){ this.re=x; this.im=y; }

// casts
public static implicit operator complex(double x) => new complex(x);
public static implicit operator complex(int i) => new complex((double)i);

public static double real(complex z) => z.Re;
public static double imag(complex z) => z.Im;

// useful numbers
public static readonly complex Zero = new complex(0,0);
public static readonly complex One  = new complex(1,0);
public static readonly complex I    = new complex(0,1);
public static readonly complex NaN  = new complex(double.NaN,double.NaN);

// conjugation
public complex conjugate => new complex(this.Re,-this.Im);
public static complex operator~(complex a) => a.conjugate;

// operator+
public static complex operator+(complex a) => a;
public static complex operator+(complex a, double b){
	return new complex(a.Re+b,a.Im);}
public static complex operator+(double a, complex b){
	return new complex(a+b.Re,b.Im);}
public static complex operator+(complex a, complex b){
	return new complex(a.Re+b.Re,a.Im+b.Im);
	}

// operator-
public static complex operator-(complex a)
   { return new complex(-a.Re,-a.Im); }
public static complex operator-(complex a, double b)
   { return new complex(a.Re-b, a.Im); }
public static complex operator-(double a, complex b)
   { return new complex(a-b.Re, -b.Im); }
public static complex operator-(complex a, complex b)
   { return new complex(a.Re-b.Re, a.Im-b.Im); }

// operator*
public static complex operator*(complex a, double b)
   { return new complex(a.Re*b, a.Im*b); }
public static complex operator*(double a, complex b)
   { return new complex(a*b.Re, a*b.Im); }
public static complex operator*(complex a, complex b)
   { return new complex(a.Re*b.Re-a.Im*b.Im, a.Re*b.Im+a.Im*b.Re); }

// magnitude/argument
public static double argument(complex z){ return Math.Atan2(z.Im,z.Re);}
public static double arg(complex z){ return Math.Atan2(z.Im,z.Re);}
public static double magnitude(complex z){
	double zr=Math.Abs(z.Re),zi=Math.Abs(z.Im),r,t;
	if(zr>zi){t=zi/zr; r=zr*Math.Sqrt(1+t*t);}
	else     {t=zr/zi; r=zi*Math.Sqrt(1+t*t);}
	return r;
	}

// operator/
public static complex operator / (complex a, complex b){
	double ar=a.Re,ai=a.Im,br=b.Re,bi=b.Im;
	double s = 1.0/magnitude(b);
	double sbr = s*br, sbi=s*bi;
	double zr = (ar * sbr + ai * sbi) * s;
	double zi = (ai * sbr - ar * sbi) * s;
	return new complex(zr,zi);
	}
public static complex operator / (complex a, double x){
	return new complex(a.Re/x,a.Im/x);
	}

// ToString
static readonly string cformat="{0:g3}+{1:g3}i";
public override string ToString() {
return String.Format(
	CultureInfo.CurrentCulture,
	cformat, this.Re, this.Im
	);
        }
public string ToString(string format) {
return String.Format(
	CultureInfo.CurrentCulture,
	cformat,
	this.Re.ToString(format, CultureInfo.CurrentCulture),
	this.Im.ToString(format, CultureInfo.CurrentCulture)
	);
        }
public string ToString(IFormatProvider provider) {
return String.Format(
	provider,
	cformat, this.Re, this.Im
	);
        }
public string ToString(string format, IFormatProvider provider) {
return String.Format(
	provider,
	cformat,
	this.Re.ToString(format, provider),
	this.Im.ToString(format, provider)
	);
        }

// bool
public static bool approx
(double a, double b, double abserr=1e-9, double relerr=1e-9){
	double d=Math.Abs(a-b),s=Math.Abs(a)+Math.Abs(b);
	if( d<abserr ) return true;
	else if ( d/s < relerr/2 ) return true;
	else return false;
	}

public static bool approx
(double a, complex b, double abserr=1e-9, double relerr=1e-9){
	return approx(a,b.Re) && approx(0,b.Im);
	}

public bool approx
(complex b, double abserr=1e-9, double relerr=1e-9){
	return approx(this.Re,b.Re) && approx(this.Im,b.Im);
	}
public bool Equals(complex b){
	return this.Re.Equals(b.Re) && this.Im.Equals(b.Im);}
public override bool Equals(System.Object obj) {
      if (obj is complex)
      {
         complex b = (complex)obj;
         return this.Equals(b);
      }
      else { return false; }
   }
public override int GetHashCode(){
   throw new System.NotImplementedException(
         "complex.GetHashCode() is not implemented." );}

}// complex
