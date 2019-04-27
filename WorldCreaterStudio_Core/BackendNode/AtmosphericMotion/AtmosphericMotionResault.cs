using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WorldCreaterStudio_Core.Tools;

namespace WorldCreaterStudio_Core.BackendNode.AtmosphericMotion {
	public class AtmosphericMotionResault : CalculatedResault<PointData> {
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
					bsw.Write ((byte)Value[i, j].direction);
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

		public override XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			XmlElement node = xmlDocument.CreateElement ("Resault");
			Save ();
			node.SetAttribute ("dataName", DataFile == null ? "" : DataFile.Name);
			node.SetAttribute ("imgrefkey", ShowImage == null ? "" : ShowImage.ResourseKey);

			if (save) { Changed = false; }
			return node;
		}

		public AtmosphericMotionResault (PointData[,] value, string dataName, Work work, string imgResKey) :
			base (value, dataName, work, imgResKey) {

		}

		public static AtmosphericMotionResault InitByXMLNode (XmlElement node, Work work) {
			if (node.Name != "Resault") return null;
			string dataname = node.Attributes["dataName"]?.Value;
			if (dataname == null) return null;
			string imgrefkey = node.Attributes["imgrefkey"]?.Value;
			if (imgrefkey == null) return null;

			AtmosphericMotionResault res = new AtmosphericMotionResault (null, dataname, work, imgrefkey);
			try {
				res.Load ();
			} catch (Exception ex) {
				return null;
			}

			return res;
		}
	}
}
