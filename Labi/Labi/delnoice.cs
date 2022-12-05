using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Labi
{
    class delnoice
    {
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }
        public static byte clamp(float value, float min, float max)
        {
            return (byte)(Math.Min(Math.Max(min, value), max));
        }

        public static int clamp(int value, int min, int max)
        {
            return (int)(Math.Min(Math.Max(min, value), max));
        }
        protected int Calculatenewpixelcolor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int intensity = (sourceColor.R + sourceColor.G + sourceColor.B) / 3;
            return intensity;
        }
        public Color GaussFilter(Bitmap source, int x, int y)
        {
            double sigma = 2;
            int radius = 1;
            int count = (int)Math.Pow(radius * 2 + 1, 2);
            double sum = 0;
            double Gaussum = 0;
            double Gauss1 = 0;
            double Gauss2 = 0;

            for (int l = -radius; l <= radius; l++)
            {
                for (int k = -radius; k <= radius; k++)
                {
                    int idX = Clamp(x + k, 0, source.Width - 1);
                    int idY = Clamp(y + l, 0, source.Height - 1);
                    Color neighborColor = source.GetPixel(idX, idY);

                    Gauss1 = 1 / (2 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-(Math.Pow(l, 2) + Math.Pow(k, 2)) / (2 * Math.Pow(sigma, 2)));
                    Gauss2 = 1 / (Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-(Math.Pow((double)neighborColor.R / 255 - (double)source.GetPixel(x, y).R / 255, 2)) / (2 * Math.Pow(sigma, 2)));
                    Gaussum += Gauss1 * Gauss2;
                    sum += Gauss1 * Gauss2 * (double)neighborColor.R / 255;

                }
            }
            return Color.FromArgb(Clamp((int)(sum / Gaussum * 255), 0, 255),
            Clamp((int)(sum / Gaussum * 255), 0, 255),
            Clamp((int)(sum / Gaussum * 255), 0, 255));


        }
        public Bitmap Gaussexecute(Bitmap source)
        {
            Bitmap resultImage = new Bitmap(source);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    resultImage.SetPixel(x, y, GaussFilter(source, x, y));
                }
            }

            return resultImage;
        }
    }
}
