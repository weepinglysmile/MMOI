using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ImageProcessing
{
   public class Point
    {
       public int x { get; set; }
       public int y { get; set; }
       public Point(int X, int Y)
       {
           x = X;
           y = Y;
       }

       public Point() { }

       public override string ToString()
       {
           return "(" + x + ", " + y + ") ";
       }


       public override int GetHashCode()
       {
           return x^y;
       }

       public override bool Equals(object obj)
       {
           if (obj is Point)
           {
               if ((((Point)obj).x == this.x) && (((Point)obj).y == this.y))
               {
                   return true;
               }
           }
           return false;
       }

       public static bool operator ==(Point p1, Point p2)
       {
           return (p1.Equals(p2));
       }
       public static bool operator !=(Point p1, Point p2)
       {
           return !(p1.Equals(p2));
       }
    }

   public class PointComparer : IEqualityComparer<Point>
   {
       #region IEqualityComparer<Point> Members

       public bool Equals(Point x, Point y)
       {
           return x.Equals(y);
       }

       public int GetHashCode(Point obj)
       {
           return obj.GetHashCode();
       }

       #endregion
   }


}
