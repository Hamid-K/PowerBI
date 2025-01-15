using System;
using System.Runtime.InteropServices;

namespace Microsoft.InfoNav.Common.WindowsInterop
{
	// Token: 0x0200008C RID: 140
	internal static class NativeMethods
	{
		// Token: 0x060004F3 RID: 1267
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
		public static extern IntPtr GetProcAddress(IntPtr module, [MarshalAs(UnmanagedType.LPStr)] string procName);

		// Token: 0x060004F4 RID: 1268
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
		public static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPStr)] string moduleName);

		// Token: 0x060004F5 RID: 1269
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr LoadLibraryEx(string dllFilePath, IntPtr file, uint flags);

		// Token: 0x060004F6 RID: 1270
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FreeLibrary(IntPtr module);

		// Token: 0x060004F7 RID: 1271
		[DllImport("kernel32.dll")]
		internal static extern uint SetErrorMode(uint uMode);
	}
}
