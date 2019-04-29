using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WorldCreaterStudio_Core;
using WorldCreaterStudio_Core.BackendNode.SolarIlluminance;
using WorldCreaterStudio_Core.ListNode;

namespace MiRaI.BE.SI.QuickCalculating {

	/// <summary>
	/// 利用法线快速模拟光照强度模拟器
	/// </summary>
	public class QuickCalculating :
		ISolarIlluminanceCalculaterAble {
		public string CreaterName => "反射面法线快速计算";

		public string CreaterProgramSet => "MiRaI.BE.SI.QC|0.1";

		public Guid CreaterGuid => typeof (QuickCalculating).GUID;

		public event DataCalculatingProcessingEventType OnProcessingChanged;

		/// <summary>
		/// 使用QuickCalculatingConfig类型的配置项进行模拟
		/// </summary>
		/// <param name="config">配置设置</param>
		/// <param name="heightMap">高度数据</param>
		/// <param name="work">执行方法的工作集</param>
		/// <returns></returns>
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


			BitmapSource image = ValueToImage.SolarIlluminance.GetBitmap (blockvalue);
			work.Images.Add ("BE.SI.Map", image, "SolarIlluminance Visual Map");
			SolarIlluminanceResault re = new SolarIlluminanceResault (blockvalue, "SolarIlluminance Visual Map", work, "BE.SI.Map");
			return re;
		}

		/// <summary>
		/// 使用一个ISolarIlluminanceConfigAble类型的配置项进行模拟。
		/// 若不为ISolarIlluminanceConfigAble类型，抛出IncongruentConfigurationException
		/// </summary>
		/// <param name="config">配置设置</param>
		/// <param name="heightMap">高度数据</param>
		/// <param name="work">执行方法的工作集</param>
		/// <returns></returns>
		public SolarIlluminanceResault GetSolarIlluminanceDatas (ISolarIlluminanceConfigAble config, int[,] heightMap, Work work) {
			if (!(config is QuickCalculatingConfig)) throw new WorldCreaterStudio_Core.Exceptions.IncongruentConfigurationException (typeof (QuickCalculatingConfig), config.GetType ());
			return GetSolarIlluminanceResaultDatasBySpecialConfig (config as QuickCalculatingConfig, heightMap, work);
		}
	}
}
