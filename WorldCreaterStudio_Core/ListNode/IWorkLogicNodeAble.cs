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
		/// 获取节点所在的工作
		/// </summary>
		Work Work { get; }

		/// <summary>
		/// 获取节点可展示的面板
		/// </summary>
		ControlTemplate ShowPanel { get; }

		/// <summary>
		/// 获取节点在工程树形图中的展示名称
		/// </summary>
		string NodeName { get; }

		/// <summary>
		/// 获取节点在工程树形图中的展示图标
		/// </summary>
		ImageSource Icon { get; }

		/// <summary>
		/// 获取节点的子节点
		/// </summary>
		ObservableCollection<IWorkLogicNodeAble> Childrens { get; }

		/// <summary>
		/// 获取节点是否有值发生了改变
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
		/// <param name="save">指示是否是在进行保存，true为正在保存</param>
		/// <returns></returns>
		XmlElement XmlNode(XmlDocument xmlDocument, bool save = false);

	}
}
