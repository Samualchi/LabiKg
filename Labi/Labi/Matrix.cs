using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labi
{
	public class Matrix : Filters
	{
		protected float[,] kernel = null;
		protected Matrix() { }
		public Matrix(float[,] kernel)
		{
			this.kernel = kernel;
		}

		protected override Color Calcnewpix(Bitmap sourceIm, int x, int y)
		{
			int radX = kernel.GetLength(0) / 2;
			int radY = kernel.GetLength(1) / 2;
			float resR = 0; float resG = 0; float resB = 0;
			for (int l = -radY; l <= radY; l++)
			{
				for (int k = -radX; k <= radX; k++)
				{
					int idX = Slice(x + k, 0, sourceIm.Width - 1);
					int idY = Slice(y + l, 0, sourceIm.Height - 1);
					Color neibCol = sourceIm.GetPixel(idX, idY);
					resR += neibCol.R * kernel[k + radX, l + radY];
					resG += neibCol.G * kernel[k + radX, l + radY];
					resB += neibCol.B * kernel[k + radX, l + radY];
				}
			}
			return Color.FromArgb(Slice((int)resR, 0, 255), Slice((int)resG, 0, 255), Slice((int)resB, 0, 255));
		}
	}
}
