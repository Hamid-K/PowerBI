using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x0200012E RID: 302
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct RSTParams
	{
		// Token: 0x04000A84 RID: 2692
		public uint cbSize;

		// Token: 0x04000A85 RID: 2693
		[MarshalAs(UnmanagedType.LPWStr)]
		public string wzServiceTarget;

		// Token: 0x04000A86 RID: 2694
		[MarshalAs(UnmanagedType.LPWStr)]
		public string wzServicePolicy;

		// Token: 0x04000A87 RID: 2695
		public uint dwTokenFlags;

		// Token: 0x04000A88 RID: 2696
		public uint dwTokenParam;
	}
}
