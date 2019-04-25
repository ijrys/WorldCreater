using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ValueToImage {
	/// <summary>
	/// 配合SolarIlluminance的值转换器
	/// </summary>
	public static class SolarIlluminance {

		/// <summary>
		/// 获取光照强度图，返回值为Gray8
		/// </summary>
		/// <param name="value">原始数据</param>
		/// <returns></returns>
		public static WriteableBitmap GetBitmap (byte[,] value) {
			int width = value.GetLength (1);
			int height = value.GetLength (0);

			WriteableBitmap wb = new WriteableBitmap (width, height, 96, 96, System.Windows.Media.PixelFormats.Gray8, null);

			Int32Rect rect = new Int32Rect (0, 0, width, height);
			wb.WritePixels (rect, value, width, 0);

			return wb;
		}
	}
}
