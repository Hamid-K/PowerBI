using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.AdomdClient.Interop;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000155 RID: 341
	internal static class WindowsRuntimeHelper
	{
		// Token: 0x060010C2 RID: 4290 RVA: 0x0003A058 File Offset: 0x00038258
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

		// Token: 0x060010C3 RID: 4291 RVA: 0x0003A0EC File Offset: 0x000382EC
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

		// Token: 0x060010C4 RID: 4292 RVA: 0x0003A19C File Offset: 0x0003839C
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

		// Token: 0x060010C5 RID: 4293 RVA: 0x0003A274 File Offset: 0x00038474
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

		// Token: 0x04000B25 RID: 2853
		private const uint GA_PARENT = 1U;

		// Token: 0x04000B26 RID: 2854
		private const uint GA_ROOT = 2U;

		// Token: 0x04000B27 RID: 2855
		private const uint GA_ROOTOWNER = 3U;

		// Token: 0x04000B28 RID: 2856
		private const byte VER_NT_WORKSTATION = 1;

		// Token: 0x04000B29 RID: 2857
		private const byte VER_NT_DOMAIN_CONTROLLER = 2;

		// Token: 0x04000B2A RID: 2858
		private const byte VER_NT_SERVER = 3;

		// Token: 0x04000B2B RID: 2859
		private static bool? isProcessWithUI;

		// Token: 0x04000B2C RID: 2860
		private static OSVERSIONINFOEX? osVersionInfo;

		// Token: 0x04000B2D RID: 2861
		private static object infoLock = new object();
	}
}
