using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Labi
{
	public class autocontrast : Filters
	{

		protected override Color Calcnewpix(Bitmap sourceIm, int x, int y)
		{
			Color sourceCol = sourceIm.GetPixel(x, y);
			int N = 20;

			Color resultCol = Color.FromArgb((int)((Slice((100 * sourceCol.R + 128 * N) / (100 - N), 0, 255))),
											 (int)(Slice((100 * sourceCol.G + 128 * N) / (100 - N), 0, 255)),
											 (int)(Slice((100 * sourceCol.B + 128 * N) / (100 - N), 0, 255)));
			return resultCol;
		}
	}
}
