using System;
using System.ServiceModel.Channels;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000440 RID: 1088
	internal interface IOutputConnection : ITransportConnection, ITransportObject
	{
		// Token: 0x0600260A RID: 9738
		void Send(Message msg);

		// Token: 0x0600260B RID: 9739
		void Send(Message msg, TimeSpan timeout);

		// Token: 0x0600260C RID: 9740
		IAsyncResult BeginSend(Message msg, AsyncCallback callback, object state);

		// Token: 0x0600260D RID: 9741
		IAsyncResult BeginSend(Message msg, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x0600260E RID: 9742
		void EndSend(IAsyncResult ar);

		// Token: 0x0600260F RID: 9743
		Message SendReceive(Message msg);

		// Token: 0x06002610 RID: 9744
		Message SendReceive(Message msg, TimeSpan timeout);

		// Token: 0x06002611 RID: 9745
		IAbortableAsyncResult BeginSendReceive(Message msg, AsyncCallback callback, object state);

		// Token: 0x06002612 RID: 9746
		IAbortableAsyncResult BeginSendReceive(Message msg, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06002613 RID: 9747
		Message EndSendReceive(IAsyncResult ar);
	}
}
