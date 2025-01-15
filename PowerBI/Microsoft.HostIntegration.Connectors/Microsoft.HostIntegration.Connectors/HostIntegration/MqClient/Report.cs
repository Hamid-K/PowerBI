using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B12 RID: 2834
	[Flags]
	public enum Report
	{
		// Token: 0x04004682 RID: 18050
		None = 0,
		// Token: 0x04004683 RID: 18051
		DefaultMessageId = 0,
		// Token: 0x04004684 RID: 18052
		PassMessageId = 128,
		// Token: 0x04004685 RID: 18053
		DefaultCorrelator = 0,
		// Token: 0x04004686 RID: 18054
		PassCorrelator = 64,
		// Token: 0x04004687 RID: 18055
		PassDiscardAndExpiry = 16384,
		// Token: 0x04004688 RID: 18056
		DiscardMessage = 134217728
	}
}
