using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace WorldCreaterStudio_Core.Resouses {
	/// <summary>
	/// 用于管理在项目节点中用引用的工作资源
	/// </summary>
	public class ImageResourceReferenceManager : IWorkLogicNodeAble {
		/// <summary>
		/// 节点所在的工作
		/// </summary>
		public Work Work { get; private set; }

		/// <summary>
		/// 节点可展示的面板
		/// </summary>
		public System.Windows.Controls.ControlTemplate ShowPanel => null;

		/// <summary>
		/// 节点展示名称
		/// </summary>
		public string NodeName => "资源库";

		/// <summary>
		/// 节点展示的图标
		/// </summary>
		public ImageSource Icon => null;

		/// <summary>
		/// 节点的子节点
		/// </summary>
		public ObservableCollection<IWorkLogicNodeAble> Childrens { get; private set; } = new ObservableCollection<IWorkLogicNodeAble>();

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


		private Dictionary<string, ImageResourceReference> ImageReferences { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		public event NodeValueChangedEventType NodeValueChanged;

		/// <summary>
		/// 获取节点的XML节点
		/// </summary>
		/// <param name="xmlDocument"></param>
		/// <returns></returns>
		public XmlElement XmlNode(XmlDocument xmlDocument, bool save = false) {
			XmlElement node = xmlDocument.CreateElement("images");
			foreach (var item in Childrens) {
				node.AppendChild(item.XmlNode(xmlDocument));
			}

			Changed = false;
			return node;
		}


		public ImageResourceReferenceManager(Work work) {
			this.Work = work;
			this.ImageReferences = new Dictionary<string, ImageResourceReference>();
			Changed = false;
		}

		/// <summary>
		/// 从XML节点中加载一个FrontEndFactory
		/// </summary>
		/// <param name="xmlnode"></param>
		/// <returns></returns>
		public static ImageResourceReferenceManager LoadFromXmlNode(XmlElement xmlnode, Work parentWork) {
			if (xmlnode.Name != "images") return null;
			ImageResourceReferenceManager re = new ImageResourceReferenceManager(parentWork);

			foreach (XmlElement item in xmlnode.ChildNodes) {
				if (item.Name == "ir") { //资源引用
										 // todo
				} else {

				}
			}

			return re;
		}

		/// <summary>
		/// 添加一个资源
		/// </summary>
		/// <param name="key"></param>
		/// <param name="image"></param>
		/// <param name="description"></param>
		public void Add(string key, BitmapSource image, string description = "") {
			Work.Images.Add(key, image, description);
			if (!ImageReferences.ContainsKey(key)) {
				ImageReferences[key] = new ImageResourceReference(this.Work, key);
				this.NodeValueChanged?.Invoke(this);
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Childrens"));
			}
		}
	}
}
