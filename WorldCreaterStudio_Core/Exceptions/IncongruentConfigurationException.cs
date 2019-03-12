using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core.Exceptions {
	/// <summary>
	/// MapCreater收到了错误的配置类类型
	/// </summary>
	public class IncongruentConfigurationException : Exception {
		public Type NeedConfigType { get; private set; }
		public Type TrueConfigType { get; private set; }

		public IncongruentConfigurationException(Type needConfigType, Type trueConfigType) : base("Configuration is incongruent") {
			NeedConfigType = needConfigType;
			TrueConfigType = trueConfigType;
		}
	}
}
