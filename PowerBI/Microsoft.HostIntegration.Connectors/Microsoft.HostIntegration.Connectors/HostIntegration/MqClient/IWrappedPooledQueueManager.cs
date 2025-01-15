using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BD2 RID: 3026
	public interface IWrappedPooledQueueManager
	{
		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x06005DED RID: 24045
		bool Failed { get; }

		// Token: 0x06005DEE RID: 24046
		ReturnCode UpdateAsyncCounters();

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x06005DEF RID: 24047
		// (set) Token: 0x06005DF0 RID: 24048
		int FailedCount { get; set; }

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x06005DF1 RID: 24049
		// (set) Token: 0x06005DF2 RID: 24050
		int SucceededCount { get; set; }

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06005DF3 RID: 24051
		// (set) Token: 0x06005DF4 RID: 24052
		int WarningCount { get; set; }

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06005DF5 RID: 24053
		int MaximumMessageSize { get; }
	}
}
