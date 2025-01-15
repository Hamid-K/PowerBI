using System;
using System.Runtime.InteropServices;
using Microsoft.InfoNav.Common.WindowsInterop;

namespace Microsoft.InfoNav.Common.Com
{
	// Token: 0x0200008E RID: 142
	internal static class ComLoader
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
		internal static IntPtr LoadLibrary(string fileName)
		{
			IntPtr intPtr = NativeMethods.LoadLibraryEx(fileName, IntPtr.Zero, 4096U);
			if (intPtr == IntPtr.Zero && Marshal.GetLastWin32Error() == 87 && NativeMethods.GetProcAddress(NativeMethods.GetModuleHandle("kernel32.dll"), "AddDllDirectory") == IntPtr.Zero)
			{
				intPtr = NativeMethods.LoadLibraryEx(fileName, IntPtr.Zero, 0U);
			}
			return intPtr;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000D04A File Offset: 0x0000B24A
		internal static ComObject CreateProxyInstanceFromDll<T>(string dllPath, Guid clsid, Guid guid, LogCallback logger) where T : class
		{
			return new ComObject(dllPath, clsid, guid, typeof(T), logger);
		}

		// Token: 0x04000126 RID: 294
		internal const int ErrorModuleNotFound = 126;

		// Token: 0x04000127 RID: 295
		private const uint LoadLibrarySearchDefaultDirs = 4096U;

		// Token: 0x04000128 RID: 296
		private const int ErrorInvalidParameter = 87;
	}
}
