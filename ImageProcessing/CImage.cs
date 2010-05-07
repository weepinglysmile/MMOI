using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageProcessing
{
  public class CImage<T> where T : new()
  {
    private T[,] a;
    private int height, width;
    public CImage(int h, int w)
    {
      a = new T[w, h];
      height = h;
      width = w;

      for (int i = 0; i < height; i++)
      {
        for (int j = 0; j < width; j++)
        {
          a[i, j] = new T();
        }
      }

    }

    public int GetH
    {
      get
      {
        return height;
      }
    }
    public int GetW
    {
      get
      {
        return width;
      }
    }
    public T this[int i, int j]
    {
      get
      {
        return a[i, j];
      }
      set
      {
        a[i, j] = value;
      }
    }

    public CImage<T> Copy(CImage<T> img)
    {
      CImage<T> newImg = new CImage<T>(img.GetH, img.GetW);
      for (int i = 0; i < img.GetH; i++)
      {
        for (int j = 0; j < img.GetW; j++)
        {
          newImg[i, j] = img[i, j];
        }
      }
      return newImg;
    }


    public static CImage<double> GetComponents(Bitmap bmp)
    {
      CImage<double> Array = new CImage<double>(bmp.Width, bmp.Height);
      int H = bmp.Height;
      int W = bmp.Width;
      for (int i = 0; i < W; i++)
      {
        for (int j = 0; j < H; j++)
        {
          Color cl = bmp.GetPixel(i, j);
          double buf = (double)cl.B;
          Array[j, i] = buf / 256;
        }

      }

      return Array;
    }



  }

}
