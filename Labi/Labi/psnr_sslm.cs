using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Labi
{
    class psnr_sslm
    {
        private static float ComputeMean(Bitmap image)
        {
            float sum = 0f;
            for (int i = 0; i < image.Height; i++)
                for (int j = 0; j < image.Width; j++)
                {
                    sum += image.GetPixel(j, i).R;
                    sum += image.GetPixel(j, i).G;
                    sum += image.GetPixel(j, i).B;
                }
            return (sum / (float)(image.Height * image.Width * 3));
        }

        private static float ComputeDis(Bitmap image, float mean)
        {
            float sum = 0f;
            for (int i = 0; i < image.Height; i++)
                for (int j = 0; j < image.Width; j++)
                {
                    sum += (float)Math.Pow(image.GetPixel(j, i).R - mean, 2);
                    sum += (float)Math.Pow(image.GetPixel(j, i).G - mean, 2);
                    sum += (float)Math.Pow(image.GetPixel(j, i).B - mean, 2);
                }
            return (float)Math.Sqrt(sum / ((float)(image.Height * image.Width) - 1f) * 3);
        }

        private static float ComputeCov(Bitmap im1, float m1, Bitmap im2, float m2)
        {
            float sum = 0f;
            for (int i = 0; i < im1.Height; i++)
                for (int j = 0; j < im1.Width; j++)
                {
                    sum += (im1.GetPixel(j, i).R - m1) * (im2.GetPixel(j, i).R - m2);
                    sum += (im1.GetPixel(j, i).G - m1) * (im2.GetPixel(j, i).G - m2);
                    sum += (im1.GetPixel(j, i).B - m1) * (im2.GetPixel(j, i).B - m2);
                }
            return (sum / ((float)(im1.Height * im1.Width) - 1f) * 3);
        }
        public float PSNR(Bitmap compareImage, Bitmap perfImage)
        {
            float max = 255f;
            if (compareImage.Size != perfImage.Size) return -1;

            float mse = ComputeMSE(compareImage, perfImage);
            Console.WriteLine("MSE = " + mse.ToString());

            float psnr = (float)(20 * Math.Log10(max / (float)Math.Sqrt(mse)));
            return psnr;

        }

        private static float ComputeMSE(Bitmap im1, Bitmap im2)
        {
            float sum = 0f;
            for (int i = 0; i < im1.Height; i++)
                for (int j = 0; j < im1.Width; j++)
                {
                    sum += (float)Math.Pow((im1.GetPixel(j, i).R - im2.GetPixel(j, i).R), 2f);
                    sum += (float)Math.Pow((im1.GetPixel(j, i).G - im2.GetPixel(j, i).G), 2f);
                    sum += (float)Math.Pow((im1.GetPixel(j, i).B - im2.GetPixel(j, i).B), 2f);
                }


            return (sum / (float)(im1.Height * im1.Width * 3));
        }


        public float SSIM(Bitmap compareImage, Bitmap perfImage)
        {
            Bitmap CompareImage2 = new Bitmap(compareImage);
            Bitmap PerfImage2 = new Bitmap(perfImage);
            float L = (float)(Math.Pow(2, 8) - 1f);
            float k1 = 0.01f, k2 = 0.03f;
            float c1 = (float)Math.Pow(k1 * L, 2);
            float c2 = (float)Math.Pow(k2 * L, 2);


            float meanX = ComputeMean(PerfImage2), meanY = ComputeMean(CompareImage2);
            float disX = ComputeDis(PerfImage2, meanX), disY = ComputeDis(CompareImage2, meanY);
            float covXY = ComputeCov(PerfImage2, meanX, CompareImage2, meanY);


            float ssim, dssim;

            ssim = (2 * meanX * meanY + c1) * (2 * covXY + c2) /
                    (float)((Math.Pow(meanX, 2) + Math.Pow(meanY, 2) + c1) *
                    (Math.Pow(disX, 2) + Math.Pow(disY, 2) + c2));

            dssim = (1 - ssim) / 2;

            return ssim;
        }
    }
}
