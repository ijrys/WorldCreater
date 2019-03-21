using System.ComponentModel;
using System.Windows;
using System.Xml;

namespace WorldCreaterStudio_Core.MapCreater {
	public abstract class Configuration : INotifyPropertyChanged {
		public abstract event PropertyChangedEventHandler PropertyChanged;

		public abstract int GetWidth();
		public abstract int GetHeight();
		public abstract int GetRandomSeed();

		public abstract System.Windows.Controls.ControlTemplate ShowPanel { get; }

		/// <summary>
		/// 从xml节点中加载设置
		/// </summary>
		/// <param name="xmlnode"></param>
		public abstract void LoadFromXMLNode(XmlElement xmlnode);

		/// <summary>
		/// 获取xml描述节点
		/// </summary>
		/// <param name="xmlDocument"></param>
		/// <returns></returns>
		public abstract XmlElement XmlNode(XmlDocument xmlDocument);


	}
}
