using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageProcessing
{
  public static class ImageExtensions
  {
    public static CImage<Complex> ToComplex(this CImage<double> img)
    {
      CImage<Complex> cImg = new CImage<Complex>(img.GetH, img.GetW);
      for (int i = 0; i < img.GetH; i++)
      {
        for (int j = 0; j < img.GetW; j++)
        {
          cImg[i, j].Re = img[i, j];
          cImg[i, j].Im = 0;
        }
      }

      return cImg;
    }

    public static Bitmap CImageToBitmap(this CImage<double> img)
    {
      int n = img.GetW;
      int m = img.GetH;
      var bmp = new Bitmap(n, m);
      for (int i = 0; i < n; i++)
      {
        for (int j = 0; j < m; j++)
        {
          var val = img[j, i];
          int c = (int)(val);
          bmp.SetPixel(i, j, Color.FromArgb(c, c, c));
        }
      }
      return bmp;
    }


    public static CImage<double> ToDouble(this CImage<Complex> img)
    {
      var cImg = new CImage<double>(img.GetH, img.GetW);
      for (int i = 0; i < img.GetH; i++)
      {
        for (int j = 0; j < img.GetW; j++)
        {
          //cImg[i, j] = img[i, j].Abs;
          cImg[i, j] = img[i, j].Re;
        }
      }

      return cImg;
    }
  }
}
