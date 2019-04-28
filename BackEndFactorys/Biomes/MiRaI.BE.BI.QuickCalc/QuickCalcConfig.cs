using System;
using System.ComponentModel;
using System.Xml;
using WorldCreaterStudio_Core;
using WorldCreaterStudio_Core.BackendNode.Biomes;

namespace MiRaI.BE.BI.QuickCalc {
	public class QuickCalcConfig :
			IBiomesConfigAble {
		public event NodeValueChangedEventType ValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		private System.Windows.Controls.ControlTemplate _showPanel;
		public System.Windows.Controls.ControlTemplate ShowPanel {
			get {
				if (_showPanel == null) {
					_showPanel = new ConfigPanel ().Resources["configTemplate"] as System.Windows.Controls.ControlTemplate;
				}
				return _showPanel;
			}
		}


		public void LoadFromXMLNode (XmlElement xmlnode) {
		}

		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			XmlElement re = xmlDocument.CreateElement ("Config");
			return re;
		}
	}
}
