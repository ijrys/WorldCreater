using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ValueToImage {
	/// <summary>
	/// 将值转化为灰度图片
	/// </summary>
	public static class ValueToGrayImage {
		/// <summary>
		/// 获取一个灰度图
		/// </summary>
		/// <param name="minvalue">指定最小的value</param>
		/// <param name="maxvalue">指定最大的value</param>
		/// <param name="mingray">指定最小value对应的gray</param>
		/// <param name="maxgray">指定最大value对应的gray</param>
		/// <param name="map">数据来源图</param>
		/// <returns></returns>
		public static WriteableBitmap GetBitmap(int minvalue, int maxvalue, byte mingray, byte maxgray, int[,] map) {
			if (maxvalue < minvalue || maxgray < mingray || map == null) return null;
			int valuediff = maxvalue - minvalue;
			int graydiff = maxgray - mingray;
			int width = map.GetLength(1);
			int height = map.GetLength(0);

			byte[] contbyte = new byte[width * height];
			
			WriteableBitmap wb = new WriteableBitmap(width, height, 96, 96, System.Windows.Media.PixelFormats.Gray8, null);
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					byte aimbyte = 0;
					if (map[i, j] <= minvalue) {
						aimbyte = mingray;
					}else if (map[i, j] >= maxgray) {
						aimbyte = maxgray;
					} else {
						aimbyte = (byte)((map[i, j] - minvalue) * graydiff / valuediff);
					}

					contbyte[i * width + j] = aimbyte;
				}
			}

			// copy data to the bitmap
			Int32Rect rect = new Int32Rect(0, 0, width, height);
			wb.WritePixels(rect, contbyte, wb.PixelWidth, 0);

			return wb;
		}

		/// <summary>
		/// 获取一个值对应的value
		/// </summary>
		/// <param name="minvalue">指定最小的value</param>
		/// <param name="maxvalue">指定最大的value，应不小于minvalue</param>
		/// <param name="mingray">指定最小value对应的gray</param>
		/// <param name="maxgray">指定最大value对应的gray，应不小于mingray</param>
		/// <param name="value">数据</param>
		/// <returns></returns>
		public static byte GetPixel (int minvalue, int maxvalue, byte mingray, byte maxgray, int value) {
			if (maxvalue < minvalue || maxgray < mingray) return 0;
			int valuediff = maxvalue - minvalue;
			int graydiff = maxgray - mingray;
			byte aimbyte = 0;
			if (value <= minvalue) {
				aimbyte = mingray;
			} else if (value >= maxgray) {
				aimbyte = maxgray;
			} else {
				aimbyte = (byte)((value - minvalue) * graydiff / valuediff);
			}
			return aimbyte;
		}

		public static WriteableBitmap GetBitmapWithError (int minvalue, int maxvalue, int[,] map) {
			if (map == null) return null;
			int w = map.GetLength(1);
			int h = map.GetLength(0);
			int vr = maxvalue - minvalue;

			byte[] cont = new byte[w * h];

			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					byte c;
					//获取颜色
					if (map[i, j] < minvalue) {
						c = 0;
					} else if (map[i, j] > maxvalue) {
						c = 255;
					} else {
						int t = map[i, j] - minvalue;
						c = (byte)((t * 253 / vr) + 1);
					}
					//赋值
					cont[i * w + j] = c;
				}
			}


			System.Windows.Media.Color[] colors = new System.Windows.Media.Color[256];
			for (int i = 0; i < 256; i++) colors[i] = System.Windows.Media.Color.FromArgb(255, (byte)i, (byte)i, (byte)i);
			colors[0] = System.Windows.Media.Color.FromArgb(255, 255, 0, 0);
			colors[255] = System.Windows.Media.Color.FromArgb(255, 0, 255, 255);
			BitmapPalette palette = new BitmapPalette(colors);
			WriteableBitmap re = new WriteableBitmap(w, h, 96, 96, System.Windows.Media.PixelFormats.Indexed8, palette);

			Int32Rect rect = new Int32Rect(0, 0, w, h);
			re.WritePixels(rect, cont, re.PixelWidth, 0);

			return re;
		}
	}
}
