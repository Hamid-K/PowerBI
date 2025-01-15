using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B0C RID: 2828
	[Flags]
	public enum MessageFlags
	{
		// Token: 0x04004667 RID: 18023
		None = 0,
		// Token: 0x04004668 RID: 18024
		SegmentationInhibited = 0,
		// Token: 0x04004669 RID: 18025
		SegmentationAllowed = 1,
		// Token: 0x0400466A RID: 18026
		MessageInAGroup = 8,
		// Token: 0x0400466B RID: 18027
		LastMessageInAGroup = 16,
		// Token: 0x0400466C RID: 18028
		Segment = 2,
		// Token: 0x0400466D RID: 18029
		LastSegment = 4
	}
}
