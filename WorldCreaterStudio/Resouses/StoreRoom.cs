using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			//public MapCreaterTypeNode(IEnumerable<MapCreaterTypeNode> childrens) : this() {
			//	Childrens.AddRange(childrens);
			//}
			//public MapCreaterTypeNode(IEnumerable<MapCreaterTypeNode> childrens, IEnumerable<string> creaters) : this() {
			//	Creaters.AddRange(creaters);
			//	Childrens.AddRange(childrens);
			//}
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


		static StoreRoom() {

		}
	}
}
