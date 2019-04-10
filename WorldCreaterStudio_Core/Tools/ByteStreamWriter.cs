using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCreaterStudio_Core.Tools {
	class ByteStreamWriter {
		private Stream _stream;
		public Stream Stream {
			get => _stream;
			private set {
				_stream = value;
			}
		}

		private byte[] buffer;
		private int nowPoint;
		private int maxPoint;

		public void Flush () {
			if (_stream == null || nowPoint == 0) return;
			_stream.Write (buffer, 0, nowPoint);
			nowPoint = 0;
		}

		public void Write (byte[] content, int count, int offset = 0) {
			int end = offset + count;
			if (end > content.Length) end = content.Length;

			for (; offset < end; offset ++) {
				buffer[nowPoint] = content[offset];
				nowPoint++;
				if (nowPoint >= maxPoint) Flush ();
			}
		}
		public void Write (byte content) {
			buffer[nowPoint] = content;
			nowPoint++;
			if (nowPoint >= maxPoint) Flush ();
		}

		public void Close () {
			Flush ();
			_stream = null;
		}

		public ByteStreamWriter (Stream stream) {
			Stream = stream;
			maxPoint = 4096;
			buffer = new byte[maxPoint + 4];
		}
	}
}
