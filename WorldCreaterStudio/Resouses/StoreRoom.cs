using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio.Resouses {
	namespace NewWork {
		public class MapCreaterCollectionNode {
			public string CollectionName { get; set; }
			public List<MapCreaterCollectionNode> Childrens { get; private set; }

			public List<string> Creaters { get; set; }

			public MapCreaterCollectionNode() {
				Childrens = new List<MapCreaterCollectionNode>();
				Creaters = new List<string>();
			}

			public MapCreaterCollectionNode(IEnumerable<string> creaters) : this() {
				Creaters.AddRange(creaters);
			}

			public MapCreaterCollectionNode(IEnumerable<MapCreaterCollectionNode> childrens) : this() {
				Childrens.AddRange(childrens);
			}
			public MapCreaterCollectionNode(IEnumerable<MapCreaterCollectionNode> childrens, IEnumerable<string> creaters) : this() {
				Creaters.AddRange(creaters);
				Childrens.AddRange(childrens);
			}
		}

	}

	public static class StoreRoom {
		public static List<NewWork.MapCreaterCollectionNode> MapCreaterCollection { get; private set; } = new List<NewWork.MapCreaterCollectionNode>() {
			new NewWork.MapCreaterCollectionNode(new string[] { "Random Tend"}) {
				CollectionName="Random Tend"
			},
			new NewWork.MapCreaterCollectionNode(new string[] { "Empty"}) {
				CollectionName="Others"
			}
		};


		static StoreRoom() {

		}
	}
}
