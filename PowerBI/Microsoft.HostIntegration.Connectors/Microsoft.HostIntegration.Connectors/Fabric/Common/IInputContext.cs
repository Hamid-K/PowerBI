using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200045A RID: 1114
	internal interface IInputContext
	{
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002703 RID: 9987
		Message ReceivedMessage { get; }

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002704 RID: 9988
		UniqueId RequestId { get; }

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002705 RID: 9989
		IInputConnection InputConnection { get; }

		// Token: 0x06002706 RID: 9990
		void Accept();

		// Token: 0x06002707 RID: 9991
		void Reject();

		// Token: 0x06002708 RID: 9992
		void Reply(Message message);

		// Token: 0x06002709 RID: 9993
		void Reply(Message message, TimeSpan timeout);

		// Token: 0x0600270A RID: 9994
		IAsyncResult BeginReply(Message message, AsyncCallback callback, object state);

		// Token: 0x0600270B RID: 9995
		IAsyncResult BeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x0600270C RID: 9996
		void EndReply(IAsyncResult result);
	}
}
