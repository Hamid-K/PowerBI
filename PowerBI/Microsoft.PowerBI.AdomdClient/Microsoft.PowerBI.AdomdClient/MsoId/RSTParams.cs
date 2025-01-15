using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x0200012E RID: 302
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct RSTParams
	{
		// Token: 0x04000A77 RID: 2679
		public uint cbSize;

		// Token: 0x04000A78 RID: 2680
		[MarshalAs(UnmanagedType.LPWStr)]
		public string wzServiceTarget;

		// Token: 0x04000A79 RID: 2681
		[MarshalAs(UnmanagedType.LPWStr)]
		public string wzServicePolicy;

		// Token: 0x04000A7A RID: 2682
		public uint dwTokenFlags;

		// Token: 0x04000A7B RID: 2683
		public uint dwTokenParam;
	}
}
