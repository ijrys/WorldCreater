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
		public Project Project { get; private set; }
		public DialogResult DialogResult { get; private set; }

		public NewProject() {
			InitializeComponent();
		}

		private void BtnDirSelect_Click(object sender, RoutedEventArgs e) {
			FolderBrowserDialog path = new FolderBrowserDialog();
			if (path.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				txtProPath.Text = path.SelectedPath;
			}
		}

		public new void ShowDialog () {
			this.Project = null;
			this.DialogResult = DialogResult.Cancel;

			base.ShowDialog();
		}

		public new void Show () {
			this.Project = null;
			this.DialogResult = DialogResult.Cancel;

			base.Show();
		}

		public Project GetNewProject() {
			ShowDialog();
			if (DialogResult == DialogResult.OK) {
				return Project;
			}

			return null;
		}

		private void BtnOk_Click(object sender, RoutedEventArgs e) {
			string proFileName = WorldCreaterStudio_Core.Tools.Path.GetAFileName(txtProName.Text);
			try {
				Project = Project.NewProject(System.IO.Path.Combine(txtProPath.Text, proFileName), proFileName + ".mrimcpro", txtProName.Text);
				DialogResult = DialogResult.OK;
				Close();
			} catch (Exception ex) {
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		private void BtnCancel_Click(object sender, RoutedEventArgs e) {
			Close();
		}

		private void Tree_CreaterType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
			Resouses.NewWork.MapCreaterCollectionNode coll = tree_CreaterType.SelectedItem as Resouses.NewWork.MapCreaterCollectionNode;
			list_CreaterSelecter.ItemsSource = coll?.Creaters;
		}
	}
}
