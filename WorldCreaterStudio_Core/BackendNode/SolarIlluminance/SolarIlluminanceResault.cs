using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core.BackendNode.SolarIlluminance {
	public class SolarIlluminanceResault : CalculatedResault<byte> {
		public override void Save (bool freshWithoutChanged = false) {
			FileStream fs = DataFile.Open (FileMode.Create);
			BinaryWriter bsw = new BinaryWriter (fs);

			int w = Value.GetLength (1), h = Value.GetLength (0);

			bsw.Write (w);
			bsw.Write (h);

			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					bsw.Write (Value[i, j]);
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
				BinaryReader br = new BinaryReader (fs);
				uint w, h;

				if (fs.Length < 8) {
					throw new Exceptions.DataResousesReadException.DataResousesFormException ("文件格式不正确");
				}

				w = br.ReadUInt32();
				h = br.ReadUInt32 ();

				uint bytecount = w * h * 4;
				if (fs.Length < bytecount + 8) {
					throw new Exceptions.DataResousesReadException.DataResousesFormException ("文件内容长度不正确");
				}

				Value = new byte[h, w];

				for (int i = 0; i < h; i++) {
					for (int j = 0; j < w; j++) {
						Value[i, j] = br.ReadByte ();
					}
				}

				fs.Close ();

				Changed = false;
			}
			catch (Exception ex) {
				throw new Exceptions.DataResousesReadException ("读写文件时发生错误", DataFile, ex);
			}
		}

		public SolarIlluminanceResault (byte[,] value, string dataName, Work work, string imgResKey) :
			base (value, dataName, work, imgResKey) {

		}
	}
}
