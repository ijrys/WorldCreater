using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml;
using WorldCreaterStudio_Core;
using WorldCreaterStudio_Core.BackendNode.AtmosphericMotion;
using WorldCreaterStudio_Core.ListNode;

namespace MiRaI.BE.AM.SingleValue {

	public class SingleValueConfig :
		IAtmosphericMotionConfigAble {
		public event NodeValueChangedEventType ValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		private Direction _direction;
		/// <summary>
		/// 获取或设置风向
		/// </summary>
		public Direction Direction {
			get=> _direction;
			set {
				if (_direction == value) return;
				Direction oldvalue = _direction;
				_direction = value;
				ValueChanged?.Invoke (null);
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Direction"));

				switch (oldvalue) {
					case Direction.C:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToC"));
						break;
					case Direction.NW:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToNW"));
						break;
					case Direction.N:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToN"));
						break;
					case Direction.NE:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToNE"));
						break;
					case Direction.E:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToE"));
						break;
					case Direction.SE:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToSE"));
						break;
					case Direction.S:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToS"));
						break;
					case Direction.SW:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToSW"));
						break;
					case Direction.W:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToW"));
						break;
					default:
						break;
				}
				switch (value) {
					case Direction.C:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToC"));
						break;
					case Direction.NW:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToNW"));
						break;
					case Direction.N:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToN"));
						break;
					case Direction.NE:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToNE"));
						break;
					case Direction.E:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToE"));
						break;
					case Direction.SE:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToSE"));
						break;
					case Direction.S:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToS"));
						break;
					case Direction.SW:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToSW"));
						break;
					case Direction.W:
						PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("IsToW"));
						break;
					default:
						break;
				}
			}
		}

		#region 风向的九个按钮
		public bool IsToC {
			get { return Direction == Direction.C; }
			set {
				if (value) { Direction = Direction.C; }
			}
		}
		public bool IsToNW {
			get { return Direction == Direction.NW; }
			set {
				if (value) { Direction = Direction.NW; }
				else { if (Direction == Direction.NW) { Direction = Direction.C; } }
			}
		}
		public bool IsToN {
			get { return Direction == Direction.N; }
			set {
				if (value) { Direction = Direction.N; }
				else { if (Direction == Direction.N) { Direction = Direction.C; } }
			}
		}
		public bool IsToNE {
			get { return Direction == Direction.NE; }
			set {
				if (value) { Direction = Direction.NE; }
				else { if (Direction == Direction.NE) { Direction = Direction.C; } }
			}
		}
		public bool IsToE {
			get { return Direction == Direction.E; }
			set {
				if (value) { Direction = Direction.E; }
				else { if (Direction == Direction.E) { Direction = Direction.C; } }
			}
		}
		public bool IsToSE {
			get { return Direction == Direction.SE; }
			set {
				if (value) { Direction = Direction.SE; }
				else { if (Direction == Direction.SE) { Direction = Direction.C; } }
			}
		}
		public bool IsToS {
			get { return Direction == Direction.S; }
			set {
				if (value) { Direction = Direction.S; }
				else { if (Direction == Direction.S) { Direction = Direction.C; } }
			}
		}
		public bool IsToSW {
			get { return Direction == Direction.SW; }
			set {
				if (value) { Direction = Direction.SW; }
				else { if (Direction == Direction.SW) { Direction = Direction.C; } }
			}
		}
		public bool IsToW {
			get { return Direction == Direction.W; }
			set {
				if (value) { Direction = Direction.W; }
				else { if (Direction == Direction.W) { Direction = Direction.C; } }
			}
		}
		#endregion

		private byte _power;
		/// <summary>
		/// 获取或设置风力
		/// </summary>
		public byte Power {
			get => _power;
			set {
				_power = value;
				ValueChanged?.Invoke (null);
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Power"));
				PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("DisplayPower"));
			}
		}

		/// <summary>
		/// 用于展示的真实风力
		/// </summary>
		public double DisplayPower {
			get => _power / 20;
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
		IAtmosphericMotionCalculaterAble {
		public string CreaterName => "单一值风力设定";

		public string CreaterProgramSet => "MiRaI.BE.AM.SV|0.1";

		public Guid CreaterGuid => typeof (SingleValue).GUID;

		public event DataCalculatingProcessingEventType OnProcessingChanged;

		public AtmosphericMotionResault GetAtmosphericMotionDatas (SingleValueConfig config, int[,] heightMap, Work work) {
			int w = heightMap.GetLength (0) - 1, h = heightMap.GetLength (1) - 1;
			PointData[,] recont = new PointData[w, h];
			PointData data = new PointData () {
				direction = config.Direction,
				power = config.Power,
			};
			
			for (int i = 0; i < w; i ++) {
				for (int j = 0; j < h; j ++) {
					recont[i, j] = data;
				}
			}

			BitmapSource image = ValueToImage.AtmosphericMotionToImage.GetBitmap (recont);
			work.Images.Add ("BE.AM.Map", image, "AtmosphericMotion Visual Map");
			AtmosphericMotionResault re = new AtmosphericMotionResault (recont, "AtmosphericMotion Visual Map", work, "BE.AM.Map");
			return re;
		}

		public AtmosphericMotionResault GetAtmosphericMotionDatas (IAtmosphericMotionConfigAble config, int[,] heightMap) {
			if (!(config is SingleValueConfig)) throw new WorldCreaterStudio_Core.Exceptions.IncongruentConfigurationException (typeof (SingleValueConfig), config.GetType ());
			return GetAtmosphericMotionDatas (config as SingleValueConfig, heightMap);
		}
	}
}
