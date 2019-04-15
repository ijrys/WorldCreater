using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCreaterStudio_Core.ListNode;
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
	enum AreaType {
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
	struct PointData {
		/// <summary>
		/// 区域类型
		/// </summary>
		AreaType AreaType;

		/// <summary>
		/// 降水强度。每单位时间降水0.01个全局高度单位
		/// </summary>
		int RainfallIntensity;

		/// <summary>
		/// 水深，0.01个全局高度单位
		/// </summary>
		int DeepOfWater;
	}

	public class RainfallMotionResault : CalculatedResault<PointData> {
		public override void Save (bool freshWithoutChanged = false) {
			FileStream fs = DataFile.Open (FileMode.Create);
			ByteStreamWriter bsw = new ByteStreamWriter (fs);

			int w = Value.GetLength (1), h = Value.GetLength (0);

			bsw.Write (BitConverter.GetBytes (w), 4);
			bsw.Write (BitConverter.GetBytes (h), 4);

			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					bsw.Write (Value[i, j].power);
				}
			}

			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					fs.WriteByte ((byte)Value[i, j].direction);
				}
			}

			bsw.Flush ();
			bsw.Close ();

			fs.Flush ();
			fs.Close ();

			Changed = false;
		}

		protected override void Load () {
			try {

				FileStream fs = DataFile.Open (FileMode.Open);

				int w, h, bufcont, bufnow = 0;
				byte[] buffer = new byte[128];
				bufcont = fs.Read (buffer, 0, buffer.Length);
				if (bufcont < 8) {
					throw new Exceptions.DataResousesReadException.DataResousesFormException ("文件格式不正确");
				}

				w = BitConverter.ToInt32 (buffer, 0);
				h = BitConverter.ToInt32 (buffer, 4);
				bufnow = 8;

				int bytecount = w * h * 2;
				if (fs.Length < bytecount + 8) {
					throw new Exceptions.DataResousesReadException.DataResousesFormException ("文件内容长度不正确");
				}

				Value = new PointData[h, w];

				for (int i = 0; i < h; i++) {
					for (int j = 0; j < w; j++) {
						if (bufnow == bufcont) {
							bufnow = 0;
							bufcont = fs.Read (buffer, 0, buffer.Length);
						}
						Value[i, j].power = buffer[bufnow];
						bufnow++;
					}
				}

				for (int i = 0; i < h; i++) {
					for (int j = 0; j < w; j++) {
						if (bufnow == bufcont) {
							bufnow = 0;
							bufcont = fs.Read (buffer, 0, buffer.Length);
						}
						byte b = buffer[bufnow];
						if (b > 8) {
							throw new Exceptions.DataResousesReadException.DataResousesFormException ("不可转换的信息");
						}
						Value[i, j].direction = (Direction)b;
						bufnow++;
					}
				}

				fs.Close ();

				Changed = false;
			}
			catch (Exception ex) {
				throw new Exceptions.DataResousesReadException ("读写文件时发生错误", DataFile, ex);
			}
		}

		public RainfallMotionResault (PointData[,] value, string dataName, Work work, string imgResKey) :
			base (value, dataName, work, imgResKey) {

		}
	}


}
