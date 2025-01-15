using System;
using System.Runtime.InteropServices;

namespace Microsoft.Data
{
	// Token: 0x0200000D RID: 13
	internal static class SafeNativeMethods
	{
		// Token: 0x060005FF RID: 1535
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr GetProcAddress(IntPtr HModule, [MarshalAs(UnmanagedType.LPStr)] [In] string funcName);
	}
}
