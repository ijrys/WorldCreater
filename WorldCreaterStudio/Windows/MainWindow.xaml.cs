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
		/// <summary>
		/// 属性Project改变的Event基本类型
		/// </summary>
		/// <param name="sender">触发改变的对象</param>
		/// <param name="value">新的Project</param>
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

		//public bool CanNewWork { get; private set; }

		public MainWindow() {
			InitializeComponent();

			CommandBinding newCommand = new CommandBinding(Commands.NewProject);
			newCommand.Executed += Command_NewProject_Executed;
			CommandBindings.Add(newCommand);

			newCommand = new CommandBinding(Commands.NewWork);
			newCommand.Executed += Command_NewWork_Executed;
			CommandBindings.Add(newCommand);
		}

		private void Command_NewWork_Executed(object sender, ExecutedRoutedEventArgs e) {
			Windows.NewProject newProject = new Windows.NewProject();

			Work work = newProject.GetNewWork(this.Project);
			if (work == null) {
				MessageBox.Show("操作已取消");
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
