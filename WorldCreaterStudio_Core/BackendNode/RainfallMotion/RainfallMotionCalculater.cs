using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WorldCreaterStudio_Core.ListNode;
using WorldCreaterStudio_Core.Resouses;
using WorldCreaterStudio_Core.Tools;

namespace WorldCreaterStudio_Core.BackendNode.RainfallMotion {
	/// <summary>
	/// 降水模拟需要的配置参数接口
	/// </summary>
	public interface IRainfallMotionConfigAble
		: IDataCalculaterConfigurationAble {
	}

	/// <summary>
	/// 降水模拟计算器
	/// </summary>
	public interface IRainfallMotionCalculaterAble : IDataCalculaterAble {
		RainfallMotionResault GetAtmosphericMotionDatas (IRainfallMotionConfigAble config, int[,] heightMap, Work work);
	}

	/// <summary>
	/// 降水模拟计算器生成工厂
	/// </summary>
	public interface IRainfallMotionCalculaterFactoryAble :
		IDataCalculaterFactoryAble<IRainfallMotionCalculaterAble, IRainfallMotionConfigAble> {

	}

	/// <summary>
	/// 区域类型枚举
	/// </summary>
	public enum AreaType {
		/// <summary>
		/// 陆地
		/// </summary>
		land = 0,
		/// <summary>
		/// 江，河
		/// </summary>
		river = 1,
		/// <summary>
		/// 湖
		/// </summary>
		lake = 2,
		/// <summary>
		/// 海，洋
		/// </summary>
		sea = 3,
		/// <summary>
		/// 沼泽，湿地
		/// </summary>
		marsh = 4,
	}

	/// <summary>
	/// 区域数据
	/// </summary>
	public struct PointData {
		/// <summary>
		/// 区域类型
		/// </summary>
		public AreaType AreaType;

		/// <summary>
		/// 降水强度。每单位时间降水0.01个全局高度单位
		/// </summary>
		public int RainfallIntensity;

		/// <summary>
		/// 水深，同全局高度单位
		/// </summary>
		public int DeepOfWater;
	}

	public class RainfallMotionResault : CalculatedResault<PointData> {
		public ImageResourceReference RainfallIntensityImage { get; private set; }
		public ImageResourceReference AreaTypeImage { get; private set; }

		public override void Save (bool freshWithoutChanged = false) {
			FileStream fs = DataFile.Open (FileMode.Create);
			BinaryWriter bw = new BinaryWriter (fs);

			int w = Value.GetLength (1), h = Value.GetLength (0);

			bw.Write (w);
			bw.Write (h);

			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					bw.Write ((byte)Value[i, j].AreaType);
					bw.Write (Value[i, j].RainfallIntensity);
					bw.Write (Value[i, j].DeepOfWater);
				}
			}

			bw.Flush ();
			bw.Close ();

			fs.Flush ();
			fs.Close ();

			Changed = false;
		}

		protected override void Load () {
			try {

				FileStream fs = DataFile.Open (FileMode.Open);
				BinaryReader br = new BinaryReader (fs);

				int w, h;
				if (fs.Length < 8) {
					throw new Exceptions.DataResousesReadException.DataResousesFormException ("文件格式不正确");
				}

				w = br.ReadInt32 ();
				h = br.ReadInt32 ();

				int bytecount = w * h * 5;
				if (fs.Length < bytecount + 8) {
					throw new Exceptions.DataResousesReadException.DataResousesFormException ("文件内容长度不正确");
				}

				Value = new PointData[h, w];

				for (int i = 0; i < h; i++) {
					for (int j = 0; j < w; j++) {
						Value[i, j].AreaType = (AreaType)br.ReadByte ();
						Value[i, j].RainfallIntensity = br.ReadInt32 ();
						Value[i, j].DeepOfWater = br.ReadInt32 ();
					}
				}
				br.Close ();
				fs.Close ();
				Changed = false;
			}
			catch (Exception ex) {
				throw new Exceptions.DataResousesReadException ("读写文件时发生错误", DataFile, ex);
			}
		}

		public RainfallMotionResault (PointData[,] value, string dataName, Work work, string overviewImgResKey, string riImgResKey, string atImgResKey) :
			base (value, dataName, work, overviewImgResKey) {
			RainfallIntensityImage = new ImageResourceReference (work, riImgResKey);
			AreaTypeImage = new ImageResourceReference (work, atImgResKey);
		}
	}


}
