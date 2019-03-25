using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCreaterStudio_Core.Resouses;

namespace WorldCreaterStudio_Core.MapCreater {
	/// <summary>
	/// Creater创建地形时进度更新的事件类型
	/// </summary>
	/// <param name="permillage">过程的千分率</param>
	/// <param name="processDescription">当前进度描述</param>
	/// <param name="freshImage">是否产生了新的预览图</param>
	/// <param name="image">新的预览图</param>
	public delegate void MapCreatingProcessingEvent(short permillage, string processDescription, bool freshImage, System.Windows.Media.ImageSource image);

	/// <summary>
	/// 所有Creater的基类
	/// </summary>
	public abstract class MapCreater {
		/// <summary>
		/// 获取或设置Creater的名称
		/// </summary>
		public string CreaterName { get; set; }

		/// <summary>
		/// 获取Creater所在的ProgramSet
		/// ProgramSet为Creater能力的唯一标识，ProgramSet相同的Creater将认定为实现效果相同
		/// </summary>
		public abstract string CreaterProgramSet { get; }

		/// <summary>
		/// 获取Creater类的GUID
		/// </summary>
		public abstract Guid CreaterGuid { get; }

		/// <summary>
		/// 创建一个地形
		/// </summary>
		/// <param name="configuration">创建地形需要的配置</param>
		/// <returns></returns>
		public abstract ValueResource CreatAMap(Configuration configuration, Work work);

		/// <summary>
		/// 当进度发生更新时事件
		/// </summary>
		public event MapCreatingProcessingEvent OnProcessingChanged;

		public Dictionary <string, ValueResource> CreateredMapValue { get; protected set; }

		/// <summary>
		/// 调用MapCreatingProcessingEvent
		/// </summary>
		/// <param name="permillage">进度执行千分比</param>
		/// <param name="processDescription">当前进度描述</param>
		/// <param name="freshImage">表示是否刷新图片</param>
		/// <param name="image">刷新的图片</param>
		protected void MapCreatingProcessing(short permillage, string processDescription, bool freshImage, System.Windows.Media.ImageSource image) {
			OnProcessingChanged?.Invoke(permillage, processDescription, freshImage, image);
		}
	}
}
