using System;
using System.Collections.Generic;
using System.Text;

namespace BaseType {
	/// <summary>
	/// 进程改变时委托类型
	/// </summary>
	/// <param name="persent">百分比，0-100</param>
	/// <param name="message">正在进行的过程</param>
	/// <param name="freshMap">是否刷新地图</param>
	/// <param name="newMap">若地图刷新，传入新的地图</param>
	public delegate void ProcessChangedDelegateType(byte persent, string message, bool freshMap, int[,] newMap);
	public abstract class Creater {
		/// <summary>
		/// 获取结果
		/// </summary>
		public IResaultAble Resault { get; protected set; }

		/// <summary>
		/// 获取配置
		/// </summary>
		public Config Config { get; protected set; }

		/// <summary>
		/// 使用已设定配置执行地图创建
		/// </summary>
		public abstract void CreateMap();

		#region Events
		/// <summary>
		/// 进行时过程百分比更新
		/// </summary>
		public ProcessChangedDelegateType OnProcessing;
		#endregion

		public Creater (Config config) {
			this.Config = config;
		}
	}
}
