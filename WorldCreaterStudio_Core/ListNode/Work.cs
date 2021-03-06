﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;

namespace WorldCreaterStudio_Core {
	using ShowPanelType = System.Windows.Controls.ControlTemplate;

	/// <summary>
	/// 表示一个工作
	/// </summary>
	public class Work : IWorkLogicNodeAble {
		#region 字段
		private DirectoryInfo _workDirectionary;
		private DirectoryInfo _workResousesDirectionary;
		private FileInfo _workFile;
		private Guid _guid;
		private bool _changed;
		private ImageSource _icon;
		private string _nodeName;
		private FrontEndFactory _frontEndNodes;
		private BackEndFactory _backEndNodes;
		#endregion

		#region 事件
		/// <summary>
		/// 节点值发生改变事件
		/// </summary>
		public event NodeValueChangedEventType NodeValueChanged;

		/// <summary>
		/// 属性发生改变时通知绑定元素
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region 属性
		/// <summary>
		/// 获取创建线程的工作队列
		/// </summary>
		public Dispatcher Dispatcher { get; private set; }

		/// <summary>
		/// 获取工作目录信息
		/// </summary>
		public DirectoryInfo WorkDirectionary => _workDirectionary;

		/// <summary>
		/// 获取节点所在的工作，这里为当前对象
		/// </summary>
		Work IWorkLogicNodeAble.Work => this;

		/// <summary>
		/// 获取节点双击工作节点时在功能面板区展示的面板模板
		/// </summary>
		public ShowPanelType ShowPanel { get => StoreRoom.ShowPanel.WorkPanel; }

		/// <summary>
		/// 获取节点名称，在这里指工作名
		/// 响应INotifyPropertyChanged
		/// </summary>
		public string NodeName {
			get => _nodeName;
			private set {
				_nodeName = value;
				Changed = true;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
			}
		}

		/// <summary>
		/// 获取节点展示的图标
		/// 响应INotifyPropertyChanged
		/// </summary>
		public ImageSource Icon => WorldCreaterStudio_Resouses.Images.Dark_Icon_Gra;

		private Resouses.ImageResourceManager _images;
		/// <summary>
		/// 获取工作的图片资源管理器
		/// </summary>
		public Resouses.ImageResourceManager Images {
			get => _images;
			private set {
				if (_images == value) return;

				if (_images != null) {
					_images.NodeValueChanged -= Children_NodeValueChanged;
				}

				if (value != null) {
					value.NodeValueChanged += Children_NodeValueChanged;
				}

				_images = value;
				if (Childrens.Count == 0) {
					Childrens.Add(value);
				} else {
					Childrens[0] = value;
				}
			}
		}


		/// <summary>
		/// 获取前端工厂节点
		/// </summary>
		public FrontEndFactory FrontEndNodes {
			get => _frontEndNodes;
			private set {
				if (_frontEndNodes != null) {
					_frontEndNodes.NodeValueChanged -= Children_NodeValueChanged;
				}
				if (value != null) {
					value.NodeValueChanged += Children_NodeValueChanged;
				}
				_frontEndNodes = value;

				if (Childrens.Count < 1) Childrens.Add(null);
				if (Childrens.Count < 2) {
					Childrens.Add(value);
				} else {
					Childrens[1] = value;
				}
			}
		}

		/// <summary>
		/// 获取后端工厂节点
		/// </summary>
		public BackEndFactory BackEndNodes {
			get => _backEndNodes;
			private set {
				if (_backEndNodes != null) {
					_backEndNodes.NodeValueChanged -= Children_NodeValueChanged;
				}
				if (value != null) {
					value.NodeValueChanged += Children_NodeValueChanged;
				}
				_backEndNodes = value;

				if (Childrens.Count < 1) Childrens.Add(null);
				if (Childrens.Count < 2) Childrens.Add(null);
				if (Childrens.Count < 3) {
					Childrens.Add(value);
				} else {
					Childrens[1] = value;
				}
			}
		}

