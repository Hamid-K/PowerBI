using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000138 RID: 312
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal struct OSVERSIONINFOEX
	{
		// Token: 0x04000ABC RID: 2748
		public uint dwOSVersionInfoSize;

		// Token: 0x04000ABD RID: 2749
		public uint dwMajorVersion;

		// Token: 0x04000ABE RID: 2750
		public uint dwMinorVersion;

		// Token: 0x04000ABF RID: 2751
		public uint dwBuildNumber;

		// Token: 0x04000AC0 RID: 2752
		public uint dwPlatformId;

		// Token: 0x04000AC1 RID: 2753
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szCSDVersion;

		// Token: 0x04000AC2 RID: 2754
		public ushort wServicePackMajor;

		// Token: 0x04000AC3 RID: 2755
		public ushort wServicePackMinor;

		// Token: 0x04000AC4 RID: 2756
		public ushort wSuiteMask;

		// Token: 0x04000AC5 RID: 2757
		public byte wProductType;

		// Token: 0x04000AC6 RID: 2758
		public byte wReserved;
	}
}
