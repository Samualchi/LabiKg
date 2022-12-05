using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Labi
{
    class noice
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
        public class ConvMatrix
        {
            public int TopLeft = 0, TopMid = 0, TopRight = 0;
            public int MidLeft = 0, Pixel = 1, MidRight = 0;
            public int BottomLeft = 0, BottomMid = 0, BottomRight = 0;
            public int Factor = 1;
            public int Offset = 0;
            public void SetAll(int nVal)
            {
                TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight =
                          BottomLeft = BottomMid = BottomRight = nVal;
            }
        }
        public class notlocal : Matrix
        {
            public void CreateGaussKernel(int radius, float sigma)
            {
                int size = 2 * radius + 1;
                kernel = new float[size, size];
                float norm = 0;
                for (int i = -radius; i <= radius; i++)
                    for (int j = -radius; j <= radius; j++)
                    {
                        kernel[i + radius, j + radius] = (float)Math.Exp(-(i * i + j * j) / (sigma * sigma));
                        norm += kernel[i + radius, j + radius];
                    }
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        kernel[i, j] /= norm;
            }
        }
        public static bool Conv3x3(Bitmap b, ConvMatrix m)
        {
            if (0 == m.Factor)
                return false; Bitmap
            bSrc = (Bitmap)b.Clone();
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                                ImageLockMode.ReadWrite,
                                PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height),
                               ImageLockMode.ReadWrite,
                               PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            int stride2 = stride * 2;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;
                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) +
                            (pSrc[5] * m.TopMid) +
                            (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) +
                            (pSrc[5 + stride] * m.Pixel) +
                            (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) +
                            (pSrc[5 + stride2] * m.BottomMid) +
                            (pSrc[8 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.TopLeft) +
                            (pSrc[4] * m.TopMid) +
                            (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) +
                            (pSrc[4 + stride] * m.Pixel) +
                            (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) +
                            (pSrc[4 + stride2] * m.BottomMid) +
                            (pSrc[7 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) +
                                       (pSrc[3] * m.TopMid) +
                                       (pSrc[6] * m.TopRight) +
                                       (pSrc[0 + stride] * m.MidLeft) +
                                       (pSrc[3 + stride] * m.Pixel) +
                                       (pSrc[6 + stride] * m.MidRight) +
                                       (pSrc[0 + stride2] * m.BottomLeft) +
                                       (pSrc[3 + stride2] * m.BottomMid) +
                                       (pSrc[6 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }

                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);
            return true;
        }

        public float[] Uniform(int size)
        {
            int a = 32;
            int b = 120;
            float sum = 0f;
            float[] uniform = new float[256];
            for (int i = 0; i < 256; i++)
            {

                int step = i;
                if (step >= a && step <= b)
                {
                    uniform[i] = (1 / (float)(b - a));
                }
                else
                {
                    uniform[i] = 0;
                }
                sum += uniform[i];
            }

            for (int i = 0; i < 256; i++)
            {
                uniform[i] /= sum;
                uniform[i] *= size;
                uniform[i] = (int)Math.Floor(uniform[i]);
            }
            return uniform;

        }

        public float[] GammaNoise(float size)
        {
            float a = 2;
            float b = 5;
            var Gamma = new float[256];
            float sum = 0f;

            for (int i = 0; i < 256; i++)
            {
                double step = (float)i * 0.05;
                if (step >= 0)
                {
                    Gamma[i] = ((float)(double)(Math.Exp(-a * step) * (Math.Pow(a, b) * Math.Pow(step, b - 1)) / factorial(b - 1)));
                }
                else
                {
                    Gamma[i] = 0;
                }
                sum += Gamma[i];
            }

            for (int i = 0; i < 256; i++)
            {
                Gamma[i] /= sum;
                Gamma[i] *= size;
                Gamma[i] = (int)Math.Floor(Gamma[i]);
            }

            return Gamma;
        }
        public byte[] MakeNoise(float[] uniform, int size)
        {
            int count = 0;
            Random rnd = new Random();
            var noise = new byte[size];
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < (int)uniform[i]; j++)
                {
                    noise[j + count] = (byte)i;
                }
                count += (int)uniform[i];
            }
            for (int i = 0; i < size - count; i++)
            {
                noise[count + i] = 0;
            }
            noise = noise.OrderBy(x => rnd.Next()).ToArray();
            return noise;
        }
        public Bitmap ExecuteHamma(Bitmap source)
        {
            return Calculatenoise(source, GammaNoise(source.Width * source.Height));
        }
        public Bitmap ExecuteUniform(Bitmap source)
        {
            return Calculatenoise(source, Uniform(source.Width * source.Height));
        }
        double factorial(double n)
        {
            if (n == 1) return 1;

            return n * factorial(n - 1);
        }

        public Bitmap Calculatenoise(Bitmap source, float[] uniform)
        {

            Bitmap resultimage = new Bitmap(source);

            int size = source.Width * source.Height;
            var noise = MakeNoise(uniform, size);


            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color color = source.GetPixel(x, y);

                    resultimage.SetPixel(x, y, Color.FromArgb(clamp(color.R + noise[source.Width * y + x], 0, 255),
                        clamp(color.G + noise[source.Width * y + x], 0, 255),
                        clamp(color.B + noise[source.Width * y + x], 0, 255)));

                }
            }

            return resultimage;
        }
    }

}
