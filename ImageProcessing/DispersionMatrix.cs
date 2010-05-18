using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
  public class DispersionMatrix
  {
    public static CImage<double> GetDMatrixImg(double[,] dMatrix, int Size)
    {
      CImage<double> dImg = new CImage<double>(Size, Size);
      CImage<double> img = CImage<double>.MatrixToCImage(dMatrix, Size);
      double maxValue = img.GetMax();
      dImg = CImage<double>.Norm(img, byte.MaxValue);
      int threshold = OtsuMethod.GetThreshold(dImg);
      double originalThrh = maxValue / byte.MaxValue * threshold;
      Thresholding(img, originalThrh);
      CImgInv(img);
      dImg = CImage<double>.Norm(img, byte.MaxValue);
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
    static void CImgInv(CImage<double> img)
    {
      double max = img.GetMax();
      for (int i = 0; i < img.GetW; i++)
      {
        for (int j = 0; j < img.GetH; j++)
        {
          img[i, j] = max - img[i, j];
        }
        
      }
    }

  }
}
