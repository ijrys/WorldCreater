using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCreaterStudio_Core.BackendNode.AtmosphericMotion;
using WorldCreaterStudio_Core.BackendNode.Biomes;
using WorldCreaterStudio_Core.BackendNode.RainfallMotion;
using WorldCreaterStudio_Core.BackendNode.SolarIlluminance;

namespace WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary {
	public static class AtmosphericMotion {
		/// <summary>
		/// 记录从ProgramSet到MapCreaterFactory的关系
		/// </summary>
		private static Dictionary<string, IAtmosphericMotionCalculaterFactoryAble> _programSetToCreaterFactory = new Dictionary<string, IAtmosphericMotionCalculaterFactoryAble> ();

		private static List<IAtmosphericMotionCalculaterFactoryAble> _calcFactories;
		public static IEnumerable<IAtmosphericMotionCalculaterFactoryAble> CalcFactories {
			get => _calcFactories;
		}

		/// <summary>
		/// 根据ProgramSet获取一个MapCreaterFactory
		/// </summary>
		/// <param name="programSet"></param>
		/// <returns></returns>
		public static IAtmosphericMotionCalculaterFactoryAble GetCreaterFactoryByProgramSet (string programSet) {
			if (string.IsNullOrEmpty (programSet)) return null;
			if (_programSetToCreaterFactory.ContainsKey (programSet)) {
				return _programSetToCreaterFactory[programSet];
			}
			return null;
		}

		/// <summary>
		/// 注册一个CreaterFactory
		/// </summary>
		/// <param name="createrFactory"></param>
		public static void RegisterACreaterFactory (IAtmosphericMotionCalculaterFactoryAble createrFactory) {
			_calcFactories = new List<IAtmosphericMotionCalculaterFactoryAble> ();
			//注册programSet
			_programSetToCreaterFactory[createrFactory.CalculaterProgramSet] = createrFactory;
			_calcFactories.Add (createrFactory);
		}
	}

	public static class RainfallMotion {
		/// <summary>
		/// 记录从ProgramSet到MapCreaterFactory的关系
		/// </summary>
		private static Dictionary<string, IRainfallMotionCalculaterFactoryAble> _programSetToCreaterFactory = new Dictionary<string, IRainfallMotionCalculaterFactoryAble> ();

		private static List<IRainfallMotionCalculaterFactoryAble> _calcFactories;
		public static IEnumerable<IRainfallMotionCalculaterFactoryAble> CalcFactories {
			get => _calcFactories;
		}

		/// <summary>
		/// 根据ProgramSet获取一个MapCreaterFactory
		/// </summary>
		/// <param name="programSet"></param>
		/// <returns></returns>
		public static IRainfallMotionCalculaterFactoryAble GetCreaterFactoryByProgramSet (string programSet) {
			if (string.IsNullOrEmpty (programSet)) return null;
			if (_programSetToCreaterFactory.ContainsKey (programSet)) {
				return _programSetToCreaterFactory[programSet];
			}
			return null;
		}

		/// <summary>
		/// 注册一个CreaterFactory
		/// </summary>
		/// <param name="createrFactory"></param>
		public static void RegisterACreaterFactory (IRainfallMotionCalculaterFactoryAble createrFactory) {
			_calcFactories = new List<IRainfallMotionCalculaterFactoryAble> ();
			//注册programSet
			_programSetToCreaterFactory[createrFactory.CalculaterProgramSet] = createrFactory;
			_calcFactories.Add (createrFactory);
		}
	}

	public static class SolarIlluminance {
		/// <summary>
		/// 记录从ProgramSet到MapCreaterFactory的关系
		/// </summary>
		private static Dictionary<string, ISolarIlluminanceCalculaterFactoryAble> _programSetToCreaterFactory = new Dictionary<string, ISolarIlluminanceCalculaterFactoryAble> ();

		private static List<ISolarIlluminanceCalculaterFactoryAble> _calcFactories;
		public static IEnumerable<ISolarIlluminanceCalculaterFactoryAble> CalcFactories {
			get => _calcFactories;
		}

		/// <summary>
		/// 根据ProgramSet获取一个MapCreaterFactory
		/// </summary>
		/// <param name="programSet"></param>
		/// <returns></returns>
		public static ISolarIlluminanceCalculaterFactoryAble GetCreaterFactoryByProgramSet (string programSet) {
			if (string.IsNullOrEmpty (programSet)) return null;
			if (_programSetToCreaterFactory.ContainsKey (programSet)) {
				return _programSetToCreaterFactory[programSet];
			}
			return null;
		}

		/// <summary>
		/// 注册一个CreaterFactory
		/// </summary>
		/// <param name="createrFactory"></param>
		public static void RegisterACreaterFactory (ISolarIlluminanceCalculaterFactoryAble createrFactory) {
			_calcFactories = new List<ISolarIlluminanceCalculaterFactoryAble> ();
			//注册programSet
			_programSetToCreaterFactory[createrFactory.CalculaterProgramSet] = createrFactory;
			_calcFactories.Add (createrFactory);
		}
	}

	public static class Biomes {
		/// <summary>
		/// 记录从ProgramSet到MapCreaterFactory的关系
		/// </summary>
		private static Dictionary<string, IBiomesCalculaterFactoryAble> _programSetToCreaterFactory = new Dictionary<string, IBiomesCalculaterFactoryAble> ();

		private static List<IBiomesCalculaterFactoryAble> _calcFactories;
		public static IEnumerable<IBiomesCalculaterFactoryAble> CalcFactories {
			get => _calcFactories;
		}

		/// <summary>
		/// 根据ProgramSet获取一个MapCreaterFactory
		/// </summary>
		/// <param name="programSet"></param>
		/// <returns></returns>
		public static IBiomesCalculaterFactoryAble GetCreaterFactoryByProgramSet (string programSet) {
			if (string.IsNullOrEmpty (programSet)) return null;
			if (_programSetToCreaterFactory.ContainsKey (programSet)) {
				return _programSetToCreaterFactory[programSet];
			}
			return null;
		}

		/// <summary>
		/// 注册一个CreaterFactory
		/// </summary>
		/// <param name="createrFactory"></param>
		public static void RegisterACreaterFactory (IBiomesCalculaterFactoryAble createrFactory) {
			_calcFactories = new List<IBiomesCalculaterFactoryAble> ();
			//注册programSet
			_programSetToCreaterFactory[createrFactory.CalculaterProgramSet] = createrFactory;
			_calcFactories.Add (createrFactory);
		}
	}
}
