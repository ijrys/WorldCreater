using System;

namespace BaseType {
	public interface ICreaterAble {
		/// <summary>
		/// 获取结果
		/// </summary>
		IResaultAble Resault { get; }

		/// <summary>
		/// 获取给定的设定
		/// </summary>
		Config Config { get; }

		/// <summary>
		/// 执行地图创建
		/// </summary>
		void CreateMap();
	}
}
