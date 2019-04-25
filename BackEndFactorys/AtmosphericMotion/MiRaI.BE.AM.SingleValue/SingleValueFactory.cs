using System;
using WorldCreaterStudio_Core.BackendNode.AtmosphericMotion;

namespace MiRaI.BE.AM.SingleValue {
	public class SingleValueFactory : IAtmosphericMotionCalculaterFactoryAble {
		public string DisplayName => "单一值风力设定";

		public string DisplayType => "BE.AM";

		public string CalculaterProgramSet => "MiRaI.BE.AM.SV|0.1";

		public Guid CalculaterGuid => typeof (SingleValue).GUID;


		private SingleValue _temp = null;
		public IAtmosphericMotionCalculaterAble GetACalculater () {
			if (_temp == null) {
				_temp = new SingleValue ();
			}
			return _temp;
		}

		public IAtmosphericMotionConfigAble GetAConfiguration () {
			return new SingleValueConfig ();
		}
	}
}
