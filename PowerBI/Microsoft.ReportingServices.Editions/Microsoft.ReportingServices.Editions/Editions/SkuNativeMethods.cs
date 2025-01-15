using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000009 RID: 9
	[SuppressUnmanagedCodeSecurity]
	internal static class SkuNativeMethods
	{
		// Token: 0x06000020 RID: 32
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr LoadLibraryW(string dllname);

		// Token: 0x06000021 RID: 33
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procname);

		// Token: 0x04000031 RID: 49
		private const string KERNEL32 = "kernel32.dll";

		// Token: 0x04000032 RID: 50
		public const int KEY_WOW64_32KEY = 512;

		// Token: 0x04000033 RID: 51
		public const int KEY_WOW64_64KEY = 256;

		// Token: 0x04000034 RID: 52
		public const uint VALUE_ERROR = 4294967295U;

		// Token: 0x04000035 RID: 53
		public const uint ERROR_SUCCESS = 0U;
	}
}
