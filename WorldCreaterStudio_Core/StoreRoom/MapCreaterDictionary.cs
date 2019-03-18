using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCreaterStudio_Core.MapCreater;

namespace WorldCreaterStudio_Core.StoreRoom {
	/// <summary>
	/// 存储应用所有的MapCreater相关的资源
	/// </summary>
	public static class MapCreaterDictionary {
		/// <summary>
		/// 记录从ProgramSet到MapCreaterFactory的关系
		/// </summary>
		private static Dictionary<string, MapCreaterFactory> _programSetToCreaterFactory = new Dictionary<string, MapCreater.MapCreaterFactory>();

		/// <summary>
		/// 根据ProgramSet获取一个MapCreaterFactory
		/// </summary>
		/// <param name="programSet"></param>
		/// <returns></returns>
		public static MapCreaterFactory GetMapCreaterFactoryByProgramSet (string programSet) {
			if (string.IsNullOrEmpty(programSet)) return null;
			if (_programSetToCreaterFactory.ContainsKey (programSet)) {
				return _programSetToCreaterFactory[programSet];
			}
			return null;
		}

		/// <summary>
		/// 注册一个CreaterFactory
		/// </summary>
		/// <param name="createrFactory"></param>
		public static void RegisterACreaterFactory(MapCreaterFactory createrFactory) {
			//注册programSet
			_programSetToCreaterFactory[createrFactory.CreaterProgramSet] = createrFactory;
		}
	}
}
