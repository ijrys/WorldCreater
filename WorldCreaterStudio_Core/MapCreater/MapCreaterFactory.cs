using System;

namespace WorldCreaterStudio_Core.MapCreater {
	public abstract class MapCreaterFactory {
		public abstract string DisplayName { get; }
		public abstract string DisplayType { get; }

		public abstract string CreaterProgramSet { get; }

		public abstract Guid CreaterGuid { get; }

		public abstract MapCreater GetACreater();
	}
}
