using System;
using WorldCreater.BaseType;

namespace RandomTend {
	public class RandomTend : Creater {
		private int[,] _map = null;
		/// <summary>
		/// Random Value
		/// </summary>
		private int[,] _rv = null;

		public RandomTend(Config config) : base(config) {
		}

		public override void CreateMap() {
			this.OnProcessing?.Invoke(0, "正在准备数据", false, null);

			int blockw = 1 << Config.BlockSize, w = Config.MapWidth, h = Config.MapHeight;
			_map = new int[h, w];
			_rv = new int[h, w];
			Random r = new Random(Config.RandomKey);
			for (int i = 0; i < h; i++) {
				Random r1 = new Random(r.Next());
				for (int j = 0; j < w; j++) _rv[i, j] = r.Next(-1048576, 1048576);
			}

			
			this.OnProcessing?.Invoke(50, "正在准备数据", true, _rv);

			for (int i = 0; i < h; i += blockw) {
				for (int j = 0; j < w; j += blockw) {
					_map[i, j] = _rv[i, j];
				}
			}

			int nowpro = 1; //当前进度
			int ranscl = 1;
			for (int nowSetp = blockw; nowSetp >= 1; nowSetp = nowSetp >> 1, nowpro *= 2, ranscl ++) {
				this.OnProcessing?.Invoke((byte)(nowpro * 50 / blockw), "遍历", true, _map);
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
			}


			this.Resault = new Resault(_map);
			Resault.SetShowInfoMap("RandomValue", _rv);
		}
	}
}
