using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorldCreaterStudio_Core;

namespace WorldCreaterStudio.Windows {
	/// <summary>
	/// NewProject.xaml 的交互逻辑
	/// </summary>
	public partial class NewProject : Window {
		private Project Project { get; set; }
		private Work Work { get; set; }
		private bool CreateNewProject { get; set; }

		public DialogResult DialogResult { get; private set; }

		public NewProject() {
			InitializeComponent();

			list_CreaterType.ItemsSource = Resouses.StoreRoom.MapCreaterCollection;
		}

		private void BtnDirSelect_Click(object sender, RoutedEventArgs e) {
			FolderBrowserDialog path = new FolderBrowserDialog();
			if (path.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				txtProPath.Text = path.SelectedPath;
			}
		}

		private void ShowDialog(Project project) {
			this.Project = project;
			this.DialogResult = DialogResult.Cancel;

			base.ShowDialog();
		}

		public new void ShowDialog() {
			this.Project = null;
			this.DialogResult = DialogResult.Cancel;

			base.ShowDialog();
		}

		public new void Show() {
			this.Project = null;
			this.DialogResult = DialogResult.Cancel;

			base.Show();
		}

		public Project GetNewProject() {
			txtProPath.IsReadOnly = false;
			txtProName.IsReadOnly = false;
			CreateNewProject = true;

			ShowDialog();
			if (DialogResult == DialogResult.OK) {
				return Project;
			}

			return null;
		}

		public Work GetNewWork(Project project) {
			txtProPath.IsReadOnly = true;
			txtProName.IsReadOnly = true;
			btnPathSelect.IsEnabled = false;
			CreateNewProject = false;
			//Project = project;
			txtProPath.Text = project.ProjectDirectionary.FullName;
			txtProName.Text = project.NodeName;

			ShowDialog(project);
			if (DialogResult == DialogResult.OK) {
				return Work;
			}

			return null;
		}

		private void BtnOk_Click(object sender, RoutedEventArgs e) {

			try {
				if (CreateNewProject) { //新工程模式
					string proFileName = WorldCreaterStudio_Core.Tools.Path.GetAFileName(txtProName.Text);
					Project = Project.NewProject(System.IO.Path.Combine(txtProPath.Text, proFileName), proFileName + ".mrimcpro", txtProName.Text);
				}

				//添加work
				string workPath = WorldCreaterStudio_Core.Tools.Path.GetAFileName(txtWorkName.Text);
				Work = Project.NewWork(workPath, workPath + ".mrimcwork", txtWorkName.Text);
				DialogResult = DialogResult.OK;
				Close();
			} catch (Exception ex) {
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		private void BtnCancel_Click(object sender, RoutedEventArgs e) {
			Close();
		}

		private void List_CreaterType_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			Resouses.NewWork.MapCreaterTypeNode createrType = list_CreaterType.SelectedItem as Resouses.NewWork.MapCreaterTypeNode;
			list_CreaterSelecter.ItemsSource = createrType.Creaters;
		}
	}
}
