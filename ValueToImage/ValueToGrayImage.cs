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
			wb.WritePixels(rect, contbyte, 1, 0);

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
	}
}
