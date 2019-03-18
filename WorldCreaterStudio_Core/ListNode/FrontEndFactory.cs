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

		public UIElement ShowPanel => null;

		public string NodeName => "前端工厂";

		public ImageSource Icon { get; set; }

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


			node.AppendChild(Configuration.XmlNode(xmlDocument));

			node.AppendChild(ImageResourceReferenceManager.XmlNode(xmlDocument));

			return node;
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
			if (xmlnode.Name != "FrontEndFactory") return null;
			string programSet = xmlnode.Attributes["creater"]?.Value;

			MapCreater.MapCreaterFactory createrFactory = StoreRoom.MapCreaterDictionary.GetMapCreaterFactoryByProgramSet(programSet);
			MapCreater.MapCreater creater = createrFactory.GetACreater();
			MapCreater.Configuration configuration = createrFactory.GetAConfiguration();

			foreach (XmlElement item in xmlnode.ChildNodes) {
				if (item.Name == "images") { //资源引用
					Resouses.ImageResourceReferenceManager imageManager = Resouses.ImageResourceReferenceManager.LoadFromXmlNode(item, parentWork); // todo
				} else if (item.Name == "setting") {
					configuration.LoadFromXMLNode(item);
				}
			}

			FrontEndFactory re = new FrontEndFactory(parentWork);
			re.Creater = creater;
			re.Configuration = configuration;
			return re;
		} 
		#endregion
	}
}
