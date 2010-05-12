using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
   public class Bresenham
    {
       public static List<Point> GetCircle(int Xc, int Yc, int R)
       {
           List<Point> points = new List<Point>();
           int x = 0;
           int y = R;
           int d = 2 * (1 - R);
           while (y >= 0)
           {
               points.Add(new Point(Xc + x, Yc + y));
               points.Add(new Point(Xc + x, Yc - y));
               points.Add(new Point(Xc - x, Yc + y));
               points.Add(new Point(Xc - x, Yc - y));
               if (d < 0)
               {
                   if (2 * (d + y) - 1 <= 0)
                   {
                       x++;
                       d += 2 * x + 1;
                   }
                   else
                   {
                       x++;
                       y--;
                       d += 2 * x - 2 * y + 2;
                   }   
 
               }
               else
                   if (d > 0)
                   {
                       if (2 * d - 2 * x - 1 <= 0)
                       {
                           x++;
                           y--;
                           d += 2 * x - 2 * y + 2;
                       }
                       else
                       {
                           y--;
                           d = d - 2 * y + 1;
                       }

                   }
                   else
                   {
                       x++;
                       y--;
                       d += 2 * x - 2 * y + 2;
                   }

           }
           return points;
       }

       public static bool IsBorder(List<Point> points, Point p)
       {
           foreach (Point point in points)
           {
               if (point == p)
               {
                   return true;
               }
           }
           return false;
       }

       public static List<Point> GetFullCircle(List<Point> border, Point startPoint)
       {
           List<Point> points = new List<Point>();
           points.Add(startPoint);
           while (points.Count != 0)
           {
               Point p = points[0];
               if (!Bresenham.IsBorder(border, p))
               {
                   border.Add(p);
                   points.Add(new Point(p.x, p.y + 1));
                   points.Add(new Point(p.x + 1, p.y));
                   points.Add(new Point(p.x - 1, p.y));
                   points.Add(new Point(p.x, p.y - 1)); 
               }
               points.Remove(p);

           }

           return border;
       }

    }


}
