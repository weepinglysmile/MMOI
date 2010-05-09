using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fftwlib;
using System.Runtime.InteropServices;


namespace ImageProcessing
{
  public class Background
  {

    public static CImage<double> GetBackground(int height, int width, double alfa, double Df, double Mf)
    {

      CImage<Complex> BackgroundImg = Background.GenareteACF(height, width, alfa, Df).ToComplex();
      var fBgImg = FourierTransform.ForwardFFT2D(BackgroundImg);
      CImage<Complex> NoiseImg = Background.GenarateNoise(height, width).ToComplex();
      var fNImg = FourierTransform.ForwardFFT2D(NoiseImg);
      CImage<Complex> img = new CImage<Complex>(height, width);
      CImage<Complex> sqrt = new CImage<Complex>(height, width);


      for (int i = 0; i < height; i++)
      {
        for (int j = 0; j < width; j++)
        {
          sqrt[i, j] = fBgImg[i, j].Sqrt;
          //img[i, j] = new Complex(Math.Sqrt(Math.Abs(fBgImg[i, j].Re)), 0) * fNImg[i, j];
          img[i, j] = sqrt[i, j] * fNImg[i, j];
        }
      }
      var res = FourierTransform.BackwardFFT2D(img).ToDouble();

      for (int i = 0; i < height; i++)
      {
        for (int j = 0; j < width; j++)
        {
          res[i, j] = res[i, j] + Mf;
        }
      }

      return res;
    }

    public static CImage<double> GenarateNoise(int height, int width)
    {
      Random rnd = new Random(1);

      CImage<double> nImg = new CImage<double>(height, width);
      for (int i = 0; i < height; i++)
      {
        for (int j = 0; j < width; j++)
        {
          nImg[i, j] = (rnd.NextDouble() - 0.5) / Math.Sqrt(1.0 / 12.0);
        }
      }
      return nImg;
    }

    public static CImage<double> GenareteACF(int height, int width, double alfa, double Df)
    {
      CImage<double> ACFimg = new CImage<double>(height, width);
      for (int i = 0; i < height; i++)
      {
        int ii = i - height / 2;
        for (int j = 0; j < width; j++)
        {
          int jj = j - width / 2;
          ACFimg[(i + height / 2) % height, (j + width / 2) % width] = Df * Math.Exp(-alfa * Math.Sqrt(ii * ii + jj * jj));
        }
      }
      return ACFimg;
    }

  }
}
