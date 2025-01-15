using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000138 RID: 312
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal struct OSVERSIONINFOEX
	{
		// Token: 0x04000AC9 RID: 2761
		public uint dwOSVersionInfoSize;

		// Token: 0x04000ACA RID: 2762
		public uint dwMajorVersion;

		// Token: 0x04000ACB RID: 2763
		public uint dwMinorVersion;

		// Token: 0x04000ACC RID: 2764
		public uint dwBuildNumber;

		// Token: 0x04000ACD RID: 2765
		public uint dwPlatformId;

		// Token: 0x04000ACE RID: 2766
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szCSDVersion;

		// Token: 0x04000ACF RID: 2767
		public ushort wServicePackMajor;

		// Token: 0x04000AD0 RID: 2768
		public ushort wServicePackMinor;

		// Token: 0x04000AD1 RID: 2769
		public ushort wSuiteMask;

		// Token: 0x04000AD2 RID: 2770
		public byte wProductType;

		// Token: 0x04000AD3 RID: 2771
		public byte wReserved;
	}
}
