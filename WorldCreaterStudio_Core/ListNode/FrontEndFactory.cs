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
using System.Windows.Media.Imaging;
using System.Xml;
using WorldCreaterStudio_Core.Resouses;

namespace WorldCreaterStudio_Core {
	/// <summary>
	/// 前端工厂
	/// </summary>
	public class FrontEndFactory : IWorkLogicNodeAble {
		#region 属性
		/// <summary>
		/// 获取当前节点所属的Work
		/// </summary>
		public Work Work { get; private set; }

		/// <summary>
		/// 获取节点双击工作节点时在功能面板区展示的面板模板
		/// </summary>
		public ControlTemplate ShowPanel => StoreRoom.ShowPanel.FrontEndFactoryPanel;

		// <summary>
		// [待删]
		// </summary>
		//private ControlTemplate ConfigurationPanel {
		//	get {
		//		if (Configuration == null) return null;
		//		return Configuration.ShowPanel;
		//	}
		//}

		/// <summary>
		/// 获取节点在工作列表中展示的名称
		/// </summary>
		public string NodeName => "前端工厂";

		/// <summary>
		/// 获取或设置节点在工作列表中展示的图标
		/// </summary>
		public ImageSource Icon { get; set; } = WorldCreaterStudio_Resouses.Images.Dark_Icon_FrontEndWork;

		/// <summary>
		/// 获取节点的所有子节点
		/// </summary>
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


		/// <summary>
		/// 获取用于Creater的设置
		/// </summary>
		public MapCreater.Configuration Configuration { get; private set; }

		/// <summary>
		/// 获取Creater
		/// </summary>
		public MapCreater.MapCreater Creater { get; private set; }


		private ImageResourceReferenceManager _imageReferenceManager;
		public ImageResourceReferenceManager ImageReferenceManager {
			get => _imageReferenceManager;
			private set {
				if (_imageReferenceManager != value) {
					//解除绑定
					if (_imageReferenceManager != null) {
						_imageReferenceManager.NodeValueChanged -= Children_NodeValueChanged;
					}
					//添加绑定
					if (value != null) {
						value.NodeValueChanged += Children_NodeValueChanged;
					}
					//更新节点
					_imageReferenceManager = value;

					//更新通知
					NodeValueChanged?.Invoke (this);
					PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Childrens"));
				}
			}
		}


		private FrontendNode.CreatingResault _resaultRandomMap;
		private FrontendNode.CreatingResault _resaultHeightMap;

		public FrontendNode.CreatingResault ResaultHeightMap {
			get => _resaultHeightMap;
			private set {
				if (_resaultHeightMap == value) return;
				_resaultHeightMap = value;
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("ResaultHeightMap"));
				NodeValueChanged?.Invoke (this);
			}
		}

		#endregion
		public event NodeValueChangedEventType NodeValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// 子节点值发生改变时事件方法
		/// </summary>
		/// <param name="node">来源节点</param>
		private void Children_NodeValueChanged (IWorkLogicNodeAble node) {
			Changed = true;
		}

		/// <summary>
		/// 配置节点值发生改变时事件方法
		/// </summary>
		private void Configuration_ValueChanged () {
			Changed = true;
		}

		#region XML相关方法
		/// <summary>
		/// 获取描述节点的XML节点
		/// </summary>
		/// <param name="xmlDocument"></param>
		/// <returns></returns>
		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			XmlElement node = xmlDocument.CreateElement ("FrontEndFactory");
			node.SetAttribute ("creater", Creater == null ? "" : Creater.CreaterProgramSet);

			if (Configuration != null) {
				node.AppendChild (Configuration.XmlNode (xmlDocument, save));
			}


			if (ResaultHeightMap != null) {
				node.AppendChild (ResaultHeightMap.XmlNode (xmlDocument, save));
			}


