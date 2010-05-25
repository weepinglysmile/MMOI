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
    double V1 { get; set; }
    double V2 { get; set; }
    int oSize { get; set; }
    int Size { get; set; }

    public CImage<double> _Hopt;
    public CImage<double> _H0;
    public CImage<double> _Hk;
    public CImage<double> _Hb;
    public CImage<double> VIT;


    public VITModel(double r, double l1, double l2, double s1, double s2, int k, double theta,
        double v1, double v2, int o_size, int size)
    {
      R = r;
      L1 = l1;
      L2 = l2;
      S1 = s1;
      S2 = s2;
      K = k;
      V1 = v1;
      V2 = v2;
      oSize = o_size;
      Size = size;
    }

    double Hopt(int i, int j)
    {
      double x = Math.PI * i / oSize;
      double y = Math.PI * j / oSize;
      var tmp = Math.Exp(-R * R / 2.0 * (x * x + y * y));
      return tmp;
    }


    public void GenerateHopt(bool added)
    {
      _Hopt = new CImage<double>(Size, Size);
      for (int i = 0; i < Size; i++)
      {
        int ii = i - Size / 2;
        for (int j = 0; j < Size; j++)
        {
          int jj = j - Size / 2;
          if (added)
            _Hopt[(i + Size / 2) % Size, (j + Size / 2) % Size] = Hopt(ii, jj);
          else 
            _Hopt[(i + Size / 2) % Size, (j + Size / 2) % Size] = 1.0;
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

    public void GenerateH0(bool added)
    {
      _H0 = new CImage<double>(Size, Size);
      for (int i = 0; i < Size; i++)
      {
        int ii = i - Size / 2;
        for (int j = 0; j < Size; j++)
        {
          int jj = j - Size / 2;
          if (added)
            _H0[(i + Size / 2) % Size, (j + Size / 2) % Size] = H0(ii, jj);
          else _H0[(i + Size / 2) % Size, (j + Size / 2) % Size] = 1;
        }
      }
    }

    double Hk(int i, int j)
    {
      if (i == 0) return 1;
      double v1 = Math.PI * i * S1 / oSize / 2;
      return (Math.Sin(v1 * K) / (Math.Sin(v1) * K));
    }

    public void GenerateHk(bool added)
    {
      _Hk = new CImage<double>(Size, Size);
      for (int i = 0; i < Size; i++)
      {
        int ii = i - Size / 2;
        for (int j = 0; j < Size; j++)
        {
          int jj = j - Size / 2;
          if (added)
            _Hk[(i + Size / 2) % Size, (j + Size / 2) % Size] = Hk(ii, jj);
          else _Hk[(i + Size / 2) % Size, (j + Size / 2) % Size] = 1;
        }
      }
    }

    double Hb(int i, int j)
    {
      if (Theta == 0) return 1;
      double a = Math.PI * i / oSize / 2 * V1 * Theta;
      double b = Math.PI * j / oSize / 2 * V2 * Theta;
      return (Math.Sin(a + b) / (a + b));
    }

    public void GenerateHb(bool added)
    {
      _Hb = new CImage<double>(Size, Size);
      for (int i = 0; i < Size; i++)
      {
        int ii = i - Size / 2;
        for (int j = 0; j < Size; j++)
        {
          int jj = j - Size / 2;
          _Hb[i, j] = Hb(ii, jj);
        }
      }
    }

    public void GenerateVIT()
    {
      VIT = new CImage<double>(Size, Size);
      for (int i = 0; i < Size; i++)
      {
        for (int j = 0; j < Size; j++)
        {
          VIT[i, j] = _Hopt[i, j] * _H0[i, j] *_Hk[i, j];
        }
      }

    }

    double GenerateNormNoise(Random rnd, double sigma, double X0)
    {
      int n = 6;
      double N = 0, S = 0;
      for (int i = 0; i < n; i++)
      {
        S += rnd.NextDouble();
      }
      N = (S - n / 2) * Math.Sqrt(12) / Math.Sqrt(n);
      N *= (sigma / Math.Sqrt(X0));
      return N;
    }

    public CImage<double> AppVIT(CImage<double> img, bool addHopt, bool addH0, bool addHk, bool addNoise, double sigma, double X0)
    {
      int height = img.GetH;
      int width = img.GetW;
      //CImage<double> imgOut = new CImage<double>(height, width);
      CImage<double> imgOut = new CImage<double>(oSize, oSize);  
  
        GenerateH0(addH0);
        GenerateHk(addHk);
        GenerateHopt(addHopt);
        GenerateVIT();

        CImage<Complex> fImg = FourierTransform.ForwardFFT2D(img.ToComplex());
        CImage<Complex> fVIT = VIT.ToComplex();
        for (int i = 0; i < Size; i++)
        {
          for (int j = 0; j < Size; j++)
          {
            fImg[i, j] = fImg[i, j] * fVIT[i, j];
          }
        }
        img = FourierTransform.BackwardFFT2D(fImg).ToDouble();

        int k = Size / oSize;
        for (int i = 0; i < oSize; i++)
        {
            for (int j = 0; j < oSize; j++)
            {
                imgOut[i, j] = img[i * k, j * k];
            }
        }

        if (addNoise)
        {
          Random rnd = new Random(7);
          for (int i = 0; i < oSize; i++)
          {
              for (int j = 0; j < oSize; j++)
            {
                double buf = imgOut[i, j] + GenerateNormNoise(rnd, sigma, X0) * Math.Sqrt(imgOut[i, j]);
                if (buf > 255)
                    imgOut[i, j] = 255;
                else
                    if (buf < 0)
                        imgOut[i, j] = 0;
                    else imgOut[i, j] = buf;
               
                //imgOut[i, j] = buf < 255 ? buf : 255;
                //imgOut[i, j] = buf > 0 ? buf : 0;
            }
          }
        }
        //for (int i = 0; i < Size; i++)
        //{
        //  for (int j = 0; j < Size; j++)
        //  {
        //    imgOut[i, j] = imgOut[i, j] + 128;
        //  }
        //}



      return imgOut;
    }

   

  }
}
