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
	public abstract class WorkLogicNode : IWorkLogicNodeAble {
		public abstract Work Work { get; }

		public UIElement ShowPanel { get; set; }
		public string NodeName { get; set; }
		public ImageSource Icon { get; set; }

		public abstract XmlElement XmlNode(XmlDocument xmlDocument);

		public ObservableCollection<IWorkLogicNodeAble> Childrens { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	}
}
