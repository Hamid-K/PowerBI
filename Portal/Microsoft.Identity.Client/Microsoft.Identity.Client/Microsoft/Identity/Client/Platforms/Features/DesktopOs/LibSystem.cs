using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs
{
	// Token: 0x02000190 RID: 400
	internal static class LibSystem
	{
		// Token: 0x060012E0 RID: 4832
		[DllImport("/System/Library/Frameworks/System.framework/System", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr dlopen(string name, int flags);

		// Token: 0x060012E1 RID: 4833
		[DllImport("/System/Library/Frameworks/System.framework/System", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr dlsym(IntPtr handle, string symbol);

		// Token: 0x060012E2 RID: 4834 RVA: 0x0003FF89 File Offset: 0x0003E189
		public static IntPtr GetGlobal(IntPtr handle, string symbol)
		{
			return Marshal.PtrToStructure<IntPtr>(LibSystem.dlsym(handle, symbol));
		}

		// Token: 0x0400072E RID: 1838
		private const string LibSystemLib = "/System/Library/Frameworks/System.framework/System";
	}
}
