using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.MsoId
{
	// Token: 0x02000123 RID: 291
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct RSTParams
	{
		// Token: 0x04000A3D RID: 2621
		public uint cbSize;

		// Token: 0x04000A3E RID: 2622
		[MarshalAs(UnmanagedType.LPWStr)]
		public string wzServiceTarget;

		// Token: 0x04000A3F RID: 2623
		[MarshalAs(UnmanagedType.LPWStr)]
		public string wzServicePolicy;

		// Token: 0x04000A40 RID: 2624
		public uint dwTokenFlags;

		// Token: 0x04000A41 RID: 2625
		public uint dwTokenParam;
	}
}
