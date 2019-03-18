using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace WorldCreaterStudio_Core {
	/// <summary>
	/// 前端工厂
	/// </summary>
	public class FrontEndFactory : IWorkLogicNodeAble {
		public UIElement ShowPanel => null;

		public string NodeName => "前端工厂";

		public ImageSource Icon { get; set; }

		public MapCreater.Configuration Configuration { get; set; }

		public MapCreater.MapCreater Creater { get; set; }

		public ObservableCollection<IWorkLogicNodeAble> Childrens => null;

		public XmlElement XmlNode(XmlDocument xmlDocument) {
			XmlElement node = xmlDocument.CreateElement("FrontEndFactory");
			node.SetAttribute("creater", Creater == null ? "" : Creater.CreaterProgramSet);

			//Todo append configuration

			return node;
		}
	}
}
