using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using WorldCreaterStudio_Core.MapCreater;
using WorldCreaterStudio_Core.Resouses;

namespace RandomTend {
	[Guid("7B2C4FC1-378A-47E3-A82D-F32D9CDCB288")]
	public class RandomTendCreater : MapCreater {
		public override string CreaterProgramSet => "MiRaI.RandomTend.RandomTend|0.1";

		public override Guid CreaterGuid => typeof(RandomTendCreater).GUID;

		public override WorldCreaterStudio_Core.FrontendNode.CreatingResault CreatAMap(Configuration configuration, WorldCreaterStudio_Core.Work work) {
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
				bitmap = ValueToImage.ValueToColorImage.GetBitmap (-2097152, 2097152, _map);
				work.FrontEndNodes.Image_Add ("FE.HeightValueColor", bitmap, "前端工厂高度值图，零分检查");
			}

			//中间数据放入资源节点
			//CreateredMapValue = new Dictionary<string, ValueResource>();
			//ValueResource hvm = new ValueResource(_map, "HVM");
			//CreateredMapValue["HVM"] = hvm;
			//CreateredMapValue["RVM"] = _map;
			WorldCreaterStudio_Core.FrontendNode.CreatingResault resault = new WorldCreaterStudio_Core.FrontendNode.CreatingResault (_map, "HeightValue", work, "FE.HeightValue");

			return resault;
		}
	}
}
