using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
  public class Complex
  {
    public Complex(double re, double im)
    {
      Re = re;
      Im = im;
    }

    public Complex()
      : this(0, 0)
    { }
    
    public double Re { get; set; }
    public double Im { get; set; }

    public double Abs
    {
      get
      {
        return Math.Sqrt(Re * Re + Im * Im);
      }
    }

    public Complex Sqrt
    {
      get
      {
        var sign = Im < 0 ? -1.0 : 1.0;
        double re = Math.Sqrt((Abs + Re) / 2);
        double im = sign * Math.Sqrt((Abs - Re) / 2);

        return new Complex(re, im);
      }
    }

    public static Complex operator*(Complex a, Complex b)
    {
        Complex c = new Complex();
        c.Re = a.Re * b.Re - a.Im * b.Im;
        c.Im = a.Im * b.Re + a.Re * b.Im;
        return c;
    }

    public override string ToString()
    {
      return string.Format("({0}; {1})", Re, Im);
    }
  }
}   
