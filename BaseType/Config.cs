using System;
using System.Collections.Generic;
using System.Text;

namespace BaseType {
	public class Config {
		byte _blockSize;
		int _blockNumW, _blockNumH, _randomLevel, _randomKey;

		/// <summary>
		/// 块边长，结果为2^n
		/// </summary>
		public byte BlockSize { get => _blockSize; set => _blockSize = value; }
		/// <summary>
		/// 横向块数量
		/// </summary>
		public int BlockNumW { get => _blockNumW; set => _blockNumW = value; }
		/// <summary>
		/// 纵向块数量
		/// </summary>
		public int BlockNumH { get => _blockNumH; set => _blockNumH = value; }
		/// <summary>
		/// 随机范围
		/// </summary>
		public int RandomLevel { get => _randomLevel; set => _randomLevel = value; }
		/// <summary>
		/// 随机种子
		/// </summary>
		public int RandomKey { get => _randomKey; set => _randomKey = value; }

		/// <summary>
		/// 地图宽度。考虑边界问题，宽度为块长*块数+1
		/// </summary>
		public int MapWidth { get => (1 << _blockSize) * _blockNumW + 1; }
		/// <summary>
		/// 地图高度。考虑边界问题，高度为块长*块数+1
		/// </summary>
		public int MapHeight { get => (1 << _blockSize) * _blockNumH + 1; }
	}
}
