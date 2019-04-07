using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WorldCreaterStudio_Core.ListNode {
	/// <summary>
	/// 数据生成器在生成数据时进度改变
	/// </summary>
	/// <param name="permillage">过程的千分率</param>
	/// <param name="processDescription">当前进度描述</param>
	/// <param name="freshImage">是否产生了新的预览图</param>
	/// <param name="image">新的预览图</param>
	public delegate void DataCalculatingProcessingEventType (short permillage, string processDescription, bool freshImage, System.Windows.Media.ImageSource image);

	/// <summary>
	/// 数据生成及模拟器
	/// </summary>
	public interface IDataCalculaterAble {
		/// <summary>
		/// 获取或设置Creater的名称
		/// </summary>
		string CreaterName { get; }

		/// <summary>
		/// 获取Creater所在的ProgramSet
		/// ProgramSet为Creater能力的唯一标识，ProgramSet相同的Creater将认定为实现效果相同
		/// </summary>
		string CreaterProgramSet { get; }

		/// <summary>
		/// 获取Creater类的GUID
		/// </summary>
		Guid CreaterGuid { get; }

		/// <summary>
		/// 进度改变时的事件
		/// </summary>
		event DataCalculatingProcessingEventType OnProcessingChanged;
	}

	public delegate void ConfigurationValueChangedDelegate ();
	public interface IDataCalculaterConfigurationAble : INotifyPropertyChanged {
		/// <summary>
		/// 获取与Configuration相配套的PanelTemplate
		/// </summary>
		System.Windows.Controls.ControlTemplate ShowPanel { get; }
		/// <summary>
		/// 配置中有值发生改变时事件
		/// </summary>
		event ConfigurationValueChangedDelegate ValueChanged;
		/// <summary>
		/// 从XML节点中加载数据
		/// </summary>
		/// <param name="xmlnode"></param>
		void LoadFromXMLNode (XmlElement xmlnode);
		/// <summary>
		/// 获取描述数据的XML节点
		/// </summary>
		/// <param name="xmlDocument"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		XmlElement XmlNode (XmlDocument xmlDocument, bool save = false);
	}

	public interface IDataCalculaterFactoryAble<CalcT, ConfigT> 
		where CalcT : IDataCalculaterAble
		where ConfigT:IDataCalculaterConfigurationAble {
		/// <summary>
		/// 数据生成及模拟器的展示名称
		/// </summary>
		string DisplayName { get; }
		/// <summary>
		/// 数据生成及模拟器的展示时的所属类型
		/// </summary>
		string DisplayType { get; }

		/// <summary>
		/// 数据生成及模拟器的程序集名称
		/// </summary>
		string CalculaterProgramSet { get; }
		/// <summary>
		/// 数据生成及模拟器的GUID
		/// </summary>
		Guid CalculaterGuid { get; }

		/// <summary>
		/// 获取一个MapCreater
		/// </summary>
		/// <returns></returns>
		CalcT GetACalculater ();

		/// <summary>
		/// 获取一个适用的Configuration
		/// </summary>
		/// <returns></returns>
		ConfigT GetAConfiguration ();
	}
}
