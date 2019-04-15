using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ShowPanelType = System.Windows.Controls.ControlTemplate;

namespace WorldCreaterStudio_Core.StoreRoom {
	public static class BackEndNodeStateTemplate {
		public delegate void StateTemplateChangedEvent (object sender, ShowPanelType newPanel);
		public static event StateTemplateChangedEvent UnablePanelChanged;
		public static event StateTemplateChangedEvent ReadyPanelChanged;
		public static event StateTemplateChangedEvent OkPanelChanged;
		public static event StateTemplateChangedEvent OutdatePanelChanged;


		private static ShowPanelType _unablePanel = null;
		private static ShowPanelType _readyPanel = null;
		private static ShowPanelType _okPanel = null;
		private static ShowPanelType _outdatePanel = null;

		public static ShowPanelType UnablePanel { get => _unablePanel; set { _unablePanel = value; UnablePanelChanged?.Invoke (null, value); } }
		public static ShowPanelType ReadyPanel { get => _readyPanel; set { _readyPanel = value; ReadyPanelChanged?.Invoke (null, value); } }
		public static ShowPanelType OkPanel { get => _okPanel; set { _okPanel = value; OkPanelChanged?.Invoke (null, value); } }
		public static ShowPanelType OutdatePanel { get => _outdatePanel; set { _outdatePanel = value; OutdatePanelChanged?.Invoke (null, value); } }
	}
}
