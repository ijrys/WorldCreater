using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace WorldCreaterStudio_Core.Resouses {
	public class ValueResource : IWorkLogicNodeAble {
		public Work Work => throw new NotImplementedException();

		public ControlTemplate ShowPanel => null;

		public string NodeName { get; private set; }

		public ImageSource Icon => throw new NotImplementedException();

		public ObservableCollection<IWorkLogicNodeAble> Childrens => null;

		public XmlElement XmlNode(XmlDocument xmlDocument, bool save = false) {
			throw new NotImplementedException();
		}

		public bool Changed { get; private set; }

		private int[,] _value;
		public int[,] Value {
			get { return _value; }
			set {
				_value = value.SyncRoot as int[,];
				Changed = true;
			}
		}

		public ImageResourceReference GrayImage;

		public event NodeValueChangedEventType NodeValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		public FileInfo DataFile { get; private set; }

		public void LoadValue () {
			FileStream fs = new FileStream (DataFile.FullName, FileMode.OpenOrCreate);
			BinaryReader br = new BinaryReader (fs);

			int w = br.ReadInt32(), h = br.ReadInt32 ();


			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					Value[i, j] = br.ReadInt32 ();
				}
			}

			fs.Flush ();
			fs.Close ();
		}

		public void Save(bool freshWithoutChanged = false) {
			if (!Changed && !freshWithoutChanged) return;

			FileStream fs = new FileStream(DataFile.FullName, FileMode.OpenOrCreate);
			BinaryWriter bw = new BinaryWriter (fs);

			int w = Value.GetLength(1), h = Value.GetLength(0);

			bw.Write (w);
			bw.Write (h);

			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					bw.Write (Value[i, j]);
				}
			}

			fs.Flush();
			fs.Close();

			Changed = false;
		}

		public ValueResource (int[,] value, string dataName) {
			Value = value;
			string filename = Tools.Path.GetAFileName(dataName);
			DataFile = new FileInfo (Path.Combine (Work.WorkDirectionary.FullName, filename));
			NodeName = dataName;
		}
	}
}
