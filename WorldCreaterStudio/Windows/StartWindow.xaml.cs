using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
	/// StartWindow.xaml 的交互逻辑
	/// </summary>
	public partial class StartWindow : Window {
		public StartWindow() {
			InitializeComponent();

			Dofun();
		}

		public async void Dofun () {
			await Task.Delay(20);
			this.Dispatcher.InvokeAsync(OpenMainWindow);
		}

		public async void OpenMainWindow() {
			Thread.Sleep(1000);

			//读取UI主题
			string uistyle = WorldCreaterStudio.Properties.Settings.Default.UIStyle;
			if (uistyle == "Dark") {
				Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/Resouses/Theme/Dark.xaml") });
			}
			else if (uistyle == "Light") {
				Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/Resouses/Theme/Light.xaml") });
			} else {
				Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(@"pack://application:,,,/Resouses/Theme/Dark.xaml") });
			}

			Thread.Sleep(200);
			//添加算法资源
			Resouses.StoreRoom.RegisterACreaterFactory(new RandomTend.RandomTendCreaterFactory());
			WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary.AtmosphericMotion.RegisterACreaterFactory(new MiRaI.BE.AM.SingleValue.SingleValueFactory());
			WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary.RainfallMotion.RegisterACreaterFactory(new MiRaI.BE.RM.SingleValue.SingleValueFactory());
			WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary.SolarIlluminance.RegisterACreaterFactory(new MiRaI.BE.SI.QuickCalculating.QuickCalculatingFactory());
			WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary.Biomes.RegisterACreaterFactory(new MiRaI.BE.BI.QuickCalc.QuickCalcFactory());



			//启动主窗体
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			this.Focus();

			await Task.Delay(1000);
			this.Opacity = 0.9;
			await Task.Delay(100);
			this.Opacity = 0.5;
			await Task.Delay(100);
			this.Opacity = 0.1;
			await Task.Delay(100);
			this.Close();
		}
	}
}
