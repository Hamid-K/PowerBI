using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000190 RID: 400
	internal interface IServerChannel : IDisposable
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000CD8 RID: 3288
		// (remove) Token: 0x06000CD9 RID: 3289
		event ConnectionCreatedEventHandler ConnectionCreatedEvent;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000CDA RID: 3290
		// (remove) Token: 0x06000CDB RID: 3291
		event ConnectionDestroyedEventHandler ConnectionDestroyedEvent;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000CDC RID: 3292
		// (remove) Token: 0x06000CDD RID: 3293
		event ConnectionVerificationEventHandler ConnectionVerificationEvent;

		// Token: 0x06000CDE RID: 3294
		void RegisterReceiveCallback(string action, OnMessageReceived callback);

		// Token: 0x06000CDF RID: 3295
		void RegisterReceiveCallback(MessageType messageType, OnMessageReceived callback);

		// Token: 0x06000CE0 RID: 3296
		void UnregisterReceiveCallback(string action, OnMessageReceived callback);

		// Token: 0x06000CE1 RID: 3297
		void UnregisterReceiveCallback(string action);

		// Token: 0x06000CE2 RID: 3298
		void RegisterDefaultReceiveCallback(OnMessageReceived callback);

		// Token: 0x06000CE3 RID: 3299
		void UnregisterDefaultReceiveCallback(OnMessageReceived callback);

		// Token: 0x06000CE4 RID: 3300
		void UnregisterDefaultReceiveCallback();

		// Token: 0x06000CE5 RID: 3301
		void RegisterRequestReplyCallback(string action, RequestReply callback);

		// Token: 0x06000CE6 RID: 3302
		void UnregisterRequestReplyCallback(string action, RequestReply callback);

		// Token: 0x06000CE7 RID: 3303
		void UnregisterRequestReplyCallback(string action);

		// Token: 0x06000CE8 RID: 3304
		void StartThrottle();

		// Token: 0x06000CE9 RID: 3305
		void StopThrottle();

		// Token: 0x06000CEA RID: 3306
		void EnablePeriodicConnectionVerification();

		// Token: 0x06000CEB RID: 3307
		long GetTotalConnectionsCount();

		// Token: 0x06000CEC RID: 3308
		long GetThrottledConnectionsCount();

		// Token: 0x06000CED RID: 3309
		void StartListening();
	}
}
