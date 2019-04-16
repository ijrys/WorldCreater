using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCreaterStudio_Core.MapCreater;
using WorldCreaterStudio_Core.BackendNode;

namespace WorldCreaterStudio.Resouses {
	namespace NewWork {
		public class MapCreaterTypeNode {
			public string TypeName { get; set; }
			//public List<MapCreaterTypeNode> Childrens { get; private set; }

			public List<WorldCreaterStudio_Core.MapCreater.MapCreaterFactory> Creaters { get; set; }

			public MapCreaterTypeNode() {
				//Childrens = new List<MapCreaterTypeNode>();
				Creaters = new List<WorldCreaterStudio_Core.MapCreater.MapCreaterFactory>();
			}

			public MapCreaterTypeNode(IEnumerable<WorldCreaterStudio_Core.MapCreater.MapCreaterFactory> creaters) : this() {
				Creaters.AddRange(creaters);
			}

		}

	}

	public static class StoreRoom {
		/// <summary>
		/// 获取MapCreaterTypeNode列表
		/// </summary>
		public static List<NewWork.MapCreaterTypeNode> MapCreaterCollection {
			get {
				return _mapCreaterTypeMayToCollection.Values.ToList();
			}
		}

		private static Dictionary<string, NewWork.MapCreaterTypeNode> _mapCreaterTypeMayToCollection { get; set; } = new Dictionary<string, NewWork.MapCreaterTypeNode>();

		/// <summary>
		/// 注册一个CreaterFactory到创建列表和创建者工厂列表【WorldCreaterStudio_Core.StoreRoom.MapCreaterDictionary】
		/// </summary>
		/// <param name="createrFactory"></param>
		public static void RegisterACreaterFactory(MapCreaterFactory createrFactory) {
			if (!_mapCreaterTypeMayToCollection.ContainsKey(createrFactory.DisplayType)) {
				_mapCreaterTypeMayToCollection[createrFactory.DisplayType] = new NewWork.MapCreaterTypeNode() {
					TypeName = createrFactory.DisplayType
				};
			}
			_mapCreaterTypeMayToCollection[createrFactory.DisplayType].Creaters.Add(createrFactory);

			WorldCreaterStudio_Core.StoreRoom.MapCreaterDictionary.RegisterACreaterFactory(createrFactory);
		}

		static StoreRoom() {

		}
	}

	public static class BackEndCalcFactories {
		public static IEnumerable<WorldCreaterStudio_Core.BackendNode.AtmosphericMotion.IAtmosphericMotionCalculaterFactoryAble> AM => WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary.AtmosphericMotion.CalcFactories;
		public static IEnumerable<WorldCreaterStudio_Core.BackendNode.RainfallMotion.IRainfallMotionCalculaterFactoryAble> RM => WorldCreaterStudio_Core.StoreRoom.BackEndCalculaterDictionary.RainfallMotion.CalcFactories;
	}
}
