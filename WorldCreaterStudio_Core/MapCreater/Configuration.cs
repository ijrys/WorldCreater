using System.ComponentModel;
using System.Windows;
using System.Xml;

namespace WorldCreaterStudio_Core.MapCreater {
	/// <summary>
	/// Creater产生地形所需要的配置文件的基类
	/// </summary>
	public abstract class Configuration : INotifyPropertyChanged {
		public delegate void ValueChangedDelegate ();

		public event ValueChangedDelegate ValueChanged;
		public abstract event PropertyChangedEventHandler PropertyChanged;

		//public abstract int GetWidth();
		//public abstract int GetHeight();
		//public abstract int GetRandomSeed();

		///// <summary>
		///// 获取地形的目标宽度
		///// </summary>
		//public abstract int Width { get; }
		///// <summary>
		///// 获取地形的目标高度
		///// </summary>
		//public abstract int Height { get; }
		///// <summary>
		///// 获取生成地形的随机种子
		///// </summary>
		//public abstract int RandomSeed { get; }

		public abstract int GetWidth();
		public abstract int GetHeight();
		public abstract int GetRandomSeed();

		/// <summary>
		/// 获取与Configuration相配套的PanelTemplate
		/// </summary>
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

		protected void ValueChangedEventCalc() {
			ValueChanged?.Invoke();
		}
	}
}
