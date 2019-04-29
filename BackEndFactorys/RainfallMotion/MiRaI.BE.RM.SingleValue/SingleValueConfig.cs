using System.ComponentModel;
using System.Xml;
using WorldCreaterStudio_Core;
using WorldCreaterStudio_Core.BackendNode.RainfallMotion;

namespace MiRaI.BE.RM.SingleValue {
	public class SingleValueConfig :
		IRainfallMotionConfigAble {
		public event NodeValueChangedEventType ValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		private int _rainfallIntensity;
		/// <summary>
		/// 获取或设置降水强度。每单位时间降水0.01个全局高度单位
		/// </summary>
		public int RainfallIntensity {
			get => _rainfallIntensity;
			set {
				_rainfallIntensity = value;
				ValueChanged?.Invoke (null);
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("RainfallIntensity"));
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("DisplayRainfallIntensity"));
			}
		}

		/// <summary>
		/// 用于展示的真实降水强度
		/// </summary>
		public double DisplayRainfallIntensity {
			get => _rainfallIntensity / 100;
		}

		private int _seaLevel;
		/// <summary>
		/// 获取或设置海平面高度，与高度图高度同单位
		/// </summary>
		public int SeaLevel {
			get => _seaLevel;
			set {
				_seaLevel = value;
				ValueChanged?.Invoke (null);
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("SeaLevel"));
			}
		}

		private System.Windows.Controls.ControlTemplate _showPanel;
		/// <summary>
		/// 获取相配套的界面模板
		/// </summary>
		public System.Windows.Controls.ControlTemplate ShowPanel {
			get {
				if (_showPanel == null) {
					_showPanel = new ConfigPanel ().Resources["configTemplate"] as System.Windows.Controls.ControlTemplate;
				}
				return _showPanel;
			}
		}


		public void LoadFromXMLNode (XmlElement xmlnode) {
			if (xmlnode.Name != "Config") return;
			string ristr = xmlnode.Attributes["ri"]?.Value;
			string sistr = xmlnode.Attributes["si"]?.Value;
			if (ristr == null || sistr == null) return;


			if (int.TryParse (ristr, out int ri) &&
				int.TryParse (sistr, out int si)) {
				RainfallIntensity = ri;
				SeaLevel = si;
			}
			else {
				return;
			}
		}

		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			XmlElement re = xmlDocument.CreateElement ("Config");
			re.SetAttribute ("ri", RainfallIntensity.ToString ());
			re.SetAttribute ("sl", SeaLevel.ToString ());
			return re;
		}
	}
}
