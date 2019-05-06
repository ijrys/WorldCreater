using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.IO;
using System.ComponentModel;

namespace WorldCreaterStudio_Core {
	/// <summary>
	/// 表示一个工程
	/// </summary>
	public class Project : IWorkLogicNodeAble {
		DirectoryInfo _projectDirectionary;
		FileInfo _projectFile;

		public event NodeValueChangedEventType NodeValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		public Work Work => null;

		public System.Windows.Controls.ControlTemplate ShowPanel => null;
		public string NodeName { get; private set; }
		public ImageSource Icon { get; private set; }
		public ObservableCollection<IWorkLogicNodeAble> Childrens { get; private set; }

		public Guid Guid { get; set; }
		/// <summary>
		/// 工程文件夹信息
		/// </summary>
		public DirectoryInfo ProjectDirectionary {
			get => _projectDirectionary;
			private set { _projectDirectionary = value; }
		}
		/// <summary>
		/// 工程文件信息
		/// </summary>
		public FileInfo ProjectFile {
			get => _projectFile;
			set => _projectFile = value;
		}
		/// <summary>
		/// 工程文件是否发生更改
		/// </summary>
		public bool Changed { get; private set; }

		public XmlElement XmlNode(XmlDocument xmlDocument, bool save = false) {
			throw new NotImplementedException();
		}

		public void Save(bool saveEvenUnchanged = false) {
			if (!saveEvenUnchanged && !Changed) return;

			XmlDocument document = new XmlDocument();
			XmlElement root = document.CreateElement("project");
			document.AppendChild(root);
			root.SetAttribute("guid", Guid.ToString());
			root.SetAttribute("name", NodeName);

			foreach (var item in Childrens) {
				if (item is Work) {
					Work wit = (item as Work);
					wit.Save();
					root.AppendChild(wit.XmlNode(document));
				}
			}


			document.Save(_projectFile.FullName);
			Changed = false;
		}

		public Work NewWork(string workPath, string filename, string workName) {
			string worDir = Path.Combine(_projectDirectionary.FullName, workPath);
			Work work = Work.NewWork(worDir, filename, workName);
			Childrens.Add(work);
			Changed = true;

			return work;
		}

		private Project(string projectPath, string filename, string proName) {
			_projectDirectionary = new DirectoryInfo(projectPath);
			string projFileFullPath = Path.Combine(projectPath, filename);
			_projectFile = new FileInfo(projFileFullPath);
			NodeName = proName;
			Childrens = new ObservableCollection<IWorkLogicNodeAble>();
			Guid = Guid.NewGuid();
		}

		public static Project OpenProject(string projectPath, string filename) {
			Project project = new Project(projectPath, filename, "");
			XmlDocument document = new XmlDocument();
			document.Load(project.ProjectFile.FullName);
			XmlNode root = document.FirstChild;
			if (root.Name != "project") return null;
			project.NodeName = root.Attributes["name"].Value;

			foreach (XmlElement item in root.ChildNodes) {
				if (item.Name != "work") continue;
				try {
					//读取一个work
					string workdir = Path.Combine(project.ProjectDirectionary.FullName, item.Attributes["dictionary"].Value);
					string workfile = item.Attributes["file"].Value;
					string workName = item.Attributes["name"].Value;
					System.Guid workguid = new Guid(item.Attributes["guid"].Value);
					Work work = Work.OpenWork(workdir, workfile, workName);
					if (work.Guid != workguid) {
						throw new Exceptions.GuidNotSameException();
					}

					project.Childrens.Add(work);
				}
				catch (Exceptions.GuidNotSameException) {

				}
				catch (Exception ex) {

				}

			}

			return project;
		}

		public static Project NewProject(string projectPath, string filename, string projectName) {
			if (Directory.Exists(projectPath)) {
				throw new Exceptions.DirectoryExistedException(projectPath);
			}
			Project project = new Project(projectPath, filename, projectName);
			//文件、目录准备
			project._projectDirectionary.Create();

			project.Save(true);

			return project;
		}
	}
}
