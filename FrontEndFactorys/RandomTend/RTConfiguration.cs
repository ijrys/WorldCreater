using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Xml;
using WorldCreaterStudio_Core.MapCreater;

namespace RandomTend {
	public class RTConfiguration : Configuration {
		private int _width = 9;
		private int _height = 9;
		private int _blockSize = 3;
		private int _widthBlockNum = 1;
		private int _heightBlockNum = 1;
		private int _maxBlockNum = 1 << (maxLength - 3);
		private int _randomSeed = 2019;

		private const int maxLength = 17;


		public override event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// 块大小
		/// </summary>
		public int BlockSize {
			get => _blockSize;
			set {
				if (value < 3) value = 3;
				else if (value > 16) value = 16;
				_blockSize = value;

				//计算最大的横向块数
				int maxbn = 1 << (maxLength - value);
				if (WidthBlockNum > maxbn) WidthBlockNum = maxbn;
				if (HeightBlockNum > maxbn) HeightBlockNum = maxbn;

				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BlockSize"));

				Width = (1 << value) * _widthBlockNum + 1;
				Height = (1 << value) * _heightBlockNum + 1;

				ValueChangedEventCalc();
			}
		}

		/// <summary>
		/// 横向块数量
		/// </summary>
		public int WidthBlockNum {
			get => _widthBlockNum;
			set {
				int bs = 1 << BlockSize;
				int valuemax = 1 << (maxLength - BlockSize);
				if (value < 1) value = 1;
				else if (value > valuemax) value = valuemax;

				_widthBlockNum = value;
				Width = (1 << BlockSize) * value + 1;

				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WidthBlockNum"));
				ValueChangedEventCalc();
			}
		}

		/// <summary>
		/// 纵向块数量
		/// </summary>
		public int HeightBlockNum {
			get => _heightBlockNum;
			set {
				int bs = 1 << BlockSize;
				int valuemax = 1 << (maxLength - BlockSize);
				if (value < 1) value = 1;
				else if (value > valuemax) value = valuemax;

				_heightBlockNum = value;
				Height = (1 << BlockSize) * value + 1;

				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HeightBlockNum"));
				ValueChangedEventCalc();
			}
		}


		public int Width {
			get => _width;
			private set {
				_width = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Width"));
			}
		}
		public int Height {
			get => _height;
			private set {
				_height = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Height"));
			}
		}

		public int MaxBlockNum {
			get => _maxBlockNum;
			private set {
				_maxBlockNum = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxBlockNum"));
			}
		}

		/// <summary>
		/// 随机值
		/// </summary>
		public int RandomSeed {
			get => _randomSeed;
			set {
				_randomSeed = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RandomSeed"));
			}
		}

		private ControlTemplate _showPanel = null;



		public override ControlTemplate ShowPanel {
			get {
				if (_showPanel == null) {
					_showPanel = (new ConfigPanel()).Resources["panelTemplate"] as ControlTemplate;
				}
				return _showPanel;
			}
		}

		public override void LoadFromXMLNode(XmlElement xmlnode) {
			if (xmlnode.Name != "setting") return;
			foreach (XmlElement item in xmlnode.ChildNodes) {
				if (item.Name == "add" && item.HasAttribute("key") && item.HasAttribute("value")) {
					int aimvalue;
					int.TryParse(item.GetAttribute("value"), out aimvalue);
					switch (item.GetAttribute("key")) {
						case "blockSize":
							BlockSize = aimvalue;
							break;
						case "wbn":
							WidthBlockNum = aimvalue;
							break;
						case "hbn":
							HeightBlockNum = aimvalue;
							break;
						case "seed":
							RandomSeed = aimvalue;
							break;
						default:
							break;
					}
				}
			}
		}

		public override XmlElement XmlNode(XmlDocument xmlDocument, bool save = false) {
			XmlElement node = xmlDocument.CreateElement("setting");

			XmlElement snode = xmlDocument.CreateElement("add");
			snode.SetAttribute("key", "blockSize");
			snode.SetAttribute("value", BlockSize.ToString());
			node.AppendChild(snode);

			snode = xmlDocument.CreateElement("add");
			snode.SetAttribute("key", "wbn");
			snode.SetAttribute("value", WidthBlockNum.ToString());
			node.AppendChild(snode);

			snode = xmlDocument.CreateElement("add");
			snode.SetAttribute("key", "hbn");
			snode.SetAttribute("value", HeightBlockNum.ToString());
			node.AppendChild(snode);

			snode = xmlDocument.CreateElement("add");
			snode.SetAttribute("key", "seed");
			snode.SetAttribute("value", RandomSeed.ToString());
			node.AppendChild(snode);

			return node;
		}

		public override int GetWidth() {
			return Width;
		}

		public override int GetHeight() {
			return Height;
		}

		public override int GetRandomSeed() {
			return RandomSeed;
		}

		public override string ToString() {
			string re = 
$"RTConfig\n" +
$"         BlockSize : {BlockSize}\n" +
$"     WidthBlockNum : {WidthBlockNum}\n" +
$"    HeightBlockNum : {HeightBlockNum}\n" +
$"        RandomSeed : {RandomSeed}\n" +
$"      ==== ==== summary ==== ====\n" +
$"             Width : {Width}\n" +
$"            Height : {Height}";
			return re;
		}
	}
}
