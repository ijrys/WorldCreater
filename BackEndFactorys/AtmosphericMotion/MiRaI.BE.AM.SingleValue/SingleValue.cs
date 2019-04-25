using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WorldCreaterStudio_Core;
using WorldCreaterStudio_Core.BackendNode.AtmosphericMotion;
using WorldCreaterStudio_Core.ListNode;

namespace MiRaI.BE.AM.SingleValue {
	/// <summary>
	/// 使用单值快速填充的空气运动模拟器
	/// </summary>
	public class SingleValue :
		IAtmosphericMotionCalculaterAble {
		public string CreaterName => "单一值风力设定";

		public string CreaterProgramSet => "MiRaI.BE.AM.SV|0.1";

		public Guid CreaterGuid => typeof (SingleValue).GUID;

		/// <summary>
		/// 模拟过程产生新进度消息时事件
		/// </summary>
		public event DataCalculatingProcessingEventType OnProcessingChanged;

		/// <summary>
		/// 使用SingleValueConfig类型的配置项进行模拟
		/// </summary>
		/// <param name="config">配置设置</param>
		/// <param name="heightMap">高度数据</param>
		/// <param name="work">执行方法的工作集</param>
		/// <returns></returns>
		public AtmosphericMotionResault GetAtmosphericMotionDatasBySpecialConfig (SingleValueConfig config, int[,] heightMap, Work work) {
			int w = heightMap.GetLength (0) - 1, h = heightMap.GetLength (1) - 1;
			PointData[,] recont = new PointData[w, h];
			PointData data = new PointData () {
				direction = config.Direction,
				power = config.Power,
			};
			
			for (int i = 0; i < w; i ++) {
				for (int j = 0; j < h; j ++) {
					recont[i, j] = data;
				}
			}

			BitmapSource image = ValueToImage.AtmosphericMotionToImage.GetBitmap (recont);
			work.Images.Add ("BE.AM.Map", image, "AtmosphericMotion Visual Map");
			AtmosphericMotionResault re = new AtmosphericMotionResault (recont, "AtmosphericMotion Visual Map", work, "BE.AM.Map");
			return re;
		}

		/// <summary>
		/// 使用一个IAtmosphericMotionConfigAble类型的配置项进行模拟。
		/// 若不为SingleValueConfig类型，抛出IncongruentConfigurationException
		/// </summary>
		/// <param name="config">配置设置</param>
		/// <param name="heightMap">高度数据</param>
		/// <param name="work">执行方法的工作集</param>
		/// <returns></returns>
		public AtmosphericMotionResault GetAtmosphericMotionDatas (IAtmosphericMotionConfigAble config, int[,] heightMap, Work work) {
			if (!(config is SingleValueConfig)) throw new WorldCreaterStudio_Core.Exceptions.IncongruentConfigurationException (typeof (SingleValueConfig), config.GetType ());
			return GetAtmosphericMotionDatasBySpecialConfig (config as SingleValueConfig, heightMap, work);
		}
	}
}
