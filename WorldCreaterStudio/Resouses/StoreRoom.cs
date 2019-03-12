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
		public static List<NewWork.MapCreaterTypeNode> MapCreaterCollection { get; private set; } = new List<NewWork.MapCreaterTypeNode>() {
			new NewWork.MapCreaterTypeNode(new WorldCreaterStudio_Core.MapCreater.MapCreaterFactory[] { new RandomTend.RandomTendFactory()}) {
				TypeName="Random Tend"
			},
			//new NewWork.MapCreaterTypeNode(new string[] { "Empty"}) {
			//	TypeName="Others"
			//}
		};


		static StoreRoom() {

		}
	}
}
