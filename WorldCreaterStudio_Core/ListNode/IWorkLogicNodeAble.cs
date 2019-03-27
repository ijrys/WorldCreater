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

namespace WorldCreaterStudio_Core {
	public delegate void NodeValueChangedEventType(IWorkLogicNodeAble node);

	/// <summary>
	/// 工作中的逻辑节点
	/// </summary>
	public interface IWorkLogicNodeAble: INotifyPropertyChanged {
		/// <summary>
		/// 节点所在的工作
		/// </summary>
		Work Work { get; }

		/// <summary>
		/// 节点可展示的面板
		/// </summary>
		ControlTemplate ShowPanel { get; }

		/// <summary>
		/// 节点展示名称
		/// </summary>
		string NodeName { get; }

		/// <summary>
		/// 节点展示的图标
		/// </summary>
		ImageSource Icon { get; }

		/// <summary>
		/// 节点的子节点
		/// </summary>
		ObservableCollection<IWorkLogicNodeAble> Childrens { get; }

		/// <summary>
		/// 指示是否有值发生了改变
		/// </summary>
		bool Changed { get; }

		/// <summary>
		/// 节点值发生改变事件，用于通知需要保存
		/// </summary>
		event NodeValueChangedEventType NodeValueChanged;

		/// <summary>
		/// 获取节点的XML节点
		/// </summary>
		/// <param name="xmlDocument"></param>
		/// <returns></returns>
		XmlElement XmlNode(XmlDocument xmlDocument);

	}
}
