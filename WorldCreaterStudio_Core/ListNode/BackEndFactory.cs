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
		private bool isinit = false;

		public Work Work { get; private set; }

		public ControlTemplate ShowPanel { get; set; }

		public string NodeName => "后端工厂";

		public ImageSource Icon { get; set; }

		public ObservableCollection<IWorkLogicNodeAble> Childrens => null;

		private bool _changed;

		public bool Changed {
			get => _changed;
			set {
				bool oldvalue = _changed;
				_changed = value;
				if (value) {
					NodeValueChanged?.Invoke (this);
				}
				if (value != oldvalue) {
					PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Changed"));
				}
			}
		}

		public event NodeValueChangedEventType NodeValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		private BackendNode.AtmosphericMotion.AtmosphericMotionNode _amNode;
		public BackendNode.AtmosphericMotion.AtmosphericMotionNode AMNode {
			get {
				//if (_amNode == null) {
				//	_amNode = new BackendNode.AtmosphericMotion.AtmosphericMotionNode ();
				//}
				return _amNode;
			}
			private set {
				_amNode = value;
				if (!isinit) {
					NodeValueChanged?.Invoke (this);
				}
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("AMNode"));
			}
		}

		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			//throw new NotImplementedException();
			return null;
		}

		public BackEndFactory (Work work) {
			this.Work = work;

			AMNode = new BackendNode.AtmosphericMotion.AtmosphericMotionNode (work);
		}
	}
}
