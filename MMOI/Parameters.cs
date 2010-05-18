using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace MMOI
{
  class Parameters
  {
    int xmin=50;
    int xmax=200;
    int mf = 125;
    int df = 100;
    int radius = 7;
    double q = 0.002;
    double l1 = 0.3;
    double l2 = 0.9;
    double s1 = 0.3333;
    double s2 = 1;
    int k = 3;
    int size = 256;
    double alfa = 0.5;
    double r=1;
    double x0 = 100;
    double thetta = 5;
    
    [CategoryAttribute("Изображение")]
    public int Size
    {
      get { return size; }
      set { size = value; }
    }

    [CategoryAttribute("Объекты")]
    public int Xmin
    {
      get { return xmin; }
      set { xmin = value; }
    }
    [CategoryAttribute("Объекты")]
    public int Xmax
    {
      get { return xmax; }
      set { xmax = value; }
    }

    [CategoryAttribute("Объекты"), DisplayName ("Радиус объектов")]
    public int Radius
    {
        get { return radius; }
        set { radius = value; }
    }
   
    [CategoryAttribute("Объекты"), DisplayName("Плотность объектов")]
    public double Q
    {
      get { return q; }
      set { q = value; }
    }
    [CategoryAttribute("Фон")]
    public double Alfa
    {
      get { return alfa; }
      set { alfa = value; }
    }
    [CategoryAttribute("Фон")]
    public int Mf
    {
      get { return mf; }
      set { mf = value; }
    }
    [CategoryAttribute("Фон")]
    public int Df
    {
      get { return df; }
      set { df = value; }
    }

    [CategoryAttribute("ВИТ"), DisplayName("Радиус пятна размытия")]
    public double R
    {
      get { return r; }
      set { r = value; }
    }

    [CategoryAttribute("ВИТ")]
    public double S1
    {
      get { return s1; }
      set { s1 = value; }
    }

    [CategoryAttribute("ВИТ")]
    public double X0
    {
      get { return x0; }
      set { x0 = value; }
    }

    [CategoryAttribute("ВИТ")]
    public double Thetta
    {
      get { return thetta; }
      set { thetta = value; }
    }

    [CategoryAttribute("ВИТ")]
    public double S2
    {
      get { return s2; }
      set { s2 = value; }
    }

    [CategoryAttribute("ВИТ")]
    public double L1
    {
      get { return l1; }
      set { l1 = value; }
    }
    [CategoryAttribute("ВИТ")]
    public double L2
    {
      get { return l2; }
      set { l2 = value; }
    }
    [CategoryAttribute("ВИТ")]
    public int K
    {
      get { return k; }
      set { k = value; }
    }
  }
}
