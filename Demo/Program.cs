using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using  WorldCreaterStudio_Core;
using  WorldCreaterStudio_Resouses;

namespace Demo {
	class Program {
		static void Main(string[] args) {
			DoFun1();
			//DoFun2();
			//Type type = typeof(RandomTend.RandomTendCreater);
			//Console.WriteLine(type.FullName);
			//Console.WriteLine(type.Module);
			//Console.WriteLine(type.GUID);
			//int[,] a = new int[8, 16];
			//Console.WriteLine(a.GetLength(0));
			//Console.WriteLine(a.GetLength(1));
			//string path = @"F:\[120808] 010\[1106...C+BK).rar";
			//Console.WriteLine(Path.GetDirectoryName(path));
			//Console.WriteLine(Path.GetFileName(path));

			Console.WriteLine("end work, press enter to exit");
			Console.ReadLine();
		}

		static void DoFun2 () {
			string fname = "123..456//{[(\\|\"':;!@#$%^&*+-*/)]}aa";
			string outname = WorldCreaterStudio_Core.Tools.Path.GetAFileName(fname);

			Console.WriteLine(fname);
			Console.WriteLine(outname);
		}

		static void DoFun1 () {
			string proDir = "a:/demo Pro";
			Console.WriteLine("清除缓存集");
			if (Directory.Exists(proDir)) {
				Directory.Delete(proDir, true);
			}
			Console.WriteLine("缓存集清除完毕，press enter to continue");
			Console.ReadLine();
			WorldCreaterStudio_Core.StoreRoom.MapCreaterDictionary.RegisterACreaterFactory(new RandomTend.RandomTendCreaterFactory());

			Project project = Project.NewProject(proDir, "demo Pro.mriwcpro", "DemoPro");

			string workDir = Path.Combine(proDir, "demowork");

			//创建工作集
			Work work = project.NewWork("demowork", "demowork.mriwcw", "测试工作集");
			work.FrontEndNodes.SetCreater("MiRaI.RandomTend.RandomTend|0.1");
			Console.WriteLine("work creat finish");

			//修改工作集并保存
			work.Images.Add("image1", Images.Dark_Icon_Pro, "测试图片1-工作集图标");
			RandomTend.RTConfiguration config = work.FrontEndNodes.Configuration as RandomTend.RTConfiguration;
			config.RandomSeed = 1024;
			config.WidthBlockNum = 5;
			config.HeightBlockNum = 4;

			project.Save(true);
			Console.WriteLine("work save finish");


			//打开工程
			project = Project.OpenProject(proDir, "demo Pro.mriwcpro");
			PrintProject(project);
		}

		static void PrintProject (Project project) {
			Console.WriteLine($"Project: {project.NodeName}");

			foreach (var item in project.Childrens) {
				if (!(item is Work)) continue;
				PrintWork(item as Work);
			}
		}

		static void PrintWork (Work work) {
			Console.WriteLine($"{work.NodeName} -- {work.Guid}");
			Console.WriteLine("|-- [image resouses]");
			foreach (var item in work.Images.Childrens) {
				Console.WriteLine($"|   |-- {item.NodeName} : {(item as WorldCreaterStudio_Core.Resouses.ImageResourse).Description}");
			}

			Console.WriteLine("|");
			Console.WriteLine("|-- Front End Work");
			if (work.FrontEndNodes == null) {
				Console.WriteLine($"|   |-- [NULL]");
			} else {
				Console.WriteLine($"|   |-- {work.FrontEndNodes.NodeName}");
				if (work.FrontEndNodes.Configuration != null) {
					Console.WriteLine(work.FrontEndNodes.Configuration.ToString());
				}
			}

			Console.WriteLine("|");
			Console.WriteLine("|-- Back End Work");
			if (work.BackEndNodes == null) {
				Console.WriteLine($"   |-- [NULL]");
			} else {
				Console.WriteLine($"   |-- {work.BackEndNodes.NodeName}");
			}
		}
	}
}
