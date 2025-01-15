using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x0200013F RID: 319
	internal struct SecPkgInfo
	{
		// Token: 0x04000AF7 RID: 2807
		public static readonly int Size = Marshal.SizeOf(typeof(SecPkgInfo));

		// Token: 0x04000AF8 RID: 2808
		public uint fCapabilities;

		// Token: 0x04000AF9 RID: 2809
		public ushort wVersion;

		// Token: 0x04000AFA RID: 2810
		public ushort wRPCID;

		// Token: 0x04000AFB RID: 2811
		public uint cbMaxToken;

		// Token: 0x04000AFC RID: 2812
		public IntPtr Name;

		// Token: 0x04000AFD RID: 2813
		public IntPtr Comment;
	}
}
