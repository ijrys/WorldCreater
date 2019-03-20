using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace WorldCreaterStudio_Core {
	using ShowPanelType = FrameworkElement;
	public class Work : IWorkLogicNodeAble, INotifyPropertyChanged {
		DirectoryInfo _workDirectionary;
		DirectoryInfo _workResousesDirectionary;
		FileInfo _workFile;
		Guid _guid;
		bool _changed;
		ImageSource _icon;
		private string _nodeName;

		Work IWorkLogicNodeAble.Work => this;
		public ShowPanelType ShowPanel { get => StoreRoom.ShowPanel.WorkPanel; }
		public string NodeName { get=>_nodeName; private set { _nodeName = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon")); } }
		public ImageSource Icon { get=>_icon; set { _icon = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon")); } }

		public Resouses.ImageResourceManager Images { get; private set; }
		public FrontEndFactory FrontEndNodes { get; private set; }
		public BackEndFactory BackEndNodes { get; private set; }
		//public Dictionary<string, Resouses.ImageResourse> first;

		public ObservableCollection<IWorkLogicNodeAble> Childrens { get; private set; }
		public Guid Guid { get => _guid; private set => _guid = value; }


		public XmlElement XmlNode(XmlDocument xmlDocument) {
			XmlElement node = xmlDocument.CreateElement("work");
			node.SetAttribute("guid", Guid.ToString());

			node.SetAttribute("dictionary", _workDirectionary.Name);
			node.SetAttribute("file", _workFile.Name);
			node.SetAttribute("name", NodeName);

			return node;
		}


		/// <summary>
		/// 随机值
		/// </summary>
		int[,] _randomMap;

		/// <summary>
		/// 高度图
		/// </summary>
		int[,] _heightMap;

		/// <summary>
		/// 地势图【起伏程度】
		/// </summary>
		byte[,] _terrainMap;

		public event PropertyChangedEventHandler PropertyChanged;

		public void Save(bool saveEvenUnchanged = false) {
			if (!saveEvenUnchanged && !_changed) return;
			XmlDocument document = new XmlDocument();
			XmlElement root = document.CreateElement("work");
			document.AppendChild(root);
			root.SetAttribute("guid", Guid.ToString());

			// image recourses
			XmlNode imgRes = Images.XmlNode(document);
			root.AppendChild(imgRes);
			Images.Save();

			// front end work
			if (FrontEndNodes != null) {
				XmlNode fefactory = FrontEndNodes.XmlNode(document);
				root.AppendChild(fefactory);
			}

			// back end work
			if (BackEndNodes != null) {
				XmlNode befactory = BackEndNodes.XmlNode(document);
				root.AppendChild(befactory);
			}

			document.Save(_workFile.FullName);

			_changed = false;
		}

		private Work(string workPath, string filename, string workName) {
			_workDirectionary = new DirectoryInfo(workPath);
			string imgDir = Path.Combine(workPath, "Images");
			_workResousesDirectionary = new DirectoryInfo(imgDir);
			string workFileFullPath = Path.Combine(workPath, filename);
			_workFile = new FileInfo(workFileFullPath);

			NodeName = workName;
			Guid = Guid.NewGuid();
			_changed = false;
			Images = new Resouses.ImageResourceManager(_workResousesDirectionary);
			Childrens = new ObservableCollection<IWorkLogicNodeAble>();
		}

		public static Work NewWork(string workPath, string filename, string workName) {
			if (Directory.Exists(workPath)) {
				throw new Exceptions.DirectoryExistedException(workPath);
			}
			Work work = new Work(workPath, filename, workName);
			//文件、目录准备
			work._workDirectionary.Create();
			work._workResousesDirectionary.Create();

			work.Save(true);

			work.Childrens.Add(work.Images);
			return work;
		}

		public static Work OpenWork(string workPath, string filename, string workName) {
			Work work = new Work(workPath, filename, workName);

			XmlDocument document = new XmlDocument();
			document.Load(work._workFile.FullName);
			XmlNode root = document.FirstChild;

			work.Guid = new Guid(root.Attributes["guid"].Value);
			foreach (XmlElement item in root.ChildNodes) {
				switch (item.Name.ToLower()) {
					case "images": //进入图片资源节点
						if (item.HasChildNodes) {
							work.Images = Resouses.ImageResourceManager.LoadFromXmlNode(item, work._workResousesDirectionary);
						}
						break;
					case "frontendfactory": //前端工厂

						break;
					case "backendfactory": //后端工厂

						break;
				}
				if (item.Name.ToLower() == "images") {

				}
			}
			work.Childrens.Append(work.Images);
			return work;
		}


		private static BitmapImage GetBitmapFromFile(string path) {
			if (!File.Exists(path)) return null;
			BitmapImage result = new BitmapImage();
			try {
				FileStream stream = new FileStream(path, FileMode.Open);
				//注意：转换的图片的原始格式ImageFormat设为BMP、JPG、PNG等

				stream.Position = 0;
				result.BeginInit();
				result.CacheOption = BitmapCacheOption.OnLoad;
				result.StreamSource = stream;
				result.EndInit();
				result.Freeze();
			}
			catch (Exception ex) {
				Console.WriteLine("read image fail");
				Console.WriteLine(ex.Message);
				result = null;
			}

			return result;
		}

	}
}
