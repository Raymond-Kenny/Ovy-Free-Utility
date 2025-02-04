using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Ovy_Free_Utility.Properties;

namespace Ovy_Free_Utility;

public class Load : Form
{
	private IContainer components = null;

	private Panel panel1;

	private PictureBox pictureBox1;

	private ProgressBar progressBar1;

	private Label label1;

	private Label label2;

	private Timer timer1;

	public Load()
	{
		InitializeComponent();
	}

	private void panel1_Paint(object sender, PaintEventArgs e)
	{
	}

	private void Load_Load(object sender, EventArgs e)
	{
		timer1.Start();
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if (progressBar1.Value < 100)
		{
			progressBar1.Value++;
			label2.Text = progressBar1.Value + "%";
			if (progressBar1.Value == 100)
			{
				Hide();
				Form1 form = new Form1();
				form.ShowDialog();
			}
		}
		else
		{
			timer1.Stop();
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ovy_Free_Utility.Load));
		this.panel1 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.progressBar1 = new System.Windows.Forms.ProgressBar();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.progressBar1);
		this.panel1.Controls.Add(this.pictureBox1);
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(350, 95);
		this.panel1.TabIndex = 0;
		this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(panel1_Paint);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.White;
		this.label2.Location = new System.Drawing.Point(203, 57);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(40, 14);
		this.label2.TabIndex = 3;
		this.label2.Text = "100%";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(115, 57);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(91, 14);
		this.label1.TabIndex = 2;
		this.label1.Text = "Initializing utility";
		this.progressBar1.Location = new System.Drawing.Point(3, 81);
		this.progressBar1.Name = "progressBar1";
		this.progressBar1.Size = new System.Drawing.Size(342, 10);
		this.progressBar1.TabIndex = 1;
		this.pictureBox1.Image = Ovy_Free_Utility.Properties.Resources.OvyLogoPNG__4_;
		this.pictureBox1.Location = new System.Drawing.Point(125, 11);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(100, 50);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.timer1.Interval = 25;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
		base.ClientSize = new System.Drawing.Size(350, 95);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Load";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Load";
		base.Load += new System.EventHandler(Load_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}
}
