using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace WorldCreaterStudio_Core {
	public class BackEndFactory : IWorkLogicNodeAble {
		public Work Work { get; private set; }

		public ControlTemplate ShowPanel { get; set; }

		public string NodeName => "后端工厂";

		public ImageSource Icon { get; set; }

		public ObservableCollection<IWorkLogicNodeAble> Childrens => null;

		public bool Changed => throw new NotImplementedException();

		public event NodeValueChangedEventType NodeValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		public XmlElement XmlNode(XmlDocument xmlDocument) {
			throw new NotImplementedException();
		}

		public BackEndFactory (Work work) {
			this.Work = work;
		}
	}
}
