using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace WorldCreaterStudio_Core.BackendNode.RainfallMotion {
	class RainfallMotionNode : IWorkLogicNodeAble {
		public Work Work { get; private set; }

		public ControlTemplate ShowPanel => StoreRoom.ShowPanel.BEF_RMPanel;

		public string NodeName => "RainfallMotion";

		public ImageSource Icon => null;

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
			}
		}
		public bool CanCalculater => NodeState != NodeState.unable;


		private IRainfallMotionCalculaterAble _calculater = null;
		/// <summary>
		/// 获取或设置模拟器
		/// </summary>
		public IRainfallMotionCalculaterAble Calculater {
			get => _calculater;
			set {
				if (_calculater == value) return;
				_calculater = value;
				Changed = true;
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Calculater"));
			}
		}

		private IRainfallMotionConfigAble _configuration;
		/// <summary>
		/// 获取AM模拟器所需要的配置对象
		/// </summary>
		public IRainfallMotionConfigAble Configuration {
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

		private IRainfallMotionCalculaterFactoryAble _factory;
		public IRainfallMotionCalculaterFactoryAble Factory {
			get => _factory;
			set {
				//if (_factory == value) return;
				Configuration = value.GetAConfiguration ();
				Calculater = value.GetACalculater ();
				_factory = value;
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Factory"));
			}
		}

		private RainfallMotionResault _resault;
		public RainfallMotionResault Resault {
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
			throw new NotImplementedException ();
		}

		/// <summary>
		/// 设置模拟器
		/// </summary>
		/// <param name="factory">模拟器产生工厂</param>
		public void SetCalculater (IRainfallMotionCalculaterFactoryAble factory) {
			Calculater = factory.GetACalculater ();
			Configuration = factory.GetAConfiguration ();
		}

		public void StartCalculating () {
			if (Work.FrontEndNodes.HeightMap.Value == null || NodeState == NodeState.unable) {
				return;
			}
			if (Calculater == null) return;
			Resault = Calculater.GetAtmosphericMotionDatas (Configuration, Work.FrontEndNodes.HeightMap.Value, this.Work);

		}

		public RainfallMotionNode (Work work) {
			this.Work = work;
		}
	}
}
