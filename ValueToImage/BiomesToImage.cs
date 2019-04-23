using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WorldCreaterStudio_Core.BackendNode.Biomes;

namespace ValueToImage {
	public static class BiomesToImage {
		private static System.Windows.Media.Color[] colors = new System.Windows.Media.Color[256];
		private static BitmapPalette palette;

		static BiomesToImage () {
			/*
		0 000 0000 沙漠（寒带）
		0 001 0000 沙漠（温带）
		0 010 0000 沙漠（热带）

		0 000 0001 寒带苔原
		0 000 0010 针叶林

		0 001 0001 温带草原
		0 001 0010 温带森林

		0 010 0001 热带草原
		0 010 0010 热带旱林
		0 010 0011 热带雨林
		1 000 0000 淡水
		1 001 0000 咸水*/
			var errcol = System.Windows.Media.Color.FromArgb (255, 255, 0, 0);
			for (int i = 0; i < 256; i++) {
				colors[i] = errcol;
			}

			colors[0b0_000_0000] =
			colors[0b0_001_0000] =
			colors[0b0_010_0000] = System.Windows.Media.Color.FromArgb (255, 255, 200, 128);

			colors[0b0_000_0001] = System.Windows.Media.Color.FromArgb (255, 63, 240, 220);
			colors[0b0_000_0010] = System.Windows.Media.Color.FromArgb (255, 45, 240, 166);

			colors[0b0_001_0001] = System.Windows.Media.Color.FromArgb (255, 96, 192, 96);
			colors[0b0_001_0010] = System.Windows.Media.Color.FromArgb (255, 64, 160, 64);

			colors[0b0_010_0001] = System.Windows.Media.Color.FromArgb (255, 64, 255, 32);
			colors[0b0_010_0010] = System.Windows.Media.Color.FromArgb (255, 32, 192, 0);
			colors[0b0_010_0011] = System.Windows.Media.Color.FromArgb (255, 16, 128, 16);

			colors[0b1_000_0000] = System.Windows.Media.Color.FromArgb (255, 96, 225, 255);
			colors[0b1_001_0000] = System.Windows.Media.Color.FromArgb (255, 96, 160, 255);

			palette = new BitmapPalette (colors);
		}

		public static WriteableBitmap GetBitmap (BiomesType[,] value) {
			int width = value.GetLength (1);
			int height = value.GetLength (0);

			WriteableBitmap wb = new WriteableBitmap (width, height, 96, 96, System.Windows.Media.PixelFormats.Indexed8, palette);
			byte[,] re = new byte[height, width];
			for (int i = 0; i < height; i ++) {
				for (int j = 0; j < width; j ++) {
					re[i, j] = (byte)value[i, j];
				}
			}

			// copy data to the bitmap
			Int32Rect rect = new Int32Rect (0, 0, width, height);
			wb.WritePixels (rect, re, width, 0);

			return wb;
		}
	}
}
