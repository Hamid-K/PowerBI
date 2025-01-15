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
		// Token: 0x060010CF RID: 4303 RVA: 0x0003A388 File Offset: 0x00038588
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

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003A41C File Offset: 0x0003861C
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

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003A4CC File Offset: 0x000386CC
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

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003A5A4 File Offset: 0x000387A4
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

		// Token: 0x04000B32 RID: 2866
		private const uint GA_PARENT = 1U;

		// Token: 0x04000B33 RID: 2867
		private const uint GA_ROOT = 2U;

		// Token: 0x04000B34 RID: 2868
		private const uint GA_ROOTOWNER = 3U;

		// Token: 0x04000B35 RID: 2869
		private const byte VER_NT_WORKSTATION = 1;

		// Token: 0x04000B36 RID: 2870
		private const byte VER_NT_DOMAIN_CONTROLLER = 2;

		// Token: 0x04000B37 RID: 2871
		private const byte VER_NT_SERVER = 3;

		// Token: 0x04000B38 RID: 2872
		private static bool? isProcessWithUI;

		// Token: 0x04000B39 RID: 2873
		private static OSVERSIONINFOEX? osVersionInfo;

		// Token: 0x04000B3A RID: 2874
		private static object infoLock = new object();
	}
}
