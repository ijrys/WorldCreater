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

		/// <summary>
		/// [待删]
		/// </summary>
		private ControlTemplate ConfigurationPanel {
			get {
				if (Configuration == null) return null;
				return Configuration.ShowPanel;
			}
		}

		/// <summary>
		/// 获取节点在工作列表中展示的名称
		/// </summary>
		public string NodeName => "前端工厂";

		/// <summary>
		/// 获取或设置节点在工作列表中展示的图标
		/// </summary>
		public ImageSource Icon { get; set; } = WorldCreaterStudio_Resouses.Images.Dark_Icon_FrontEndWork;

		/// <summary>
		/// 获取用于Creater的设置
		/// </summary>
		public MapCreater.Configuration Configuration { get; private set; }

		/// <summary>
		/// 获取Creater
		/// </summary>
		public MapCreater.MapCreater Creater { get; private set; }

		/// <summary>
		/// 获取节点的图片资源引用管理器
		/// </summary>
		public Resouses.ImageResourceReferenceManager ImageResourceReferenceManager { get; private set; }

		/// <summary>
		/// 获取节点的所有子节点
		/// </summary>
		public ObservableCollection<IWorkLogicNodeAble> Childrens { get; private set; }


		public ValueResource RandomMap { get => CreateredMapValue?["RVM"]; }

		public ValueResource HeightMap { get => CreateredMapValue?["HVM"]; }

		public Dictionary<string, ValueResource> CreateredMapValue { get; private set; }

		private bool _changed;

		public bool Changed {
			get => _changed;
			set {
				bool oldvalue = _changed;
				_changed = value;
				if (value) {
					NodeValueChanged?.Invoke(this);
				}
				if (value != oldvalue) {
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Changed"));
				}
			}
		}

		public event NodeValueChangedEventType NodeValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region XML相关方法
		/// <summary>
		/// 获取描述节点的XML节点
		/// </summary>
		/// <param name="xmlDocument"></param>
		/// <returns></returns>
		public XmlElement XmlNode(XmlDocument xmlDocument) {
			XmlElement node = xmlDocument.CreateElement("FrontEndFactory");
			node.SetAttribute("creater", Creater == null ? "" : Creater.CreaterProgramSet);

			if (Configuration != null) {
				node.AppendChild(Configuration.XmlNode(xmlDocument));
			}

			if (ImageResourceReferenceManager != null) {
				node.AppendChild(ImageResourceReferenceManager.XmlNode(xmlDocument));
			}

			return node;
		}

		/// <summary>
		/// 使用xml节点初始化前端工厂
		/// </summary>
		/// <param name="xmlnode"></param>
		public bool InitByXMLNode(XmlElement xmlnode) {
			if (xmlnode.Name != "FrontEndFactory") return false;
			string programSet = xmlnode.Attributes["creater"]?.Value;

			SetCreater(programSet);

			foreach (XmlElement item in xmlnode.ChildNodes) {
				if (item.Name == "images") { //资源引用
					Resouses.ImageResourceReferenceManager imageManager = Resouses.ImageResourceReferenceManager.LoadFromXmlNode(item, this.Work); // todo
				} else if (item.Name == "setting") {
					this.Configuration.LoadFromXMLNode(item);
				}
			}

			return true;
		}

		#endregion


		/// <summary>
		/// 设置前端工厂
		/// </summary>
		/// <param name="programSet"></param>
		public void SetCreater(string programSet) {
			MapCreater.MapCreaterFactory createrFactory = StoreRoom.MapCreaterDictionary.GetMapCreaterFactoryByProgramSet(programSet);

			if (createrFactory == null) {
				throw new Exceptions.NoCreaterFactoryException(programSet);
			} else {
				SetCreater(createrFactory);
			}

		}

		/// <summary>
		/// 设置前端工厂
		/// </summary>
		/// <param name="createrFactory"></param>
		public void SetCreater(MapCreater.MapCreaterFactory createrFactory) {
			if (createrFactory == null) return;

			Creater = createrFactory.GetACreater();
			if (Configuration != null) { // 接触数据绑定
				Configuration.ValueChanged -= Configuration_ValueChanged;
			}
			Configuration = createrFactory.GetAConfiguration();
			Configuration.ValueChanged += Configuration_ValueChanged;

			Changed = true;
		}

		private void Configuration_ValueChanged() {
			this.Changed = true;
			//Work?.ChildrenValueChanged(this);
		}

		/// <summary>
		/// 创建一个地图
		/// </summary>
		/// <returns></returns>
		public ValueResource CreateAMap() {
			Creater.CreatAMap(Configuration, Work);
			CreateredMapValue = Creater.CreateredMapValue;
			
			return HeightMap;
		}

		#region 构造函数和静态获取方法

		public FrontEndFactory(Work parentWork) {
			this.Work = parentWork;
			ImageResourceReferenceManager = new Resouses.ImageResourceReferenceManager(parentWork);
			this.Childrens = ImageResourceReferenceManager.Childrens;
		}

		/// <summary>
		/// 从XML节点中加载一个FrontEndFactory
		/// </summary>
		/// <param name="xmlnode"></param>
		/// <returns></returns>
		public static FrontEndFactory LoadFromXmlNode(XmlElement xmlnode, Work parentWork) {
			//if (xmlnode.Name != "FrontEndFactory") return null;
			//string programSet = xmlnode.Attributes["creater"]?.Value;

			//MapCreater.MapCreaterFactory createrFactory = StoreRoom.MapCreaterDictionary.GetMapCreaterFactoryByProgramSet(programSet);
			//MapCreater.MapCreater creater = createrFactory.GetACreater();
			//MapCreater.Configuration configuration = createrFactory.GetAConfiguration();

			//foreach (XmlElement item in xmlnode.ChildNodes) {
			//	if (item.Name == "images") { //资源引用
			//		Resouses.ImageResourceReferenceManager imageManager = Resouses.ImageResourceReferenceManager.LoadFromXmlNode(item, parentWork); // todo
			//	} else if (item.Name == "setting") {
			//		configuration.LoadFromXMLNode(item);
			//	}
			//}

			//re.Creater = creater;
			//re.Configuration = configuration;
			FrontEndFactory re = new FrontEndFactory(parentWork);
			if (re.InitByXMLNode(xmlnode)) return re;
			return null;
		}
		#endregion
	}
}
