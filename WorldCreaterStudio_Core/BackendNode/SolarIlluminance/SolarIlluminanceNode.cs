﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace WorldCreaterStudio_Core.BackendNode.SolarIlluminance {

	/// <summary>
	/// 空气流动节点
	/// </summary>
	public class SolarIlluminanceNode : IWorkLogicNodeAble {

		public Work Work { get; private set; }

		public ControlTemplate ShowPanel => StoreRoom.ShowPanel.BEF_SIPanel;

		public string NodeName => "SolarIlluminance";

		public ImageSource Icon => WorldCreaterStudio_Resouses.Images.Dark_Icon_SINode;

		public ObservableCollection<IWorkLogicNodeAble> Childrens => null;

		public event NodeValueChangedEventType NodeValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		private bool _changed;
		/// <summary>
		/// 获取节点内容是否发生了改变
		/// </summary>
		public bool Changed {
			get => _changed;
			private set {
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

		private NodeState _nodeState = NodeState.unable;
		public NodeState NodeState {
			get => _nodeState;
			set {
				if (value == _nodeState) return;
				_nodeState = value;
				Changed = true;
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("NodeState"));
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("StateDisplayTemplate"));
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("CanCalculater"));

				NodestateChanged?.Invoke (this, value);
			}
		}
		public bool CanCalculater => NodeState != NodeState.unable;
		public event NodeStateChangedDelegate NodestateChanged;

		private ISolarIlluminanceCalculaterAble _calculater = null;
		/// <summary>
		/// 获取或设置模拟器
		/// </summary>
		public ISolarIlluminanceCalculaterAble Calculater {
			get => _calculater;
			set {
				if (_calculater == value) return;
				_calculater = value;
				Changed = true;
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Calculater"));
			}
		}

		private ISolarIlluminanceConfigAble _configuration;
		/// <summary>
		/// 获取SI模拟器所需要的配置对象
		/// </summary>
		public ISolarIlluminanceConfigAble Configuration {
			get => _configuration;
			set {
				//if (_configuration == null) return;
				if (_configuration != null) {
					_configuration.ValueChanged -= Children_NodeValueChanged;
				}
				if (value != null) {
					value.ValueChanged += Children_NodeValueChanged;
				}
				_configuration = value;
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Configuration"));
			}
		}

		private ISolarIlluminanceCalculaterFactoryAble _factory;
		public ISolarIlluminanceCalculaterFactoryAble Factory {
			get => _factory;
			set {
				Configuration = value.GetAConfiguration ();
				Calculater = value.GetACalculater ();
				_factory = value;
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Factory"));
			}
		}

		private SolarIlluminanceResault _resault;
		public SolarIlluminanceResault Resault {
			get => _resault;
			private set {
				_resault = value;
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Resault"));
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("ResaultImage"));
			}
		}
		public ImageSource ResaultImage => Resault?.ShowImage?.Image;

		public ObservableCollection<IWorkLogicNodeAble> DisplayBGImages {
			get => Work?.Images?.Childrens;
		}

		public ControlTemplate StateDisplayTemplate {
			get {
				ControlTemplate re = null;
				switch (NodeState) {
					case NodeState.unable:
						re = StoreRoom.BackEndNodeStateTemplate.UnablePanel;
						break;
					case NodeState.ready:
						re = StoreRoom.BackEndNodeStateTemplate.ReadyPanel;
						break;
					case NodeState.ok:
						re = StoreRoom.BackEndNodeStateTemplate.OkPanel;
						break;
					case NodeState.outdate:
						re = StoreRoom.BackEndNodeStateTemplate.OutdatePanel;
						break;
					default:
						break;
				}
				return re;
			}
		}

		private void Children_NodeValueChanged (IWorkLogicNodeAble node) {
			Changed = true;
		}

		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			XmlElement node = xmlDocument.CreateElement ("SINode");
			node.SetAttribute ("creater", Calculater == null ? "" : Calculater.CreaterProgramSet);
			node.SetAttribute ("state", NodeState.ToString ());

			if (Resault != null) {
				node.AppendChild (Resault.XmlNode (xmlDocument, save));
			}

			if (Configuration != null) {
				node.AppendChild (Configuration.XmlNode (xmlDocument, save));
			}

			if (save) {
				Changed = false;
			}
			return node;
		}

		public bool InitByXMLNode (XmlElement xmlnode) {
			if (xmlnode.Name != "SINode") return false;
			string creater = xmlnode.Attributes["creater"]?.Value;
			string statestr = xmlnode.Attributes["state"]?.Value;
			NodeState state;
			if (creater == null || statestr == null || !Enum.TryParse (statestr, out state)) return false;
			NodeState = state;

			if (!string.IsNullOrEmpty (creater)) {
				SetCalculater (StoreRoom.BackEndCalculaterDictionary.SolarIlluminance.GetCreaterFactoryByProgramSet (creater));
			}

			foreach (XmlElement item in xmlnode.ChildNodes) {
				if (item.Name == "Resault") {
					Resault = SolarIlluminanceResault.InitByXMLNode (item, Work);
				}
				else if (item.Name == "Config") {
					Configuration.LoadFromXMLNode (item);
				}
			}

			return true;
		}

		/// <summary>
		/// 设置模拟器
		/// </summary>
		/// <param name="factory">模拟器产生工厂</param>
		public void SetCalculater (ISolarIlluminanceCalculaterFactoryAble factory) {
			Calculater = factory.GetACalculater ();
			Configuration = factory.GetAConfiguration ();
		}

		public void StartCalculating () {
			//if (Work.BackEndNodes.RMNode.Resault == null ||
			//	NodeState == NodeState.unable) {
			//	return;
			//}
			//if (Calculater == null) return;
			//Resault = Calculater.GetSolarIlluminanceDatas (Configuration, Work.FrontEndNodes.HeightMap.Value, this.Work);

			//this.NodeState = NodeState.ok;

			//if (Work.BackEndNodes.BINode.NodeState == BackendNode.NodeState.ok) {
			//	Work.BackEndNodes.BINode.NodeState = BackendNode.NodeState.outdate;
			//}
			//else if (Work.BackEndNodes.BINode.NodeState == BackendNode.NodeState.unable) {
			//	Work.BackEndNodes.BINode.NodeState = BackendNode.NodeState.ready;
			//}
			if (Work.BackEndNodes.RMNode.Resault == null ||
				Work.FrontEndNodes.ResaultHeightMap?.Value == null ||
				NodeState == NodeState.unable) {
				return;
			}
			if (Calculater == null) return;
			Resault = Calculater.GetSolarIlluminanceDatas (Configuration, Work.FrontEndNodes.ResaultHeightMap.Value, this.Work);

			this.NodeState = NodeState.ok;

			//if (Work.BackEndNodes.BINode.NodeState == BackendNode.NodeState.ok) {
			//	Work.BackEndNodes.BINode.NodeState = BackendNode.NodeState.outdate;
			//}
			//else if (Work.BackEndNodes.BINode.NodeState == BackendNode.NodeState.unable) {
			//	Work.BackEndNodes.BINode.NodeState = BackendNode.NodeState.ready;
			//}
		}

		public SolarIlluminanceNode (Work work) {
			this.Work = work;
		}
	}
}