			if (save) Changed = false;
			return node;
		}

		/// <summary>
		/// 使用xml节点初始化前端工厂
		/// </summary>
		/// <param name="xmlnode">信息来源的XML节点</param>
		public bool InitByXMLNode (XmlElement xmlnode) {
			if (xmlnode.Name != "FrontEndFactory") return false;
			string programSet = xmlnode.Attributes["creater"]?.Value;

			SetCreater (programSet);

			foreach (XmlElement item in xmlnode.ChildNodes) {
				if (item.Name == "images") { //资源引用
				}
				else if (item.Name == "setting") {
					Configuration.LoadFromXMLNode (item);
				}
				else if (item.Name == "Resault") {
					FrontendNode.CreatingResault resault = FrontendNode.CreatingResault.GetResaultByXMLNode (Work, item);
					if (resault != null) ResaultHeightMap = resault;
				}
			}
			return true;
		}

		#endregion

		#region ImageReference相关
		/// <summary>
		/// 添加一个图片资源
		/// </summary>
		/// <param name="key">资源的键</param>
		/// <param name="image">资源</param>
		/// <param name="description">描述信息</param>
		public void Image_Add (string key, BitmapSource image, string description = "") {
			ImageReferenceManager.Add (key, image, description);
		}
		#endregion

		/// <summary>
		/// 设置前端工厂
		/// </summary>
		/// <param name="programSet"></param>
		public void SetCreater (string programSet) {
			MapCreater.MapCreaterFactory createrFactory = StoreRoom.MapCreaterDictionary.GetMapCreaterFactoryByProgramSet (programSet);

			if (createrFactory == null) {
				throw new Exceptions.NoCreaterFactoryException (programSet);
			}
			else {
				SetCreater (createrFactory);
			}

		}

		/// <summary>
		/// 设置前端工厂
		/// </summary>
		/// <param name="createrFactory"></param>
		public void SetCreater (MapCreater.MapCreaterFactory createrFactory) {
			if (createrFactory == null) return;

			Creater = createrFactory.GetACreater ();
			if (Configuration != null) { // 接触数据绑定
				Configuration.ValueChanged -= Configuration_ValueChanged;
			}
			Configuration = createrFactory.GetAConfiguration ();
			Configuration.ValueChanged += Configuration_ValueChanged;

			Changed = true;
		}


		/// <summary>
		/// 创建一个地图
		/// </summary>
		/// <returns></returns>
		public FrontendNode.CreatingResault CreateAMap () {
			ResaultHeightMap = Creater.CreatAMap (Configuration, Work);

			if (Work.BackEndNodes.AMNode.NodeState == BackendNode.NodeState.ok) {
				Work.BackEndNodes.AMNode.NodeState = BackendNode.NodeState.outdate;
			}
			else if (Work.BackEndNodes.AMNode.NodeState == BackendNode.NodeState.unable) {
				Work.BackEndNodes.AMNode.NodeState = BackendNode.NodeState.ready;
			}
			return ResaultHeightMap;
		}

		#region 构造函数和静态获取方法

		public FrontEndFactory (Work parentWork) {
			this.Work = parentWork;
			ImageReferenceManager = new ImageResourceReferenceManager (parentWork);
			//ImageResourceReferenceManager = new Resouses.ImageResourceReferenceManager(parentWork);
			//this.Childrens = ImageResourceReferenceManager.Childrens;
		}

		/// <summary>
		/// 从XML节点中获取一个FrontEndFactory
		/// </summary>
		/// <param name="xmlnode">信息来源的XML节点</param>
		/// <param name="parentWork">所属的工作</param>
		/// <returns></returns>
		public static FrontEndFactory LoadFromXmlNode (XmlElement xmlnode, Work parentWork) {
			FrontEndFactory re = new FrontEndFactory (parentWork);
			if (re.InitByXMLNode (xmlnode)) {
				re.Changed = false;
				return re;
			}
			return null;
		}
		#endregion
	}
}
