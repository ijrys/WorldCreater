﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace WorldCreaterStudio_Core.Resouses {
	/// <summary>
	/// 引用的图片资源
	/// </summary>
	public class ImageResourceReference : IWorkLogicNodeAble {
		public Work Work { get; private set; }

		public System.Windows.Controls.ControlTemplate ShowPanel => null;

		public string NodeName => throw new NotImplementedException();

		public ImageSource Icon => throw new NotImplementedException();

		public ObservableCollection<IWorkLogicNodeAble> Childrens => throw new NotImplementedException();

		public ImageResourse ReferencedResourse => Work.Images[this.ResourseKey];

		public string ResourseKey { get; private set; }

		public BitmapSource Image {
			get {
				return ReferencedResourse?.Image;
			}
		}

		public bool Changed => throw new NotImplementedException();

		public event NodeValueChangedEventType NodeValueChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		public XmlElement XmlNode(XmlDocument xmlDocument, bool save = false) {
			XmlElement node = xmlDocument.CreateElement ("ImgRef");
			node.SetAttribute ("key", this.ResourseKey);
			return node;
		}

		public ImageResourceReference(Work work, string key) {
			this.Work = work;
			ResourseKey = key;
		}

		public static ImageResourceReference LoadFromXmlNode(XmlElement xmlnode, Work parentWork) {
			if (xmlnode.Name != "ir") return null;
			ImageResourceReference re = new ImageResourceReference(parentWork, "");

			foreach (XmlElement item in xmlnode.ChildNodes) {
				if (item.Name == "ir") { //资源引用
										 // todo
				} else {

				}
			}

			return re;
		}
	}
}
