using System.ComponentModel;
using System.Xml;
using WorldCreaterStudio_Core;
using WorldCreaterStudio_Core.BackendNode.SolarIlluminance;

namespace MiRaI.BE.SI.QuickCalculating {
	public class QuickCalculatingConfig :
		ISolarIlluminanceConfigAble {
		public event NodeValueChangedEventType ValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		private double _angle;
		/// <summary>
		/// 获取或设置光照角度
		/// </summary>
		public double Angle {
			get => _angle;
			set {
				_angle = value;
				ValueChanged?.Invoke (null);
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Angle"));
			}
		}

		private System.Windows.Controls.ControlTemplate _showPanel;
		public System.Windows.Controls.ControlTemplate ShowPanel {
			get {
				if (_showPanel == null) {
					_showPanel = new ConfigPanel ().Resources["configTemplate"] as System.Windows.Controls.ControlTemplate;
				}
				return _showPanel;
			}
		}

		/// <summary>
		/// 从XML节点中加载配置数据
		/// </summary>
		/// <param name="xmlnode">数据来源的XML节点</param>
		public void LoadFromXMLNode (XmlElement xmlnode) {
			if (xmlnode.Name != "Config") return;
			string angstr = xmlnode.Attributes["power"]?.Value;
			if (angstr == null) return;


			if (double.TryParse (angstr, out double ang)) {
				Angle = ang;
			}
			else {
				return;
			}
		}

		/// <summary>
		/// 获取配置的XML描述节点
		/// </summary>
		/// <param name="xmlDocument">XML节点所在文档的根节点</param>
		/// <param name="save">是否为保存动作</param>
		/// <returns></returns>
		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			XmlElement re = xmlDocument.CreateElement ("Config");
			re.SetAttribute ("angle", Angle.ToString ());
			return re;
		}
	}
}
