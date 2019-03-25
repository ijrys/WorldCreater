using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Test {
	class Program {
		static void Main(string[] args) {
			//int[,] a = new int[8, 16];
			//Console.WriteLine(a.GetLength(0));
			//Console.WriteLine(a.GetLength(1));

			WriteableBitmap wb = new WriteableBitmap(16, 16, 96, 96, System.Windows.Media.PixelFormats.Gray8, null);
			Console.WriteLine(wb.Format.BitsPerPixel);
			Console.WriteLine(wb.Format.BitsPerPixel * wb.PixelWidth / 8);


			Console.WriteLine("end work, press enter to exit");
			Console.ReadLine();
		}
	}
}
