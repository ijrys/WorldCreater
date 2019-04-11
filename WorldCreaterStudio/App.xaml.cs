﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WorldCreaterStudio {
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application {
		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);
			//添加FE
			Resouses.StoreRoom.RegisterACreaterFactory(new RandomTend.RandomTendCreaterFactory());
			WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary.AtmosphericMotion.RegisterACreaterFactory (new MiRaI.BE.AM.SingleValue.SingleValueFactory ());
		}
	}
}
