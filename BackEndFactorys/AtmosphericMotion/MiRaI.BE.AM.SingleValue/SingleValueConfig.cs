using System;
using System.ComponentModel;
using System.Xml;
using WorldCreaterStudio_Core;
using WorldCreaterStudio_Core.BackendNode.AtmosphericMotion;

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
		/// 获取用于界面展示的风力
		/// </summary>
		public double DisplayPower {
			get => _power / 20.0;
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


		public void LoadFromXMLNode (XmlElement xmlnode) {
			throw new NotImplementedException ();
		}

		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			throw new NotImplementedException ();
		}
	}
}
