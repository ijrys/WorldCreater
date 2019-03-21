using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core.Exceptions {
	/// <summary>
	/// 未找到前端工厂
	/// </summary>
	class NoCreaterFactoryException : Exception {
		public string FrontFactoryProgramSet { get; private set; }
		public NoCreaterFactoryException(string programSet) : base("未找到的前端工厂：" + programSet) {
			FrontFactoryProgramSet = programSet;
		}
	}
}
