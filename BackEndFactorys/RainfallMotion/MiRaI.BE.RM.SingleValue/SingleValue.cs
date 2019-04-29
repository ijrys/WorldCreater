using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WorldCreaterStudio_Core;
using WorldCreaterStudio_Core.BackendNode.RainfallMotion;
using WorldCreaterStudio_Core.ListNode;

namespace MiRaI.BE.RM.SingleValue {

	/// <summary>
	/// 设置单值的模拟器
	/// </summary>
	public class SingleValue :
		IRainfallMotionCalculaterAble {
		public string CreaterName => "单一值快速设定";

		public string CreaterProgramSet => "MiRaI.BE.RM.SV|0.1";

		public Guid CreaterGuid => typeof (SingleValue).GUID;

		public event DataCalculatingProcessingEventType OnProcessingChanged;

		public RainfallMotionResault GetAtmosphericMotionDatasBySpecialConfig (SingleValueConfig config, int[,] heightMap, Work work) {
			int w = heightMap.GetLength (0) - 1, h = heightMap.GetLength (1) - 1;
			PointData[,] recont = new PointData[w, h];
			int ri = config.RainfallIntensity;
			int sl = config.SeaLevel;

			for (int i = 0; i < w; i++) {
				for (int j = 0; j < h; j++) {
					int hdiff = heightMap[i, j] - sl;
					AreaType areaType;
					if (hdiff > 0) areaType = AreaType.land;
					//else if (hdiff > -2) areaType = AreaType.marsh;
					else areaType = AreaType.sea;

					if (hdiff > 0) hdiff = 0;
					else hdiff = -hdiff;

					recont[i, j] = new PointData {
						RainfallIntensity = ri,
						DeepOfWater = -hdiff,
						AreaType = areaType
					};

				}
			}

			BitmapSource image = ValueToImage.RainfallMotionToImage.GetRainfallIntensityBitmap (recont);
			work.Images.Add ("BE.RM.RIMap", image, "RainfallMotion Visual Map");
			image = ValueToImage.RainfallMotionToImage.GetAreaTpyeBitmap (recont);
			work.Images.Add ("BE.RM.ATMap", image, "RainfallMotion Visual Map");
			RainfallMotionResault re = new RainfallMotionResault (recont, "RainfallMotion Visual Map", work, "BE.RM.RIMap", "BE.RM.RIMap", "BE.RM.ATMap");

			image = ValueToImage.RainfallMotionToImage.GetWaterDeepBitmap (recont);
			work.Images.Add ("BE.RM.WDMap", image, "RainfallMotion WaterDeep Map");

			return re;
		}

		public RainfallMotionResault GetAtmosphericMotionDatas (IRainfallMotionConfigAble config, int[,] heightMap, Work work) {
			if (!(config is SingleValueConfig)) throw new WorldCreaterStudio_Core.Exceptions.IncongruentConfigurationException (typeof (SingleValueConfig), config.GetType ());
			return GetAtmosphericMotionDatasBySpecialConfig (config as SingleValueConfig, heightMap, work);
		}
	}
}
