using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x0200013F RID: 319
	internal struct SecPkgInfo
	{
		// Token: 0x04000AEA RID: 2794
		public static readonly int Size = Marshal.SizeOf(typeof(SecPkgInfo));

		// Token: 0x04000AEB RID: 2795
		public uint fCapabilities;

		// Token: 0x04000AEC RID: 2796
		public ushort wVersion;

		// Token: 0x04000AED RID: 2797
		public ushort wRPCID;

		// Token: 0x04000AEE RID: 2798
		public uint cbMaxToken;

		// Token: 0x04000AEF RID: 2799
		public IntPtr Name;

		// Token: 0x04000AF0 RID: 2800
		public IntPtr Comment;
	}
}
