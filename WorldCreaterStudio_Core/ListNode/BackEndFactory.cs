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
using WorldCreaterStudio_Core.BackendNode;

namespace WorldCreaterStudio_Core {

	public class BackEndFactory : IWorkLogicNodeAble {
		private bool isinit = false;

		public Work Work { get; private set; }

		public ControlTemplate ShowPanel => StoreRoom.ShowPanel.BackEndFactoryPanel;

		public string NodeName => "后端工厂";

		public ImageSource Icon { get; set; } = WorldCreaterStudio_Resouses.Images.Dark_Icon_BackEndWork;

		public ObservableCollection<IWorkLogicNodeAble> Childrens { get; private set; }

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
				return _amNode;
			}
			private set {
				if (_amNode == value) return;
				if (_amNode != null) {
					_amNode.NodeValueChanged -= ChildNodeValueChanged;
					_amNode.NodestateChanged -= AM_NodestateChanged;
				}
				_amNode = value;
				if (value != null) {
					value.NodeValueChanged += ChildNodeValueChanged;
					value.NodestateChanged += AM_NodestateChanged;
				}
				if (!isinit) {
					NodeValueChanged?.Invoke (this);
				}
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("AMNode"));
			}
		}

		private BackendNode.RainfallMotion.RainfallMotionNode _rmNode;
		public BackendNode.RainfallMotion.RainfallMotionNode RMNode {
			get {
				return _rmNode;
			}
			private set {
				if (_rmNode == value) return;
				if (_rmNode != null) {
					_rmNode.NodeValueChanged -= ChildNodeValueChanged;
					//_rmNode.NodestateChanged -= RM_NodestateChanged; ;
				}
				_rmNode = value;
				if (value != null) {
					value.NodeValueChanged += ChildNodeValueChanged;
					//value.NodestateChanged += AM_NodestateChanged;
				}
				if (!isinit) {
					NodeValueChanged?.Invoke (this);
				}
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("RMNode"));
			}
		}

		private BackendNode.SolarIlluminance.SolarIlluminanceNode _siNode;
		public BackendNode.SolarIlluminance.SolarIlluminanceNode SINode {
			get {
				return _siNode;
			}
			private set {
				if (_siNode == value) return;
				if (_siNode != null) {
					_siNode.NodeValueChanged -= ChildNodeValueChanged;
					//_siNode.NodestateChanged -= SI_NodestateChanged;
				}
				_siNode = value;
				if (value != null) {
					value.NodeValueChanged += ChildNodeValueChanged;
					//value.NodestateChanged += AM_NodestateChanged;
				}
				if (!isinit) {
					NodeValueChanged?.Invoke (this);
				}
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("SINode"));
			}
		}

		private BackendNode.Biomes.BiomesNode _biNode;
		public BackendNode.Biomes.BiomesNode BINode {
			get {
				return _biNode;
			}
			private set {
				if (_biNode == value) return;
				if (_biNode != null) {
					_biNode.NodeValueChanged -= ChildNodeValueChanged;
					//_amNode.NodestateChanged -= AM_NodestateChanged; ;
				}
				_biNode = value;
				if (value != null) {
					value.NodeValueChanged += ChildNodeValueChanged;
					//value.NodestateChanged += AM_NodestateChanged; ;
				}
				if (!isinit) {
					NodeValueChanged?.Invoke (this);
				}
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("BINode"));
			}
		}

		#region NodeStateChanged
		private void AM_NodestateChanged (object sender, NodeState newvalue) {
			if (newvalue == NodeState.ok && RMNode != null) {
				if (RMNode.NodeState == NodeState.ok) {
					RMNode.NodeState = NodeState.outdate;
				}
				else if (RMNode.NodeState == NodeState.unable) {
					RMNode.NodeState = NodeState.ready;
				}
			}
		}

		private void RM_NodestateChanged (object sender, NodeState newvalue) {
			if (newvalue == NodeState.ok && SINode != null) {
				if (SINode.NodeState == NodeState.ok) {
					SINode.NodeState = NodeState.outdate;
				}
				else if (SINode.NodeState == NodeState.unable) {
					SINode.NodeState = NodeState.ready;
				}
			}
		}

		private void SI_NodestateChanged (object sender, NodeState newvalue) {
			if (newvalue == NodeState.ok && BINode != null) {
				if (BINode.NodeState == NodeState.ok) {
					BINode.NodeState = NodeState.outdate;
				}
				else if (BINode.NodeState == NodeState.unable) {
					BINode.NodeState = NodeState.ready;
				}
			}
		}
		#endregion

		private void ChildNodeValueChanged (IWorkLogicNodeAble node) {
			if (!isinit) {
				this.Changed = true;
			}
		}


		#region XML相关
		public bool InitByXMLNode (XmlElement xmlnode) {
			if (xmlnode.Name != "BackEndFactory") return false;
			isinit = true;

			foreach (XmlElement item in xmlnode.ChildNodes) {
				if (item.Name == "AMNode") { //资源引用
					AMNode.InitByXMLNode (item);
				}
			}

			isinit = false;
			return true;
		}

		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			XmlElement re = xmlDocument.CreateElement ("BackEndFactory");
			re.AppendChild (AMNode.XmlNode (xmlDocument, save));
			return re;
		}
		#endregion

		public BackEndFactory (Work work) {
			this.Work = work;
			Childrens = new ObservableCollection<IWorkLogicNodeAble> ();

			AMNode = new BackendNode.AtmosphericMotion.AtmosphericMotionNode (work);
			Childrens.Add (AMNode);
			AMNode.NodeState = BackendNode.NodeState.unable;

			RMNode = new BackendNode.RainfallMotion.RainfallMotionNode (work);
			Childrens.Add (RMNode);
			RMNode.NodeState = BackendNode.NodeState.unable;

			SINode = new BackendNode.SolarIlluminance.SolarIlluminanceNode (work);
			Childrens.Add (SINode);
			SINode.NodeState = BackendNode.NodeState.unable;

			BINode = new BackendNode.Biomes.BiomesNode (work);
			Childrens.Add (BINode);
			BINode.NodeState = BackendNode.NodeState.unable;
		}
	}
}
