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
			string workDir = "a:/demo work";
			Console.WriteLine("清除缓存集");
			if (Directory.Exists(workDir)) {
				Directory.Delete(workDir, true);
			}
			Console.WriteLine("缓存集清除完毕，press enter to continue");
			Console.ReadLine();

			//创建工作集
			Work work = Work.NewWork(workDir, "demowork.mriwcw", "测试工作集");
			Console.WriteLine("work creat finish");

			//修改工作集并保存
			work.Images.Add("image1", Images.Dark_Icon_Pro, "测试图片1-工作集图标");
			work.Save(true);
			Console.WriteLine("work save finish");

			//打开工作集
			Work work2 = Work.OpenWork(workDir, "demowork.mriwcw", "测试工作集");
			PrintWork(work2);

			//修改工作集并保存
			work2.Images.Add("image2", Images.Dark_Icon_Gra, "测试图片1-地形图标");
			work2.Save(true);
			PrintWork(work2);
			Console.WriteLine("work save finish");



			Console.WriteLine("end work, press enter to exit");
			Console.ReadLine();
		}

		static void PrintWork (Work work) {
			Console.WriteLine($"{work.NodeName} -- {work.Guid}");
			Console.WriteLine("|-- [image resouses]");
			foreach (var item in work.Images.Childrens) {
				Console.WriteLine($"|   |-- {item.NodeName} : {(item as ImageResourse).Description}");
			}

			Console.WriteLine("|");
			Console.WriteLine("|-- Front End Work");
			if (work.FrontEndNodes == null) {
				Console.WriteLine($"|   |-- [NULL]");
			} else {
				Console.WriteLine($"|   |-- {work.FrontEndNodes.NodeName}");
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
