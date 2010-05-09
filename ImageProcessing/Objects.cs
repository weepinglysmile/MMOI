using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
 public class Objects
  {

    static int GetNumber(int imgHeight, int imgWidth, double Q, double r)
    {
      return (int)(imgHeight * imgWidth * Q/(Math.PI*r*r));
    }

    public static CImage<double> GetObjects(CImage<double> background, int r, double Q, int Xmin, int Xmax)
    {
      int height = background.GetH;
      int width = background.GetW;
      CImage<double> img = background.Copy();
      int ObjNumber = GetNumber(height, width, Q, r);
      Random rnd = new Random();
      for (int i = 0; i < ObjNumber; i++)
      {
        int X = rnd.Next(0, width + 1);
        int Y = rnd.Next(0, height + 1);
        int L = rnd.Next(Xmin, Xmax + 1);
        DrawCircle(X, Y, L, img, r);
        
      }
      return img;
    }

    static void DrawCircle(int X, int Y, int L, CImage<double> img, int r)
    {
      for (int i = -r; i <= r; i++)
      {
        for (int j = -r; j <= r; j++)
        {
          if (Math.Sqrt((Math.Abs(i) * (Math.Abs(i)) + (Math.Abs(j)) * (Math.Abs(j)))) <= r)
          { if ((X+i<img.GetW)&&(Y+j<img.GetH)&&(Y+j>0)&&(X+i>0))
            img[X + i, Y + j] = L; }

        }

      }
    }
  }
}
