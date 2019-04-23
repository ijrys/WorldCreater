using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCreaterStudio_Core;
using WorldCreaterStudio_Core.BackendNode.Biomes;
using WorldCreaterStudio_Core.ListNode;
using WorldCreaterStudio_Core.BackendNode.RainfallMotion;
using System.Windows.Media.Imaging;

namespace MiRaI.BE.BI.QuickCalc {

	/// <summary>
	/// 设置单值的模拟器
	/// </summary>
	public class QuickCalc :
		IBiomesCalculaterAble {
		public string CreaterName => "快速运算";

		public string CreaterProgramSet => "MiRaI.BE.BI.QC|0.1";

		public Guid CreaterGuid => typeof (QuickCalc).GUID;

		public event DataCalculatingProcessingEventType OnProcessingChanged;

		public BiomesResault GetBiomesDatasBySpecialConfig (QuickCalcConfig config, int[,] heightMap, Work work) {
			int w = heightMap.GetLength (0) - 1, h = heightMap.GetLength (1) - 1;
			BiomesType[,] recont = new BiomesType[w, h];

			double hpower = 0.000001;
			double rpower = 0.01;

			var rdata = work.BackEndNodes.RMNode.Resault.Value;
			var sdata = work.BackEndNodes.SINode.Resault.Value;


			for (int i = 0; i < w; i++) {
				for (int j = 0; j < h; j++) {
					double temperature = sdata[i, j] - heightMap[i, j] * hpower;
					double wv = rdata[i, j].RainfallIntensity * rpower - temperature;
					if (wv > 0) wv = wv * 0.7;

					if (rdata[i, j].AreaType == AreaType.land) { //陆地类型
						if (temperature < 10) { // 寒带
							if (wv < 0) { // 沙漠
								recont[i, j] = BiomesType.Desert;
							}
							else if (wv < 30) {
								recont[i, j] = BiomesType.Tundra;
							}
							else {
								recont[i, j] = BiomesType.ConiferousForest;
							}
						}
						else if (temperature < 25) { // 温带
							if (wv < 5) { // 沙漠
								recont[i, j] = BiomesType.Desert;
							}
							else if (wv < 100) {
								recont[i, j] = BiomesType.TemperateGrassy;
							}
							else {
								recont[i, j] = BiomesType.TemperateForest;
							}
						}
						else { // 热带
							if (wv < 10) { // 沙漠
								recont[i, j] = BiomesType.Desert;
							}
							else if (wv < 90) {
								recont[i, j] = BiomesType.TropicalSavanna;
							}
							else if (wv < 180) {
								recont[i, j] = BiomesType.ThornForest;
							} else {
								recont[i, j] = BiomesType.TropicalRainforest;
							}
						}
					}
					else { //水域类型
						if (rdata[i, j].AreaType == AreaType.sea) {
							recont[i, j] = BiomesType.SaltWater;
						} else {
							recont[i, j] = BiomesType.FreshWater;
						}
					}
				}
			}

			BitmapSource image = ValueToImage.BiomesToImage.GetBitmap (recont);
			work.Images.Add ("MiRaI.BE.BIMap", image, "Biomes Visual Map");
			BiomesResault re = new BiomesResault (recont, "Biomes Visual Map", work, "MiRaI.BE.BIMap");

			return re;
		}

		public BiomesResault GetBiomesDatas (IBiomesConfigAble config, int[,] heightMap, Work work) {
			if (!(config is QuickCalcConfig)) throw new WorldCreaterStudio_Core.Exceptions.IncongruentConfigurationException (typeof (QuickCalcConfig), config.GetType ());
			return GetBiomesDatasBySpecialConfig (config as QuickCalcConfig, heightMap, work);
		}
	}
}
