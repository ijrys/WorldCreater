using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace WorldCreaterStudio_Core {
	public class ImageResoureseManager : IWorkLogicNodeAble, INotifyPropertyChanged {
		private Dictionary<string, ImageResourse> _res;
		private ObservableCollection<IWorkLogicNodeAble> _imgs;
		private DirectoryInfo _workResousesDir;
		private ImageSource _icon;

		public event PropertyChangedEventHandler PropertyChanged;

		ImageResourse this[string key] {
			get {
				if (_res.ContainsKey(key)) {
					return _res[key];
				} else {
					return null;
				}
			}
			set {
				_res[key] = value;
			}
		}

		public UIElement ShowPanel => null;

		public string NodeName => "Image Resourese";

		public ImageSource Icon { get=>_icon; set { _icon = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon")); } }

		public ObservableCollection<IWorkLogicNodeAble> Childrens {
			get { return _imgs; }
		}

		public void Add(string key, BitmapImage image, string description) {
			if (_res.ContainsKey(key)) {
				_res[key].Image = image;
			} else {
				ImageResourse imgres  = new ImageResourse(key) {
					Description = description,
					Image = image,
				};
				_res[key] = imgres;
				_imgs.Add(imgres);
			}
		}
		public void Add(ImageResourse resourse) {
			string key = resourse.Key;
			if (_res.ContainsKey(key)) {
				_res[key].Image = resourse.Image;
			} else {
				_res[key] = resourse;
				_imgs.Add(resourse);
			}
		}

		public void Remove (string key) {
			if (_res.ContainsKey(key)) {
				ImageResourse item = _res[key];
				_res.Remove(key);
				_imgs.Remove(item);
			}
		}

		/// <summary>
		/// 保存资源文件到默认资源文件夹
		/// </summary>
		public void Save() {
			Save(_workResousesDir.FullName);
		}
		/// <summary>
		/// 保存资源文件到指定文件夹
		/// </summary>
		/// <param name="basePath"></param>
		public void Save(string basePath) {
			foreach (var item in _res) {
				if (item.Value.DataChanged) {
					item.Value.Save(basePath);
				}
			}
		}

		/// <summary>
		/// 获取描述的xml节点
		/// </summary>
		/// <param name="xmlDocument"></param>
		/// <returns></returns>
		public XmlElement XmlNode(XmlDocument xmlDocument) {
			XmlElement node = xmlDocument.CreateElement("images");

			foreach (KeyValuePair<string, ImageResourse> irkvp in _res) {
				node.AppendChild(irkvp.Value.XmlNode(xmlDocument));
			}

			return node;
		}

		/// <summary>
		/// 从xml节点中初始化类
		/// </summary>
		/// <param name="xmlnode"></param>
		/// <param name="resDir"></param>
		/// <returns></returns>
		public static ImageResoureseManager LoadFromXmlNode(XmlElement xmlnode, DirectoryInfo resDir) {
			if (xmlnode.Name != "images") return null;

			ImageResoureseManager re = new ImageResoureseManager(resDir);

			foreach (XmlElement item in xmlnode.ChildNodes) {
				ImageResourse ir = ImageResourse.LoadFromXmlNode(item, resDir.FullName);
				re.Add(ir);
			}

			return re;
		}

		public ImageResoureseManager (DirectoryInfo resDir) {
			_res = new Dictionary<string, ImageResourse>();
			_imgs = new ObservableCollection<IWorkLogicNodeAble>();
			_workResousesDir = resDir;
		}
	}
}
