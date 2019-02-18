using WorldCreater.BaseType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPanel {
	delegate void EventDelegate(object[] args);

	public partial class Form1 : Form {
		int _picPersent = 100;
		bool _picAutoSize = false;
		public int PicPersent {
			get => _picPersent;
			set {
				if (value < 10) value = 10;
				else if (value > 800) value = 800;

				_picPersent = value;
				pictureBox1.Width = pictureBox1.Image.Width * PicPersent / 100;
				pictureBox1.Height = pictureBox1.Image.Height * PicPersent / 100;
				PicAutoSize = false;
				labPciScalSize.Text = value.ToString();
			}
		}
		public bool PicAutoSize {
			get => _picAutoSize;
			set {
				if (_picAutoSize == value) return;
				if (value) {
					pictureBox1.Dock = DockStyle.Fill;
				}
				else {
					pictureBox1.Dock = DockStyle.None;
				}
				_picAutoSize = value;
			}
		}

		private ResaultShowInfoColl _res = null;


		public Image ShowImg {
			get => pictureBox1.Image;
			set {
				pictureBox1.Image = value;
				if (value == null) return;
				if (PicAutoSize) {
				}
				else {
					pictureBox1.Width = value.Width * PicPersent / 100;
					pictureBox1.Height = value.Height * PicPersent / 100;
				}
			}
		}


		public Form1() {
			InitializeComponent();
		}

		/// <summary>
		/// 异步调用时输出一个信息
		/// </summary>
		/// <param name="message"></param>
		public void AsyncMessage(string message) {
			textMessage.BeginInvoke(new EventDelegate((object[] args) => {
				Message(message);
				this.Refresh();
			}));
		}
		/// <summary>
		/// 输出一个信息，异步调用请使用AsyncMessage
		/// </summary>
		/// <param name="message"></param>
		public void Message(string message) {
			textMessage.AppendText(DateTime.Now.ToString("hh-MM-ss"));
			textMessage.AppendText(":");
			textMessage.AppendText(message);
			textMessage.AppendText(Environment.NewLine);
			this.Refresh();
		}
		/// <summary>
		/// 异步调用时更换正在展示的图像
		/// </summary>
		/// <param name="img"></param>
		public void AsyncSetShowImage(Image img) {
			pictureBox1.BeginInvoke(new EventDelegate((object[] args) => {
				SetShowImage(img);
				this.Refresh();
			}));
		}
		/// <summary>
		/// 更换正在展示的图像，异步调用时请使用AsyncSetShowImage
		/// </summary>
		/// <param name="img"></param>
		public void SetShowImage(Image img) {
			ShowImg = img;
		}

		private void btnPicAuto_Click(object sender, EventArgs e) {
			PicAutoSize = true;
		}

		private void btnPic100_Click(object sender, EventArgs e) {
			PicPersent = 100;
		}

		private void btnPicSub_Click(object sender, EventArgs e) {
			PicPersent = PicPersent - 10;
		}

		private void btnPicAdd_Click(object sender, EventArgs e) {
			PicPersent = PicPersent + 10;
		}

		private void btnRTWay_Click(object sender, EventArgs e) {
			Config config = new Config() {
				BlockSize = 8,
				BlockNumW = 8,
				BlockNumH = 8,
				RandomLevel = 4096,
				RandomKey = (int)DateTime.Now.Ticks, //1234
			};
			RandomTend.RandomTend th = new RandomTend.RandomTend(config);
			th.OnProcessing += (byte persent, string message, bool freshMap, int[,] newMap) => {
				Message(persent.ToString() + " : " + message);
				if (freshMap) {
					SetShowImage(MapConvert.HeightToImage.BWImage(newMap, -2097152, 2097152, true));
				}
			};

			th.CreateMap();

			//th.Resault.SetShowInfoMap("bwimg", ShowImg);
			int[,] rv = th.Resault.GetShowInfoMap("RandomValue") as int[,];

			Bitmap rvm = null;
			Bitmap heim = null;
			Bitmap colorm = null;

			colorm = MapConvert.HeightToImage.ColorImage(th.Resault.GetResault(), -2097152, 2097152);
			heim = MapConvert.HeightToImage.BWImage(th.Resault.GetResault(), -2097152, 2097152, true);
			if (rv != null) {
				rvm = MapConvert.HeightToImage.BWImage(rv, -2097152, 2097152, true);
			}
			this._res = new ResaultShowInfoColl(th.Resault, rvm, heim, colorm);
			SetShowImage(colorm);
		}

		#region 展示信息切换按钮
		/// <summary>
		/// 展示随机值按钮事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnShowImgRandom_Click(object sender, EventArgs e) {
			SetShowImage(_res.RandomMap);
		}
		/// <summary>
		/// 展示高度图按钮事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnShowImgHeight_Click(object sender, EventArgs e) {
			SetShowImage(_res.HeightMap);
		}
		/// <summary>
		/// 展示彩图按钮事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnShowImgColor_Click(object sender, EventArgs e) {
			SetShowImage(_res.ColorMap);
		}
		#endregion

		private void button1_Click(object sender, EventArgs e) {
			Image img = pictureBox1.Image;
			if (img == null) return;
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if (saveFileDialog.ShowDialog() == DialogResult.OK) {
				try {
					img.Save(saveFileDialog.FileName);
				} catch {
					Message("取消时发生错误");
				}
			} else { 
				Message("取消保存");
			}
		}
	}
}
