﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCreaterStudio_Core.ListNode;
namespace WorldCreaterStudio_Core.BackendNode.AtmosphericMotion {
	/// <summary>
	/// 空气运动模拟需要的配置参数接口
	/// </summary>
	public interface IAtmosphericMotionConfigAble
		: IDataCalculaterConfigurationAble {
	}

	/// <summary>
	/// 空气运动模拟计算器
	/// </summary>
	public interface IAtmosphericMotionCalculaterAble : IDataCalculaterAble {
		PointData[,] GetAtmosphericMotionDatas (IAtmosphericMotionConfigAble config, int[,] heightMap);
	}

	/// <summary>
	/// 空气运动模拟计算器生成工厂
	/// </summary>
	public interface IAtmosphericMotionCalculaterFactoryAble :
		IDataCalculaterFactoryAble<IAtmosphericMotionCalculaterAble, IAtmosphericMotionConfigAble> {

	}


	/// <summary>
	/// 空气运动模拟计算器
	/// </summary>
	//public interface IAtmosphericMotionCalculaterAble<ConfigT>
	//	: IAtmosphericMotionCalculaterAble
	//	where ConfigT: IAtmosphericMotionConfigAble {
	//	PointData[,] GetAtmosphericMotionDatas (ConfigT config, int[,] heightMap);
	//}
}
