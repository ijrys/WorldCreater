using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ValueToImage {

	/// <summary>
	/// 将高度数据转换为可视的彩色图
	/// </summary>
	public static class ValueToColorImage {
		private static BitmapPalette LineColorPalette {
			get; set;
		}

		static ValueToColorImage () {
			System.Windows.Media.Color[] colors = new System.Windows.Media.Color [256];
			for (int i = 0; i < 128; i ++) {
				colors[127 - i] = System.Windows.Media.Color.FromArgb (255, 0, 0, (byte)i);
			}
			colors[128] = System.Windows.Media.Color.FromArgb (255, 255, 255, 0);
			for (int i = 1; i < 128; i++) {
				colors[128 + i] = System.Windows.Media.Color.FromArgb (255, (byte)i, (byte)i, (byte)i);
			}
			LineColorPalette = new BitmapPalette (colors);
		}

		/// <summary>
		/// 获取一张彩色图。海平面值为0
		/// </summary>
		/// <param name="minvalue">高度数据的下限，小于这个值的区域将视为minvalue</param>
		/// <param name="maxvalue">高度数据的上限，大于这个值的区域将视为maxvalue</param>
		/// <param name="map">高度数据</param>
		/// <returns></returns>
		public static WriteableBitmap GetBitmap (int minvalue, int maxvalue, int[,] map) {
			if (maxvalue < minvalue || map == null) return null;

			int width = map.GetLength (1);
			int height = map.GetLength (0);

			byte[] contbyte = new byte[width * height];

			WriteableBitmap wb = new WriteableBitmap (width, height, 96, 96, System.Windows.Media.PixelFormats.Indexed8, LineColorPalette);
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					byte aimbyte = 0;
					if (map[i, j] <= minvalue) {
						aimbyte = 0;
					}
					else if (map[i, j] >= maxvalue) {
						aimbyte = 255;
					}
					else if (map[i, j] <= 0) {
						aimbyte = (byte)((map[i, j] - minvalue) * 127 / -minvalue);
					}
					else {
						aimbyte = (byte)((map[i, j]) * 127 / maxvalue + 128);
					}

					contbyte[i * width + j] = aimbyte;
				}
			}

			// copy data to the bitmap
			Int32Rect rect = new Int32Rect (0, 0, width, height);
			wb.WritePixels (rect, contbyte, wb.PixelWidth, 0);

			return wb;
		}
	}
}
