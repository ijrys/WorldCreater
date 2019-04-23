using System;
using WorldCreaterStudio_Core.BackendNode.Biomes;

namespace MiRaI.BE.BI.QuickCalc {
	public class QuickCalcFactory : IBiomesCalculaterFactoryAble {
		public string DisplayName => "快速运算";

		public string DisplayType => "BE.BI";

		public string CalculaterProgramSet => "MiRaI.BE.BI.QC|0.1";

		public Guid CalculaterGuid => typeof (QuickCalc).GUID;


		private QuickCalc _temp = null;
		public IBiomesCalculaterAble GetACalculater () {
			if (_temp == null) {
				_temp = new QuickCalc ();
			}
			return _temp;
		}

		public IBiomesConfigAble GetAConfiguration () {
			return new QuickCalcConfig ();
		}
	}
}
