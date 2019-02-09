using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace WorldCreaterStudio_Core {
	public class BackEndFactory : IWorkLogicNodeAble {
		public UIElement ShowPanel { get; set; }

		public string NodeName => "后端工厂";

		public ImageSource Icon { get; set; }

		public IEnumerable<IWorkLogicNodeAble> Childrens => null;

		public XmlElement XmlNode(XmlDocument xmlDocument) {
			throw new NotImplementedException();
		}
	}
}
