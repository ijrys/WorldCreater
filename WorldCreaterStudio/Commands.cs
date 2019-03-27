using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorldCreaterStudio {
	/// <summary>
	/// Studio使用到的自定义命令
	/// </summary>
	public static class Commands {
		public static RoutedUICommand NewWork { get; private set; }
		public static RoutedUICommand NewProject { get; private set; }
		public static RoutedUICommand Save { get; private set; }
		static Commands() {
			//New Work
			InputGestureCollection igc1 = new InputGestureCollection();
			igc1.Add(new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl+N"));
			NewWork = new RoutedUICommand("New Work", "NewWork", typeof(Commands), igc1);

			//New Project
			igc1 = new InputGestureCollection();
			igc1.Add(new KeyGesture(Key.N, ModifierKeys.Control | ModifierKeys.Shift, "Ctrl+Shift+N"));
			NewProject = new RoutedUICommand("New Project", "NewProject", typeof(Commands), igc1);

			//Save
			igc1 = new InputGestureCollection();
			igc1.Add(new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S"));
			Save = new RoutedUICommand("Save", "SaveProject", typeof(Commands), igc1);
		}
	}
}
