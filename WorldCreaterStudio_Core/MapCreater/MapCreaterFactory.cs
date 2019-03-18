using System;

namespace WorldCreaterStudio_Core.MapCreater {
	public abstract class MapCreaterFactory {
		public abstract string DisplayName { get; }
		public abstract string DisplayType { get; }

		public abstract string CreaterProgramSet { get; }

		public abstract Guid CreaterGuid { get; }

		/// <summary>
		/// 获取一个MapCreater
		/// </summary>
		/// <returns></returns>
		public abstract MapCreater GetACreater();

		/// <summary>
		/// 获取一个适用的Configuration
		/// </summary>
		/// <returns></returns>
		public abstract Configuration GetAConfiguration();
	}
}
