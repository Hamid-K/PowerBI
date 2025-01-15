using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AE5 RID: 2789
	[Flags]
	public enum InitializationErrorFlag1
	{
		// Token: 0x0400457F RID: 17791
		None = 0,
		// Token: 0x04004580 RID: 17792
		Ccsid = 1,
		// Token: 0x04004581 RID: 17793
		Encoding = 2,
		// Token: 0x04004582 RID: 17794
		MaximumTransmissionSize = 4,
		// Token: 0x04004583 RID: 17795
		FapLevel = 8,
		// Token: 0x04004584 RID: 17796
		MessageSize = 16,
		// Token: 0x04004585 RID: 17797
		MaximumMessageBatch = 32,
		// Token: 0x04004586 RID: 17798
		SequenceWrap = 64,
		// Token: 0x04004587 RID: 17799
		HeartBeatInterval = 128
	}
}
