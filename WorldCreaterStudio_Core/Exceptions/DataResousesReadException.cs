using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core.Exceptions {
	class DataResousesReadException : Exception {
		public FileInfo TheFile { get; private set; }
		public DataResousesReadException(string message, FileInfo file, Exception innerException) : base (message, innerException) {
			TheFile = file;
		}


		public class DataResousesFormException : Exception {
			public DataResousesFormException(string message):base(message) {

			}
		}
	}
}
