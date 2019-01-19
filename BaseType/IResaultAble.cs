using System;
using System.Collections.Generic;
using System.Text;

namespace WorldCreater.BaseType {
	public interface IResaultAble {
		/// <summary>
		/// 获取结果矩阵
		/// </summary>
		/// <returns></returns>
		int[,] GetResault();

		/// <summary>
		/// 获取结果相关内容
		/// </summary>
		/// <returns></returns>
		object GetShowInfoMap(string key);

		/// <summary>
		/// 设置结果相关内容
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		void SetShowInfoMap(string key, object value);

		/// <summary>
		/// 获取或设置结果相关内容
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		object this[string key] {
			get;
			set;
		}

		/// <summary>
		/// 获取或设置结果相关内容
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		object this[int index] {
			set;
			get;
		}
	}
}
