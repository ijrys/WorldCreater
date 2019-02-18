using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core.Tools {
	public static class Path {
		public static string GetAFileName(string name) {
			char[] arr = new char[name.Length];
			int arrlen = 0;
			foreach (var item in name) {
				if (item < 40) continue; //小于48直接跳过
				if (item < 128) {
					if ((item >= '0' && item <= '9') ||
						(item >= 65 && item <= 91) ||
						(item >= 97 && item <= 123) ||
						(item == ']') ||
						(item == '}') ||
						(item == '(') ||
						(item == ')')) {
						arr[arrlen] = item;
						arrlen++;
					} else {
						arr[arrlen] = '_';
						arrlen++;
					}
				} else {
					arr[arrlen] = item;
					arrlen++;
				}
			}

			return new string(arr, 0, arrlen);
		}
	}
}
