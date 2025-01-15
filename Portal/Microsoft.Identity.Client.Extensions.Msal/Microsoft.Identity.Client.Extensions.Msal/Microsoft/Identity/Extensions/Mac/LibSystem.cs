using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Extensions.Mac
{
	// Token: 0x02000007 RID: 7
	internal static class LibSystem
	{
		// Token: 0x0600001D RID: 29
		[DllImport("/usr/lib/libSystem.dylib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr dlopen(string name, int flags);

		// Token: 0x0600001E RID: 30
		[DllImport("/usr/lib/libSystem.dylib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr dlsym(IntPtr handle, string symbol);

		// Token: 0x0600001F RID: 31 RVA: 0x00002111 File Offset: 0x00000311
		public static IntPtr GetGlobal(IntPtr handle, string symbol)
		{
			return (IntPtr)Marshal.PtrToStructure(LibSystem.dlsym(handle, symbol), typeof(IntPtr));
		}

		// Token: 0x04000009 RID: 9
		private const string LibSystemLib = "/usr/lib/libSystem.dylib";
	}
}
