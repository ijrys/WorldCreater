﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WorldCreaterStudio_Core.BackendNode.RainfallMotion;

namespace ValueToImage {
	public static class RainfallMotionToImage {
		private static System.Windows.Media.Color[] ricolors = new System.Windows.Media.Color[256];
		private static System.Windows.Media.Color[] atcolors = new System.Windows.Media.Color[256];
		private static BitmapPalette ripalette;
		private static BitmapPalette atpalette;

		static RainfallMotionToImage () {
			for (byte i = 0; i < 128; i++) {
				ricolors[i] = new System.Windows.Media.Color () { A = 255, G = i, R = i, B = (byte)(i + 128) };
			}
			for (int i = 128; i < 256; i++) {
				ricolors[i] = new System.Windows.Media.Color () { A = 255, G = (byte)i, R = (byte)i, B = 255 };
			}
			ripalette = new BitmapPalette (ricolors);

			// land
			atcolors[0] = new System.Windows.Media.Color () { A = 255, R = 56, G = 87, B = 35 };
			// river
			atcolors[1] = new System.Windows.Media.Color () { A = 255, R = 96, G = 159, B = 255 };
			// lake 2
			atcolors[2] = new System.Windows.Media.Color () { A = 255, R = 63, G = 96, B = 255 };
			// sea
			atcolors[3] = new System.Windows.Media.Color () { A = 255, R = 0, G = 63, B = 192 };
			// marsh
			atcolors[4] = new System.Windows.Media.Color () { A = 255, R = 127, G = 96, B = 0 };

			// err
			var errcol = new System.Windows.Media.Color () { A = 255, R = 255, G = 0, B = 0 };
			for (int i = 5; i < 256; i ++) {
				atcolors[i] = errcol;
			}
			atpalette = new BitmapPalette (atcolors);
		}

		public static WriteableBitmap GetRainfallIntensityBitmap (PointData[,] datas) {
			int width = datas.GetLength (1);
			int height = datas.GetLength (0);

			WriteableBitmap re = new WriteableBitmap (width, height, 96, 96, System.Windows.Media.PixelFormats.Indexed1, ripalette);
			byte[] tmp = new byte[width];

			// 累计
			for (int h = 0; h < height; h++) {
				for (int w = 0; w < width; w++) {
					tmp[w] = (byte)(datas[h, w].RainfallIntensity >> 24);
				}
				re.WritePixels (new System.Windows.Int32Rect (0, h, width, 1), tmp, width, 0);
			}


			return re;
		}

		public static WriteableBitmap GetAreaTpyeBitmap (PointData[,] datas) {
			int width = datas.GetLength (1);
			int height = datas.GetLength (0);

			WriteableBitmap re = new WriteableBitmap (width, height, 96, 96, System.Windows.Media.PixelFormats.Indexed1, ripalette);
			byte[] tmp = new byte[width];

			// 累计
			for (int h = 0; h < height; h++) {
				for (int w = 0; w < width; w++) {
					tmp[w] = (byte)(datas[h, w].AreaType);
				}
				re.WritePixels (new System.Windows.Int32Rect (0, h, width, 1), tmp, width, 0);
			}

			return re;
		}
	}
}
