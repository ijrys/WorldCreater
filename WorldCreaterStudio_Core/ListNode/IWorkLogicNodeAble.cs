using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace WorldCreaterStudio_Core {
	public interface IWorkLogicNodeAble {
		UIElement ShowPanel { get; }
		string NodeName { get; }
		ImageSource Icon { get; }
		XmlElement XmlNode(XmlDocument xmlDocument);

		IEnumerable<IWorkLogicNodeAble> Childrens { get; }
	}
}
