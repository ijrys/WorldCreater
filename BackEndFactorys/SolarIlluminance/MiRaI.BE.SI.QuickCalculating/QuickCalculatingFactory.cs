using System;
using WorldCreaterStudio_Core.BackendNode.SolarIlluminance;

namespace MiRaI.BE.SI.QuickCalculating {
	/// <summary>
	/// 用于创建、获取相应的Calculater和Config的工厂类
	/// </summary>
	public class QuickCalculatingFactory : ISolarIlluminanceCalculaterFactoryAble {
		public string DisplayName => "反射面法线快速计算";

		public string DisplayType => "BE.SI";

		public string CalculaterProgramSet => "MiRaI.BE.SI.QC|0.1";

		public Guid CalculaterGuid => typeof (QuickCalculating).GUID;


		private QuickCalculating _temp = null;
		public ISolarIlluminanceCalculaterAble GetACalculater () {
			if (_temp == null) {
				_temp = new QuickCalculating ();
			}
			return _temp;
		}

		public ISolarIlluminanceConfigAble GetAConfiguration () {
			return new QuickCalculatingConfig ();
		}
	}
}
