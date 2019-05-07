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
	/// About.xaml 的交互逻辑
	/// </summary>
	public partial class AboutWindow : Window {
		public AboutWindow() {
			InitializeComponent();

			stack_Info.Children.Add(new TextBlock() { Text = "AtmosphericMotion", Margin = new Thickness(2, 8, 2, 2) });
			foreach (var item in WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary.AtmosphericMotion.CalcFactories) {
				Expander expander = new Expander() {
					Header = item.DisplayName
				};

				TextBox text = new TextBox() {
					IsReadOnly = true,
				};
				text.AppendText(item.CalculaterProgramSet);
				text.AppendText(Environment.NewLine);

				text.AppendText(item.CalculaterGuid.ToString());
				text.AppendText(Environment.NewLine);

				Type itemtype = item.GetType();

				text.AppendText("    " + itemtype.AssemblyQualifiedName);
				text.AppendText(Environment.NewLine);

				text.AppendText("    " + itemtype.Module.Name);

				expander.Content = text;
				stack_Info.Children.Add(expander);
			}

			stack_Info.Children.Add(new TextBlock() { Text = "RainfallMotion", Margin = new Thickness(2, 8, 2, 2) });
			foreach (var item in WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary.RainfallMotion.CalcFactories) {
				Expander expander = new Expander() {
					Header = item.DisplayName
				};

				TextBox text = new TextBox() {
					IsReadOnly = true,
				};
				text.AppendText(item.CalculaterProgramSet);
				text.AppendText(Environment.NewLine);

				text.AppendText(item.CalculaterGuid.ToString());
				text.AppendText(Environment.NewLine);

				Type itemtype = item.GetType();

				text.AppendText("    " + itemtype.AssemblyQualifiedName);
				text.AppendText(Environment.NewLine);

				text.AppendText("    " + itemtype.Module.Name);

				expander.Content = text;
				stack_Info.Children.Add(expander);
			}

			stack_Info.Children.Add(new TextBlock() { Text = "SolarIlluminance", Margin = new Thickness(2, 8, 2, 2) });
			foreach (var item in WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary.SolarIlluminance.CalcFactories) {
				Expander expander = new Expander() {
					Header = item.DisplayName
				};

				TextBox text = new TextBox() {
					IsReadOnly = true,
				};
				text.AppendText(item.CalculaterProgramSet);
				text.AppendText(Environment.NewLine);

				text.AppendText(item.CalculaterGuid.ToString());
				text.AppendText(Environment.NewLine);

				Type itemtype = item.GetType();

				text.AppendText("    " + itemtype.AssemblyQualifiedName);
				text.AppendText(Environment.NewLine);

				text.AppendText("    " + itemtype.Module.Name);

				expander.Content = text;
				stack_Info.Children.Add(expander);
			}

			stack_Info.Children.Add(new TextBlock() { Text = "Biomes", Margin = new Thickness(2, 8, 2, 2) });
			foreach (var item in WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary.Biomes.CalcFactories) {
				Expander expander = new Expander() {
					Header = item.DisplayName
				};

				TextBox text = new TextBox() {
					IsReadOnly = true,
				};
				text.AppendText(item.CalculaterProgramSet);
				text.AppendText(Environment.NewLine);

				text.AppendText(item.CalculaterGuid.ToString());
				text.AppendText(Environment.NewLine);

				Type itemtype = item.GetType();

				text.AppendText("    " + itemtype.AssemblyQualifiedName);
				text.AppendText(Environment.NewLine);

				text.AppendText("    " + itemtype.Module.Name);

				expander.Content = text;
				stack_Info.Children.Add(expander);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			this.Close();
		}
	}
}
