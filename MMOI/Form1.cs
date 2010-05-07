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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          pictureBox1.Image = Background.GetBackground(128, 128, 0.5, 100, 128).CImageToBitmap();
        }
    }
}
