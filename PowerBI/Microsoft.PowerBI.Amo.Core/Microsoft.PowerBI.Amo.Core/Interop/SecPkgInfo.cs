using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Interop
{
	// Token: 0x02000134 RID: 308
	internal struct SecPkgInfo
	{
		// Token: 0x04000AB0 RID: 2736
		public static readonly int Size = Marshal.SizeOf(typeof(SecPkgInfo));

		// Token: 0x04000AB1 RID: 2737
		public uint fCapabilities;

		// Token: 0x04000AB2 RID: 2738
		public ushort wVersion;

		// Token: 0x04000AB3 RID: 2739
		public ushort wRPCID;

		// Token: 0x04000AB4 RID: 2740
		public uint cbMaxToken;

		// Token: 0x04000AB5 RID: 2741
		public IntPtr Name;

		// Token: 0x04000AB6 RID: 2742
		public IntPtr Comment;
	}
}
