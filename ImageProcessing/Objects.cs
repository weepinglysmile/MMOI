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
      return (int)(128 * 128 * Q);
    }

    public static List<Point> GetObjects(CImage<double> background, out CImage<double> img, int r, double Q, int Xmin, int Xmax)
    {
        List<Point> obj = new List<Point>();
      int height = background.GetH;
      int width = background.GetW;
      img = background.Copy();
      int ObjNumber = GetNumber(height, width, Q, r);
      Random rndX = new Random(1);
      Random rndY = new Random(2);
      Random rndL = new Random(3);
      for (int i = 0; i < ObjNumber; i++)
      {
        int X = rndX.Next(0, width + 1);
        int Y = rndY.Next(0, height + 1);
        int L = rndL.Next(Xmin, Xmax + 1);

          List<Point> points = new List<Point>();
          points = Bresenham.GetCircle(X,Y,r);
          points = Bresenham.GetFullCircle(points, new Point(X, Y));
          foreach (Point p in points)
          {
              if ((p.x >= 0) && (p.y >= 0)&&(p.x<img.GetW)&&(p.y<img.GetW))
              {
                  img[p.x, p.y] = L;
              }
          }
        
      }
      return obj;
    }

    static bool CanAdd(List<Point> points, Point p1, int R)
    {
        foreach (Point p2 in points)
        {
            double dist = Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
            if (dist < 2 * R)
                return false;
        }
        return true;
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
