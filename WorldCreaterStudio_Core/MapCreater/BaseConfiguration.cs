using System;

namespace WorldCreaterStudio_Core.MapCreater {
	public class BaseConfiguration : Configuration {
		public int Width { get; set; }

		public int Height { get; set; }

		public int RandomSeed { get; set; }

		public override int GetHeight() {
			return Height;
		}

		public override int GetRandomSeed() {
			return RandomSeed;
		}

		public override int GetWidth() {
			return Width;
		}

		public void RandomTheSeed() {
			RandomSeed = new Random().Next();
		}
	}
}
