﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Xml;

namespace WorldCreaterStudio_Core.MapCreater {
	public class BaseConfiguration : Configuration {
		public int Width { get; set; }

		public int Height { get; set; }

		public int RandomSeed { get; set; }

		public override System.Windows.Controls.ControlTemplate ShowPanel => null; // todo

		public override event PropertyChangedEventHandler PropertyChanged;

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

		public override void LoadFromXMLNode(XmlElement xmlnode) {
			//throw new NotImplementedException();
			// todo
		}

		public override XmlElement XmlNode(XmlDocument xmlDocument, bool save = false) {
			//throw new NotImplementedException();
			// todo

			return null;
		}
	}
}
