﻿namespace MMOI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
          this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
          this.panel1 = new System.Windows.Forms.Panel();
          this.panel2 = new System.Windows.Forms.Panel();
          this.button1 = new System.Windows.Forms.Button();
          this.pictureBox1 = new System.Windows.Forms.PictureBox();
          this.tableLayoutPanel1.SuspendLayout();
          this.panel1.SuspendLayout();
          this.panel2.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
          this.SuspendLayout();
          // 
          // tableLayoutPanel1
          // 
          this.tableLayoutPanel1.ColumnCount = 2;
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.70422F));
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.29577F));
          this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
          this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
          this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
          this.tableLayoutPanel1.Name = "tableLayoutPanel1";
          this.tableLayoutPanel1.RowCount = 2;
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.9313F));
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.0687F));
          this.tableLayoutPanel1.Size = new System.Drawing.Size(467, 262);
          this.tableLayoutPanel1.TabIndex = 0;
          // 
          // panel1
          // 
          this.panel1.Controls.Add(this.pictureBox1);
          this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel1.Location = new System.Drawing.Point(3, 3);
          this.panel1.Name = "panel1";
          this.panel1.Size = new System.Drawing.Size(347, 226);
          this.panel1.TabIndex = 0;
          // 
          // panel2
          // 
          this.panel2.Controls.Add(this.button1);
          this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel2.Location = new System.Drawing.Point(356, 3);
          this.panel2.Name = "panel2";
          this.panel2.Size = new System.Drawing.Size(108, 226);
          this.panel2.TabIndex = 1;
          // 
          // button1
          // 
          this.button1.Location = new System.Drawing.Point(15, 9);
          this.button1.Name = "button1";
          this.button1.Size = new System.Drawing.Size(75, 23);
          this.button1.TabIndex = 0;
          this.button1.Text = "button1";
          this.button1.UseVisualStyleBackColor = true;
          this.button1.Click += new System.EventHandler(this.button1_Click);
          // 
          // pictureBox1
          // 
          this.pictureBox1.Location = new System.Drawing.Point(0, 0);
          this.pictureBox1.Name = "pictureBox1";
          this.pictureBox1.Size = new System.Drawing.Size(100, 50);
          this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
          this.pictureBox1.TabIndex = 0;
          this.pictureBox1.TabStop = false;
          // 
          // Form1
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(467, 262);
          this.Controls.Add(this.tableLayoutPanel1);
          this.Name = "Form1";
          this.Text = "Form1";
          this.tableLayoutPanel1.ResumeLayout(false);
          this.panel1.ResumeLayout(false);
          this.panel1.PerformLayout();
          this.panel2.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

