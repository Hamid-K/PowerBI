using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000139 RID: 313
	internal struct SChannelCred
	{
		// Token: 0x04000AD4 RID: 2772
		public const uint SCHANNEL_CRED_VERSION = 4U;

		// Token: 0x04000AD5 RID: 2773
		public static readonly int Size = Marshal.SizeOf(typeof(SChannelCred));

		// Token: 0x04000AD6 RID: 2774
		public uint dwVersion;

		// Token: 0x04000AD7 RID: 2775
		public uint cCreds;

		// Token: 0x04000AD8 RID: 2776
		public IntPtr paCred;

		// Token: 0x04000AD9 RID: 2777
		public IntPtr hRootStore;

		// Token: 0x04000ADA RID: 2778
		public uint cMappers;

		// Token: 0x04000ADB RID: 2779
		public IntPtr aphMappers;

		// Token: 0x04000ADC RID: 2780
		public uint cSupportedAlgs;

		// Token: 0x04000ADD RID: 2781
		public IntPtr palgSupportedAlgs;

		// Token: 0x04000ADE RID: 2782
		public uint grbitEnabledProtocols;

		// Token: 0x04000ADF RID: 2783
		public uint dwMinimumCipherStrength;

		// Token: 0x04000AE0 RID: 2784
		public uint dwMaximumCipherStrength;

		// Token: 0x04000AE1 RID: 2785
		public uint dwSessionLifespan;

		// Token: 0x04000AE2 RID: 2786
		public uint dwFlags;

		// Token: 0x04000AE3 RID: 2787
		public uint dwCredFormat;
	}
}
