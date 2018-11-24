using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapConvert {
	public static class HeightToImage {
		public static Bitmap BWImage(int[,] map, int min, int max, bool showError) {
			if (map == null) return null;
			int w = map.GetLength(1);
			int h = map.GetLength(0);
			int vr = max - min;

			Bitmap re = new Bitmap(w, h);
			for (int i = 0; i < h; i ++) {
				for (int j = 0; j < w; j ++) {
					Color c;
					//获取颜色
					if (map[i, j] < min) {
						if (showError) { c = Color.Red; }
						else { c = Color.Black; }
					} else if (map[i, j] >= max) {
						if (showError) { c = Color.Green; }
						else { c = Color.White; }
					} else {
						int t = map[i, j] - min;
						int b = t * 255 / vr;
						c = Color.FromArgb(b, b, b);
					}
					//赋值
					re.SetPixel(j, i, c);
				}
			}

			return re;
		}




		private static Color GetColorByHeight(float height) {
			if (height < 0.3) {
				return Color.FromArgb(32, 64, 255);
			}
			else if (height < 0.495) {
				return Color.FromArgb(32, 128, 255);
			}
			else if (height < 0.505) {
				return Color.FromArgb(255, 255, 64);
			}
			else if (height < 0.6) {
				return Color.FromArgb(64, 255, 64);
			}
			else if (height < 0.8) {
				return Color.FromArgb(32, 192, 32);
			}
			else if (height < 0.9) {
				return Color.FromArgb(32, 128, 32);
			} else {
				return Color.FromArgb(255, 255, 255);
			}
		}
		public static Bitmap ColorImage (int[,] map, int min, int max) {
			if (map == null) return null;
			int w = map.GetLength(1);
			int h = map.GetLength(0);
			float vr = max - min;

			Bitmap re = new Bitmap(w, h);
			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					Color c = GetColorByHeight((map[i, j] - min) / vr);
					//赋值
					re.SetPixel(j, i, c);
				}
			}

			return re;
		}
	}
}
