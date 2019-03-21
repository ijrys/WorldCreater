using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WorldCreaterStudio_Core.StoreRoom {
	using ShowPanelType = System.Windows.Controls.ControlTemplate;
	/// <summary>
	/// 存储各种用于展示的面板，由WorldCreaterStudio注入
	/// </summary>
	public static class ShowPanel {
		public delegate void ShowPanelChangedEvent(object sender, ShowPanelType newPanel);
		public static event ShowPanelChangedEvent ImagePanelChanged;
		public static event ShowPanelChangedEvent WorkPanelChanged;
		public static event ShowPanelChangedEvent FrontEndFactoryPanelChanged;

		private static ShowPanelType _imagePanel = null;
		private static ShowPanelType _workPanel = null;
		private static ShowPanelType _frontEndFactoryPanel = null;

		/// <summary>
		/// 用于展示图片资源
		/// </summary>
		public static ShowPanelType ImagePanel { get => _imagePanel; set { _imagePanel = value; ImagePanelChanged?.Invoke(null, value); } }

		/// <summary>
		/// 用于展示工作的信息
		/// </summary>
		public static ShowPanelType WorkPanel { get => _workPanel; set { _workPanel = value; WorkPanelChanged?.Invoke(null, value); } }

		/// <summary>
		/// 用于展示前端工厂的信息
		/// </summary>
		public static ShowPanelType FrontEndFactoryPanel { get => _frontEndFactoryPanel; set { _frontEndFactoryPanel = value; FrontEndFactoryPanelChanged?.Invoke(null, value); } }
	}
}
