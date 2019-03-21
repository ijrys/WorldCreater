using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using WorldCreaterStudio_Core.MapCreater;
namespace RandomTend {
	public class RTConfiguration : Configuration {
		private int _width = 8;
		private int _height = 8;
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

				Width = (1 << value) * _widthBlockNum;
				Height = (1 << value) * _heightBlockNum;
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
				Width = (1 << BlockSize) * value;

				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WidthBlockNum"));
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
				Height = (1 << BlockSize) * value;

				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HeightBlockNum"));
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


		public override int GetHeight() {
			return Height;
		}

		public override int GetRandomSeed() {
			return RandomSeed;
		}

		public override int GetWidth() {
			return Width;
		}

		public override void LoadFromXMLNode(XmlElement xmlnode) {
			throw new NotImplementedException();
		}

		public override XmlElement XmlNode(XmlDocument xmlDocument) {
			throw new NotImplementedException();
		}
	}


	[Guid("7B2C4FC1-378A-47E3-A82D-F32D9CDCB288")]
	public class RandomTendCreater : MapCreater {
		public override string CreaterProgramSet => "MiRaI.RandomTend.RandomTend|0.1";

		public override Guid CreaterGuid => typeof(RandomTendCreater).GUID;

		public override int[,] CreatAMap(Configuration configuration) {
			if (!(configuration is RTConfiguration)) throw new WorldCreaterStudio_Core.Exceptions.IncongruentConfigurationException(typeof(RTConfiguration), configuration.GetType());

			RTConfiguration rtconfig = (configuration as RTConfiguration);

			int[,] re = new int[rtconfig.GetWidth(), rtconfig.GetHeight()];

			return re;
		}
	}

	public class RandomTendCreaterFactory : MapCreaterFactory {
		public override string DisplayName => "Random Tend";

		public override string DisplayType => "Random Tend";

		public override string CreaterProgramSet => "MiRaI.RandomTend.RandomTend|0.1";

		public override Guid CreaterGuid => new Guid("FDAAD2ED-3072-4110-B685-AD1D5139F90B");

		public override Configuration GetAConfiguration() {
			return new RTConfiguration();
		}

		public override MapCreater GetACreater() {
			return new RandomTendCreater();
		}
	}
}
