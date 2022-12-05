using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labi
{
    public partial class Form1 : Form
    {
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Title = "save file as...";
                saveFile.OverwritePrompt = true;

                saveFile.CheckPathExists = true;
                saveFile.Filter = "Image file | *.png; *.jpg; *.bmp;*.jpeg | All files (*.*)| *.* ";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(saveFile.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Can't save the image", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grayscale graysc = new grayscale();
            Bitmap resultIm = graysc.procIm(image);
            pictureBox1.Image = resultIm;
            pictureBox1.Refresh();
        }

        private void averageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            average aver = new average();
            Bitmap resultIm =aver.procIm(image);
            pictureBox1.Image = resultIm;
            pictureBox1.Refresh();
        }

        private void autocontrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autocontrast autoc = new autocontrast();
            Bitmap resultIm = autoc.procIm(image);
            pictureBox1.Image = resultIm;
            pictureBox1.Refresh();
        }

        private void pointToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void histglobToolStripMenuItem_Click(object sender, EventArgs e)
        {

            histglob histg = new histglob();
            Bitmap resultIm = histg.procIm(image);
            pictureBox1.Image = resultIm;
            pictureBox1.Refresh();
        }

        private void niblackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void pSNRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            psnr_sslm psnr = new psnr_sslm();
            MessageBox.Show(psnr.PSNR((Bitmap)pictureBox1.Image, image).ToString());
        }

        private void sSIMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            psnr_sslm sslm = new psnr_sslm();
            MessageBox.Show(sslm.SSIM((Bitmap)pictureBox1.Image, image).ToString());
        }

        private void noToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gammaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noice noi = new noice();
            Bitmap resultImage = noi.ExecuteHamma(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void steadyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noice noik = new noice();
            Bitmap resultImage = noik.ExecuteUniform(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void gaussFilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delnoice del = new delnoice();
            Bitmap resultImage = del.Gaussexecute(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void histoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void notlocToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
