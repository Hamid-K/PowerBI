using System;
using System.Runtime.InteropServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003E9 RID: 1001
	public static class RuntimeUtils
	{
		// Token: 0x060016CB RID: 5835
		[DllImport("libc")]
		private static extern int uname(IntPtr buf);

		// Token: 0x060016CC RID: 5836 RVA: 0x00045CB8 File Offset: 0x00043EB8
		public static bool IsRunningOnMac()
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.AllocHGlobal(8192);
				if (RuntimeUtils.uname(intPtr) == 0 && Marshal.PtrToStringAnsi(intPtr) == "Darwin")
				{
					return true;
				}
			}
			catch
			{
				return false;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return false;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00045D30 File Offset: 0x00043F30
		public static bool IsRunningOnMono()
		{
			return Type.GetType("Mono.Runtime") != null;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00045D42 File Offset: 0x00043F42
		public static bool IsRunningOnWindows()
		{
			return Environment.OSVersion.Platform == PlatformID.Win32NT;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00045D54 File Offset: 0x00043F54
		public static string Platform()
		{
			return Environment.OSVersion.Platform.ToString();
		}
	}
}
