using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Interop
{
	// Token: 0x0200012E RID: 302
	internal struct SChannelCred
	{
		// Token: 0x04000A8D RID: 2701
		public const uint SCHANNEL_CRED_VERSION = 4U;

		// Token: 0x04000A8E RID: 2702
		public static readonly int Size = Marshal.SizeOf(typeof(SChannelCred));

		// Token: 0x04000A8F RID: 2703
		public uint dwVersion;

		// Token: 0x04000A90 RID: 2704
		public uint cCreds;

		// Token: 0x04000A91 RID: 2705
		public IntPtr paCred;

		// Token: 0x04000A92 RID: 2706
		public IntPtr hRootStore;

		// Token: 0x04000A93 RID: 2707
		public uint cMappers;

		// Token: 0x04000A94 RID: 2708
		public IntPtr aphMappers;

		// Token: 0x04000A95 RID: 2709
		public uint cSupportedAlgs;

		// Token: 0x04000A96 RID: 2710
		public IntPtr palgSupportedAlgs;

		// Token: 0x04000A97 RID: 2711
		public uint grbitEnabledProtocols;

		// Token: 0x04000A98 RID: 2712
		public uint dwMinimumCipherStrength;

		// Token: 0x04000A99 RID: 2713
		public uint dwMaximumCipherStrength;

		// Token: 0x04000A9A RID: 2714
		public uint dwSessionLifespan;

		// Token: 0x04000A9B RID: 2715
		public uint dwFlags;

		// Token: 0x04000A9C RID: 2716
		public uint dwCredFormat;
	}
}
