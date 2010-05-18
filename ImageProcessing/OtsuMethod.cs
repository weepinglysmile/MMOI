using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
  class OtsuMethod
  {
    public static double[] GetHistogram(CImage<double> img)
    {
      double[] hist = new double[256];
      for (int i = 0; i < img.GetH; i++)
      {
        for (int j = 0; j < img.GetW; j++)
        {
          hist[(int)img[i, j]]++;         
        }
      }
      for (int i = 0; i < 256; i++)
			{
			     hist[i]/=(img.GetW*img.GetH);
			}
      return hist;
    }

    static double W(int t0, int t1, double[] hist)
    {
      double W = 0;
      for (int i = t0; i <= t1; i++)
      {
        W+=hist[i];
      }
      return W;
    }

    static double M(int t0, int t1, double W, double[] hist)
    {
      double M = 0;
      for (int i = t0; i < t1; i++)
      {
        M += i * hist[i] / W;
      }
      return M;
    }

    public static int GetThreshold(CImage<double> img)
    {
      int threshold = 0;
      double[] hist = GetHistogram(img);
      double[] D = new double[256];
      for (int t = 0; t < 256; t++)
      {
        double w0 = W(0, t, hist);
        double w1 = W(t + 1, 255, hist);
        double m0 = M(0, t, w0, hist);
        double m1 = M(t + 1, 255, w1, hist);
        D[t] = w0 * w1 * (m0 - m1) * (m0 - m1);
      }

      double DMax = D[0];
      for (int i = 0; i < D.Length; i++)
      {
        if (D[i] > DMax)
        {
          DMax = D[i];
          threshold = i;
        }       
      }
      return threshold;
    }

  }
}
