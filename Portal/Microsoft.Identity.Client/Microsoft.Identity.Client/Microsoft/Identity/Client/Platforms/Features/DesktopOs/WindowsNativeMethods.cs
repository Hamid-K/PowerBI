using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs
{
	// Token: 0x02000193 RID: 403
	internal static class WindowsNativeMethods
	{
		// Token: 0x060012F1 RID: 4849
		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentProcessId();

		// Token: 0x060012F2 RID: 4850
		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		// Token: 0x060012F3 RID: 4851
		[DllImport("kernel32.dll")]
		private static extern void GetNativeSystemInfo(ref WindowsNativeMethods.SYSTEM_INFO lpSystemInfo);

		// Token: 0x060012F4 RID: 4852 RVA: 0x000401DC File Offset: 0x0003E3DC
		public static string GetProcessorArchitecture()
		{
			string text;
			try
			{
				WindowsNativeMethods.SYSTEM_INFO system_INFO = default(WindowsNativeMethods.SYSTEM_INFO);
				WindowsNativeMethods.GetNativeSystemInfo(ref system_INFO);
				short wProcessorArchitecture = system_INFO.wProcessorArchitecture;
				if (wProcessorArchitecture != 0)
				{
					switch (wProcessorArchitecture)
					{
					case 5:
						return "ARM";
					case 6:
					case 9:
						return "x64";
					}
					text = "Unknown";
				}
				else
				{
					text = "x86";
				}
			}
			catch (Exception)
			{
				text = "Unknown";
			}
			return text;
		}

		// Token: 0x060012F5 RID: 4853
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		public static extern bool GetUserNameEx(int nameFormat, StringBuilder userName, ref uint userNameSize);

		// Token: 0x060012F6 RID: 4854
		[DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int NetGetJoinInformation(string server, out IntPtr domain, out WindowsNativeMethods.NetJoinStatus status);

		// Token: 0x060012F7 RID: 4855
		[DllImport("Netapi32.dll")]
		public static extern int NetApiBufferFree(IntPtr Buffer);

		// Token: 0x060012F8 RID: 4856
		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		// Token: 0x060012F9 RID: 4857
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetConsoleWindow();

		// Token: 0x04000732 RID: 1842
		private const int PROCESSOR_ARCHITECTURE_AMD64 = 9;

		// Token: 0x04000733 RID: 1843
		private const int PROCESSOR_ARCHITECTURE_ARM = 5;

		// Token: 0x04000734 RID: 1844
		private const int PROCESSOR_ARCHITECTURE_IA64 = 6;

		// Token: 0x04000735 RID: 1845
		private const int PROCESSOR_ARCHITECTURE_INTEL = 0;

		// Token: 0x04000736 RID: 1846
		public const int ErrorSuccess = 0;

		// Token: 0x02000414 RID: 1044
		public enum NetJoinStatus
		{
			// Token: 0x04001223 RID: 4643
			NetSetupUnknownStatus,
			// Token: 0x04001224 RID: 4644
			NetSetupUnjoined,
			// Token: 0x04001225 RID: 4645
			NetSetupWorkgroupName,
			// Token: 0x04001226 RID: 4646
			NetSetupDomainName
		}

		// Token: 0x02000415 RID: 1045
		private struct SYSTEM_INFO
		{
			// Token: 0x04001227 RID: 4647
			public readonly short wProcessorArchitecture;

			// Token: 0x04001228 RID: 4648
			public readonly short wReserved;

			// Token: 0x04001229 RID: 4649
			public readonly int dwPageSize;

			// Token: 0x0400122A RID: 4650
			public readonly IntPtr lpMinimumApplicationAddress;

			// Token: 0x0400122B RID: 4651
			public readonly IntPtr lpMaximumApplicationAddress;

			// Token: 0x0400122C RID: 4652
			public readonly IntPtr dwActiveProcessorMask;

			// Token: 0x0400122D RID: 4653
			public readonly int dwNumberOfProcessors;

			// Token: 0x0400122E RID: 4654
			public readonly int dwProcessorType;

			// Token: 0x0400122F RID: 4655
			public readonly int dwAllocationGranularity;

			// Token: 0x04001230 RID: 4656
			public readonly short wProcessorLevel;

			// Token: 0x04001231 RID: 4657
			public readonly short wProcessorRevision;
		}
	}
}
