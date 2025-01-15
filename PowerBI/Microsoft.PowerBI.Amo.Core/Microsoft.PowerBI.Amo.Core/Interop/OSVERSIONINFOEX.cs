using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Interop
{
	// Token: 0x0200012D RID: 301
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal struct OSVERSIONINFOEX
	{
		// Token: 0x04000A82 RID: 2690
		public uint dwOSVersionInfoSize;

		// Token: 0x04000A83 RID: 2691
		public uint dwMajorVersion;

		// Token: 0x04000A84 RID: 2692
		public uint dwMinorVersion;

		// Token: 0x04000A85 RID: 2693
		public uint dwBuildNumber;

		// Token: 0x04000A86 RID: 2694
		public uint dwPlatformId;

		// Token: 0x04000A87 RID: 2695
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szCSDVersion;

		// Token: 0x04000A88 RID: 2696
		public ushort wServicePackMajor;

		// Token: 0x04000A89 RID: 2697
		public ushort wServicePackMinor;

		// Token: 0x04000A8A RID: 2698
		public ushort wSuiteMask;

		// Token: 0x04000A8B RID: 2699
		public byte wProductType;

		// Token: 0x04000A8C RID: 2700
		public byte wReserved;
	}
}
