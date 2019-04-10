//#define Test
//#define OF

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
namespace Test {
	class Program {
		static void Main (string[] args) {
			//int[,] a = new int[8, 16];
			//Console.WriteLine(a.GetLength(0));
			//Console.WriteLine(a.GetLength(1));

			//WriteableBitmap wb = new WriteableBitmap (16, 16, 96, 96, System.Windows.Media.PixelFormats.Gray8, null);
			//Console.WriteLine (wb.Format.BitsPerPixel);
			//Console.WriteLine (wb.Format.BitsPerPixel * wb.PixelWidth / 8);

			//AMSign.AMSignOut ();
			//AMSign.AMSignOutClass8 ();
			//AMSign.AMSignOutXX ();
			//AMSign.AMSignOutClass8XX ();


			CartesianToPolar (1, 1);
			CartesianToPolar (-1, 1);
			CartesianToPolar (-1, -1);
			CartesianToPolar (1, -1);

			CartesianToPolar (2, 1);
			CartesianToPolar (1, 2);
			CartesianToPolar (-2, 1);
			CartesianToPolar (-1, 2);

#if !OF
			Console.WriteLine ("end work, press enter to exit");
			Console.ReadLine ();
#endif
		}
		/// <summary>
		/// 笛卡尔转极坐标
		/// </summary>
		static void CartesianToPolar (double x, double y) {
			Console.WriteLine ($"{x:0.00} {y:0.00}");
			double r, coss, sins, s;
			r = Math.Sqrt (x * x + y * y);
			coss = x / r;
			sins = y / r;
			Console.WriteLine ("    arcsin " + Math.Asin (sins) * 180 / Math.PI);
			Console.WriteLine ("    arccos " + Math.Acos (coss) * 180 / Math.PI);
			if (sins > 0) {
				s = Math.Acos (coss) * 180 / Math.PI;
			}
			else {
				s = 360 - Math.Acos (coss) * 180 / Math.PI;
			}
			Console.WriteLine ("  " + s);

		}

		static class AMSign {
			public static void AMSignOut () {
				byte[,] sign = new byte[16, 16];

				for (int i = 3; i < 14; i++) { sign[i, 6] = 1; } //竖线
				for (int power = 1; power < 8; power++) {
					int h = power + 2;
					if (power % 2 == 0) h--;
					if (power % 2 == 1) {
						sign[h, 7] = sign[h, 8] = 1;
					}
					else {
						sign[h, 9] = sign[h, 10] = 1;
					}
					//DemoValue (sign);
					Console.WriteLine ($"// {power} ========");
#if OF
					Console.Write ($"SignData[6, {power}] = new byte[] {{");
					OutValue (sign);
					Console.WriteLine ($" }};");
#else
					Console.WriteLine ("N->S 6 ========");
					OutValue (sign);
					Console.WriteLine ();
					Console.ReadLine ();
#endif

					byte[,] tmp = XZ (sign);
#if OF
					Console.Write ($"SignData[8, {power}] = new byte[] {{");
					OutValue (tmp);
					Console.WriteLine ($" }};");
#else
					Console.WriteLine ("E->W 8 ========");
					OutValue (tmp);
					Console.WriteLine ();
					Console.ReadLine ();
#endif

					tmp = XZ (tmp);
#if OF
					Console.Write ($"SignData[2, {power}] = new byte[] {{");
					OutValue (tmp);
					Console.WriteLine ($" }};");
#else
					Console.WriteLine ("S->N 2 ========");
					OutValue (tmp);
					Console.WriteLine ();
					Console.ReadLine ();
#endif

					tmp = XZ (tmp);
#if OF
					Console.Write ($"SignData[4, {power}] = new byte[] {{");
					OutValue (tmp);
					Console.WriteLine ($" }};");
#else
					Console.WriteLine ("W->E 4 ========");
					OutValue (tmp);
					Console.WriteLine ();
					Console.ReadLine ();
#endif
				}

			}

			public static void AMSignOutClass8 () {
				byte[,] sign = new byte[16, 16];
				for (int i = 3; i < 14; i++) { sign[i, 6] = 1; } //竖线
				sign[3, 7] = 1;
				sign[4, 8] = 1;
				sign[4, 9] = 1;
				sign[5, 10] = 1;
				sign[5, 11] = 1;
				sign[6, 8] = 1;
				sign[6, 9] = 1;
				sign[7, 7] = 1;

				Console.Write ($"SignData[6, 8] = new byte[] {{");
				OutValue (sign);
				Console.WriteLine ($" }};");

				sign = XZ (sign);
				Console.Write ($"SignData[8, 8] = new byte[] {{");
				OutValue (sign);
				Console.WriteLine ($" }};");

				sign = XZ (sign);
				Console.Write ($"SignData[2, 8] = new byte[] {{");
				OutValue (sign);
				Console.WriteLine ($" }};");

				sign = XZ (sign);
				Console.Write ($"SignData[4, 8] = new byte[] {{");
				OutValue (sign);
				Console.WriteLine ($" }};");
			}

