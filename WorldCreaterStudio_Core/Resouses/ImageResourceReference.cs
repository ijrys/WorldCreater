using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace WorldCreaterStudio_Core.Resouses {
	/// <summary>
	/// 引用的图片资源
	/// </summary>
	class ImageResourceReference : IWorkLogicNodeAble {
		public Work Work { get; private set; }

		public System.Windows.Controls.ControlTemplate ShowPanel => null;

		public string NodeName => throw new NotImplementedException();

		public ImageSource Icon => throw new NotImplementedException();

		public ObservableCollection<IWorkLogicNodeAble> Childrens => throw new NotImplementedException();

		public ImageResourse ReferencedResourse;

		public XmlElement XmlNode(XmlDocument xmlDocument) {
			throw new NotImplementedException();
		}

		public ImageResourceReference (Work work) {
			this.Work = work;
		}

		public static ImageResourceReference LoadFromXmlNode(XmlElement xmlnode, Work parentWork) {
			if (xmlnode.Name != "ir") return null;
			ImageResourceReference re = new ImageResourceReference(parentWork);

			foreach (XmlElement item in xmlnode.ChildNodes) {
				if (item.Name == "ir") { //资源引用
										 // todo
				} else {

				}
			}

			return re;
		}
	}
}
