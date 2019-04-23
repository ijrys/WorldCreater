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

namespace WorldCreaterStudio_Core.BackendNode.Biomes {
	/// <summary>
	/// 降水模拟需要的配置参数接口
	/// </summary>
	public interface IBiomesConfigAble
		: IDataCalculaterConfigurationAble {
	}

	/// <summary>
	/// 降水模拟计算器
	/// </summary>
	public interface IBiomesCalculaterAble : IDataCalculaterAble {
		BiomesResault GetBiomesDatas (IBiomesConfigAble config, int[,] heightMap, Work work);
	}

	/// <summary>
	/// 降水模拟计算器生成工厂
	/// </summary>
	public interface IBiomesCalculaterFactoryAble :
		IDataCalculaterFactoryAble<IBiomesCalculaterAble, IBiomesConfigAble> {

	}

	/// <summary>
	/// 区域类型枚举
	/// </summary>
	public enum BiomesType {
		/*
		0 000 0000 沙漠（寒带）
		0 001 0000 沙漠（温带）
		0 010 0000 沙漠（热带）

		0 000 0001 寒带苔原
		0 000 0010 针叶林

		0 001 0001 温带草原
		0 001 0010 温带森林

		0 010 0001 热带草原
		0 010 0010 热带旱林
		0 010 0011 热带雨林

		1 000 0000 淡水
		1 001 0000 咸水
		*/
		/// <summary>
		/// 淡水水域
		/// </summary>
		FreshWater = 0b1_000_0000,
		/// <summary>
		/// 咸水水域
		/// </summary>
		SaltWater = 0b1_001_0000,
		/// <summary>
		/// 沙漠
		/// </summary>
		Desert = 0b0_000_0000,
		/// <summary>
		/// 炎热草原
		/// </summary>
		TropicalSavanna = 0b0_010_0001,
		/// <summary>
		/// 炎热旱林
		/// </summary>
		ThornForest = 0b0_010_0010,
		/// <summary>
		/// 炎热雨林
		/// </summary>
		TropicalRainforest = 0b0_010_0011,
		/// <summary>
		/// 温和草原
		/// </summary>
		TemperateGrassy = 0b0_001_0001,
		/// <summary>
		/// 温和森林
		/// </summary>
		TemperateForest = 0b0_001_0010,
		/// <summary>
		/// 冰雪苔原
		/// </summary>
		Tundra = 0b0_000_0001,
		/// <summary>
		/// 针叶林
		/// </summary>
		ConiferousForest = 0b0_000_0010,
	}

	public class BiomesResault : CalculatedResault<BiomesType> {
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
					bw.Write ((byte)Value[i, j]);
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

				int bytecount = w * h;
				if (fs.Length < bytecount + 8) {
					throw new Exceptions.DataResousesReadException.DataResousesFormException ("文件内容长度不正确");
				}

				Value = new BiomesType[h, w];

				for (int i = 0; i < h; i++) {
					for (int j = 0; j < w; j++) {
						Value[i, j] = (BiomesType)br.ReadByte ();
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

		public BiomesResault (BiomesType[,] value, string dataName, Work work, string overviewImgResKey) :
			base (value, dataName, work, overviewImgResKey) {
		}
	}


}
