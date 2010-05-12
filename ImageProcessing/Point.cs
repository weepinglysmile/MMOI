using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

       public override int GetHashCode()
       {
           return base.GetHashCode();
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
}
