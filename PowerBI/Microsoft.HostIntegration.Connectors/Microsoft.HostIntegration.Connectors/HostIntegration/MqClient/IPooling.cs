using System;
using Microsoft.HostIntegration.EventLogging;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BD0 RID: 3024
	public interface IPooling
	{
		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x06005DD8 RID: 24024
		// (set) Token: 0x06005DD9 RID: 24025
		bool Pool { get; set; }

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x06005DDA RID: 24026
		// (set) Token: 0x06005DDB RID: 24027
		int Timeout { get; set; }

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x06005DDC RID: 24028
		// (set) Token: 0x06005DDD RID: 24029
		int QueueManagersPerConversation { get; set; }

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x06005DDE RID: 24030
		// (set) Token: 0x06005DDF RID: 24031
		bool AllowDifferentChannels { get; set; }

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x06005DE0 RID: 24032
		// (set) Token: 0x06005DE1 RID: 24033
		bool OneUserPerConversation { get; set; }

		// Token: 0x06005DE2 RID: 24034
		ReturnCode AcquireQueueManager(QueueManagerConnectionParameters qmParameters, TcpConnectionParameters tcpParameters, out IWrappedPooledQueueManager iWrappedPooledQueueManager);

		// Token: 0x06005DE3 RID: 24035
		ReturnCode ReturnQueueManager(IWrappedPooledQueueManager iWrappedPooledQueueManager);

		// Token: 0x06005DE4 RID: 24036
		IPooledQueue AcquireQueue(QueueConnectionParameters qParameters, IWrappedPooledQueueManager iWrappedPooledQueueManager);

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x06005DE5 RID: 24037
		// (set) Token: 0x06005DE6 RID: 24038
		EventLogContainer EventLogContainer { get; set; }
	}
}
