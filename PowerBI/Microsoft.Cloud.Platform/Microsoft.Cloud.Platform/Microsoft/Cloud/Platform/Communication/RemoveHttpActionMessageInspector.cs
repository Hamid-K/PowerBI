using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004D5 RID: 1237
	internal class RemoveHttpActionMessageInspector : IClientMessageInspector
	{
		// Token: 0x06002599 RID: 9625 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AfterReceiveReply(ref Message reply, object correlationState)
		{
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x0008588E File Offset: 0x00083A8E
		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			if (OperationContext.Current != null)
			{
				OperationContext.Current.OutgoingMessageHeaders.Action = "";
			}
			request.Headers.Action = "";
			return request;
		}
	}
}
