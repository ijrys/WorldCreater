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

namespace WorldCreaterStudio_Core.Resouses {
	/// <summary>
	/// 用于管理整个工作中使用到的图片资源
	/// </summary>
	public class ImageResourceManager : IWorkLogicNodeAble {
		private Dictionary<string, ImageResourse> _res;
		private ObservableCollection<IWorkLogicNodeAble> _imgs;
		private DirectoryInfo _workResousesDir;
		private ImageSource _icon;

		public event PropertyChangedEventHandler PropertyChanged;
		public event NodeValueChangedEventType NodeValueChanged;

		public ImageResourse this[string key] {
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

		public Work Work { get; private set; }

		public System.Windows.Controls.ControlTemplate ShowPanel => null;

		public string NodeName => "Image Resourese";

		public ImageSource Icon => WorldCreaterStudio_Resouses.Images.Dark_Icon_ResLib;

		public ObservableCollection<IWorkLogicNodeAble> Childrens {
			get { return _imgs; }
		}

		private bool _changed;

		public bool Changed {
			get => _changed;
			set {
				bool oldvalue = _changed;
				_changed = value;
				if (value) {
					NodeValueChanged?.Invoke(this);
				}
				if (value != oldvalue) {
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Changed"));
				}
			}
		}


		/// <summary>
		/// 添加一个资源
		/// </summary>
		/// <param name="key"></param>
		/// <param name="image"></param>
		/// <param name="description"></param>
		public ImageResourse Add(string key, BitmapSource image, string description) {
			Changed = true;
			if (_res.ContainsKey(key)) {
				_res[key].Image = image;
				return _res[key];
			} else {
				ImageResourse imgres  = new ImageResourse(key) {
					Description = description,
					Image = image,
				};
				_res[key] = imgres;
				_imgs.Add(imgres);
				return imgres;
			}
		}

		/// <summary>
		/// 添加一个资源
		/// </summary>
		/// <param name="resourse"></param>
		public void Add(ImageResourse resourse) {
			Changed = true;
			string key = resourse.Key;
			if (_res.ContainsKey(key)) {
				_res[key].Image = resourse.Image;
			} else {
				_res[key] = resourse;
				_imgs.Add(resourse);
			}
		}

		/// <summary>
		/// 移除一个资源
		/// </summary>
		/// <param name="key"></param>
		public void Remove (string key) {
			Changed = true;
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
			Changed = false;
		}

		/// <summary>
		/// 获取描述的xml节点
		/// </summary>
		/// <param name="xmlDocument"></param>
		/// <returns></returns>
		public XmlElement XmlNode(XmlDocument xmlDocument, bool save = false) {
			XmlElement node = xmlDocument.CreateElement("images");

			foreach (KeyValuePair<string, ImageResourse> irkvp in _res) {
				node.AppendChild(irkvp.Value.XmlNode(xmlDocument, save));
			}

			if (save) {
				Save();
				Changed = false;
			}
			return node;
		}

		/// <summary>
		/// 从xml节点中初始化类
		/// </summary>
		/// <param name="xmlnode"></param>
		/// <param name="resDir"></param>
		/// <returns></returns>
		public static ImageResourceManager LoadFromXmlNode(XmlElement xmlnode, DirectoryInfo resDir, Work work) {
			if (xmlnode.Name != "images") return null;

			ImageResourceManager re = new ImageResourceManager(resDir, work);

			foreach (XmlElement item in xmlnode.ChildNodes) {
				ImageResourse ir = ImageResourse.LoadFromXmlNode(item, resDir.FullName);
				re.Add(ir);
			}

			return re;
		}

		public ImageResourceManager (DirectoryInfo resDir, Work work) {
			_res = new Dictionary<string, ImageResourse>();
			_imgs = new ObservableCollection<IWorkLogicNodeAble>();
			_workResousesDir = resDir;

			Work = work;
		}
	}
}
