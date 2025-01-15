using System;
using System.ServiceModel.Channels;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002B0 RID: 688
	internal interface IClientChannel : IDisposable
	{
		// Token: 0x06001930 RID: 6448
		OperationResult Send(EndpointID endpoint, ICreateMessage message);

		// Token: 0x06001931 RID: 6449
		OperationResult Send(EndpointID endpoint, ICreateMessage message, TimeSpan timeout);

		// Token: 0x06001932 RID: 6450
		OperationResult AsyncSend(EndpointID endpoint, ICreateMessage message, WaitCallback callback, object state);

		// Token: 0x06001933 RID: 6451
		OperationResult AsyncSend(EndpointID endpoint, ICreateMessage message, TimeSpan timeout, WaitCallback callback, object state);

		// Token: 0x06001934 RID: 6452
		OperationResult SendReceive(EndpointID endpoint, ICreateMessage request, out Message response);

		// Token: 0x06001935 RID: 6453
		OperationResult SendReceive(EndpointID endpoint, ICreateMessage request, TimeSpan timeout, out Message response);

		// Token: 0x06001936 RID: 6454
		void RegisterReceiveCallback(string action, OnMessageReceived callback);

		// Token: 0x06001937 RID: 6455
		void RegisterReceiveCallback(MessageType messageType, OnMessageReceived callback);

		// Token: 0x06001938 RID: 6456
		void UnregisterReceiveCallback(string action, OnMessageReceived callback);

		// Token: 0x06001939 RID: 6457
		void UnregisterReceiveCallback(MessageType messageType, OnMessageReceived callback);

		// Token: 0x0600193A RID: 6458
		void UnregisterReceiveCallback(string action);

		// Token: 0x0600193B RID: 6459
		void RegisterDefaultReceiveCallback(OnMessageReceived callback);

		// Token: 0x0600193C RID: 6460
		void UnregisterDefaultReceiveCallback(OnMessageReceived callback);

		// Token: 0x0600193D RID: 6461
		void UnregisterDefaultReceiveCallback();

		// Token: 0x0600193E RID: 6462
		void RegisterDeadCallback(OnRemoteGoingDown callback);

		// Token: 0x0600193F RID: 6463
		void UnregisterDeadCallback(OnRemoteGoingDown callback);

		// Token: 0x06001940 RID: 6464
		void UnregisterDeadCallback();

		// Token: 0x06001941 RID: 6465
		void MarkAuthorizationTokenInvalid(string currentToken);
	}
}
