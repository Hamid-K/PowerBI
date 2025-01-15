using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000139 RID: 313
	internal struct SChannelCred
	{
		// Token: 0x04000AC7 RID: 2759
		public const uint SCHANNEL_CRED_VERSION = 4U;

		// Token: 0x04000AC8 RID: 2760
		public static readonly int Size = Marshal.SizeOf(typeof(SChannelCred));

		// Token: 0x04000AC9 RID: 2761
		public uint dwVersion;

		// Token: 0x04000ACA RID: 2762
		public uint cCreds;

		// Token: 0x04000ACB RID: 2763
		public IntPtr paCred;

		// Token: 0x04000ACC RID: 2764
		public IntPtr hRootStore;

		// Token: 0x04000ACD RID: 2765
		public uint cMappers;

		// Token: 0x04000ACE RID: 2766
		public IntPtr aphMappers;

		// Token: 0x04000ACF RID: 2767
		public uint cSupportedAlgs;

		// Token: 0x04000AD0 RID: 2768
		public IntPtr palgSupportedAlgs;

		// Token: 0x04000AD1 RID: 2769
		public uint grbitEnabledProtocols;

		// Token: 0x04000AD2 RID: 2770
		public uint dwMinimumCipherStrength;

		// Token: 0x04000AD3 RID: 2771
		public uint dwMaximumCipherStrength;

		// Token: 0x04000AD4 RID: 2772
		public uint dwSessionLifespan;

		// Token: 0x04000AD5 RID: 2773
		public uint dwFlags;

		// Token: 0x04000AD6 RID: 2774
		public uint dwCredFormat;
	}
}
