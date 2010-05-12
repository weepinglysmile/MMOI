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
     Objects.GetObjects(background, out bgAndObj, param.Radius, param.Q, param.Xmin, param.Xmax);
      pictureBox1.Image = bgAndObj.CImageToBitmap();
    }

  }
}
