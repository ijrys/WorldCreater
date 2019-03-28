using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorldCreaterStudio_Core.MapCreater;

namespace RandomTend {

	public class RandomTendCreaterFactory : MapCreaterFactory {
		public override string DisplayName => "Random Tend";

		public override string DisplayType => "Random Tend";

		public override string CreaterProgramSet => "MiRaI.RandomTend.RandomTend|0.1";

		public override Guid CreaterGuid => new Guid("FDAAD2ED-3072-4110-B685-AD1D5139F90B");

		public override Configuration GetAConfiguration() {
			return new RTConfiguration();
		}

		public override MapCreater GetACreater() {
			return new RandomTendCreater();
		}
	}
}
