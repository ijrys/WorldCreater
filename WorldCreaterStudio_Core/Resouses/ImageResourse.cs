using System;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Xml;
using System.Collections.ObjectModel;

namespace WorldCreaterStudio_Core {
	/// <summary>
	/// 缓存的图像文件
	/// </summary>
	public class ImageResourse : IResourseAble {
		private bool _infochanged;
		private bool _datachanged;
		private string _key;
		private string _filePath;
		private string _description;
		private BitmapImage _image;

		/// <summary>
		/// 基本信息是否修改
		/// </summary>
		public bool InfoChanged {
			get => _infochanged;
			private set => _infochanged = value;
		}
		/// <summary>
		/// 数据是否修改
		/// </summary>
		public bool DataChanged {
			get => _datachanged;
			private set => _datachanged = value;
		}

		/// <summary>
		/// 资源键
		/// </summary>
		public string Key {
			get => _key;
			private set { _key = value; }
		}
		/// <summary>
		/// 文件名
		/// </summary>
		public string FilePath {
			get => _filePath;
			private set { _filePath = value; InfoChanged = true; }
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Description {
			get => _description;
			set { _description = value; InfoChanged = true; }
		}
		/// <summary>
		/// 图片
		/// </summary>
		public BitmapImage Image {
			get => _image;
			set { _image = value; DataChanged = true; InfoChanged = true; }
		}

		public UIElement ShowPanel => null;

		public string NodeName => Key;

		public ImageSource Icon { get; set; }

		public ObservableCollection<IWorkLogicNodeAble> Childrens => null;

		public void Save(string basePath = "") {
			if (DataChanged && Image != null) {
				string path = Path.Combine(basePath, FilePath);
				SaveBitmap(Image, path);
			}
		}

		/// <summary>
		/// 获取xml节点
		/// </summary>
		/// <param name="xmlDocument"></param>
		/// <returns></returns>
		public XmlElement XmlNode(XmlDocument xmlDocument) {
			XmlElement re = xmlDocument.CreateElement("image");
			re.SetAttribute("key", Key);
			re.SetAttribute("file", _filePath);
			re.SetAttribute("description", Description);

			return re;
		}

		/// <summary>
		/// 通过一个xml节点初始化图片资源节点
		/// </summary>
		/// <param name="xmlnode"></param>
		/// <param name="imgresDir"></param>
		/// <returns></returns>
		public static ImageResourse LoadFromXmlNode(XmlElement xmlnode, string imgresDir) {
			if (xmlnode.Name != "image") return null;
			string key = xmlnode.Attributes["key"]?.Value;
			string fname = xmlnode.Attributes["file"]?.Value;
			if (key == null || fname == null) return null;
			string description = xmlnode.Attributes["description"]?.Value;
			ImageResourse re = new ImageResourse(key, fname, description);

			string imgpath = Path.Combine(imgresDir, fname);
			re._image = OpenBitmap(imgpath);

			re.DataChanged = false;
			re.InfoChanged = false;

			return re;
		}

		#region 工具函数
		private static void SaveBitmap(BitmapSource image, string filepath) {
			BitmapEncoder encoder = new PngBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(image));
			using (var fileStream = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate)) {
				encoder.Save(fileStream);
			}
		}

		private static BitmapImage OpenBitmap(string filepath) {
			BitmapImage result = new BitmapImage();
			using (FileStream stream = new FileStream(filepath, FileMode.Open)) {
				result.BeginInit();
				result.CacheOption = BitmapCacheOption.OnLoad;
				result.StreamSource = stream;
				result.EndInit();
			}

			return result;
		}
		#endregion

		#region 构造函数
		public ImageResourse(string key) {
			string name = Guid.NewGuid().ToString("N");
			name = name + "--" + DateTime.Now.Ticks.ToString("X") + ".png";
			FilePath = name;
			_key = key;
		}

		private ImageResourse(string key, string filePath, string desc) {
			_key = key;
			_filePath = filePath;
			_description = desc;

			DataChanged = false;
			InfoChanged = false;
		} 
		#endregion
	}
}
