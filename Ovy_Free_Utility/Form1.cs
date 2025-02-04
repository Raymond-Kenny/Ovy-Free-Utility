using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Ovy_Free_Utility.Properties;
using Ovy_Free_Utility.Resources;

namespace Ovy_Free_Utility;

public class Form1 : Form
{
	private static RegistryKey MemoryManagement = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management", writable: true);

	private static RegistryKey Schedule = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile", writable: true);

	private static RegistryKey win32ps = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\PriorityControl", writable: true);

	private static RegistryKey svchost = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control");

	private static RegistryKey mmcss = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games", writable: true);

	private static RegistryKey scheduler = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers\\Scheduler", writable: true);

	private static RegistryKey kbd = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\services\\kbdclass\\Parameters", writable: true);

	private static RegistryKey mou = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\services\\mouclass\\Parameters", writable: true);

	private static RegistryKey subKey1 = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Power\\PowerThrottling", writable: true);

	private static RegistryKey subKey4 = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Power", writable: true);

	private static RegistryKey energydrv = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\GpuEnergyDrv", writable: true);

	private static RegistryKey backapps = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\BackgroundAccessApplications", writable: true);

	private static RegistryKey transparency = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", writable: true);

	private static RegistryKey GameConfigStore = Registry.CurrentUser.CreateSubKey("SYSTEM\\GameConfigStore", writable: true);

	private static RegistryKey GameBar = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\GameBar", writable: true);

	private static RegistryKey NTFSMitigation = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager", writable: true);

	private static RegistryKey alttab = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer");

	private static RegistryKey cloudcontent = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\CloudContent", writable: true);

	private static RegistryKey telemetry = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection", writable: true);

	private static RegistryKey smartscreen = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\System", writable: true);

	private static RegistryKey kernel = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\kernel", writable: true);

	private static RegistryKey cortana = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search", writable: true);

	private static RegistryKey graphicsdrivers = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers", writable: true);

	private static RegistryKey pwrssnmgr = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Power", writable: true);

	private static RegistryKey nvmoduletracker = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\NvModuleTracker", writable: true);

	private static RegistryKey nvvhci = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\nvvhci", writable: true);

	private static RegistryKey audio = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\nvvad_WaveExtensible", writable: true);

	private static RegistryKey FTHreg = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\FTH", writable: true);

	private static RegistryKey FVE = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\FVE", writable: true);

	private static RegistryKey DGSystem = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceGuard", writable: true);

	private static RegistryKey key;

	private static RegistryKey classkey;

