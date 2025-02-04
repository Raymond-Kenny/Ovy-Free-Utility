using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Ovy_Free_Utility.Resources;

internal class AMD
{
	private static RegistryKey GPU = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e968-e325-11ce-bfc1-08002be10318}", writable: true);

	private static Regex rx = new Regex("\\d{4}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

	private static RegistryKey classkey;

	private static RegistryKey umdkey;

	private static RegistryKey dxvakey;

	public static void GetKey()
	{
		string[] subKeyNames = GPU.GetSubKeyNames();
		foreach (string text in subKeyNames)
		{
			if (!rx.IsMatch(text))
			{
				continue;
			}
			classkey = GPU.OpenSubKey(text, writable: true);
			if (classkey.GetValue("DriverDesc").ToString().Contains("Radeon") && classkey.GetSubKeyNames().Contains("UMD"))
			{
				umdkey = classkey.OpenSubKey("UMD", writable: true);
				if (umdkey.GetSubKeyNames().Contains("DXVA"))
				{
					dxvakey = umdkey.OpenSubKey("DXVA", writable: true);
				}
			}
		}
	}

	public static void amdtweaks()
	{
		GetKey();
		try
		{
			ManagementObjectCollection managementObjectCollection = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController").Get();
			string text = "";
			foreach (ManagementBaseObject item in managementObjectCollection)
			{
				text = (string)((ManagementObject)item)["Name"];
				if (text.Contains("AMD") || text.Contains("Vega") || text.Contains("Radeon"))
				{
					umdkey.SetValue("Main3D_DEF", "1", RegistryValueKind.String);
					umdkey.SetValue("Main3D", new byte[2] { 49, 0 }, RegistryValueKind.Binary);
					umdkey.SetValue("FlipQueueSize", new byte[2] { 49, 0 }, RegistryValueKind.Binary);
					classkey.SetValue("StutterMode", "0", RegistryValueKind.DWord);
					classkey.SetValue("PP_ThermalAutoThrottlingEnable", "0", RegistryValueKind.DWord);
					classkey.SetValue("EnableVCNPreemption", "0", RegistryValueKind.DWord);
					classkey.SetValue("KMD_EnableComputePreemption", "0", RegistryValueKind.DWord);
					classkey.SetValue("KMD_EnableGfxMidCmdPreemption", "0", RegistryValueKind.DWord);
					classkey.SetValue("KMD_EnablePreemptionLogging", "0", RegistryValueKind.DWord);
					classkey.SetValue("KMD_EnableSDMAPreemption", "0", RegistryValueKind.DWord);
					classkey.SetValue("EnableUlps", "0", RegistryValueKind.DWord);
					classkey.SetValue("DisablePowerGating", "1", RegistryValueKind.DWord);
					classkey.SetValue("DisableDrmdmaPowerGating", "1", RegistryValueKind.DWord);
					classkey.SetValue("DisableDMACopy", "1", RegistryValueKind.DWord);
					classkey.SetValue("DisableBlockWrite", "0", RegistryValueKind.DWord);
					classkey.SetValue("DisableAllClockGating", "1", RegistryValueKind.DWord);
					classkey.SetValue("KMD_DeLagEnabled", "0", RegistryValueKind.DWord);
					classkey.SetValue("KMD_EnableP2PIOWriteCombineWorkaround", "0", RegistryValueKind.DWord);
					MessageBox.Show("Tweaks applied successfully.", "Ovy Free Utility", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			if (text.Contains("NVIDIA"))
			{
				int num = (int)MessageBox.Show("NVIDIA GPU Detected!", "Ovy Free Utility", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch
		{
			MessageBox.Show("Tweaks not applied.", "Ovy Free Utility", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}
}
