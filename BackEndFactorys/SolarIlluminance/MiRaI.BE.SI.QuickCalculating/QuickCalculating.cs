using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml;
using WorldCreaterStudio_Core;
using WorldCreaterStudio_Core.BackendNode.SolarIlluminance;
using WorldCreaterStudio_Core.ListNode;

namespace MiRaI.BE.SI.QuickCalculating {

	public class QuickCalculatingConfig :
		ISolarIlluminanceConfigAble {
		public event NodeValueChangedEventType ValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		private double _angle;
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

		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			XmlElement re = xmlDocument.CreateElement ("Config");
			re.SetAttribute ("angle", Angle.ToString ());
			return re;
		}
	}

	/// <summary>
	/// 设置单值的模拟器
	/// </summary>
	public class QuickCalculating :
		ISolarIlluminanceCalculaterAble {
		public string CreaterName => "反射面法线快速计算";

		public string CreaterProgramSet => "MiRaI.BE.SI.QC|0.1";

		public Guid CreaterGuid => typeof (QuickCalculating).GUID;

		public event DataCalculatingProcessingEventType OnProcessingChanged;

		public SolarIlluminanceResault GetSolarIlluminanceResaultDatasBySpecialConfig (QuickCalculatingConfig config, int[,] heightMap, Work work) {
			int w = heightMap.GetLength (1), h = heightMap.GetLength (0), tw = w - 1, th = h - 1;
			double lx = 0, ly = 0, lz = 0, heightscale = 0.001, xyscale = 100;
			double angle = Math.PI * -2 / 360 * config.Angle;
			lx = Math.Sin (angle);
			ly = Math.Cos (angle);

			double lenofline = Math.Sqrt (lx * lx + ly * ly + lz * lz);

			byte[,] blockvalue = new byte[th, tw];
			//byte[,] recont = new byte[h, w];
			int[,] tmphei = new int[th, tw];

			double lx1, ly1, lz1, lx2, ly2, lz2;
			double di, dj, dk;
			double lenofnv, doubletmp;

			double[,] normalVector = new double[th, tw];
			for (int i = 0; i < th; i++) {
				for (int j = 0; j < tw; j++) {
					tmphei[i, j] = (int)(((long)heightMap[i, j] + heightMap[i, j + 1] + heightMap[i + 1, j] + heightMap[i + 1, j + 1]) / 4);
				}
			}

			for (int i = 0; i < th; i++) {
				for (int j = 0; j < tw; j++) {
					// 计算权重
					double pow = 0;


					#region power1
					lx1 = -1; ly1 = 0; lz1 = (heightMap[i, j] - heightMap[i, j + 1]) * heightscale / xyscale;
					lx2 = 1; ly2 = 1; lz2 = (tmphei[i, j] - heightMap[i, j]) * 2 * heightscale / xyscale;

					di = ly1 * lz2 - lz1 * ly2;
					dj = lz1 * lx2 - lx1 * lz2;
					dk = lx1 * ly2 - ly1 * lx2;

					lenofnv = Math.Sqrt (di * di + dj * dj + dk * dk);
					doubletmp = (di * lx + dj * ly + dk * lz) / lenofnv / lenofline;
					pow += doubletmp;
					#endregion

					#region power2
					lx1 = 0; ly1 = -1; lz1 = (heightMap[i, j + 1] - heightMap[i + 1, j + 1]) * heightscale / xyscale;
					lx2 = -1; ly2 = 1; lz2 = (tmphei[i, j] - heightMap[i, j + 1]) * 2 * heightscale / xyscale;

					di = ly1 * lz2 - lz1 * ly2;
					dj = lz1 * lx2 - lx1 * lz2;
					dk = lx1 * ly2 - ly1 * lx2;

					lenofnv = Math.Sqrt (di * di + dj * dj + dk * dk);
					doubletmp = (di * lx + dj * ly + dk * lz) / lenofnv / lenofline;
					pow += doubletmp;
					#endregion

					#region power3
					lx1 = 1; ly1 = 0; lz1 = (heightMap[i + 1, j + 1] - heightMap[i + 1, j]) * heightscale / xyscale;
					lx2 = -1; ly2 = -1; lz2 = (tmphei[i, j] - heightMap[i + 1, j + 1]) * 2 * heightscale / xyscale;

					di = ly1 * lz2 - lz1 * ly2;
					dj = lz1 * lx2 - lx1 * lz2;
					dk = lx1 * ly2 - ly1 * lx2;

					lenofnv = Math.Sqrt (di * di + dj * dj + dk * dk);
					doubletmp = (di * lx + dj * ly + dk * lz) / lenofnv / lenofline;
					pow += doubletmp;
					#endregion

					#region power4
					lx1 = 0; ly1 = 1; lz1 = (heightMap[i + 1, j] - heightMap[i, j]) * heightscale / xyscale;
					lx2 = 1; ly2 = -1; lz2 = (tmphei[i, j] - heightMap[i + 1, j]) * 2 * heightscale / xyscale;

					di = ly1 * lz2 - lz1 * ly2;
					dj = lz1 * lx2 - lx1 * lz2;
					dk = lx1 * ly2 - ly1 * lx2;

					lenofnv = Math.Sqrt (di * di + dj * dj + dk * dk);
					doubletmp = (di * lx + dj * ly + dk * lz) / lenofnv / lenofline;
					pow += doubletmp;
					#endregion

					pow /= 4;
					if (pow < 0) pow = 0;
					else if (pow > 1) {
						pow = 1;
					}

					blockvalue[i, j] = (byte)(255 * pow);
				}
			}

			//// blockvalue to pointvalue
			//for (int i = 1; i < th; i++) {
			//	for (int j = 1; j < tw; j++) {
			//		recont[i, j] = (byte)((blockvalue[i, j] + blockvalue[i - 1, j] + blockvalue[i, j - 1] + blockvalue[i - 1, j - 1]) / 4);
			//	}
			//}
			//for (int i = 1; i < tw; i ++) { // 最上一行
			//	recont[0, i] = (byte)((blockvalue[0, i - 1] + blockvalue[0, i]) / 2);
			//}
			//for (int i = 1; i < tw; i++) { // 最下一行
			//	recont[th, i] = (byte)((blockvalue[th - 1, i - 1] + blockvalue[th - 1, i]) / 2);
			//}
			//for (int i = 1; i < th; i++) { // 最左一列
			//	recont[i, 0] = (byte)((blockvalue[i - 1, 0] + blockvalue[i, 0]) / 2);
			//}
			//for (int i = 1; i < th; i++) { // 最右一列
			//	recont[i, tw] = (byte)((blockvalue[i - 1, tw - 1] + blockvalue[i, tw - 1]) / 2);
			//}
			//// 四角
			//recont[0, 0] = blockvalue[0, 0];
			//recont[th, 0] = blockvalue[th - 1, 0];
			//recont[0, tw] = blockvalue[0, tw - 1];
			//recont[th, tw] = blockvalue[th - 1, tw - 1];

			BitmapSource image = ValueToImage.SolarIlluminance.GetBitmap (blockvalue);
			work.Images.Add ("BE.SI.Map", image, "SolarIlluminance Visual Map");
			SolarIlluminanceResault re = new SolarIlluminanceResault (blockvalue, "SolarIlluminance Visual Map", work, "BE.SI.Map");
			return re;
		}

		//public double Power (int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3, double lx, double ly, double lz) {
		//	//计算法向量的反向量
		//	double lx1, ly1, lz1, lx2, ly2, lz2;
		//	lx1 = x2 - x1;
		//	ly1 = y2 - y1;
		//	lz1 = z2 - z1;

		//	lx2 = x3 - x2;
		//	ly2 = y3 - y2;
		//	lz2 = z3 - z2;

		//	double i, j, k;
		//	i = ly1 * lz2 - lz1 * ly2;
		//	j = lz1 * lx2 - lx1 * lz2;
		//	k = lx1 * ly2 - ly1 * lx2;


		//	//计算角度
		//	double lenofline = Math.Sqrt (lx * lx + ly * ly + lz * lz);
		//	double lenofnv = Math.Sqrt (i * i + j * j + k * k);
		//	double cos = (i * lx + j * ly + k * lz) / lenofline / lenofnv;

		//	return cos;
		//}

		public SolarIlluminanceResault GetSolarIlluminanceDatas (ISolarIlluminanceConfigAble config, int[,] heightMap, Work work) {
			if (!(config is QuickCalculatingConfig)) throw new WorldCreaterStudio_Core.Exceptions.IncongruentConfigurationException (typeof (QuickCalculatingConfig), config.GetType ());
			return GetSolarIlluminanceResaultDatasBySpecialConfig (config as QuickCalculatingConfig, heightMap, work);
		}
	}


	public class QuickCalculatingFactory : ISolarIlluminanceCalculaterFactoryAble {
		public string DisplayName => "反射面法线快速计算";

		public string DisplayType => "BE.SI";

		public string CalculaterProgramSet => "MiRaI.BE.SI.QC|0.1";

		public Guid CalculaterGuid => typeof (QuickCalculating).GUID;


		private QuickCalculating _temp = null;
		public ISolarIlluminanceCalculaterAble GetACalculater () {
			if (_temp == null) {
				_temp = new QuickCalculating ();
			}
			return _temp;
		}

		public ISolarIlluminanceConfigAble GetAConfiguration () {
			return new QuickCalculatingConfig ();
		}
	}
}
