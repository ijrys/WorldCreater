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

namespace WorldCreaterStudio_Core.BackendNode {
	/// <summary>
	/// 计算结果
	/// </summary>
	/// <typeparam name="CellT"></typeparam>
	public interface ICalculatedResaultAble<CellT> : IWorkLogicNodeAble {
		CellT[,] Value { get; }
		ImageResourceReference ShowImage { get; }
		FileInfo DataFile { get; }

		void Save (bool freshWithoutChanged = false);
	}

	/// <summary>
	/// 存储、管理运算结果，ICalculatedResaultAble的抽象实现
	/// </summary>
	/// <typeparam name="CellT"></typeparam>
	public abstract class CalculatedResault<CellT> : ICalculatedResaultAble<CellT> {
		public Work Work => throw new NotImplementedException ();

		public ControlTemplate ShowPanel => throw new NotImplementedException ();

		public ImageSource Icon => throw new NotImplementedException ();

		private CellT[,] _value;
		public CellT[,] Value {
			get {
				if (_value == null) Load ();
				return _value;
			}
			set {
				if (_value.Equals (value)) return;
				_value = value;
				Changed = true;
			}
		}

		public ImageResourceReference ShowImage {
			get; protected set;
		}

		public FileInfo DataFile { get; protected set; }

		public string NodeName { get; protected set; }

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

		public event NodeValueChangedEventType NodeValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		public XmlElement XmlNode (XmlDocument xmlDocument, bool save = false) {
			throw new NotImplementedException ();
		}

		public abstract void Save (bool freshWithoutChanged = false);
		protected abstract void Load ();


		public CalculatedResault(CellT[,] value, string dataName) {
			Value = value;
			string filename = Tools.Path.GetAFileName (dataName);
			NodeName = dataName;
		}
	}
}
