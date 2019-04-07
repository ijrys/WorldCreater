using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WorldCreaterStudio_Core.BackendNode.AtmosphericMotion;
using WorldCreaterStudio_Core.ListNode;

namespace MiRaI.BE.AM.SingleValue {

	public class SingleValueConfig :
		IAtmosphericMotionConfigAble {
		public event ConfigurationValueChangedDelegate ValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		private Direction _direction;
		/// <summary>
		/// 获取或设置风向
		/// </summary>
		public Direction Direction {
			get=> _direction;
			set {
				_direction = value;
				ValueChanged?.Invoke ();
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Direction"));
			}
		}

		private byte _power;
		/// <summary>
		/// 获取或设置风力
		/// </summary>
		public byte Power {
			get => _power;
			set {
				_power = value;
				ValueChanged?.Invoke ();
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Power"));
			}
		}

		public System.Windows.Controls.ControlTemplate ShowPanel => throw new NotImplementedException ();

		public void LoadFromXMLNode (XmlElement xmlnode) {
			throw new NotImplementedException ();
		}

		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			throw new NotImplementedException ();
		}
	}

	/// <summary>
	/// 设置单值的模拟器
	/// </summary>
	public class SingleValue :
		IAtmosphericMotionCalculaterAble<SingleValueConfig> {
		public string CreaterName => "单一值风力设定";

		public string CreaterProgramSet => "MiRaI.BE.AM.SV|0.1";

		public Guid CreaterGuid => typeof (SingleValue).GUID;

		public event DataCalculatingProcessingEventType OnProcessingChanged;

		public PointData[,] GetAtmosphericMotionDatas (SingleValueConfig config, int[,] heightMap) {
			int w = heightMap.GetLength (0) - 1, h = heightMap.GetLength (1) - 1;
			PointData[,] re = new PointData[w, h];
			PointData data = new PointData () {
				direction = config.Direction,
				power = config.Power,
			};
			
			for (int i = 0; i < w; i ++) {
				for (int j = 0; j < h; j ++) {
					re[i, j] = data;
				}
			}
			
			return re;
		}
	}
}
