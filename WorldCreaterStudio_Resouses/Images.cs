using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WorldCreaterStudio_Resouses {
	public static class Images {

		#region Tools
		/// <summary>
		/// 转换bitmap到bitmapimage
		/// </summary>
		/// <param name="bitmap"></param>
		/// <returns></returns>
		private static BitmapImage BitmapConvert(Stream stream) {
			BitmapImage result = new BitmapImage();

			stream.Position = 0;
			result.BeginInit();
			result.CacheOption = BitmapCacheOption.OnLoad;
			result.StreamSource = stream;
			result.EndInit();

			return result;
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
			}

			return result;
		}
		#endregion
		#endregion

		#region catch
		private static BitmapImage _dark_Icon_Res = null;
		private static BitmapImage _dark_Icon_BackEndWork = null;
		private static BitmapImage _dark_Icon_FrontEndWork = null;
		private static BitmapImage _dark_Icon_Gra = null;
		private static BitmapImage _dark_Icon_Pro = null;
		private static BitmapImage _dark_Icon_ResLib = null;
		private static BitmapImage _dark_Icon_AMNode = null;
		private static BitmapImage _dark_Icon_RMNode = null;
		private static BitmapImage _dark_Icon_SINode = null;
		private static BitmapImage _dark_Icon_BINode = null;
		#endregion

		/// <summary>
		///   Dark_Icon_Res
		/// </summary>
		public static BitmapImage Dark_Icon_Res {
			get {
				if (_dark_Icon_Res == null) {
					_dark_Icon_Res = BitmapConvert(Properties.Resources.Dark_Icon_Res);
				}

				return _dark_Icon_Res;
			}
		}

		/// <summary>
		///   Dark_Icon_BackEndWork
		/// </summary>
		public static BitmapImage Dark_Icon_BackEndWork {
			get {
				if (_dark_Icon_BackEndWork == null) {
					_dark_Icon_BackEndWork = BitmapConvert(Properties.Resources.Dark_Icon_BackEndWork);
				}

				return _dark_Icon_BackEndWork;
			}
		}

		/// <summary>
		///   Dark_Icon_Gra
		/// </summary>
		public static BitmapImage Dark_Icon_Gra {
			get {
				if (_dark_Icon_Gra == null) {
					_dark_Icon_Gra = BitmapConvert(Properties.Resources.Dark_Icon_Gra);
				}

				return _dark_Icon_Gra;
			}
		}

		/// <summary>
		///   Dark_Icon_Pro
		/// </summary>
		public static BitmapImage Dark_Icon_Pro {
			get {
				if (_dark_Icon_Pro == null) {
					_dark_Icon_Pro = BitmapConvert(Properties.Resources.Dark_Icon_Pro);
				}

				return _dark_Icon_Pro;
			}
		}

		/// <summary>
		///   Dark_Icon_ResLib
		/// </summary>
		public static BitmapImage Dark_Icon_ResLib {
			get {
				if (_dark_Icon_ResLib == null) {
					_dark_Icon_ResLib = BitmapConvert(Properties.Resources.Dark_Icon_ResLib);
				}

				return _dark_Icon_ResLib;
			}
		}

		/// <summary>
		///   Dark_Icon_FrontEndWork
		/// </summary>
		public static BitmapImage Dark_Icon_FrontEndWork {
			get {
				if (_dark_Icon_FrontEndWork == null) {
					_dark_Icon_FrontEndWork = BitmapConvert(Properties.Resources.Dark_Icon_FrontEndWork);
				}

				return _dark_Icon_FrontEndWork;
			}
		}

		/// <summary>
		///   Dark_Icon_AMNode
		/// </summary>
		public static BitmapImage Dark_Icon_AMNode {
			get {
				if (_dark_Icon_AMNode == null) {
					_dark_Icon_AMNode = BitmapConvert (Properties.Resources.Dark_Icon_AMNode);
				}

				return _dark_Icon_AMNode;
			}
		}

		/// <summary>
		///   Dark_Icon_RMNode
		/// </summary>
		public static BitmapImage Dark_Icon_RMNode {
			get {
				if (_dark_Icon_RMNode == null) {
					_dark_Icon_RMNode = BitmapConvert (Properties.Resources.Dark_Icon_RMNode);
				}

				return _dark_Icon_RMNode;
			}
		}

		/// <summary>
		///   Dark_Icon_SINode
		/// </summary>
		public static BitmapImage Dark_Icon_SINode {
			get {
				if (_dark_Icon_SINode == null) {
					_dark_Icon_SINode = BitmapConvert (Properties.Resources.Dark_Icon_SINode);
				}

				return _dark_Icon_SINode;
			}
		}

		/// <summary>
		///   Dark_Icon_BINode
		/// </summary>
		public static BitmapImage Dark_Icon_BINode {
			get {
				if (_dark_Icon_BINode == null) {
					_dark_Icon_BINode = BitmapConvert (Properties.Resources.Dark_Icon_BINode);
				}

				return _dark_Icon_BINode;
			}
		}
	}
}
