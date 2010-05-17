using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImageProcessing;

namespace MMOI
{
  public partial class Form1 : Form
  {
    Parameters param = new Parameters();
    CImage<double> background;
    CImage<double> bgAndObj;
    CImage<double> vitImg;

    public Form1()
    {
      InitializeComponent();
      propertyGrid1.SelectedObject = param;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      VITModel vit = new VITModel(2, 0.3, 0.9, 0.333, 1, 3, 0, 0, 0, 128, 128);
     // pictureBox1.Image = vit.AppVIT(Background.GetBackground(128, 128, 0.05, 625, 128)).CImageToBitmap();
      //pictureBox1.Image = Background.GetBackground(512, 512, 0.05, 625, 128).CImageToBitmap();
    }

    private void BackgroundBtn_Click(object sender, EventArgs e)
    {
      background = Background.GetBackground(param.Size, param.Size, param.Alfa, param.Df, param.Mf);
      pictureBox1.Image = background.CImageToBitmap();

    }

    private void VITBtn_Click(object sender, EventArgs e)
    {
      VITModel model = new VITModel(param.R, param.L1, param.L2, param.S1, param.S2, param.K, 0,0,0, param.Size, param.Size);
      vitImg = model.AppVIT(bgAndObj, OptSysCheckBox.Checked, H0CheckBox.Checked, HkCheckBox.Checked);
      pictureBox2.Image = vitImg.CImageToBitmap();
    }

    private void ObjBtn_Click(object sender, EventArgs e)
    {
        
     List<ImageProcessing.Point> points = (Objects.GetObjects(background, out bgAndObj, param.Radius, param.Q, param.Xmin, param.Xmax, !checkBox1.Checked)).OrderBy(x=>x.x).ThenBy(y=>y.y).ToList();
        foreach (ImageProcessing.Point p in points)
     {
         ObjList.Items.Add(p);
     }
        pictureBox1.Image = bgAndObj.CImageToBitmap();
    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
        textBox1.Text = "(" + e.X.ToString() + ", " + e.Y.ToString() + ")";
    }

    private void Do_Click(object sender, EventArgs e)
    {
        FoundObj.Items.Clear();
        double [,] DMap;
        CImage<double> outImg;
        List<ImageProcessing.Point> points = Objects.FindObjects(vitImg, out outImg, int.Parse(textBox3.Text), out DMap, double.Parse(textBox2.Text)).OrderBy(x => x.x).ThenBy(y => y.y).ToList();
       pictureBox2.Image = outImg.CImageToBitmap();
       CImage<double> dispersionMatrix = CImage<double>.MatrixToCImage(DMap, param.Size);
       pictureBox1.Image = (CImage<double>.Norm(dispersionMatrix, byte.MaxValue)).CImageToBitmap();
       foreach (ImageProcessing.Point p in points)
       {
           FoundObj.Items.Add(p);
       }
    }


    private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
    {
        textBox1.Text = "(" + e.X.ToString() + ", " + e.Y.ToString() + ")";
    }

    private void button1_Click_1(object sender, EventArgs e)
    {

    }

  }
}
