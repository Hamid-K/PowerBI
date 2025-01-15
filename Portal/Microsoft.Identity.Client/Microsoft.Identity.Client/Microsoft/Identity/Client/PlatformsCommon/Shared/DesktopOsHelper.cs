using System;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs;
using Microsoft.Win32;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001EE RID: 494
	internal static class DesktopOsHelper
	{
		// Token: 0x0600152D RID: 5421 RVA: 0x00046928 File Offset: 0x00044B28
		public static bool IsWindows()
		{
			return Environment.OSVersion.Platform == PlatformID.Win32NT;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00046937 File Offset: 0x00044B37
		public static bool IsLinux()
		{
			return Environment.OSVersion.Platform == PlatformID.Unix;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00046946 File Offset: 0x00044B46
		public static bool IsMac()
		{
			return Environment.OSVersion.Platform == PlatformID.MacOSX;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00046955 File Offset: 0x00044B55
		private static bool IsWamSupportedOSInternal()
		{
			return DesktopOsHelper.IsWindows() && Win32VersionApi.IsWamSupportedOs();
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00046968 File Offset: 0x00044B68
		private static string GetWindowsVersionStringInternal()
		{
			string text = (string)Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion").GetValue("ProductName");
			if (string.IsNullOrEmpty(text))
			{
				return Environment.OSVersion.ToString();
			}
			return text;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x000469A8 File Offset: 0x00044BA8
		public static string GetWindowsVersionString()
		{
			return DesktopOsHelper.s_winVersionLazy.Value;
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x000469B4 File Offset: 0x00044BB4
		public static bool IsWin10OrServerEquivalent()
		{
			return DesktopOsHelper.s_wamSupportedOSLazy.Value;
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x000469C0 File Offset: 0x00044BC0
		public static bool IsUserInteractive()
		{
			if (DesktopOsHelper.IsWindows())
			{
				return DesktopOsHelper.IsInteractiveSessionWindows();
			}
			if (DesktopOsHelper.IsMac())
			{
				return DesktopOsHelper.IsInteractiveSessionMac();
			}
			if (DesktopOsHelper.IsLinux())
			{
				return DesktopOsHelper.IsInteractiveSessionLinux();
			}
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000469F0 File Offset: 0x00044BF0
		private unsafe static bool IsInteractiveSessionWindows()
		{
			IntPtr processWindowStation = User32.GetProcessWindowStation();
			if (processWindowStation != IntPtr.Zero)
			{
				USEROBJECTFLAGS userobjectflags = default(USEROBJECTFLAGS);
				uint num = 0U;
				if (User32.GetUserObjectInformation(processWindowStation, 1, (void*)(&userobjectflags), (uint)sizeof(USEROBJECTFLAGS), ref num))
				{
					return (userobjectflags.dwFlags & 1) != 0;
				}
			}
			return true;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00046A3C File Offset: 0x00044C3C
		private static bool IsInteractiveSessionMac()
		{
			int num;
			SessionAttributeBits sessionAttributeBits;
			return (SecurityFramework.SessionGetInfo(-1, out num, out sessionAttributeBits) == 0 && (sessionAttributeBits & SessionAttributeBits.SessionHasGraphicAccess) != (SessionAttributeBits)0) || DesktopOsHelper.IsInteractiveSessionLinux();
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00046A62 File Offset: 0x00044C62
		private static bool IsInteractiveSessionLinux()
		{
			return !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("DISPLAY"));
		}

		// Token: 0x040008C4 RID: 2244
		private static Lazy<bool> s_wamSupportedOSLazy = new Lazy<bool>(new Func<bool>(DesktopOsHelper.IsWamSupportedOSInternal));

		// Token: 0x040008C5 RID: 2245
		private static Lazy<string> s_winVersionLazy = new Lazy<string>(new Func<string>(DesktopOsHelper.GetWindowsVersionStringInternal));
	}
}
