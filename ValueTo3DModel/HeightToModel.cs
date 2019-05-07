using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ValueTo3DModel {
	public static class HeightToModel {
		public static MeshGeometry3D GetModelWithSubdivide (int[,] heightmap, double heightscale = 0.1) {
			MeshGeometry3D fbx = new MeshGeometry3D ();
			Point3DCollection points = new Point3DCollection ();

			int w = heightmap.GetLength (1), h = heightmap.GetLength (0), ws = w - 1, hs = h - 1;
			double bl = heightscale;

			double[,] midvalue = new double[h - 1, w - 1];
			for (int i = 0; i < hs; i++) {
				for (int j = 0; j < ws; j++) {
					midvalue[i, j] = (heightmap[i, j] + heightmap[i, j + 1] + heightmap[i + 1, j] + heightmap[i + 1, j + 1]) / 4.0;
				}
			}

			for (int z = 0; z < hs; z++) {
				for (int x = 0; x < w; x++) {
					Point3D point = new Point3D (x * 2, (heightmap[z, x]) * bl, z * 2);
					points.Add (point);
				}
				for (int x = 0; x < ws; x++) {
					Point3D point = new Point3D (x * 2 + 1, (midvalue[z, x]) * bl, z * 2 + 1);
					points.Add (point);
				}
			}
			for (int x = 0; x < w; x++) {
				Point3D point = new Point3D (x * 2, (heightmap[hs, x]) * bl, hs * 2);
				points.Add (point);
			}

			fbx.Positions = points;
			Int32Collection sjxs = new Int32Collection ();

			int t = w * 2 - 1;
			for (int z = 0; z < hs; z++) {
				for (int x = 0; x < ws; x++) {
					int i1 = z * t + x;
					int i2 = i1 + 1;
					int i4 = i1 + t;
					int i3 = i4 + 1;
					int i5 = i1 + w;

					// p1
					sjxs.Add (i2);
					sjxs.Add (i1);
					sjxs.Add (i5);
					// p2
					sjxs.Add (i3);
					sjxs.Add (i2);
					sjxs.Add (i5);
					// p3
					sjxs.Add (i4);
					sjxs.Add (i3);
					sjxs.Add (i5);
					// p4
					sjxs.Add (i1);
					sjxs.Add (i4);
					sjxs.Add (i5);
				}
			}
			fbx.TriangleIndices = sjxs;

			return fbx;
		}
	}
}
