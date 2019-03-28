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

		public void Save(bool freshWithoutChanged = false) {
			if (!Changed && !freshWithoutChanged) return;

			FileStream fs = new FileStream(DataFile.FullName, FileMode.OpenOrCreate);
			int w = Value.GetLength(1), h = Value.GetLength(0);

			fs.SetLength(8192);
			int maxIntCount = 8192 / 8, nowIntCount = 2;

			fs.Write(BitConverter.GetBytes(w), 0, 4);
			fs.Write(BitConverter.GetBytes(h), 0, 4);

			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					fs.Write(BitConverter.GetBytes(Value[i, j]), 0, 4);
					nowIntCount++;
					if (nowIntCount >= maxIntCount) { fs.Flush(); nowIntCount = 0; }
				}
			}

			fs.Flush();
			fs.Close();

			Changed = false;
		}

		public ValueResource (int[,] value, string dataName) {
			Value = value;
			string filename = Tools.Path.GetAFileName(dataName);
			NodeName = dataName;
		}
	}
}
