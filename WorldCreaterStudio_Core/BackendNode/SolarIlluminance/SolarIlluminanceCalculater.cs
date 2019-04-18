using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCreaterStudio_Core.ListNode;

namespace WorldCreaterStudio_Core.BackendNode.SolarIlluminance {
	/// <summary>
	/// 空气运动模拟需要的配置参数接口
	/// </summary>
	public interface ISolarIlluminanceConfigAble
		: IDataCalculaterConfigurationAble {
	}

	/// <summary>
	/// 空气运动模拟计算器
	/// </summary>
	public interface ISolarIlluminanceCalculaterAble : IDataCalculaterAble {
		SolarIlluminanceResault GetSolarIlluminanceDatas (ISolarIlluminanceConfigAble config, int[,] heightMap, Work work);
	}

	/// <summary>
	/// 空气运动模拟计算器生成工厂
	/// </summary>
	public interface ISolarIlluminanceCalculaterFactoryAble :
		IDataCalculaterFactoryAble<ISolarIlluminanceCalculaterAble, ISolarIlluminanceConfigAble> {

	}

}
