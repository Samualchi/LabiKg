using System;
using System.Drawing;

namespace Labi
{
	class grayscale : Filters
	{
		protected override Color Calcnewpix(Bitmap sourceIm, int x, int y)
		{
			Color sourceCol = sourceIm.GetPixel(x, y);
			Color resultCol = Color.FromArgb((int)(0.299 * sourceCol.R + 0.587 * sourceCol.G + 0.114 * sourceCol.B),
											 (int)(0.299 * sourceCol.R + 0.587 * sourceCol.G + 0.114 * sourceCol.B),
											 (int)(0.299 * sourceCol.R + 0.587 * sourceCol.G + 0.114 * sourceCol.B));
			return resultCol;
		}
	}
}
