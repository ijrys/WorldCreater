using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WorldCreaterStudio_Core.BackendNode.RainfallMotion;

namespace ValueTo3DModel {
	public static class WaterToModel {
		private struct WaterData {
			public byte count;
			public int value;
		}
		public static MeshGeometry3D GetModel (int[,] heightmap, int[,] midheightmap, PointData[,] areadata) {
			int w = heightmap.GetLength (1), h = heightmap.GetLength (0), ws = w - 1, hs = h - 1;
			double bl = 1.0;

			WaterData[,] wlofpoint = new WaterData[h, w]; // 指示一个点的情况
			int[,] wlofarea = new int[hs, ws]; // 指示一个区域的水平面高度
			bool[,] iswa = new bool[hs, ws]; // 指示一个区域是否是水域
			Dictionary<int, int> pointtrans = new Dictionary<int, int> ();

			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					wlofpoint[i, j].count = 0;
					wlofpoint[i, j].value = 0;
				}
			}

			for (int i = 0; i < hs; i++) {
				for (int j = 0; j < ws; j++) {
					if (areadata[i, j].AreaType != AreaType.land) {
						int wl = areadata[i, j].DeepOfWater + midheightmap[i, j];
						iswa[i, j] = true;
						wlofarea[i, j] = wl;

						wlofpoint[i, j].count++;
						wlofpoint[i, j].value += wl;

						wlofpoint[i, j + 1].count++;
						wlofpoint[i, j + 1].value += wl;

						wlofpoint[i + 1, j].count++;
						wlofpoint[i + 1, j].value += wl;

						wlofpoint[i + 1, j + 1].count++;
						wlofpoint[i + 1, j + 1].value += wl;
					}
					else {
						iswa[i, j] = false;
					}
				}
			}

			// 制作模型
			MeshGeometry3D fbx = new MeshGeometry3D ();
			Point3DCollection points = new Point3DCollection ();
			Int32Collection sjxs = new Int32Collection ();
			int t = w * 2 - 1;
			int pointid = 0;
			for (int i = 0; i < hs; i++) {
				for (int j = 0; j < ws; j++) {
					if (iswa[i, j]) {
						int pid, p1id, p2id, p3id, p4id, p5id;
						// lt 1
						pid = i * t + j;
						if (!pointtrans.ContainsKey (pid)) {
							pointtrans[pid] = pointid;
							points.Add (new Point3D (j * 2, wlofpoint[i, j].value * bl / wlofpoint[i, j].count, i * 2));
							p1id = pointid;
							pointid++;
						} else {
							p1id = pointtrans[pid];
						}

						// rt 2
						pid++;
						if (!pointtrans.ContainsKey (pid)) {
							pointtrans[pid] = pointid;
							points.Add (new Point3D ((j + 1) * 2, wlofpoint[i, j + 1].value * bl / wlofpoint[i, j + 1].count, i * 2));
							p2id = pointid;
							pointid++;
						} else {
							p2id = pointtrans[pid];
						}

						// lb 4
						pid = (i + 1) * t + j;
						if (!pointtrans.ContainsKey (pid)) {
							pointtrans[pid] = pointid;
							points.Add (new Point3D (j * 2, wlofpoint[i + 1, j].value * bl / wlofpoint[i + 1, j].count, (i + 1) * 2));
							p4id = pointid;
							pointid++;
						} else {
							p4id = pointtrans[pid];
						}

						// rb 3
						pid++;
						if (!pointtrans.ContainsKey (pid)) {
							pointtrans[pid] = pointid;
							points.Add (new Point3D ((j + 1) * 2, wlofpoint[i + 1, j + 1].value * bl / wlofpoint[i + 1, j + 1].count, (i + 1) * 2));
							p3id = pointid;
							pointid++;
						} else {
							p3id = pointtrans[pid];
						}

						// c 5
						pid = i * t + w + j;
						if (!pointtrans.ContainsKey (pid)) {
							pointtrans[pid] = pointid;
							points.Add (new Point3D (j* 2 + 1, (areadata[i, j].DeepOfWater + midheightmap[i, j]) * bl, i * 2 + 1));
							p5id = pointid;
							pointid++;
						} else {
							p5id = pointtrans[pid];
						}

						sjxs.Add (p2id);
						sjxs.Add (p1id);
						sjxs.Add (p5id);

						sjxs.Add (p3id);
						sjxs.Add (p2id);
						sjxs.Add (p5id);

						sjxs.Add (p4id);
						sjxs.Add (p3id);
						sjxs.Add (p5id);

						sjxs.Add (p1id);
						sjxs.Add (p4id);
						sjxs.Add (p5id);
					}
					else {
						iswa[i, j] = false;
					}
				}
			}

			fbx.Positions = points;
			fbx.TriangleIndices = sjxs;

			return fbx;
		}
	}
}
