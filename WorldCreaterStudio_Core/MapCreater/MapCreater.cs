using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core.MapCreater {
	public delegate void MapCreatingProcessingEvent(short permillage, string processDescription, bool freshImage, System.Windows.Media.ImageSource image);

	public abstract class MapCreater {
		public string CreaterName { get; set; }

		public abstract string CreaterProgramSet { get; }

		public abstract Guid CreaterGuid { get; }

		public abstract int[,] CreatAMap(Configuration configuration);

		public event MapCreatingProcessingEvent OnProcessingChanged;
	}
}
