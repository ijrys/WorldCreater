using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using System.Collections.ObjectModel;
using WorldCreaterStudio_Core;

namespace WorldCreaterStudio {

	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window {
		public static class Commands {
			public static RoutedUICommand NewWork { get; private set; }
			public static RoutedUICommand NewProject { get; private set; }
			static Commands() {
				//New Work
				InputGestureCollection igc1 = new InputGestureCollection();
				igc1.Add(new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl+N"));
				NewWork = new RoutedUICommand("New Work", "NewWork", typeof(Commands), igc1);

				//New Project
				igc1 = new InputGestureCollection();
				igc1.Add(new KeyGesture(Key.N, ModifierKeys.Control | ModifierKeys.Shift, "Ctrl+Shift+N"));
				NewProject = new RoutedUICommand("New Project", "NewProject", typeof(Commands), igc1);
			}
		}

		/// <summary>
		/// 属性Project改变的Event基本类型
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="value"></param>
		public delegate void ProjectChangedEventType(object sender, Project value);
		private Project _project;
		public Project Project {
			get => _project;
			set {
				_project = value;
				ProjectChanged?.Invoke(this, value);
				ProjectChangedFunction(value);
			}
		}
		/// <summary>
		/// 属性Project改变后
		/// </summary>
		public event ProjectChangedEventType ProjectChanged;

		public bool CanNewWork { get; private set; }

		public MainWindow() {
			//string workDir = "A:\\ProjectDemo";
			//if (Directory.Exists(workDir)) {
			//	foreach(string dpath in Directory.GetDirectories(workDir)) {
			//		Directory.Delete(dpath, true);
			//	}
			//	foreach (string fpath in Directory.GetFiles(workDir)) {
			//		File.Delete(fpath);
			//	}
			//}

			InitializeComponent();

			//ObservableCollection<WorldCreaterStudio_Core.Work> works = new ObservableCollection<WorldCreaterStudio_Core.Work>();
			//Tree_Project.ItemsSource = works;

			//WorldCreaterStudio_Core.Work work1 = WorldCreaterStudio_Core.Work.NewWork("A:\\ProjectDemo\\Work1", "Work1.mrimcw", "work1");
			//WorldCreaterStudio_Core.Work work2 = WorldCreaterStudio_Core.Work.NewWork("A:\\ProjectDemo\\Work2", "Work2.mrimcw", "work2");
			//work1.Icon = Resouses.Icons.Dark_Icon_Work;
			//work2.Icon = Resouses.Icons.Dark_Icon_Work;

			//works.Add(work1);
			//works.Add(work2);

			CommandBinding newCommand = new CommandBinding(Commands.NewProject);
			newCommand.Executed += Command_NewProject_Executed;
			CommandBindings.Add(newCommand);

			newCommand = new CommandBinding(Commands.NewWork);
			newCommand.Executed += Command_NewWork_Executed;
			CommandBindings.Add(newCommand);
		}

		private void Command_NewWork_Executed(object sender, ExecutedRoutedEventArgs e) {
			Windows.NewProject newProject = new Windows.NewProject();

			WorldCreaterStudio_Core.Project project = newProject.GetNewProject();
			if (project == null) {
				MessageBox.Show("操作已取消");
			} else {
				Project = project;
			}
		}

		private void Command_NewProject_Executed(object sender, ExecutedRoutedEventArgs e) {
			Windows.NewProject newProject = new Windows.NewProject();

			WorldCreaterStudio_Core.Project project = newProject.GetNewProject();
			if (project == null) {
				MessageBox.Show("操作已取消");
			} else {
				Project = project;
			}
		}

		private void ProjectChangedFunction(Project value) {
			if (value == null) {
				Tree_Project.ItemsSource = null;
			} else {
				Tree_Project.ItemsSource = value.Childrens;
			}
		}


	}

}
