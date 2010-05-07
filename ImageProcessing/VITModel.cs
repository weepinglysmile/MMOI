using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
  public class VITModel
    {
       double R { get; set; }
       double L1 { get; set; }
       double L2 { get; set; }
       double S1 { get; set; }
       double S2 { get; set; }
       int K { get; set; }
       double Theta { get; set; }
       double V { get; set; }
       int oSize { get; set; }
       int Size { get; set; }

       public CImage<double> _Hopt;
       public CImage<double> _H0;
       public CImage<double> _Hk;
       public CImage<double> _Hb;


      public VITModel(double r, double l1, double l2, double s1, double s2, int k, double theta,
          double v, int o_size, int size)
      {
          R = r;
          L1 = l1;
          L2 = l2;
          S1 = s1;
          S2 = s2;
          K = k;
          V = v;
          oSize = o_size;
          Size = size;
      }

      double Hopt(int i, int j)
      {
          double x = Math.PI * i / oSize;
          double y = Math.PI * j / oSize;
          return Math.Exp(-R * R / 2 * (x * x + y * y));
      }


      public void GenerateHopt()
      {
          _Hopt = new CImage<double>(Size,Size);
          for (int i = 0; i < Size; i++)
          {
              int ii = i - Size / 2;
              for (int j = 0; j < Size; j++)
              {
                  int jj = j - Size / 2;
                  _Hopt[i, j] = Hopt(ii, jj);
              }
          }
      }

      double H0(int i, int j)
      {
          double sin1 = 1;
          double sin2 = 1;
          if (i != 0)
          {
              double v1 = Math.PI * i * L1 / oSize / 2;
              sin1 = Math.Sin(v1) / v1;
          }
          if (j != 0)
          {
              double v2 = Math.PI * j * L2 / oSize / 2;
              sin2 = Math.Sin(v2) / v2;
          }
          return sin1 * sin2;
      }

      public void GenerateH0()
      {
          _H0 = new CImage<double>(Size, Size);
          for (int i = 0; i < Size; i++)
          {
              int ii = i - Size / 2;
              for (int j = 0; j < Size; j++)
              {
                  int jj = j - Size / 2;
                  _H0[i, j] = H0(ii, jj);
              }
          }
      }

      double Hk(int i, int j)
      {
          if (i == 0) return 1;
          double v1 = Math.PI * i * S1 / oSize / 2;
          return (Math.Sin(v1 * K) / (Math.Sin(v1) * K));
      }

      public void GenerateHk()
      {
          _Hk = new CImage<double>(Size, Size);
          for (int i = 0; i < Size; i++)
          {
              int ii = i - Size / 2;
              for (int j = 0; j < Size; j++)
              {
                  int jj = j - Size / 2;
                  _Hk[i, j] = Hk(ii, jj);
              }
          }
      }

     

    }
}