		/// <summary>
		/// 获取所有子节点，用于工作列表展示，其他逻辑节点请通过相应属性或方法更改
		/// </summary>
		public ObservableCollection<IWorkLogicNodeAble> Childrens { get; private set; }
		
		/// <summary>
		/// 获取工作的GUID
		/// </summary>
		public Guid Guid { get => _guid; private set => _guid = value; }

		/// <summary>
		/// 表示在上次保存后是否有值发生了改变
		/// </summary>
		public bool Changed {
			get => _changed;
			private set {
				bool oldvalue = _changed;
				_changed = value;
				if (value) {
					NodeValueChanged?.Invoke(this);
				}
				if (value != oldvalue) {
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Changed"));
				}
			}
		}

		#endregion

		private void Children_NodeValueChanged(IWorkLogicNodeAble node) {
			Changed = true;
		}

		/// <summary>
		/// 获取表示节点的XML节点，用于方便Project的管理
		/// </summary>
		/// <param name="xmlDocument"></param>
		/// <returns></returns>
		public XmlElement XmlNode(XmlDocument xmlDocument, bool save = false) {
			XmlElement node = xmlDocument.CreateElement("work");
			node.SetAttribute("guid", Guid.ToString());

			node.SetAttribute("dictionary", _workDirectionary.Name);
			node.SetAttribute("file", _workFile.Name);
			node.SetAttribute("name", NodeName);

			if (save) Changed = false;
			return node;
		}

		/// <summary>
		/// 保存工作到存储器
		/// </summary>
		/// <param name="saveEvenUnchanged">指示在工程没有发生更改时是否强制保存。false：不强制保存,true：强制保存</param>
		public void Save(bool saveEvenUnchanged = false) {
			if (!saveEvenUnchanged && !Changed) return;
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
				XmlNode fefactory = FrontEndNodes.XmlNode(document, true);
				root.AppendChild(fefactory);
			}

			// back end work
			if (BackEndNodes != null) {
				XmlNode befactory = BackEndNodes.XmlNode (document);
				root.AppendChild (befactory);
			}

			document.Save(_workFile.FullName);

			Changed = false;
		}

		#region 获得方法
		private Work(string workPath, string filename, string workName) {
			_workDirectionary = new DirectoryInfo(workPath);
			string imgDir = Path.Combine(workPath, "Images");
			_workResousesDirectionary = new DirectoryInfo(imgDir);
			string workFileFullPath = Path.Combine(workPath, filename);
			_workFile = new FileInfo(workFileFullPath);

			NodeName = workName;
			Guid = Guid.NewGuid();
			Childrens = new ObservableCollection<IWorkLogicNodeAble>();

			Images = new Resouses.ImageResourceManager(_workResousesDirectionary, this);
			FrontEndNodes = new FrontEndFactory(this);
			BackEndNodes = new BackEndFactory(this);

			Dispatcher = Dispatcher.FromThread (Thread.CurrentThread);

			Changed = false;
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

			//work.Childrens.Add(work.Images);
			//work.Childrens.Add(work.FrontEndNodes);

			return work;
		}

		public static Work NewWork(string workPath, string filename, string workName, MapCreater.MapCreaterFactory createrFactory) {
			Work re = NewWork(workPath, filename, workName);
			re.FrontEndNodes.SetCreater(createrFactory);

			return re;
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
							work.Images = Resouses.ImageResourceManager.LoadFromXmlNode(item, work._workResousesDirectionary, work);
						}
						break;
					case "frontendfactory": //前端工厂
						work.FrontEndNodes.InitByXMLNode(item);
						break;
					case "backendfactory": //后端工厂
						work.BackEndNodes.InitByXMLNode (item);
						break;
				}
				if (item.Name.ToLower() == "images") {

				}
			}

			work.Changed = false;
			return work;
		}
		#endregion

		#region 工具方法
		/// <summary>
		/// 从文件中加载一张图片
		/// </summary>
		/// <param name="path">图片路径</param>
		/// <returns></returns>
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
			} catch (Exception ex) {
				Console.WriteLine("read image fail");
				Console.WriteLine(ex.Message);
				result = null;
			}

			return result;
		}

		#endregion
	}
}
