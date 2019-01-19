using System;
using System.Collections.Generic;
using System.Text;

namespace WorldCreater.BaseType {
	public class Resault : IResaultAble {
		private Dictionary<string, object> _res = new Dictionary<string, object>();
		private int[,] _rmap;
		public object this[string key] { get => _res[key]; set => _res[key] = value; }
		public object this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public int[,] GetResault() {
			return _rmap;
		}

		public object GetShowInfoMap(string key) {
			return this[key];
		}

		public void SetShowInfoMap(string key, object value) {
			this[key] = value;
		}

		public Resault (int[,] re) {
			this._rmap = re;
		}
	}
}
