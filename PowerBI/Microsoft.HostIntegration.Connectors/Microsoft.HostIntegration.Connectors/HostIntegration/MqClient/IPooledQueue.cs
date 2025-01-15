using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BD1 RID: 3025
	public interface IPooledQueue
	{
		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x06005DE7 RID: 24039
		bool Failed { get; }

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x06005DE8 RID: 24040
		int ObjectHandle { get; }

		// Token: 0x06005DE9 RID: 24041
		ReturnCode Open(bool forOutput, int options, string dynamicQueuePrefix, out string resolvedQueueName);

		// Token: 0x06005DEA RID: 24042
		ReturnCode Close();

		// Token: 0x06005DEB RID: 24043
		ReturnCode Send(object message);

		// Token: 0x06005DEC RID: 24044
		ReturnCode Receive(object options, out object message);
	}
}
