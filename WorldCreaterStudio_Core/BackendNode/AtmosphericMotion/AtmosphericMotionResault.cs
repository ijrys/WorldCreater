using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core.BackendNode.AtmosphericMotion {
	class AtmosphericMotionResault : CalculatedResault<PointData> {
		public override void Save (bool freshWithoutChanged = false) {
			throw new NotImplementedException ();
		}

		protected override void Load () {
			throw new NotImplementedException ();
		}

		AtmosphericMotionResault() {

		}
	}
}
