using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core.BackendNode {
	/// <summary>
	/// 表示节点的状态
	/// </summary>
	public enum NodeState {
		/// <summary>
		/// 不可用，条件不充足
		/// </summary>
		unable,
		/// <summary>
		/// 待开始
		/// </summary>
		ready,
		/// <summary>
		/// 已完成
		/// </summary>
		ok,
		/// <summary>
		/// 需更新，在前一节点发生更新后，本节点需要重新计算
		/// </summary>
		outdate
	}
}
