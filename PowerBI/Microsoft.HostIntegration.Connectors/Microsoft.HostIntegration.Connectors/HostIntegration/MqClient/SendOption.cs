using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B04 RID: 2820
	[Flags]
	public enum SendOption
	{
		// Token: 0x04004634 RID: 17972
		None = 0,
		// Token: 0x04004635 RID: 17973
		NewMessageId = 64,
		// Token: 0x04004636 RID: 17974
		NewCorrelationId = 128,
		// Token: 0x04004637 RID: 17975
		FailOnQuiesce = 8192,
		// Token: 0x04004638 RID: 17976
		LogicalOrder = 32768
	}
}
