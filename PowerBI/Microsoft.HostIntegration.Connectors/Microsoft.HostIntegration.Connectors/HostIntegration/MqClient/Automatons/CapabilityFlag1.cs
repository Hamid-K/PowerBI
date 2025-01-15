using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AE2 RID: 2786
	[Flags]
	public enum CapabilityFlag1
	{
		// Token: 0x04004567 RID: 17767
		None = 0,
		// Token: 0x04004568 RID: 17768
		MessageSequence = 1,
		// Token: 0x04004569 RID: 17769
		ConversionCapable = 2,
		// Token: 0x0400456A RID: 17770
		SplitMessage = 4,
		// Token: 0x0400456B RID: 17771
		RequestInitiation = 8,
		// Token: 0x0400456C RID: 17772
		RequestSecurity = 16,
		// Token: 0x0400456D RID: 17773
		MqRequest = 32,
		// Token: 0x0400456E RID: 17774
		ServerConnectionSecurity = 64,
		// Token: 0x0400456F RID: 17775
		RuntimeApplication = 128
	}
}
