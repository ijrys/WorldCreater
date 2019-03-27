﻿using System;
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
using System.Threading;

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

			//命令绑定
			CommandBinding newCommand = new CommandBinding(Commands.NewProject);
			newCommand.Executed += Command_NewProject_Executed;
			CommandBindings.Add(newCommand);

			newCommand = new CommandBinding(Commands.NewWork);
			newCommand.Executed += Command_NewWork_Executed;
			CommandBindings.Add(newCommand);

			newCommand = new CommandBinding(Commands.Save);
			newCommand.Executed += Command_Save_Executed; ;
			CommandBindings.Add(newCommand);

			//面板注册
			RegShowPanel();
		}

		private void Command_Save_Executed(object sender, ExecutedRoutedEventArgs e) {
			if (Project == null) return;
			Project.Save();
			foreach (var item in Project.Childrens) {
				if (item is Work) {
					(item as Work).Save();
				}
			}

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

		#region 功能面板相关
		/// <summary>
		/// 向Core注册展示板
		/// </summary>
		private void RegShowPanel() {
			ControlTemplate panel = Resources["frontEndFactoryShowPanel"] as ControlTemplate;
			WorldCreaterStudio_Core.StoreRoom.ShowPanel.FrontEndFactoryPanel = panel;
			WorldCreaterStudio_Core.StoreRoom.ShowPanel.ImagePanel = Resources["ImageShowPanel"] as ControlTemplate;
		}

		/// <summary>
		/// 表示正在展示的面板
		/// </summary>
		private ControlTemplate _showingFunctionPanel;

		/// <summary>
		/// 展示一个功能面板
		/// </summary>
		/// <param name="panel"></param>
		private void ShowFunctionPanel(object dataprovider, ControlTemplate panel) {
			if (panel == null || dataprovider == null) return;
			if (_showingFunctionPanel != panel) {
				FunctionPanelConter.Template = panel;
				//FunctionPanelConter.Children.Add(panel);
				_showingFunctionPanel = panel;
			}
			ImgShow.Visibility = Visibility.Collapsed;
			FunctionPanelConter.DataContext = dataprovider;
		}

		private void Tree_Project_Item_DoubleClick(object sender, MouseButtonEventArgs e) {
			Button btn = sender as Button;
			IWorkLogicNodeAble workLogicNode = btn.DataContext as IWorkLogicNodeAble;
			ShowFunctionPanel(workLogicNode, workLogicNode.ShowPanel);
		}

		private void ShowAImage(ImageSource image) {
			ImgShow.Source = image;
			ImgShow.Visibility = Visibility.Visible;
		}
		#endregion

		private void btn_NewGra_Click(object sender, RoutedEventArgs e) {
			FrontEndFactory fefactory = FunctionPanelConter.DataContext as FrontEndFactory;
			if (fefactory == null) return;
			fefactory.Creater.OnProcessingChanged += Creater_OnProcessingChanged;
			fefactory.Creater.CreatAMap(fefactory.Configuration, fefactory.Work);

			
		}

		private delegate void OnProcessingChangedDelegate(short permillage, string processDescription, bool freshImage, ImageSource image);
		private void Creater_OnProcessingChanged(short permillage, string processDescription, bool freshImage, ImageSource image) {
			this.txtState.Text = processDescription;
			this.txtPermillage.Text = permillage.ToString();
			if (freshImage) {
				ShowAImage(image);
			}
			this.UpdateLayout();
		}
	}

}
