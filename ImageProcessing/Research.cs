using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
    public class Research
    {
        public static List<Point> ExactObj(List<Point> obj, List<Point> foundObj)
        {
            return obj.Intersect(foundObj, new PointComparer()).ToList(); 
        }

        public static List<Point> InexactObj(List<Point> obj, List<Point> foundObj, int R, out List<Point> objects)
        {
            objects = new List<Point>();
            List<Point> res = new List<Point>();
            var buf1 = ExactObj(obj, foundObj);
            var buf2 = foundObj.Except(buf1);
            foreach (var f in buf2)
            {
                foreach (var o in obj)
                {
                    if ((f.x != o.x) || (f.y != o.y))
                    {
                        double dist = Math.Sqrt((f.x - o.x) * (f.x - o.x) + (f.y - o.y) * (f.y - o.y));
                        if (dist <= R)
                        {

                            res.Add(f);
                            objects.Add(o);
                        }
                    }
                }
            }
            return res;
        }

        public static List<Point> FalseObj(List<Point> obj, List<Point> foundObj, int R)
        {
            var eObj = ExactObj(obj, foundObj);
            List<Point> objects = new List<Point>();
            var ieObj = InexactObj(obj, foundObj, R, out objects);
            return (foundObj.Except(eObj)).Except(ieObj).ToList();
        }
        public static List<Point> LostObj(List<Point> obj, List<Point> foundObj, int R)
        {
            var eObj = ExactObj(obj, foundObj);
            List<Point> objects = new List<Point>();
            var ieObj = InexactObj(obj, foundObj, R, out objects);
            return obj.Except(eObj).Except(objects).ToList();
        }

        static double Dist(Point p1, Point p2)
        {
            return Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
        }

        public static double GetErrM(List<Point> trueObj, List<Point> inexactObj, int R, int N)
        {
            double ErrM = 0;
            foreach (var f in trueObj)
            {
                foreach (var o in inexactObj)
                {
                    double dist = Math.Sqrt((f.x - o.x) * (f.x - o.x) + (f.y - o.y) * (f.y - o.y));
                    if (dist <= R)
                    {
                        ErrM += Dist(f, o);
                       
                    }
                }
            }
            ErrM /= N;
            return ErrM;           
        }

        public static double GetErrSKO(List<Point> trueObj, List<Point> inexactObj, int R,int N)
        {
            double ErrSKO = 0;
            double ErrM = GetErrM(trueObj, inexactObj, R, N);
            foreach (var f in trueObj)
            {
                foreach (var o in inexactObj)
                {
                    double dist = Math.Sqrt((f.x - o.x) * (f.x - o.x) + (f.y - o.y) * (f.y - o.y));
                    if (dist <= R)
                    {
                        ErrSKO += Math.Pow(ErrM - Dist(f,o), 2);
                       
                    }
                }
            }
            ErrSKO /= N;
            ErrSKO = Math.Sqrt(ErrSKO);
            return ErrSKO;
        }


    }


}
