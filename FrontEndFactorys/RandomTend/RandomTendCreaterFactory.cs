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

		public override Guid CreaterGuid => typeof (RandomTendCreater).GUID;

		/// <summary>
		/// 获取一个相应的配置对象
		/// </summary>
		/// <returns></returns>
		public override Configuration GetAConfiguration() {
			return new RTConfiguration();
		}

		/// <summary>
		/// 获取一个创建器
		/// </summary>
		/// <returns></returns>
		public override MapCreater GetACreater() {
			return new RandomTendCreater();
		}
	}
}
