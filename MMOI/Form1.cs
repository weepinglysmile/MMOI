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
    List<ImageProcessing.Point> obj;

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
      background = Background.GetBackground(param.IMGSize, param.IMGSize, param.Alfa, param.Df, param.Mf);
      pictureBox1.Image = background.CImageToBitmap();
      background.CImageToBitmap().Save("bg.bmp");

    }

    private void VITBtn_Click(object sender, EventArgs e)
    {
      VITModel model = new VITModel(param.R, param.L1, param.L2, param.S1, param.S2, param.K, 0,0,0, param.Size, param.IMGSize);
      vitImg = model.AppVIT(bgAndObj, OptSysCheckBox.Checked, H0CheckBox.Checked, HkCheckBox.Checked, NoiseCheckBox.Checked, param.Sigma, param.X0);
      pictureBox2.Image = vitImg.CImageToBitmap();
      vitImg.CImageToBitmap().Save("vit.bmp");
    }

    private void ObjBtn_Click(object sender, EventArgs e)
    {   
    obj = (Objects.GetObjects(background, out bgAndObj, param.IMGSize/256* param.Radius, param.Q, param.Xmin, param.Xmax, !checkBox1.Checked)).OrderBy(x=>x.x).ThenBy(y=>y.y).ToList();
    bgAndObj.CImageToBitmap().Save("img.bmp");
    foreach (ImageProcessing.Point p in obj)
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
        List<ImageProcessing.Point> points; 
        Objects.GetDispMatrix(vitImg, int.Parse(textBox3.Text), out DMap);
      
       CImage<double> origDispersionMatrix;
       CImage<double> DMatrix = DispersionMatrix.GetDMatrixImg(DMap, out origDispersionMatrix, param.Size, vitImg, param.Sigma, param.X0);
       pictureBox3.Image = DMatrix.CImageToBitmap();
       points = Objects.GetFoundObj(double.Parse(textBox2.Text), origDispersionMatrix);
       CImage<double> img = vitImg.Copy();
       points = Objects.Skeletonization(points);
       Objects.MarkPoint(points, img);
        pictureBox2.Image = img.CImageToBitmap();
        foreach (ImageProcessing.Point p in points)
       {
           FoundObj.Items.Add(p);
       }
        List<ImageProcessing.Point> objects = new List<ImageProcessing.Point>();
        List<ImageProcessing.Point> exactObj = Research.ExactObj(obj, points);
        List<ImageProcessing.Point> inexactObj = Research.InexactObj(obj, points, param.Radius, out objects);
        List<ImageProcessing.Point> falseObj = Research.FalseObj(obj, points, param.Radius);
        List<ImageProcessing.Point> lostObj = Research.LostObj(obj, points, param.Radius);
        double ErrM = Research.GetErrM(objects, inexactObj, param.Radius);
        double ErrSKO = Research.GetErrSKO(objects, inexactObj, param.Radius);
        FoundObjTB.Text = (exactObj.Count() + inexactObj.Count()).ToString();
        LostObgTB.Text = lostObj.Count().ToString();
        FalseObjTB.Text = falseObj.Count().ToString();
        MErrNormTB.Text = ErrM.ToString();
        SKOErrNormTB.Text = ErrSKO.ToString();
        LostAndFalseObjTB.Text = (falseObj.Count() + lostObj.Count()).ToString();
    }


    private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
    {
        textBox1.Text = "(" + e.X.ToString() + ", " + e.Y.ToString() + ")";
    }

    private void VITAnalyssBTN_Click(object sender, EventArgs e)
    {
        CImage<double> background4N = Background.GetBackground(1024, 1024, param.Alfa, param.Df, param.Mf);
        CImage<double> bgAndObj4N = new CImage<double>(1024, 1024);
        Objects.GetObjects(background4N, out bgAndObj4N, 1024 / 256 * param.Radius, param.Q, param.Xmin, param.Xmax, !checkBox1.Checked);
        CImage<double> bgAndObj2N = new CImage<double>(512, 512);
        for (int i = 0; i < 512; i++)
        {
            for (int j = 0; j < 512; j++)
            {
                bgAndObj2N[i, j] = bgAndObj4N[i * 2, j * 2];
            }
        }

        CImage<double> bgAndObjN = new CImage<double>(256, 256);
        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < 256; j++)
            {
                bgAndObjN[i, j] = bgAndObj4N[i * 4, j * 4];
            }
        }


        VITModel modelN = new VITModel(param.R, param.L1, param.L2, param.S1, param.S2, param.K, 0, 0, 0, 256, 256);
        CImage<double> vitImgN = modelN.AppVIT(bgAndObjN, OptSysCheckBox.Checked, H0CheckBox.Checked, HkCheckBox.Checked, NoiseCheckBox.Checked, param.Sigma, param.X0);

        VITModel model2N = new VITModel(param.R, param.L1, param.L2, param.S1, param.S2, param.K, 0, 0, 0, 256, 512);
        CImage<double> vitImg2N = model2N.AppVIT(bgAndObj2N, OptSysCheckBox.Checked, H0CheckBox.Checked, HkCheckBox.Checked, NoiseCheckBox.Checked, param.Sigma, param.X0);

        VITModel model4N = new VITModel(param.R, param.L1, param.L2, param.S1, param.S2, param.K, 0, 0, 0, 256, 1024);
        CImage<double> vitImg4N = model4N.AppVIT(bgAndObj4N, OptSysCheckBox.Checked, H0CheckBox.Checked, HkCheckBox.Checked, NoiseCheckBox.Checked, param.Sigma, param.X0);

        pictureBox1.Image = vitImgN.CImageToBitmap();
        pictureBox2.Image = vitImg2N.CImageToBitmap();
        pictureBox3.Image = vitImg4N.CImageToBitmap();

        double SKO = 0;
        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < 256; j++)
            {
                SKO += ((vitImgN[i, j] - vitImg2N[i, j]) * (vitImgN[i, j] - vitImg2N[i, j]));
            }
        }
        SKO /= (256 * 256);
        SKOTB12.Text = SKO.ToString();
        SKO = 0;
        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < 256; j++)
            {
                SKO += ((vitImg2N[i, j] - vitImg4N[i, j]) * (vitImg2N[i, j] - vitImg4N[i, j]));
            }
        }
        SKO /= (256 * 256);
        SKOTB24.Text = SKO.ToString();

    }

  

  }
}
