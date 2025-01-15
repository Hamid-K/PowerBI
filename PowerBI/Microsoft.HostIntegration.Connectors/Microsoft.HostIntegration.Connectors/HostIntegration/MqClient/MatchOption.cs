using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B09 RID: 2825
	[Flags]
	public enum MatchOption
	{
		// Token: 0x04004657 RID: 18007
		None = 0,
		// Token: 0x04004658 RID: 18008
		MessageId = 1,
		// Token: 0x04004659 RID: 18009
		Correlator = 2,
		// Token: 0x0400465A RID: 18010
		GroupId = 4,
		// Token: 0x0400465B RID: 18011
		SequenceNumber = 8,
		// Token: 0x0400465C RID: 18012
		Offset = 16,
		// Token: 0x0400465D RID: 18013
		Token = 32
	}
}
