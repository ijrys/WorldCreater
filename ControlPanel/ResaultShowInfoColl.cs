using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCreater.BaseType;
namespace ControlPanel {
	class ResaultShowInfoColl {
		public IResaultAble Resault { get; }
		public Bitmap HeightMap { get; } = null;
		public Bitmap RandomMap { get; } = null;
		public Bitmap ColorMap { get; } = null;

		public ResaultShowInfoColl (Resault re) {
			this.Resault = re;
		}
		public ResaultShowInfoColl (IResaultAble re, Bitmap randomMap, Bitmap heightMap, Bitmap colorMap) {
			this.Resault = re;
			this.HeightMap = heightMap;
			this.RandomMap = randomMap;
			this.ColorMap = colorMap;
		}
	}
}
