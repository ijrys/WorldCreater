using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WorldCreaterStudio_Core {
	interface IWorkLogicNodeAble {
		UIElement ShowPanel { get; set; }
		string NodeName { get; set; }
		ImageSource Icon { get; set; }

		IEnumerable<IWorkLogicNodeAble> Childrens { get; set; }
	}
}
