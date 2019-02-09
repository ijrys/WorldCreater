namespace ControlPanel {
	partial class Form1 {
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnRTWay = new System.Windows.Forms.Button();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnShowImgRandom = new System.Windows.Forms.Button();
			this.btnShowImgHeight = new System.Windows.Forms.Button();
			this.btnShowImgColor = new System.Windows.Forms.Button();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.labPciScalSize = new System.Windows.Forms.Label();
			this.btnPicSub = new System.Windows.Forms.Button();
			this.btnPicAdd = new System.Windows.Forms.Button();
			this.btnPic100 = new System.Windows.Forms.Button();
			this.btnPicAuto = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(5);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(812, 449);
			this.splitContainer1.SplitterDistance = 207;
			this.splitContainer1.SplitterWidth = 7;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(5);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.textMessage);
			this.splitContainer2.Size = new System.Drawing.Size(207, 449);
			this.splitContainer2.SplitterDistance = 308;
			this.splitContainer2.SplitterWidth = 7;
			this.splitContainer2.TabIndex = 0;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.flowLayoutPanel1);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.flowLayoutPanel3);
			this.splitContainer3.Size = new System.Drawing.Size(207, 308);
			this.splitContainer3.SplitterDistance = 154;
			this.splitContainer3.TabIndex = 1;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnRTWay);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(207, 154);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btnRTWay
			// 
			this.btnRTWay.Location = new System.Drawing.Point(3, 3);
			this.btnRTWay.Name = "btnRTWay";
			this.btnRTWay.Size = new System.Drawing.Size(160, 32);
			this.btnRTWay.TabIndex = 0;
			this.btnRTWay.Text = "随机趋势化算法";
			this.btnRTWay.UseVisualStyleBackColor = true;
			this.btnRTWay.Click += new System.EventHandler(this.btnRTWay_Click);
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.btnShowImgRandom);
			this.flowLayoutPanel3.Controls.Add(this.btnShowImgHeight);
			this.flowLayoutPanel3.Controls.Add(this.btnShowImgColor);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(207, 150);
			this.flowLayoutPanel3.TabIndex = 0;
			// 
			// btnShowImgRandom
			// 
			this.btnShowImgRandom.Location = new System.Drawing.Point(3, 3);
			this.btnShowImgRandom.Name = "btnShowImgRandom";
			this.btnShowImgRandom.Size = new System.Drawing.Size(160, 32);
			this.btnShowImgRandom.TabIndex = 0;
			this.btnShowImgRandom.Text = "随机值图";
			this.btnShowImgRandom.UseVisualStyleBackColor = true;
			this.btnShowImgRandom.Click += new System.EventHandler(this.btnShowImgRandom_Click);
			// 
			// btnShowImgHeight
			// 
			this.btnShowImgHeight.Location = new System.Drawing.Point(3, 41);
			this.btnShowImgHeight.Name = "btnShowImgHeight";
			this.btnShowImgHeight.Size = new System.Drawing.Size(160, 32);
			this.btnShowImgHeight.TabIndex = 1;
			this.btnShowImgHeight.Text = "黑白图";
			this.btnShowImgHeight.UseVisualStyleBackColor = true;
			this.btnShowImgHeight.Click += new System.EventHandler(this.btnShowImgHeight_Click);
			// 
			// btnShowImgColor
			// 
			this.btnShowImgColor.Location = new System.Drawing.Point(3, 79);
			this.btnShowImgColor.Name = "btnShowImgColor";
			this.btnShowImgColor.Size = new System.Drawing.Size(160, 32);
			this.btnShowImgColor.TabIndex = 2;
			this.btnShowImgColor.Text = "彩色图";
			this.btnShowImgColor.UseVisualStyleBackColor = true;
			this.btnShowImgColor.Click += new System.EventHandler(this.btnShowImgColor_Click);
			// 
			// textMessage
			// 
			this.textMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textMessage.Location = new System.Drawing.Point(0, 0);
			this.textMessage.Margin = new System.Windows.Forms.Padding(5);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.Size = new System.Drawing.Size(207, 134);
			this.textMessage.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(598, 449);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowLayoutPanel2.Controls.Add(this.labPciScalSize);
			this.flowLayoutPanel2.Controls.Add(this.btnPicSub);
			this.flowLayoutPanel2.Controls.Add(this.btnPicAdd);
			this.flowLayoutPanel2.Controls.Add(this.btnPic100);
			this.flowLayoutPanel2.Controls.Add(this.btnPicAuto);
			this.flowLayoutPanel2.Controls.Add(this.button1);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 382);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(592, 64);
			this.flowLayoutPanel2.TabIndex = 0;
			// 
			// labPciScalSize
			// 
			this.labPciScalSize.AutoSize = true;
			this.labPciScalSize.Location = new System.Drawing.Point(3, 3);
			this.labPciScalSize.Margin = new System.Windows.Forms.Padding(3);
			this.labPciScalSize.Name = "labPciScalSize";
			this.labPciScalSize.Size = new System.Drawing.Size(37, 21);
			this.labPciScalSize.TabIndex = 0;
			this.labPciScalSize.Text = "100";
			// 
			// btnPicSub
			// 
			this.btnPicSub.Location = new System.Drawing.Point(46, 3);
			this.btnPicSub.Name = "btnPicSub";
			this.btnPicSub.Size = new System.Drawing.Size(40, 30);
			this.btnPicSub.TabIndex = 1;
			this.btnPicSub.Text = "-";
			this.btnPicSub.UseVisualStyleBackColor = true;
			this.btnPicSub.Click += new System.EventHandler(this.btnPicSub_Click);
			// 
			// btnPicAdd
			// 
			this.btnPicAdd.Location = new System.Drawing.Point(92, 3);
			this.btnPicAdd.Name = "btnPicAdd";
			this.btnPicAdd.Size = new System.Drawing.Size(40, 30);
			this.btnPicAdd.TabIndex = 3;
			this.btnPicAdd.Text = "+";
			this.btnPicAdd.UseVisualStyleBackColor = true;
			this.btnPicAdd.Click += new System.EventHandler(this.btnPicAdd_Click);
			// 
			// btnPic100
			// 
			this.btnPic100.Location = new System.Drawing.Point(138, 3);
			this.btnPic100.Name = "btnPic100";
			this.btnPic100.Size = new System.Drawing.Size(60, 30);
			this.btnPic100.TabIndex = 2;
			this.btnPic100.Text = "100%";
			this.btnPic100.UseVisualStyleBackColor = true;
			this.btnPic100.Click += new System.EventHandler(this.btnPic100_Click);
			// 
			// btnPicAuto
			// 
			this.btnPicAuto.Location = new System.Drawing.Point(204, 3);
			this.btnPicAuto.Name = "btnPicAuto";
			this.btnPicAuto.Size = new System.Drawing.Size(60, 30);
			this.btnPicAuto.TabIndex = 4;
			this.btnPicAuto.Text = "Auto";
			this.btnPicAuto.UseVisualStyleBackColor = true;
			this.btnPicAuto.Click += new System.EventHandler(this.btnPicAuto_Click);
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(592, 373);
			this.panel1.TabIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(700, 305);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(270, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 30);
			this.button1.TabIndex = 5;
			this.button1.Text = "保存";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(812, 449);
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Name = "Form1";
			this.Text = "Form1";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.TextBox textMessage;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Label labPciScalSize;
		private System.Windows.Forms.Button btnPicSub;
		private System.Windows.Forms.Button btnPicAdd;
		private System.Windows.Forms.Button btnPic100;
		private System.Windows.Forms.Button btnPicAuto;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnRTWay;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.Button btnShowImgRandom;
		private System.Windows.Forms.Button btnShowImgHeight;
		private System.Windows.Forms.Button btnShowImgColor;
		private System.Windows.Forms.Button button1;
	}
}

