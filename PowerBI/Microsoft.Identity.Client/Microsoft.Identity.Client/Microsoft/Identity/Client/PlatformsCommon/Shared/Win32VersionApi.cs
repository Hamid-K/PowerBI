using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001FA RID: 506
	internal static class Win32VersionApi
	{
		// Token: 0x0600158B RID: 5515
		[DllImport("ntdll.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern int RtlGetVersion(ref Win32VersionApi.OSVERSIONINFOEXW versionInformation);

		// Token: 0x0600158C RID: 5516 RVA: 0x00047CD8 File Offset: 0x00045ED8
		public static bool IsWamSupportedOs()
		{
			bool flag;
			try
			{
				Win32VersionApi.OSVERSIONINFOEXW osversioninfoexw = new Win32VersionApi.OSVERSIONINFOEXW
				{
					dwOSVersionInfoSize = Marshal.SizeOf<Win32VersionApi.OSVERSIONINFOEXW>()
				};
				if (Win32VersionApi.RtlGetVersion(ref osversioninfoexw) == 0)
				{
					byte wProductType = osversioninfoexw.wProductType;
					if (wProductType != 1)
					{
						if (wProductType - 2 > 1)
						{
							flag = false;
						}
						else if (osversioninfoexw.dwMajorVersion == 10)
						{
							if (osversioninfoexw.dwBuildNumber >= 17763)
							{
								flag = true;
							}
							else
							{
								flag = false;
							}
						}
						else
						{
							flag = false;
						}
					}
					else if (osversioninfoexw.dwMajorVersion == 10)
					{
						if (osversioninfoexw.dwBuildNumber >= 15063)
						{
							flag = true;
						}
						else
						{
							flag = false;
						}
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					flag = false;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x040008E3 RID: 2275
		private const byte VER_NT_WORKSTATION = 1;

		// Token: 0x040008E4 RID: 2276
		private const byte VER_NT_DOMAIN_CONTROLLER = 2;

		// Token: 0x040008E5 RID: 2277
		private const byte VER_NT_SERVER = 3;

		// Token: 0x040008E6 RID: 2278
		private const byte NT_STATUS_SUCCESS = 0;

		// Token: 0x040008E7 RID: 2279
		private const int WamSupportedWindows10BuildNumber = 15063;

		// Token: 0x040008E8 RID: 2280
		private const int Windows2019BuildNumber = 17763;

		// Token: 0x02000472 RID: 1138
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct OSVERSIONINFOEXW
		{
			// Token: 0x040013CB RID: 5067
			public int dwOSVersionInfoSize;

			// Token: 0x040013CC RID: 5068
			public int dwMajorVersion;

			// Token: 0x040013CD RID: 5069
			public int dwMinorVersion;

			// Token: 0x040013CE RID: 5070
			public int dwBuildNumber;

			// Token: 0x040013CF RID: 5071
			public int dwPlatformId;

			// Token: 0x040013D0 RID: 5072
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string scZSDVersion;

			// Token: 0x040013D1 RID: 5073
			public ushort wServicePackMajor;

			// Token: 0x040013D2 RID: 5074
			public ushort wServicePackMinor;

			// Token: 0x040013D3 RID: 5075
			public short wSuiteMask;

			// Token: 0x040013D4 RID: 5076
			public byte wProductType;

			// Token: 0x040013D5 RID: 5077
			public byte wReserved;
		}
	}
}
