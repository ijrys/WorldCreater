using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core.Exceptions {
	public class DirectoryExistedException : Exception {
		public DirectoryExistedException(string path) : base("work directory has existed") {
			Data["path"] = path;
		}
	}
}
