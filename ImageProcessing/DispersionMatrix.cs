using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
  public class DispersionMatrix
  {
    public static CImage<double> GetDMatrixImg(double[,] dMatrix, out CImage<double> img, int Size, CImage<double> InImg, double sigma, double X0)
    {
      CImage<double> dImg = new CImage<double>(Size, Size);
      img = CImage<double>.MatrixToCImage(dMatrix, Size);
      SubtractNoiseDisp(img, sigma, X0);
      double maxValue = img.GetMax();
      dImg = CImage<double>.Norm(img, byte.MaxValue);
      int threshold = OtsuMethod.GetThreshold(dImg);
      double originalThrh = maxValue / byte.MaxValue * threshold;
      Thresholding(img, originalThrh);
      
      dImg = CImage<double>.Norm(CImgInv(img), byte.MaxValue);
      return dImg;
    }

    static void Thresholding(CImage<double> img, double threshold)
    {
      for (int i = 0; i < img.GetH; i++)
      {
        for (int j = 0; j < img.GetW; j++)
        {
          if (img[i, j] > threshold)
            img[i, j] = threshold;
        }
      }
    }
    static CImage<double> CImgInv(CImage<double> img)
    {
        CImage<double> OutImg = new CImage<double>(img.GetH, img.GetW);
      double max = img.GetMax();
      for (int i = 0; i < img.GetW; i++)
      {
        for (int j = 0; j < img.GetH; j++)
        {
          OutImg[i, j] = max - img[i, j];
        }  
      }
      return OutImg;
    }

    static double GetNoiseDispersion(int i, int j, CImage<double> img, double sigma, double X0)
    {
        double D = 0;
        int h = img.GetH;
        int w = img.GetW;
        double M = (img[(i + 1 + w) % w, j]
            + img[(i - 1 + w) % w, j]
            + img[i, (j + 1 + h) % h]
            + img[i, (j - 1 + h) % h]
            + img[i, j]) / 5;
        D = M * sigma * sigma / X0;
        return D;
    }

    static void SubtractNoiseDisp(CImage<double> img, double sigma, double X0)
    {
        CImage<double> noiseDisp = new CImage<double>(img.GetW, img.GetH);
        for (int i = 0; i < img.GetW; i++)
        {
            for (int j = 0; j < img.GetH; j++)
            {
                img[i,j] -= GetNoiseDispersion(i, j, img, sigma, X0);
            }
        } 
    }

  }
}
