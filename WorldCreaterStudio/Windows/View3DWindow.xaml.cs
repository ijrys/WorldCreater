using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace WorldCreaterStudio.Windows {
	/// <summary>
	/// View3DWindow.xaml 的交互逻辑
	/// </summary>
	public partial class View3DWindow : Window {
		double x, z;
		/// <summary>
		/// 纵向缩放倍率
		/// </summary>
		double heightBL;
		int[,] _map;

		public View3DWindow(int[,] maps) {
			InitializeComponent();
			_map = maps;
			DrawMap();

			x = 270;
			z = 45;
			SetCameraJD();
			//cameria.Position = new System.Windows.Media.Media3D.Point3D (-16, 16, 1024);
			int h, w;
			h = _map.GetLength(0);
			w = _map.GetLength(1);
			SetPosition(0, _map[h / 2, w / 2] * heightBL + 100, 0);

			//SetPosition (-2, 2, 2);
		}


		private void DrawMap() {
			int max = _map[0, 0];
			int min = max;
			foreach (int item in _map) {
				if (max < item)
					max = item;
				else if (min > item)
					min = item;
			}
			int mid = (max + min) / 2;
			int diff = (max - min);
			double bl = Math.Max(_map.GetLength(0), _map.GetLength(1)) / 1024.0;
			if (bl > 1.0) bl = 1.0;
			else if (bl < 0.2) bl = 0.2;
			bl = bl * 1000 / diff;

			this.heightBL = bl;
			MeshGeometry3D fbx = new MeshGeometry3D();
			Point3DCollection points = new Point3DCollection();
			int h, w;
			h = _map.GetLength(0);
			w = _map.GetLength(1);
			for (int z = 0; z < h; z++) {
				for (int x = 0; x < w; x++) {
					Point3D point = new Point3D(x * 2 - w, (_map[z, x] - mid) * bl, z * 2 - h);
					points.Add(point);
				}
			}
			fbx.Positions = points;
			Int32Collection sjxs = new Int32Collection();
			for (int z = 0; z < h - 1; z++) {
				for (int x = 0; x < w - 1; x++) {
					sjxs.Add((x + 1) * _map.GetLength(1) + z + 1);
					sjxs.Add(x * _map.GetLength(1) + z + 1);
					sjxs.Add(x * _map.GetLength(1) + z);

					sjxs.Add(x * _map.GetLength(1) + z);
					sjxs.Add((x + 1) * _map.GetLength(1) + z);
					sjxs.Add((x + 1) * _map.GetLength(1) + z + 1);
				}
			}
			fbx.TriangleIndices = sjxs;
			fBXCont.Geometry = fbx;

		}
		private void SetCameraJD() {
			if (this.z > 89.5) this.z = 89.5;
			else if (this.z < -89.5) this.z = -89.5;
			double x, y, z;
			x = Math.Cos(this.x / 180 * Math.PI) * Math.Cos(this.z / 180 * Math.PI);
			z = Math.Sin(this.x / 180 * Math.PI) * Math.Cos(this.z / 180 * Math.PI);
			y = -Math.Sin(this.z / 180 * Math.PI);

			cameria.LookDirection = new Vector3D(x, y, z);
			trans.Text = this.x.ToString() + " " + this.z.ToString();
		}

		private void SetPosition(double x, double y, double z) {
			cameria.Position = new System.Windows.Media.Media3D.Point3D(x, y, z);
			position.Text = x.ToString("0.0") + " " + y.ToString("0.0") + " " + z.ToString("0.0");
		}

		private void QJ(out double x, out double y, out double z) {
			//double x, y, z;
			x = Math.Cos(this.x / 180 * Math.PI) * Math.Cos(this.z / 180 * Math.PI);
			z = Math.Sin(this.x / 180 * Math.PI) * Math.Cos(this.z / 180 * Math.PI);
			y = -Math.Sin(this.z / 180 * Math.PI);
			//return new Tuple<double, double, double> (x, y, z);
		}

		#region WASD
		private void ButtonW_Click(object sender, RoutedEventArgs e) {
			z = (z + 360 - 0.5) % 360;
			SetCameraJD();
		}
		private void ButtonA_Click(object sender, RoutedEventArgs e) {
			x = (x + 360 - 0.5) % 360;
			SetCameraJD();
		}

		private void ButtonS_Click(object sender, RoutedEventArgs e) {
			z = (z + 0.5) % 360;
			SetCameraJD();
		}

		private void ButtonD_Click(object sender, RoutedEventArgs e) {
			x = (x + 0.5) % 360;
			SetCameraJD();
		}
		#endregion

		#region 方向键
		private void ButtonUp_Click(object sender, RoutedEventArgs e) {
			QJ(out double x, out double y, out double z);
			SetPosition(cameria.Position.X + x, cameria.Position.Y + y, cameria.Position.Z + z);
		}

		private void ButtonLeft_Click(object sender, RoutedEventArgs e) {
			//double xjd = (this.x + 270) % 360;
			SetPosition(cameria.Position.X + Math.Sin(this.x / 180 * Math.PI), cameria.Position.Y, cameria.Position.Z - Math.Cos(this.x / 180 * Math.PI));
		}

		private void ButtonRight_Click(object sender, RoutedEventArgs e) {
			//double xjd = (this.x + 90) % 360;
			SetPosition(cameria.Position.X - Math.Sin(this.x / 180 * Math.PI), cameria.Position.Y, cameria.Position.Z + Math.Cos(this.x / 180 * Math.PI));
			//SetPosition (cameria.Position.X + 0.1, cameria.Position.Y, cameria.Position.Z);

		}

		private void ButtonDown_Click(object sender, RoutedEventArgs e) {
			QJ(out double x, out double y, out double z);
			SetPosition(cameria.Position.X - x, cameria.Position.Y - y, cameria.Position.Z - z);

		}
		#endregion

		#region XYZ US
		private void Buttonxu_Click(object sender, RoutedEventArgs e) {
			SetPosition(cameria.Position.X + 1, cameria.Position.Y, cameria.Position.Z);
		}

		private void Buttonxs_Click(object sender, RoutedEventArgs e) {
			SetPosition(cameria.Position.X - 1, cameria.Position.Y, cameria.Position.Z);

		}

		private void Buttonyu_Click(object sender, RoutedEventArgs e) {
			SetPosition(cameria.Position.X, cameria.Position.Y + 1, cameria.Position.Z);

		}

		private void Buttonys_Click(object sender, RoutedEventArgs e) {
			SetPosition(cameria.Position.X, cameria.Position.Y - 1, cameria.Position.Z);

		}

		private void Buttonzu_Click(object sender, RoutedEventArgs e) {
			SetPosition(cameria.Position.X, cameria.Position.Y, cameria.Position.Z + 1);

		}

		private void Buttonzs_Click(object sender, RoutedEventArgs e) {
			SetPosition(cameria.Position.X, cameria.Position.Y, cameria.Position.Z - 1);

		}
		#endregion

		#region 摄像机相对移动
		private void ForwardOnce() {
			QJ(out double x, out double y, out double z);
			SetPosition(cameria.Position.X + x * _movespeed, cameria.Position.Y + y * _movespeed, cameria.Position.Z + z * _movespeed);
		}
		private void BackwardOnce() {
			QJ(out double x, out double y, out double z);
			SetPosition(cameria.Position.X - x * _movespeed, cameria.Position.Y - y * _movespeed, cameria.Position.Z - z * _movespeed);
		}
		private void LeftwardOnce() {
			SetPosition(cameria.Position.X + Math.Sin(this.x / 180 * Math.PI) * _movespeed, cameria.Position.Y, cameria.Position.Z - Math.Cos(this.x / 180 * Math.PI) * _movespeed);
		}
		private void RightwardOnce() {
			SetPosition(cameria.Position.X - Math.Sin(this.x / 180 * Math.PI) * _movespeed, cameria.Position.Y, cameria.Position.Z + Math.Cos(this.x / 180 * Math.PI) * _movespeed);
		}
		private void UpwardOnce() {
			SetPosition(cameria.Position.X, cameria.Position.Y + _movespeed, cameria.Position.Z);
		}
		private void DownwardOnce() {
			SetPosition(cameria.Position.X, cameria.Position.Y - _movespeed, cameria.Position.Z);
		}

		bool _forward, _backward, _leftward, _rightward, _upward, _downward;
		int _movespeed;
		const int jgjs = 50;
		private async void Forward(int speed = 1) {
			_movespeed = speed;
			if (_forward) return;
			_backward = false;
			_forward = true;
			while (_forward) {
				ForwardOnce();
				if (_movespeed < 1) _movespeed = 1;
				await Task.Delay(jgjs / _movespeed);
			}
		}

		private async void Backward(int speed = 1) {
			_movespeed = speed;
			if (_backward) return;
			_forward = false;
			_backward = true;
			while (_backward) {
				BackwardOnce();
				if (_movespeed < 1) _movespeed = 1;
				await Task.Delay(jgjs / _movespeed);
			}
		}

		private async void Leftward(int speed = 1) {
			_movespeed = speed;
			if (_leftward) return;
			_rightward = false;
			_leftward = true;
			while (_leftward) {
				LeftwardOnce();
				if (_movespeed < 1) _movespeed = 1;
				await Task.Delay(jgjs / _movespeed);
			}
		}

		private async void Rightward(int speed = 1) {
			_movespeed = speed;
			if (_rightward) return;
			_leftward = false;
			_rightward = true;
			while (_rightward) {
				RightwardOnce();
				if (_movespeed < 1) _movespeed = 1;
				await Task.Delay(jgjs / _movespeed);
			}
		}

		private async void Upward(int speed = 1) {
			_movespeed = speed;
			if (_upward) return;
			_downward = false;
			_upward = true;
			while (_upward) {
				UpwardOnce();
				if (_movespeed < 1) _movespeed = 1;
				await Task.Delay(jgjs / _movespeed);
			}
		}
		private async void Downward(int speed = 1) {
			_movespeed = speed;
			if (_downward) return;
			_upward = false;
			_downward = true;
			while (_downward) {
				DownwardOnce();
				if (_movespeed < 1) _movespeed = 1;
				await Task.Delay(jgjs / _movespeed);
			}
		}
		#endregion

		#region 摄像机旋转
		private void RotateLeftOnce() {
			this.x = (this.x - 0.5 * _rotatespeed + 360) % 360;
			SetCameraJD();
		}
		private void RotateRightOnce() {
			this.x = (this.x + 0.5 * _rotatespeed) % 360;
			SetCameraJD();
		}
		private void RotateUpOnce() {
			this.z = (this.z - 0.5 * _rotatespeed);
			SetCameraJD();
		}
		private void RotateDownOnce() {
			this.z = (this.z + 0.5 * _rotatespeed);
			SetCameraJD();
		}
		bool _rotateleft, _rotateright, _rotateup, _rotatedown;
		int _rotatespeed;
		private async void RotateLeft(int speed = 1) {
			_rotatespeed = speed;
			if (_rotateleft) return;
			_rotateright = false;
			_rotateleft = true;
			while (_rotateleft) {
				RotateLeftOnce();
				if (_rotatespeed < 1) _rotatespeed = 1;
				await Task.Delay(jgjs / _rotatespeed);
			}
		}
		private async void RotateRight(int speed = 1) {
			_rotatespeed = speed;
			if (_rotateright) return;
			_rotateleft = false;
			_rotateright = true;
			while (_rotateright) {
				RotateLeftOnce();
				if (_rotatespeed < 1) _rotatespeed = 1;
				await Task.Delay(jgjs / _rotatespeed);
			}
		}
		private async void RotateUp(int speed = 1) {
			_rotatespeed = speed;
			if (_rotateup) return;
			_rotatedown = false;
			_rotateup = true;
			while (_rotateup) {
				RotateUpOnce();
				if (_rotatespeed < 1) _rotatespeed = 1;
				await Task.Delay(jgjs / _rotatespeed);
			}
		}
		private async void RotateDown(int speed = 1) {
			_rotatespeed = speed;
			if (_rotatedown) return;
			_rotateup = false;
			_rotatedown = true;
			while (_rotatedown) {
				RotateUpOnce();
				if (_rotatespeed < 1) _rotatespeed = 1;
				await Task.Delay(jgjs / _rotatespeed);
			}
		}
		#endregion

		private void ButtonSave_Click(object sender, RoutedEventArgs e) {

			try {
				if (fBXCont == null || fBXCont.Geometry == null) {
					System.Windows.Forms.MessageBox.Show("模型不能为空");
					return;
				}
				System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
				dialog.Filter = "模型(*.obj)|*.obj";
				dialog.FileName = Title;
				dialog.Title = "保存模型";

				//double r = sliderrate.Value;
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					SaveOBJ.ObjWriter ow = new SaveOBJ.ObjWriter(fBXCont.Geometry as MeshGeometry3D, null);
					ow.OutPut(dialog.FileName, dialog.FileName);
					System.Windows.Forms.MessageBox.Show("Output Success !");
				}
			} catch (Exception ex) {
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

		}

		Point lp;
		//private void Window_MouseMove(object sender, MouseEventArgs e) {
		//	//Point p = e.;
		//	//if (p.X == this.Width / 2 && p.Y == this.Height / 2) return;
		//	//else ()
		//	/*int dx = (int) (p.X - lp.X);
		//	int dy = (int) (p.Y - lp.Y);
		//	if (dx > 0) ButtonW_Click (null, null);
		//	else if (dx < 0) ButtonS_Click (null, null);
		//	lp = p;
		//	mposition.Text = dx.ToString ("0.0") + " " + dy.ToString ("0.0");
		//	SetCursorPos ((int) (this.Left + this.Width / 2), (int) (this.Top + this.Height / 2));*/
		//	//mposition.Text = (p.X - (int)(this.Width / 2)).ToString ("0.0") + " " + (p.Y - (int) (this.Height / 2)).ToString ("0.0");
		//	//SetCursorPos ((int) (this.Left + this.Width / 2 + this.w), (int) (this.Top + this.Height / 2));

		//}
		[DllImport("user32.dll")]
		static extern bool SetCursorPos(int X, int Y);

		private void Window_MouseWheel(object sender, MouseWheelEventArgs e) {
			int d = e.Delta;
			int speed = _movespeed;
			_movespeed = 4;
			if (d < 0) {
				BackwardOnce();
			} else {
				ForwardOnce();
			}
			_movespeed = speed;
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
			mousemovecp = e.GetPosition(this);
			this.MouseMove += MainWindow_MouseMove;
		}
		Point mousemovecp;
		private void MainWindow_MouseMove(object sender, MouseEventArgs e) {
			Point p = e.GetPosition(this);
			if (p.X == mousemovecp.X && p.Y == mousemovecp.Y) return;
			double dx = (p.X - mousemovecp.X) / this.Width;
			double dy = (p.Y - mousemovecp.Y) / this.Height;
			double dlen = (p.X - mousemovecp.X) * (p.X - mousemovecp.X) + (p.Y - mousemovecp.Y) * (p.Y - mousemovecp.Y);
			dlen = Math.Sqrt(dlen / (this.Width * this.Width + this.Height * this.Height));
			if (dlen < 0.01) return;
			this.z = (this.z - dy * 180);
			this.x = (this.x - dx * 180 + 360) % 360;
			SetCameraJD();
			mousemovecp = p;
		}

		private void Window_KeyUp(object sender, KeyEventArgs e) {
			switch (e.Key) {
				case Key.W:
					_forward = false;
					break;
				case Key.S:
					_backward = false;
					break;
				case Key.A:
					_leftward = false;
					break;
				case Key.D:
					_rightward = false;
					break;
				case Key.LeftShift:
				case Key.Right:
					_downward = false;
					break;
				case Key.Space:
					_upward = false;
					break;
			}
		}

		private void Window_MouseUp(object sender, MouseButtonEventArgs e) {
			MouseMove -= MainWindow_MouseMove;
		}

		private void Window_MouseLeave(object sender, MouseEventArgs e) {
			MouseMove -= MainWindow_MouseMove;

		}

		private void Grid_KeyDown(object sender, KeyEventArgs e) {
			switch (e.Key) {
				case Key.Up:
					ButtonW_Click(null, null);
					break;
				case Key.Left:
					ButtonA_Click(null, null);
					break;
				case Key.Down:
					ButtonS_Click(null, null);
					break;
				case Key.Right:
					ButtonD_Click(null, null);
					break;
				case Key.O:
					Buttonxs_Click(null, null);
					break;
				case Key.P:
					Buttonxu_Click(null, null);
					break;
				case Key.K:
					Buttonys_Click(null, null);
					break;
				case Key.L:
					Buttonyu_Click(null, null);
					break;
				case Key.N:
					Buttonzs_Click(null, null);
					break;
				case Key.M:
					Buttonzu_Click(null, null);
					break;
				case Key.W:
					//ButtonUp_Click (null, null);
					Forward();
					break;
				case Key.A:
					Leftward();
					//ButtonLeft_Click (null, null);
					break;
				case Key.D:
					Rightward();
					break;
				case Key.S:
					Backward();
					break;
				case Key.Space:
					Upward();
					break;
				case Key.LeftShift:
				case Key.RightShift:
					Downward();
					break;
				case Key.LeftCtrl:
				case Key.RightCtrl:
					_movespeed = 2;
					break;
			}
		}

	}
}
