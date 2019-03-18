using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace WorldCreaterStudio_Core.Resouses {
	/// <summary>
	/// 用于管理在项目节点中用引用的工作资源
	/// </summary>
	class ImageResourceReferenceManager : IWorkLogicNodeAble, INotifyPropertyChanged {
		public UIElement ShowPanel => null;

		public string NodeName => "资源库";

		public ImageSource Icon => throw new NotImplementedException();

		public ObservableCollection<IWorkLogicNodeAble> Childrens { get; private set; } = new ObservableCollection<IWorkLogicNodeAble>();

		public event PropertyChangedEventHandler PropertyChanged;

		public XmlElement XmlNode(XmlDocument xmlDocument) {
			XmlElement node = xmlDocument.CreateElement("images");
			foreach (var item in Childrens) {
				node.AppendChild(item.XmlNode(xmlDocument));
			}

			return node;
		}
	}
}
