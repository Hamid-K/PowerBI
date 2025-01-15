using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Interop;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x0200014A RID: 330
	internal static class WindowsRuntimeHelper
	{
		// Token: 0x0600115D RID: 4445 RVA: 0x0003CC8C File Offset: 0x0003AE8C
		public static bool IsProcessWithUserInterface()
		{
			object obj;
			if (WindowsRuntimeHelper.isProcessWithUI != null)
			{
				obj = WindowsRuntimeHelper.infoLock;
				lock (obj)
				{
					return WindowsRuntimeHelper.isProcessWithUI.Value;
				}
			}
			bool flag2 = WindowsRuntimeHelper.GetProcessMainWindowOrRootConsole() != IntPtr.Zero;
			obj = WindowsRuntimeHelper.infoLock;
			lock (obj)
			{
				WindowsRuntimeHelper.isProcessWithUI = new bool?(flag2);
			}
			return flag2;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0003CD20 File Offset: 0x0003AF20
		public static bool IsProcessWithUserInterface(out IntPtr mainWindow)
		{
			bool flag2;
			if (WindowsRuntimeHelper.isProcessWithUI != null)
			{
				object obj = WindowsRuntimeHelper.infoLock;
				lock (obj)
				{
					flag2 = WindowsRuntimeHelper.isProcessWithUI.Value;
				}
				if (flag2)
				{
					mainWindow = WindowsRuntimeHelper.GetProcessMainWindowOrRootConsole();
				}
				else
				{
					mainWindow = IntPtr.Zero;
				}
			}
			else
			{
				mainWindow = WindowsRuntimeHelper.GetProcessMainWindowOrRootConsole();
				flag2 = mainWindow != IntPtr.Zero;
				object obj = WindowsRuntimeHelper.infoLock;
				lock (obj)
				{
					WindowsRuntimeHelper.isProcessWithUI = new bool?(flag2);
				}
			}
			return flag2;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0003CDD0 File Offset: 0x0003AFD0
		public static void GetOperatingSystemVersion(out Version version, out bool isWorkstation)
		{
			OSVERSIONINFOEX osversioninfoex;
			object obj;
			if (WindowsRuntimeHelper.osVersionInfo == null)
			{
				osversioninfoex = default(OSVERSIONINFOEX);
				osversioninfoex.dwOSVersionInfoSize = (uint)Marshal.SizeOf(typeof(OSVERSIONINFOEX));
				int num = NativeMethods.RtlGetVersion(ref osversioninfoex);
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
				obj = WindowsRuntimeHelper.infoLock;
				lock (obj)
				{
					WindowsRuntimeHelper.osVersionInfo = new OSVERSIONINFOEX?(osversioninfoex);
					goto IL_008A;
				}
			}
			obj = WindowsRuntimeHelper.infoLock;
			lock (obj)
			{
				osversioninfoex = WindowsRuntimeHelper.osVersionInfo.Value;
			}
			IL_008A:
			version = new Version((int)osversioninfoex.dwMajorVersion, (int)osversioninfoex.dwMinorVersion, (int)osversioninfoex.dwBuildNumber);
			isWorkstation = osversioninfoex.wProductType == 1;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0003CEA8 File Offset: 0x0003B0A8
		private static IntPtr GetProcessMainWindowOrRootConsole()
		{
			Process currentProcess = Process.GetCurrentProcess();
			if (currentProcess.MainWindowHandle != IntPtr.Zero)
			{
				return currentProcess.MainWindowHandle;
			}
			IntPtr consoleWindow = NativeMethods.GetConsoleWindow();
			if (consoleWindow != IntPtr.Zero)
			{
				IntPtr ancestor = NativeMethods.GetAncestor(consoleWindow, 3U);
				if (ancestor != IntPtr.Zero)
				{
					return ancestor;
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x04000AEB RID: 2795
		private const uint GA_PARENT = 1U;

		// Token: 0x04000AEC RID: 2796
		private const uint GA_ROOT = 2U;

		// Token: 0x04000AED RID: 2797
		private const uint GA_ROOTOWNER = 3U;

		// Token: 0x04000AEE RID: 2798
		private const byte VER_NT_WORKSTATION = 1;

		// Token: 0x04000AEF RID: 2799
		private const byte VER_NT_DOMAIN_CONTROLLER = 2;

		// Token: 0x04000AF0 RID: 2800
		private const byte VER_NT_SERVER = 3;

		// Token: 0x04000AF1 RID: 2801
		private static bool? isProcessWithUI;

		// Token: 0x04000AF2 RID: 2802
		private static OSVERSIONINFOEX? osVersionInfo;

		// Token: 0x04000AF3 RID: 2803
		private static object infoLock = new object();
	}
}
