using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core {
	interface IResourseAble : IWorkLogicNodeAble {
		bool InfoChanged { get; }
		bool DataChanged { get; }
		string Key { get; }
		string FilePath { get; }
		string Description { get; }
		void Save(string basePath);
	}
}
