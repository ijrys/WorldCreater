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

namespace WorldCreaterStudio_Core.BackendNode.AtmosphericMotion {
	/// <summary>
	/// 方位按照上北下南左西右东确定
	/// NW(1)  N(2)  NE(3)
	///  W(8)  C(0)   E(4)
	/// SW(7)  S(6)  SE(5)
	/// </summary>
	public enum Direction {
		/// <summary>
		/// 中心
		/// </summary>
		C = 0,
		/// <summary>
		/// 西北
		/// </summary>
		NW = 1,
		/// <summary>
		/// 北
		/// </summary>
		N = 2,
		/// <summary>
		/// 东北
		/// </summary>
		NE = 3,
		/// <summary>
		/// 东
		/// </summary>
		E = 4,
		/// <summary>
		/// 东南
		/// </summary>
		SE = 5,
		/// <summary>
		/// 南
		/// </summary>
		S = 6,
		/// <summary>
		/// 西南
		/// </summary>
		SW = 7,
		/// <summary>
		/// 西
		/// </summary>
		W = 8,
	}

	/// <summary>
	/// 表示节点的状态
	/// </summary>
	public enum NodeState {
		/// <summary>
		/// 不可用，条件不充足
		/// </summary>
		unable,
		/// <summary>
		/// 待开始
		/// </summary>
		ready,
		/// <summary>
		/// 已完成
		/// </summary>
		ok,
		/// <summary>
		/// 需更新，在前一节点发生更新后，本节点需要重新计算
		/// </summary>
		outdate
	}

	public struct PointData {
		/// <summary>
		/// 当前节点的风向
		/// </summary>
		public Direction direction;
		/// <summary>
		/// 当前节点的风力
		/// </summary>
		public byte power;
	}

	/// <summary>
	/// 空气流动节点
	/// </summary>
	public class AtmosphericMotionNode : IWorkLogicNodeAble {

		public Work Work { get; private set; }

		public ControlTemplate ShowPanel => null;

		public string NodeName => "AtmosphericMotion";

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
			}
		}

		private IAtmosphericMotionCalculaterAble _calculater = null;
		/// <summary>
		/// 获取或设置模拟器
		/// </summary>
		public IAtmosphericMotionCalculaterAble Calculater {
			get => _calculater;
			set {
				if (_calculater == value) return;
				_calculater = value;
				Changed = true;
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Calculater"));
			}
		}

		private IAtmosphericMotionConfigAble _configuration;
		/// <summary>
		/// 获取AM模拟器所需要的配置对象
		/// </summary>
		public IAtmosphericMotionConfigAble Configuration {
			get => _configuration;
			set {
				if (_configuration == null) return;
				if (_configuration != null) {
					_configuration.ValueChanged -= Children_NodeValueChanged;
				}
				if (value != null) {
					value.ValueChanged += Children_NodeValueChanged;
				}
				_configuration = value;
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
		public void SetCalculater (IAtmosphericMotionCalculaterFactoryAble factory) {
			Calculater = factory.GetACalculater ();
			Configuration = factory.GetAConfiguration ();
		}

		public void StartCalculating () {
			if (Calculater == null) return;
			Calculater.GetAtmosphericMotionDatas (Configuration, Work.FrontEndNodes.HeightMap.Value);

		}

		public AtmosphericMotionNode (Work work) {
			this.Work = work;
		}
	}
}
