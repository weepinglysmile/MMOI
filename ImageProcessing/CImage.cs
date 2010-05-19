using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace ImageProcessing
{
  public class CImage<T> where T : IComparable, new()
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

    public CImage<T> Copy()
    {
      CImage<T> newImg = new CImage<T>(GetH, GetW);
      for (int i = 0; i < GetH; i++)
      {
        for (int j = 0; j < GetW; j++)
        {
          newImg[i, j] = this[i, j];
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
          Array[i, j] = buf / 256;
        }

      }

      return Array;
    }

    public static CImage<T> MatrixToCImage(T[,] arr, int size)
    {
        CImage<T> Array = new CImage<T>(size, size);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Array[i, j] = arr[i, j];
            }
        }
        return Array;
    }

    public T GetMax()
    {
        T max = this[0, 0];
        bool f;
        for (int i = 0; i < GetH ; i++)
        {
            for (int j = 0; j < GetW; j++)
            {
                f = max.CompareTo(this[i,j]) <= 0;
                if (f)
                {
                    max = this[i, j];
                }
            }
            
        }
        return max;
    }

    public T GetMin()
    {
        T max = this[0, 0];
        bool f;
        for (int i = 0; i < GetH; i++)
        {
            for (int j = 0; j < GetW; j++)
            {
                f = max.CompareTo(this[i, j]) >= 0;
                if (f)
                {
                    max = this[i, j];
                }
            }

        }
        return max;
    }

    public static CImage<double> Norm(CImage<double> img, double maxValue) 
    {
        double max = img.GetMax();
        CImage<double> outImg = new CImage<double>(img.GetH, img.GetW);
        for (int i = 0; i < img.GetH; i++)
        {
            for (int j = 0; j < img.GetW; j++)
            {
                outImg[i, j] = (img[i, j] / max) * maxValue;
            }
        }
        return outImg;
    }
  }

}
