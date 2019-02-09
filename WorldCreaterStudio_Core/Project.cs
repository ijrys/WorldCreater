using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace WorldCreaterStudio_Core {
	public class Project : IWorkLogicNodeAble {
		string _projectPath;

		public UIElement ShowPanel => null;
		public string NodeName { get; private set; }
		public ImageSource Icon { get; private set; }
		public IEnumerable<IWorkLogicNodeAble> Childrens { get; private set; }


		public string ProjectPath {
			get => _projectPath;
			set { _projectPath = value; }
		}

		public XmlElement XmlNode(XmlDocument xmlDocument) {
			throw new NotImplementedException();
		}
	}
}
