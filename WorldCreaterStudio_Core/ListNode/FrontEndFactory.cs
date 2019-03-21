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
	/// <summary>
	/// 前端工厂
	/// </summary>
	public class FrontEndFactory : IWorkLogicNodeAble {
		/// <summary>
		/// 当前节点所属的Work
		/// </summary>
		public Work Work { get; private set; }

		public FrameworkElement ShowPanel => StoreRoom.ShowPanel.FrontEndFactoryPanel;
		public FrameworkElement ConfigurationPanel {
			get {
				if (Configuration == null) return null;
				return Configuration.ShowPanel;
			}
		}

		public string NodeName => "前端工厂";

		public ImageSource Icon { get; set; } = WorldCreaterStudio_Resouses.Images.Dark_Icon_FrontEndWork;

		public MapCreater.Configuration Configuration { get; private set; }

		public MapCreater.MapCreater Creater { get; private set; }

		public Resouses.ImageResourceReferenceManager ImageResourceReferenceManager { get; private set; }

		public ObservableCollection<IWorkLogicNodeAble> Childrens { get; private set; }


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
			Configuration = createrFactory.GetAConfiguration();
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
