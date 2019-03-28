using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;
using WorldCreaterStudio_Core.MapCreater;
using WorldCreaterStudio_Core.Resouses;

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
			throw new NotImplementedException();
		}

		public override XmlElement XmlNode(XmlDocument xmlDocument, bool save = false) {
			return xmlDocument.CreateElement("setting");
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
	}


	[Guid("7B2C4FC1-378A-47E3-A82D-F32D9CDCB288")]
	public class RandomTendCreater : MapCreater {
		public override string CreaterProgramSet => "MiRaI.RandomTend.RandomTend|0.1";

		public override Guid CreaterGuid => typeof(RandomTendCreater).GUID;

		public override ValueResource CreatAMap(Configuration configuration, WorldCreaterStudio_Core.Work work) {
			//设置检查
			if (!(configuration is RTConfiguration)) throw new WorldCreaterStudio_Core.Exceptions.IncongruentConfigurationException(typeof(RTConfiguration), configuration.GetType());
			RTConfiguration rtconfig = (configuration as RTConfiguration);

			MapCreatingProcessing(0, "正在准备数据", false, null);

			int blockw = 1 << rtconfig.BlockSize, w = rtconfig.Width, h = rtconfig.Height;

			int[,] _map = new int[h, w]; //高度图
			int[,] _rv = new int[h, w]; //随机值图
			Random r = new Random(rtconfig.RandomSeed);

			for (int i = 0; i < h; i++) {
				Random r1 = new Random(r.Next());
				for (int j = 0; j < w; j++) _rv[i, j] = r.Next(-1048576, 1048576);
			}

			WriteableBitmap bitmap = ValueToImage.ValueToGrayImage.GetBitmapWithError(-1048576, 1048576, _rv);
			MapCreatingProcessing(500, "正在准备数据", true, bitmap);
			if (work != null) {
				work.FrontEndNodes.Image_Add("FE.RandomValue", bitmap, "前端工厂的随机值图");
			}

			for (int i = 0; i < h; i += blockw) {
				for (int j = 0; j < w; j += blockw) {
					_map[i, j] = _rv[i, j];
				}
			}

			int nowpro = 1; //当前进度
			int ranscl = 1;
			for (int nowSetp = blockw; nowSetp >= 1; nowSetp = nowSetp >> 1, nowpro++, ranscl++) {

				#region DoCore
				for (int i = 0; i < h; i += nowSetp) {
					for (int j = nowSetp / 2; j < w; j += nowSetp) {
						int v = _map[i, j - nowSetp / 2] + _map[i, j + nowSetp / 2];
						_map[i, j] = v / 2 + (_rv[i, j] >> ranscl);
					}
				}

				for (int i = nowSetp / 2; i < h; i += nowSetp) {
					for (int j = 0; j < w; j += nowSetp) {
						int v = _map[i - nowSetp / 2, j] + _map[i + nowSetp / 2, j];
						_map[i, j] = v / 2 + (_rv[i, j] >> ranscl);
					}
				}


				for (int i = nowSetp / 2; i < h; i += nowSetp) {
					for (int j = nowSetp / 2; j < w; j += nowSetp) {
						int v1 = _map[i - nowSetp / 2, j] + _map[i + nowSetp / 2, j];
						int v2 = _map[i, j - nowSetp / 2] + _map[i, j + nowSetp / 2];
						_map[i, j] = v1 / 4 + v2 / 4 + (_rv[i, j] >> ranscl);
					}
				}
				#endregion

				int pross = (nowSetp) * (nowSetp);
				pross = 1000 / pross;
				MapCreatingProcessing((short)pross, "正在创建地图", true, ValueToImage.ValueToGrayImage.GetBitmapWithError(-2097152, 2097152, _map));
			}

			//bitmap = ValueToImage.ValueToGrayImage.GetBitmap(-2097152, 2097152, 0, 255, _map);
			bitmap = ValueToImage.ValueToGrayImage.GetBitmapWithError(-2097152, 2097152, _map);
			if (work != null) {
				work.FrontEndNodes.Image_Add("FE.HeightValue", bitmap, "前端工厂高度值图");
			}

			//中间数据放入资源节点
			CreateredMapValue = new Dictionary<string, ValueResource>();
			ValueResource hvm = new ValueResource(_map, "HVM");
			CreateredMapValue["HVM"] = hvm;
			//CreateredMapValue["RVM"] = _map;

			return hvm;
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