	private static Regex rx = new Regex("\\d{4}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

	private static RegistryKey nvlddmkm = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\nvlddmkm", writable: true);

	private static RegistryKey GPU = Registry.LocalMachine.CreateSubKey("SYSTEM\\ControlSet001\\Control\\Class\\{4d36e968-e325-11ce-bfc1-08002be10318}", writable: true);

	private IContainer components = null;

	private Panel panel1;

	private Label label1;

	private Button button1;

	private Button button2;

	private Button button3;

	private PictureBox pictureBox1;

	private Panel panel2;

	private Timer timer1;

	private Timer timer2;

	private Timer timer3;

	private Timer timer4;

	public Form1()
	{
		InitializeComponent();
	}

	[DllImport("user32.DLL")]
	private static extern void ReleaseCapture();

	[DllImport("user32.DLL")]
	private static extern void SendMessage(IntPtr one, int two, int three, int four);

	private async Task DownloadFileAsync(string url, string path)
	{
		HttpClient client = new HttpClient();
		try
		{
			HttpResponseMessage response = await client.GetAsync(url);
			response.EnsureSuccessStatusCode();
			File.WriteAllBytes(path, await response.Content.ReadAsByteArrayAsync());
		}
		finally
		{
			((IDisposable)client)?.Dispose();
		}
	}

	private void panel1_Paint(object sender, PaintEventArgs e)
	{
	}

	private void panel1_mousedown(object sender, MouseEventArgs e)
	{
		ReleaseCapture();
		SendMessage(base.Handle, 274, 61458, 0);
	}

	public static void GetKey()
	{
		string[] subKeyNames = GPU.GetSubKeyNames();
		foreach (string text in subKeyNames)
		{
			if (rx.IsMatch(text))
			{
				classkey = GPU.OpenSubKey(text);
				if (classkey.GetValue("DriverDesc") != null && classkey.GetValue("DriverDesc").ToString().ToUpper()
					.Contains("NVIDIA"))
				{
					key = GPU.OpenSubKey(text, writable: true);
				}
			}
		}
	}

	private void pictureBox1_Click(object sender, EventArgs e)
	{
		MessageBox.Show("You need to restart your computer in order for changes to take effect.", "Ovy Free Utility", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		Application.Exit();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		try
		{
			Schedule.SetValue("NetworkThrottlingIndex", 10, RegistryValueKind.DWord);
			Schedule.SetValue("SystemResponsiveness", 0, RegistryValueKind.DWord);
			energydrv.SetValue("Start", 4, RegistryValueKind.DWord);
			win32ps.SetValue("Win32PrioritySeparation", 22, RegistryValueKind.DWord);
			svchost.SetValue("SvcHostSplitThresholdInKB", 376926742, RegistryValueKind.DWord);
			mmcss.SetValue("GPU Priority", 12, RegistryValueKind.DWord);
			mmcss.SetValue("Priority", 6, RegistryValueKind.DWord);
			mmcss.SetValue("Scheduling Category", "High", RegistryValueKind.String);
			mmcss.SetValue("SFIO Priority", "High", RegistryValueKind.String);
			scheduler.SetValue("EnablePreemption", "0", RegistryValueKind.DWord);
			subKey4.SetValue("HibernateEnabled", "0", RegistryValueKind.DWord);
			subKey4.SetValue("HibernateEnabledDefault", "0", RegistryValueKind.DWord);
			GameBar.SetValue("AllowAutoGameMode", "0", RegistryValueKind.DWord);
			GameBar.SetValue("AutoGameModeEnabled", "0", RegistryValueKind.DWord);
			pwrssnmgr.SetValue("HiberbootEnabled", "0", RegistryValueKind.DWord);
			graphicsdrivers.SetValue("HwSchMode", "1", RegistryValueKind.DWord);
			cortana.SetValue("AllowCortana", "0", RegistryValueKind.DWord);
			transparency.SetValue("EnableTransparency", "0", RegistryValueKind.DWord);
			kernel.SetValue("GlobalTimerResolutionRequests", "1", RegistryValueKind.DWord);
			backapps.SetValue("GlobalUserDisabled", "1", RegistryValueKind.DWord);
			alttab.SetValue("AltTabSettings", "1", RegistryValueKind.DWord);
			telemetry.SetValue("AllowTelemetry", "0", RegistryValueKind.DWord);
			RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\AppPrivacy", writable: true);
			registryKey.SetValue("LetAppsAccessAccountInfo", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessGazeInput", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessCallHistory", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessContacts", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsGetDiagnosticInfo", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessEmail", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessLocation", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessMessaging", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessMotion", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessNotifications", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessTasks", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessCalendar", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessTrustedDevices", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessBackgroundSpatialPerception", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsActivateWithVoice", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsActivateWithVoiceAboveLock", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsSyncWithDevices", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessRadios", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsAccessPhone", "2", RegistryValueKind.DWord);
			registryKey.SetValue("LetAppsRunInBackground", "2", RegistryValueKind.DWord);
			RegistryKey registryKey2 = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\MicrosoftEdgeElevationService", writable: true);
			registryKey2.SetValue("Start", "4", RegistryValueKind.DWord);
			registryKey2.Close();
			RegistryKey registryKey3 = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\edgeupdate", writable: true);
			registryKey3.SetValue("Start", "4", RegistryValueKind.DWord);
			registryKey3.Close();
			RegistryKey registryKey4 = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\edgeupdatem", writable: true);
			registryKey4.SetValue("Start", "4", RegistryValueKind.DWord);
			registryKey4.Close();
			RegistryKey registryKey5 = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\GoogleChromeElevationService", writable: true);
			registryKey5.SetValue("Start", "4", RegistryValueKind.DWord);
			registryKey5.Close();
			RegistryKey registryKey6 = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\gupdate", writable: true);
			registryKey6.SetValue("Start", "4", RegistryValueKind.DWord);
			registryKey6.Close();
			RegistryKey registryKey7 = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\gupdatem", writable: true);
			registryKey7.SetValue("Start", "4", RegistryValueKind.DWord);
			registryKey7.Close();
			Process process = new Process();
			process.StartInfo.FileName = "powercfg.exe";
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.Arguments = "-restoredefaultschemes";
			process.Start();
			process.WaitForExit();
			process.StartInfo.Arguments = "-setactive 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c";
			process.Start();
			process.WaitForExit();
			MessageBox.Show("Tweaks applied successfully.", "Ovy Free Utility", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch
		{
			MessageBox.Show("Tweaks not applied.", "Ovy Free Utility", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private async void button2_Click(object sender, EventArgs e)
	{
		GetKey();
		try
		{
			ManagementObjectCollection managementObjectCollection = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController").Get();
			string input = "";
			foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
			{
				input = (string)((ManagementObject)managementBaseObject)["Name"];
				if (input.Contains("NVIDIA"))
				{
					audio.SetValue("Start", "4", RegistryValueKind.DWord);
					nvvhci.SetValue("Start", "4", RegistryValueKind.DWord);
					nvmoduletracker.SetValue("Start", "4", RegistryValueKind.DWord);
					key.SetValue("RMHdcpKeyglobZero", "1", RegistryValueKind.DWord);
					key.SetValue("DisableDynamicPstate", "1", RegistryValueKind.DWord);
					key.SetValue("RMD3Feature", 2, RegistryValueKind.DWord);
					key.SetValue("RMDisableGpuASPMFlags", 3, RegistryValueKind.DWord);
					key.SetValue("RMBlcg", 286331153, RegistryValueKind.DWord);
					key.SetValue("RMElcg", 1431655765, RegistryValueKind.DWord);
					key.SetValue("RMElpg", 4095, RegistryValueKind.DWord);
					key.SetValue("RMFspg", 15, RegistryValueKind.DWord);
					key.SetValue("RMSlcg", 262143, RegistryValueKind.DWord);
					Directory.CreateDirectory("C:\\Ovy Free Utility Resources");
					string url1 = "https://drive.usercontent.google.com/download?id=13CBa_lq0tJb0QcE6hDx-pQ8ex-uhEhzE&export=download&authuser=0&confirm=t&uuid=28f606db-6ca1-4147-b389-b8655f815e96&at=AENtkXY1cl09nOqFhfsIOE0Chcmf%3A1730321077083";
					string url2 = "https://drive.usercontent.google.com/download?id=1EdTFvwKlvFu09PAP83FzWJvfHTaret_p&export=download&authuser=0";
					string path1 = "C:\\Ovy Free Utility Resources\\Inspector.exe";
					string path2 = "C:\\Ovy Free Utility Resources\\util.nip";
					try
					{
						await DownloadFileAsync(url1, path1);
						await DownloadFileAsync(url2, path2);
					}
					catch (Exception)
					{
					}
					Process profile = new Process();
					profile.StartInfo.FileName = "C:\\Ovy Free Utility Resources\\Inspector.exe";
					profile.StartInfo.Arguments = "\"C:\\Ovy Free Utility Resources\\util.nip\"";
					profile.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					profile.StartInfo.CreateNoWindow = true;
					profile.Start();
					profile.WaitForExit();
					MessageBox.Show("Tweaks applied successfully.", "Ovy Free Utility", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			if (input.Contains("AMD") || input.Contains("Vega") || input.Contains("Radeon"))
			{
				MessageBox.Show("AMD GPU Detected!", "Ovy Free Utility", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch
		{
			MessageBox.Show("Tweaks not applied.", "Ovy Free Utility", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
		AMD.amdtweaks();
	}

	private void label1_Click(object sender, EventArgs e)
	{
		Process.Start("https://x.com/ovymgmt");
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ovy_Free_Utility.Form1));
		this.panel1 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.panel2 = new System.Windows.Forms.Panel();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		this.timer3 = new System.Windows.Forms.Timer(this.components);
		this.timer4 = new System.Windows.Forms.Timer(this.components);
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.Black;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.pictureBox1);
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(400, 25);
		this.panel1.TabIndex = 0;
		this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(panel1_Paint);
		this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(panel1_mousedown);
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(150, 5);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(99, 14);
		this.label1.TabIndex = 1;
		this.label1.Text = "Ovy Free Utility";
		this.label1.Click += new System.EventHandler(label1_Click);
		this.pictureBox1.Image = Ovy_Free_Utility.Properties.Resources.image__17_;
		this.pictureBox1.Location = new System.Drawing.Point(380, 5);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(17, 17);
		this.pictureBox1.TabIndex = 4;
		this.pictureBox1.TabStop = false;
		this.pictureBox1.Click += new System.EventHandler(pictureBox1_Click);
		this.button1.BackColor = System.Drawing.Color.Black;
		this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.Font = new System.Drawing.Font("Tahoma", 8.25f);
		this.button1.ForeColor = System.Drawing.Color.White;
		this.button1.Location = new System.Drawing.Point(12, 33);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(376, 25);
		this.button1.TabIndex = 1;
		this.button1.Text = "Apply System Tweaks";
		this.button1.UseVisualStyleBackColor = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.BackColor = System.Drawing.Color.Black;
		this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Font = new System.Drawing.Font("Tahoma", 8.25f);
		this.button2.ForeColor = System.Drawing.Color.White;
		this.button2.Location = new System.Drawing.Point(12, 63);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(376, 25);
		this.button2.TabIndex = 2;
		this.button2.Text = "Apply NVIDIA Tweaks";
		this.button2.UseVisualStyleBackColor = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button3.BackColor = System.Drawing.Color.Black;
		this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Red;
		this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button3.Font = new System.Drawing.Font("Tahoma", 8.25f);
		this.button3.ForeColor = System.Drawing.Color.White;
		this.button3.Location = new System.Drawing.Point(12, 93);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(376, 25);
		this.button3.TabIndex = 3;
		this.button3.Text = "Apply AMD Tweaks";
		this.button3.UseVisualStyleBackColor = false;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(400, 128);
		this.panel2.TabIndex = 7;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
		base.ClientSize = new System.Drawing.Size(400, 128);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Form1";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Form1";
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}
}
