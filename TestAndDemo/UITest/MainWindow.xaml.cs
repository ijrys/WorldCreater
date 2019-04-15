using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UITest {
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow () {
			InitializeComponent ();
		}
		private void BtnT1_Click (object sender, RoutedEventArgs e) {
			Color[] colors = new Color[2] {
				Color.FromArgb(0,0,0,0),
				Color.FromArgb(255,255,0,0),
			};
			WriteableBitmap bitmap = new WriteableBitmap (128, 128, 96, 96, PixelFormats.Indexed1, new BitmapPalette (colors));

			byte[] cont = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x80, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00 };

			int pxwk = bitmap.Format.BitsPerPixel;

			bitmap.WritePixels (new System.Windows.Int32Rect (16, 16, 16, 16), cont, 2, 0);

			imgshow.Source = bitmap;
		}

		private void BtnT2_Click (object sender, RoutedEventArgs e) {
			WriteableBitmap bitmap = new WriteableBitmap (128, 128, 96, 96, PixelFormats.BlackWhite, null);

			byte[] cont = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x80, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00 };

			int pxwk = bitmap.Format.BitsPerPixel;

			bitmap.WritePixels (new System.Windows.Int32Rect (16, 16, 16, 16), cont, 2, 0);

			imgshow.Source = bitmap;
		}
	}
}
