using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core.BackendNode.AtmosphericMotion {
	/// <summary>
	/// 方位按照上北下南左西右东确定
	/// NW(1)  N(2)  NE(3)
	///  W(8)  C(0)   E(4)
	/// SW(7)  S(6)  SE(5)
	/// </summary>
	public enum Direction {
		/// <summary>
		/// 中心
		/// </summary>
		C = 0,
		/// <summary>
		/// 西北
		/// </summary>
		NW = 1,
		/// <summary>
		/// 北
		/// </summary>
		N = 2,
		/// <summary>
		/// 东北
		/// </summary>
		NE = 3,
		/// <summary>
		/// 东
		/// </summary>
		E = 4,
		/// <summary>
		/// 东南
		/// </summary>
		SE = 5,
		/// <summary>
		/// 南
		/// </summary>
		S = 6,
		/// <summary>
		/// 西南
		/// </summary>
		SW = 7,
		/// <summary>
		/// 西
		/// </summary>
		W = 8,
	}

	public struct PointData {
		/// <summary>
		/// 当前节点的风向
		/// </summary>
		public Direction direction;
		/// <summary>
		/// 当前节点的风力
		/// </summary>
		public byte power;
	}

	/// <summary>
	/// 空气流动节点
	/// </summary>
	class AtmosphericMotionNode {



	}
}
