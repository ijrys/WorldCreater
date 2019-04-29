using System;
using WorldCreaterStudio_Core.BackendNode.RainfallMotion;

namespace MiRaI.BE.RM.SingleValue {
	public class SingleValueFactory : IRainfallMotionCalculaterFactoryAble {
		public string DisplayName => "单一值快速设定";

		public string DisplayType => "BE.RM";

		public string CalculaterProgramSet => "MiRaI.BE.RM.SV|0.1";

		public Guid CalculaterGuid => typeof (SingleValue).GUID;


		private SingleValue _temp = null;
		public IRainfallMotionCalculaterAble GetACalculater () {
			if (_temp == null) {
				_temp = new SingleValue ();
			}
			return _temp;
		}

		public IRainfallMotionConfigAble GetAConfiguration () {
			return new SingleValueConfig ();
		}
	}
}
