using System;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000012 RID: 18
	internal static class NativeMethodsWrapper
	{
		// Token: 0x06000048 RID: 72
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x06000049 RID: 73
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeLibraryHandle LoadLibrary(string libName);
	}
}
