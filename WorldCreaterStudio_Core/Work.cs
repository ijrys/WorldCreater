using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCreater.BaseType;

namespace WorldCreaterStudio_Core {
	class Work {
		/// <summary>
		/// 缓存的图像文件
		/// </summary>
		class Image {
			string name;
			string filePath;
			string description;
		}


		string _guid;
		Config _config;
		Dictionary<string, Image> Images;

		Dictionary<string, Image> first

		/// <summary>
		/// 随机值
		/// </summary>
		int[,] _randomMap;

		/// <summary>
		/// 高度图
		/// </summary>
		int[,] _heightMap;

		/// <summary>
		/// 地势图【起伏程度】
		/// </summary>
		byte[,] _terrainMap;
	}
}
