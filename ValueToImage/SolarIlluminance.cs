using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ValueToImage {
	public static class SolarIlluminance {
		public static WriteableBitmap GetBitmap (byte[,] value) {
			int width = value.GetLength (1);
			int height = value.GetLength (0);

			WriteableBitmap wb = new WriteableBitmap (width, height, 96, 96, System.Windows.Media.PixelFormats.Gray8, null);
			//for (int i = 0; i < height; i++) {
			//	for (int j = 0; j < width; j++) {
			//		byte aimbyte = 0;
			//		if (map[i, j] <= minvalue) {
			//			aimbyte = mingray;
			//		}
			//		else if (map[i, j] >= maxgray) {
			//			aimbyte = maxgray;
			//		}
			//		else {
			//			aimbyte = (byte)((map[i, j] - minvalue) * graydiff / valuediff);
			//		}

			//		contbyte[i * width + j] = aimbyte;
			//	}
			//}

			// copy data to the bitmap
			Int32Rect rect = new Int32Rect (0, 0, width, height);
			wb.WritePixels (rect, value, width, 0);

			return wb;
		}
	}
}
