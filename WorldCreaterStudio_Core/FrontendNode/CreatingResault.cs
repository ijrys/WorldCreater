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
using WorldCreaterStudio_Core.Resouses;
using WorldCreaterStudio_Core.Tools;

namespace WorldCreaterStudio_Core.FrontendNode {
	public class CreatingResault: IWorkLogicNodeAble {
		public event NodeValueChangedEventType NodeValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		private int[,] _value = null;
		public int[,] Value {
			get => _value;
			set {
				_value = value;
				NodeValueChanged?.Invoke (this);
			}
		}

		public Work Work { get; private set; }

		public ControlTemplate ShowPanel => null;

		public string NodeName { get; set; }

		public ImageSource Icon => null;

		public ObservableCollection<IWorkLogicNodeAble> Childrens => null;

		private bool _changed;
		public bool Changed {
			get => _changed;
			set {
				bool oldvalue = _changed;
				_changed = value;
				if (value) {
					NodeValueChanged?.Invoke (this);
				}
				if (value != oldvalue) {
					PropertyChanged?.Invoke (this, new PropertyChangedEventArgs ("Changed"));
				}
			}
		}

		public FileInfo DataFile { get; private set; }
		public ImageResourceReference ShowImageRef { get; private set; }
		public ImageSource ShowImage => ShowImageRef?.Image;

		private void LoadValue () {
			FileStream fs = new FileStream (DataFile.FullName, FileMode.OpenOrCreate);
			BinaryReader br = new BinaryReader (fs);

			int w = br.ReadInt32 (), h = br.ReadInt32 ();
			Value = new int[h, w];

			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					Value[i, j] = br.ReadInt32 ();
				}
			}

			fs.Flush ();
			fs.Close ();
		}

		private void Save (bool freshWithoutChanged = false) {
			//if (!Changed && !freshWithoutChanged) return;

			FileStream fs = new FileStream (DataFile.FullName, FileMode.OpenOrCreate);
			BinaryWriter bw = new BinaryWriter (fs);

			int w = Value.GetLength (1), h = Value.GetLength (0);

			bw.Write (w);
			bw.Write (h);

			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					bw.Write (Value[i, j]);
				}
			}

			fs.Flush ();
			fs.Close ();

			Changed = false;
		}

		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			XmlElement node = xmlDocument.CreateElement ("Resault");
			Save ();
			node.SetAttribute ("name", NodeName);
			node.SetAttribute ("value", DataFile == null ? "" : DataFile.Name);
			node.SetAttribute ("imgrefkey", ShowImage == null ? "" : ShowImageRef.ResourseKey);

			if (save) {
				Changed = false;
			}
			return node;
		}

		public bool InitByXMLNode (XmlElement xmlnode) {
			if (xmlnode.Name != "Resault") return false;
			string name = xmlnode.Attributes["name"]?.Value;
			string dfile = xmlnode.Attributes["value"]?.Value;
			string imgkey = xmlnode.Attributes["imgrefkey"]?.Value;
			if (name == null || dfile == null || imgkey == null) return false;
			NodeName = name;
			DataFile = new FileInfo (System.IO.Path.Combine (Work.WorkDirectionary.FullName, dfile));
			ShowImageRef = new ImageResourceReference (Work, imgkey);

			LoadValue ();

			return true;
		}

		public CreatingResault (int[,] value, string dataName, Work work, string imgResKey){
			Work = work;
			Value = value;
			string fname = Tools.Path.GetAFileName (dataName);
			DataFile = new FileInfo (System.IO.Path.Combine (work.WorkDirectionary.FullName, fname));

			ShowImageRef = new ImageResourceReference (work, imgResKey);
		}

		public static CreatingResault GetResaultByXMLNode (Work work, XmlElement xmlnode) {
			CreatingResault re = new CreatingResault (null, "", work, "");
			if (re.InitByXMLNode (xmlnode)) {
				return re;
			} else {
				return null;
			}
		}

	}
}