			public static void AMSignOutXX () {
				byte[,] sign = new byte[16, 16];

				for (int x = 4, y = 6, i = 0; i < 8; i++, x++, y++) { sign[y, x] = 1; } //斜线

				for (int power = 1; power < 8; power++) {
					// 画线
					switch (power) {
						case 1:
							sign[5, 5] = 1;
							break;
						case 2:
							sign[4, 6] = 1;
							sign[3, 7] = 1;
							break;
						case 3:
							sign[7, 6] = 1;
							sign[6, 7] = 1;
							break;
						case 4:
							sign[5, 8] = 1;
							sign[4, 9] = 1;
							break;
						case 5:
							sign[8, 8] = 1;
							break;
						case 6:
							sign[7, 9] = 1;
							sign[6, 10] = 1;
							break;
						case 7:
							sign[10, 9] = 1;
							sign[9, 10] = 1;
							break;

					}



					Console.WriteLine ($"// {power} ========");
#if OF
					Console.Write ($"SignData[5, {power}] = new byte[] {{");
					OutValue (sign);
					Console.WriteLine ($" }};");
#else
					Console.WriteLine ("5 ========");
					OutValue (sign);
					Console.WriteLine ();
					Console.ReadLine ();
#endif

					byte[,] tmp = XZ (sign);
#if OF
					Console.Write ($"SignData[7, {power}] = new byte[] {{");
					OutValue (tmp);
					Console.WriteLine ($" }};");
#else
					Console.WriteLine ("7 ========");
					OutValue (tmp);
					Console.WriteLine ();
					Console.ReadLine ();
#endif

					tmp = XZ (tmp);
#if OF
					Console.Write ($"SignData[1, {power}] = new byte[] {{");
					OutValue (tmp);
					Console.WriteLine ($" }};");
#else
					Console.WriteLine ("1 ========");
					OutValue (tmp);
					Console.WriteLine ();
					Console.ReadLine ();
#endif

					tmp = XZ (tmp);
#if OF
					Console.Write ($"SignData[3, {power}] = new byte[] {{");
					OutValue (tmp);
					Console.WriteLine ($" }};");
#else
					Console.WriteLine ("3 ========");
					OutValue (tmp);
					Console.WriteLine ();
					Console.ReadLine ();
#endif
				}

			}

			public static void AMSignOutClass8XX () {
				byte[,] sign = new byte[16, 16];
				for (int x = 4, y = 6, i = 0; i < 8; i++, x++, y++) { sign[y, x] = 1; } //斜线
				sign[5, 5] = 1;
				sign[5, 6] = 1;
				sign[4, 7] = 1;
				sign[4, 8] = 1;
				sign[4, 9] = 1;
				sign[5, 9] = 1;
				sign[6, 9] = 1;
				sign[7, 8] = 1;
				sign[8, 8] = 1;

				Console.Write ($"SignData[5, 8] = new byte[] {{");
				OutValue (sign);
				Console.WriteLine ($" }};");

				sign = XZ (sign);
				Console.Write ($"SignData[7, 8] = new byte[] {{");
				OutValue (sign);
				Console.WriteLine ($" }};");

				sign = XZ (sign);
				Console.Write ($"SignData[1, 8] = new byte[] {{");
				OutValue (sign);
				Console.WriteLine ($" }};");

				sign = XZ (sign);
				Console.Write ($"SignData[3, 8] = new byte[] {{");
				OutValue (sign);
				Console.WriteLine ($" }};");
			}



			public static byte[,] XZ (byte[,] v) {
				int w = v.GetLength (0);
				int h = v.GetLength (1);
				byte[,] re = new byte[h, w];

				for (int y = 0; y < h; y++) {
					for (int x = 0; x < w; x++) {
						re[x, w - 1 - y] = v[y, x];
					}
				}
				return re;
			}

			public static void DemoValue (byte[,] value) {
				int now = 0;
				foreach (var item in value) {
					now++;
					if (item == 0) Console.Write ("□");
					else Console.Write ("■");
					if (now % 16 == 0) {
						Console.WriteLine ();
					}
				}
			}
			public static void OutValue (byte[,] value) {
#if Test
				DemoValue (value);
#endif
				int now = 0;
				int h = value.GetLength (0);
				int w = value.GetLength (1);


				for (int y = 0; y < h; y++) {
					int tmp = 0;
					for (int x = 0; x < w; x++) {
						if (value[y, x] == 1) {
							tmp += (1 << (w - x - 1));
						}
					}
#if OF
					Console.Write (" 0x{0:X2}, 0x{1:X2}", tmp / 256, tmp % 256);
					if (y != h - 1) Console.Write (',');
#else
					Console.WriteLine ("  0x{0:X2}, 0x{1:X2},", tmp / 256, tmp % 256);
#endif
				}
				//#if OF
				//				Console.WriteLine ();
				//#endif
			}
		}
	}
}
