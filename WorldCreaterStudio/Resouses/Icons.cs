using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WorldCreaterStudio.Resouses {
	static class Icons {
		/// <summary>
		/// 暗色主题 工程
		/// </summary>
		public static BitmapSource Dark_Icon_Pro { get; set; }
		/// <summary>
		/// 暗色主题 工作
		/// </summary>
		public static BitmapSource Dark_Icon_Work { get; set; }
		/// <summary>
		/// 暗色主题 资源
		/// </summary>
		public static BitmapSource Dark_Icon_Res { get; set; }
		/// <summary>
		/// 暗色主题 资源库
		/// </summary>
		public static BitmapSource Dark_Icon_ResLib { get; set; }
		/// <summary>
		/// 暗色主题 后端工作
		/// </summary>
		public static BitmapSource Dark_Icon_BackEndWork { get; set; }

		static Icons () {
			Dark_Icon_Pro = BitmapConvert(WorldCreaterStudio_Resouses.Properties.Resources.Dark_Icon_Pro);
			Dark_Icon_Work = BitmapConvert(WorldCreaterStudio_Resouses.Properties.Resources.Dark_Icon_Gra);
			Dark_Icon_Res = BitmapConvert(WorldCreaterStudio_Resouses.Properties.Resources.Dark_Icon_Res);
			Dark_Icon_ResLib = BitmapConvert(WorldCreaterStudio_Resouses.Properties.Resources.Dark_Icon_ResLib);
			Dark_Icon_BackEndWork = BitmapConvert(WorldCreaterStudio_Resouses.Properties.Resources.Dark_Icon_BackEndWork);
		}
		#region Tools
		/// <summary>
		/// 转换bitmap到bitmapimage
		/// </summary>
		/// <param name="bitmap"></param>
		/// <returns></returns>
		private static BitmapImage BitmapConvert(Bitmap bitmap) {
			BitmapImage result = new BitmapImage();
			using (MemoryStream stream = new MemoryStream()) {
				//注意：转换的图片的原始格式ImageFormat设为BMP、JPG、PNG等
				bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

				stream.Position = 0;
				result.BeginInit();
				result.CacheOption = BitmapCacheOption.OnLoad;
				result.StreamSource = stream;
				result.EndInit();
				result.Freeze();
			}

			return result;
		}
		#endregion
	}
}
