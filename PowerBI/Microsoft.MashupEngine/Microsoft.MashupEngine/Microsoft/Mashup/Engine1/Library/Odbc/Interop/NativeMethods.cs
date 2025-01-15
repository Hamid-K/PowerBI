using System;
using System.Runtime.InteropServices;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x0200068B RID: 1675
	internal static class NativeMethods
	{
		// Token: 0x06003492 RID: 13458
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern int lstrlenW(IntPtr ptr);

		// Token: 0x06003493 RID: 13459
		[DllImport("kernel32.dll")]
		public static extern void ZeroMemory(IntPtr dest, IntPtr length);

		// Token: 0x06003494 RID: 13460
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LocalAlloc(int flags, IntPtr countOfBytes);

		// Token: 0x06003495 RID: 13461
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x04001797 RID: 6039
		private const string kernel32Dll = "kernel32.dll";
	}
}
