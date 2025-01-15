using System;
using System.Runtime.InteropServices;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000E7 RID: 231
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct MEMMAP
	{
		// Token: 0x04000733 RID: 1843
		[MarshalAs(UnmanagedType.U4)]
		internal uint dbgpid;

		// Token: 0x04000734 RID: 1844
		[MarshalAs(UnmanagedType.U4)]
		internal uint fOption;

		// Token: 0x04000735 RID: 1845
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		internal byte[] rgbMachineName;

		// Token: 0x04000736 RID: 1846
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] rgbDllName;

		// Token: 0x04000737 RID: 1847
		[MarshalAs(UnmanagedType.U4)]
		internal uint cbData;

		// Token: 0x04000738 RID: 1848
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
		internal byte[] rgbData;
	}
}
