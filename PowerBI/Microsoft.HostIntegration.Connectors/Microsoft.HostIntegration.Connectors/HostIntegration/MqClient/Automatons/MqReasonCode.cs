using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AEC RID: 2796
	public enum MqReasonCode
	{
		// Token: 0x040045A7 RID: 17831
		Unknown,
		// Token: 0x040045A8 RID: 17832
		NoMessageAvailable = 2033,
		// Token: 0x040045A9 RID: 17833
		MessageIsTruncated = 2079,
		// Token: 0x040045AA RID: 17834
		GetInhibited = 2016,
		// Token: 0x040045AB RID: 17835
		StreamingFailure = 2037,
		// Token: 0x040045AC RID: 17836
		QueueManagerNotAvailable = 2059
	}
}
