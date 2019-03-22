using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
	class Program {
		static void Main(string[] args) {
			int[,] a = new int[8, 16];
			Console.WriteLine(a.GetLength(0));
			Console.WriteLine(a.GetLength(1));


			Console.WriteLine("end work, press enter to exit");
			Console.ReadLine();
		}
	}
}
