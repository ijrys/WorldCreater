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
		public static event ShowPanelChangedEvent BackEndFactoryPanelChanged;
		public static event ShowPanelChangedEvent BEF_AMPanelChanged;
		public static event ShowPanelChangedEvent BEF_RMPanelChanged;
		public static event ShowPanelChangedEvent BEF_SIPanelChanged;


		private static ShowPanelType _imagePanel = null;
		private static ShowPanelType _workPanel = null;
		private static ShowPanelType _frontEndFactoryPanel = null;
		private static ShowPanelType _backEndFactoryPanel = null;
		private static ShowPanelType _bef_AMPanel = null;
		private static ShowPanelType _bef_RMPanel = null;
		private static ShowPanelType _bef_SIPanel = null;

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

		/// <summary>
		/// 用于展示后端工厂的信息
		/// </summary>
		public static ShowPanelType BackEndFactoryPanel { get => _backEndFactoryPanel; set { _backEndFactoryPanel = value; BackEndFactoryPanelChanged?.Invoke(null, value); } }

		/// <summary>
		/// 用于展示后端工厂.AM的信息
		/// </summary>
		public static ShowPanelType BEF_AMPanel { get => _bef_AMPanel; set { _bef_AMPanel = value; BEF_AMPanelChanged?.Invoke(null, value); } }

		/// <summary>
		/// 用于展示后端工厂.RM的信息
		/// </summary>
		public static ShowPanelType BEF_RMPanel { get => _bef_RMPanel; set { _bef_RMPanel = value; BEF_RMPanelChanged?.Invoke(null, value); } }

		/// <summary>
		/// 用于展示后端工厂.RM的信息
		/// </summary>
		public static ShowPanelType BEF_SIPanel { get => _bef_SIPanel; set { _bef_SIPanel = value; BEF_SIPanelChanged?.Invoke (null, value); } }

	}
}
