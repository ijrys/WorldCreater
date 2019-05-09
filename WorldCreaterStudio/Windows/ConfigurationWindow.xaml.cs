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
using System.Windows.Shapes;

namespace WorldCreaterStudio.Windows {
	/// <summary>
	/// ConfigurationWindow.xaml 的交互逻辑
	/// </summary>
	public partial class ConfigurationWindow : Window {
		public ConfigurationWindow() {
			InitializeComponent();
			string uistyle = WorldCreaterStudio.Properties.Settings.Default.UIStyle;
			if (uistyle == "Dark") {
				ComboTheme.SelectedIndex = 0;
			}
			else if (uistyle == "Light") {
				ComboTheme.SelectedIndex = 1;
			}
			else {
				ComboTheme.SelectedIndex = 0;
			}

		}

		private void BtnOK_Click(object sender, RoutedEventArgs e) {
			WorldCreaterStudio.Properties.Settings.Default.UIStyle = (ComboTheme.SelectedItem as ComboBoxItem).Tag.ToString();
			WorldCreaterStudio.Properties.Settings.Default.Save();

			MessageBox.Show("部分设置将在应用重新启动后更新");
			this.Close();
		}

		private void BtnCancle_Click(object sender, RoutedEventArgs e) {
			this.Close();
		}
	}
}
