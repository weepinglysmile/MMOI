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

    public static List<Point> GetObjects(CImage<double> background, out CImage<double> img, int r, double Q, int Xmin, int Xmax, bool crossing)
    {
        List<Point> obj = new List<Point>();
      int height = background.GetH;
      int width = background.GetW;
      img = background.Copy();
      int ObjNumber = GetNumber(height, width, Q, r);
      Random rndX = new Random(1);
      Random rndY = new Random(9);
      Random rndL = new Random(6);
      for (int i = 0; i < ObjNumber; i++)
      {
        int X, Y; 
        int L = rndL.Next(Xmin, Xmax + 1);
        if (!crossing)
        {
          Point point = GetNewPoint(obj, rndX, rndY, height, width, r);
          obj.Add(point);
          X = point.x;
          Y = point.y;
        }
        else
        {
          X = rndX.Next(0, width + 1);
          Y = rndY.Next(0, height + 1);
          obj.Add(new Point(X, Y));
        }
          List<Point> points = new List<Point>();
          points = Bresenham.GetCircle(X,Y,r);
          points = Bresenham.GetFullCircle(points, new Point(X, Y));
          foreach (Point p in points)
          {
            int x = (p.x + width) % width;
            int y = (p.y + height) % height;
            img[x, y] = L;
            //if ((p.x >= 0) && (p.y >= 0) && (p.x < img.GetW) && (p.y < img.GetW))
            //{
            //  img[p.x, p.y] = 200;
            //}
          }
        
      }
      return obj;
    }

    static Point GetNewPoint(List<Point> points, Random rndX, Random rndY, int heigth, int width, int r)
    {
      Point p = new Point(0,0);
      bool f = false;
      while (f == false)
      {
        p.x = rndX.Next(0, width + 1);
        p.y = rndY.Next(0, heigth + 1);
        if (CanAdd(points, p, r,heigth))
          f = true;
      }
      return p;
    }                                                      
    static bool CanAdd(List<Point> points, Point p1, int R, int size)
    {
        foreach (Point p2 in points)
        {
            double dist = Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
            if ((Math.Abs(p1.x - p2.x) > size - 2 * R) && (Math.Abs(p2.y - p2.y) < R * 2))
              return false;
            if ((Math.Abs(p1.y - p2.y) > size - 2 * R) && (Math.Abs(p2.x - p2.x) < R * 2))
              return false;
            if (dist < 2 * R+1)
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
