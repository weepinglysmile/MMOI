using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageProcessing;

namespace TestApp
{
  class Program
  {
    static void Main(string[] args)
    {
      Func<int, double> func = i => (i > 1 && i < 6) ? 1 : 0;
      var image = new CImage<double>(8, 8);

      //написать по крутому пока пишем по ламерски

      for (int i = 0; i < 8; i++)
      {
        for (int j = 0; j < 8; j++)
        {
          image[i, j] = func(i) * func(j);
        }
      }


      var res1 = FourierTransform.ForwardFFT2D(image.ToComplex());
      var res = FourierTransform.BackwardFFT2D(res1);

      for (int i = 0; i < 8; i++)
      {
        for (int j = 0; j < 8; j++)
        {
          Console.Write("{0} ", res[i, j]);
        }
        Console.WriteLine();
      }


      var c1 = new Complex(-123, 456);
      var c2 = c1.Sqrt;
      Console.WriteLine(c2*c2);
    }
  }
}
