using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ValueToImage {
	/// <summary>
	/// 将高度数据转换为可视的灰度图
	/// </summary>
	public static class ValueToGrayImage {
		/// <summary>
		/// 获取一个灰度图
		/// </summary>
		/// <param name="minvalue">高度的最小值，小于或等于该值的将显示为最小亮度</param>
		/// <param name="maxvalue">高度的最大值，大于或等于该值的将显示为最大亮度</param>
		/// <param name="mingray">设定最小亮度</param>
		/// <param name="maxgray">设定最大亮度</param>
		/// <param name="map">高度数据</param>
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
		/// 获取一个值在高度范围内的转换结果
		/// </summary>
		/// <param name="minvalue">高度的最小值，小于或等于该值的将返回mingray</param>
		/// <param name="maxvalue">高度的最大值，大于或等于该值的将返回maxgray</param>
		/// <param name="mingray">设定最小亮度</param>
		/// <param name="maxgray">设定最大亮度</param>
		/// <param name="value">高度数据</param>
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

		/// <summary>
		/// 获取一个带有高度超出检查的灰度图，用于检查生成的地形高度数据是否在合法范围内
		/// </summary>
		/// <param name="minvalue">高度数据的下限，小于这个值的区域将设置为红色(255,0,0)</param>
		/// <param name="maxvalue">高度数据的上限，大于这个值的区域将设置为青色(0,255,255)</param>
		/// <param name="map">高度数据</param>
		/// <returns></returns>
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
